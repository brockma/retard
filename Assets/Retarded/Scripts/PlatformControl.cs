using UnityEngine;
using System.Collections;

public class PlatformControl : MonoBehaviour {

	private GameObject	nextLaunchDrone;
	public 	GUIText 	debugText1;

	private Quaternion 	originalRotation;
	private GameModel 	gameModel;
	
	public GUIText 		gameOverText;
	public GUIText 		shipsLeftText;
	
	public GameObject 	droneSpawnPoint;
	
	// ENEMIES
	public GameObject[] enemies;	
	public Vector3 		enemySpawnPosition;
	private float 		startWait = 1.0f;
	private float 		waveWait = 1.0f;
	private float 		spawnWait = 0.5f;
	private int 		enemyCount = 5;
	
	private float 		gameTime = 0.0f;
	private int 		shipsLeft = 10;
	
	private bool 		gameOver = false;
	
	public void setGameOver() {
		gameOver = true;
	}

	// Use this for initialization
	void Start () {
		gameModel = FindObjectOfType<GameModel>();
		
		foreach(KnobShipController k in gameModel.schemas) {
			k.gameObject.SetActive(false);
		}
		
		StartCoroutine(SpawnWaves());
	}
	
	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		while (gameOver == false) {
			for (int i = 0; i < enemyCount; i++) {
				if (gameOver) {
					break;
				}
			
				GameObject enemy = enemies[Random.Range(0, enemies.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-enemySpawnPosition.x, enemySpawnPosition.x), enemySpawnPosition.y, enemySpawnPosition.z);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject newEnemy = Instantiate (enemy, spawnPosition, spawnRotation) as GameObject;
				newEnemy.transform.LookAt(droneSpawnPoint.transform.position);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			enemyCount++;			
		}
		gameOverText.text = "Game Over";
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver) {
			if (Input.GetMouseButtonDown(0) == true && nextLaunchDrone != null) {

				}
			return;
		}
		
		gameTime += Time.deltaTime;
		if(gameTime > 1.0f) {
			shipsLeft++;
			gameTime -= 1.0f;
			shipsLeftText.text = "Ships available: " + shipsLeft;
		}	
	
		if (nextLaunchDrone != null) {
			Vector3 mouseScreenPoint = Input.mousePosition;
			Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);
			mouseWorldPoint.y = nextLaunchDrone.transform.position.y;
	//		Vector3 launchPlatformToMouse = (mouseWorldPoint - launchPlatform.transform.position).normalized;
	
			// Point the objects at the cursor
			nextLaunchDrone.transform.LookAt(mouseWorldPoint);
			nextLaunchDrone.transform.rotation = nextLaunchDrone.transform.rotation * originalRotation;
		} else {
			int schemaNum = Random.Range(0,4);
			KnobShipController schema = gameModel.schemas[schemaNum];
			nextLaunchDrone = Instantiate(schema.gameObject) as GameObject;
			originalRotation = Quaternion.Euler(new Vector3(0, 180, 180));
			nextLaunchDrone.SetActive(true);
			nextLaunchDrone.transform.position = droneSpawnPoint.transform.position;
			nextLaunchDrone.transform.localScale *= 2.0f;
			
			Rigidbody rigidbody = nextLaunchDrone.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.angularDrag = 0;
			rigidbody.mass = 1;
			rigidbody.velocity = new Vector3(0,0,0);
			
			foreach(Slot slot in nextLaunchDrone.GetComponentsInChildren<Slot>()) {
				// Destroy the insert-component-here box from the slots
				foreach (MeshRenderer slotrenderer in slot.GetComponents<MeshRenderer>()) {
					Destroy(slotrenderer);
				}
				
				// The slot is empty, so no need to do anything more
				if (slot.transform.childCount == 0) {
					continue;
				}
			
				// Take the first grandchild of the slot, and make it the first direct child
				// Destroy the original first child
				Transform child = slot.transform.GetChild(0);
				Transform childchild = child.GetChild(0);
				child.parent = null;
				DestroyObject(child.gameObject);
				childchild.parent = slot.transform;
				
				// Connect the component rigidly into the main ship
				// This way, for instance, the thrusters move the whole ship, not just the thruster
				FixedJoint joint = childchild.gameObject.AddComponent<FixedJoint>();
				joint.transform.parent = childchild;
				joint.connectedBody = nextLaunchDrone.rigidbody;
			}
			
			// Turn on all scripts in children
			foreach(MonoBehaviour script in nextLaunchDrone.GetComponentsInChildren<MonoBehaviour>()) {
				script.enabled = true;
			}
		}
				
		if (Input.GetMouseButtonDown(0) == true && nextLaunchDrone != null) {
			if(shipsLeft > 0) {
				shipsLeft--;
				shipsLeftText.text = "Ships available: " + shipsLeft;
				
				KnobShipController controller = (KnobShipController) nextLaunchDrone.GetComponent(typeof(KnobShipController));
				controller.launch();
				nextLaunchDrone = null;
			}
		}
	}
}
