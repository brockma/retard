using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {
	public KnobShipController[] schemas;	
	public GameObject schemasReference;
	
	private bool init = false;
	private GameObject[] components;
	
	// Use this for initialization
	void Start () {
		schemas = FindObjectsOfType<KnobShipController>();
		Debug.Log("Schemas found: " + schemas.GetLength(0));
		DontDestroyOnLoad(schemasReference);
	}
	
	public void StartGame() {
		DontDestroyOnLoad(this);
		foreach(KnobShipController k in schemas) {
		
		}
		Application.LoadLevel ("Main");
	}
	
	// Update is called once per frame
	void Update () {
		if(!init) {
			init = true;
			
			components = GameObject.FindGameObjectsWithTag("Component");
			
			foreach(KnobShipController k in schemas) {
				bool first = true;
				foreach(Slot s in k.getSlots()) {
					if(s) {
						int r = Random.Range(0, 4);
						if(first) r = 2;
						first = false;
						
						GameObject copy = Instantiate(components[r], s.transform.position, components[r].transform.rotation) as GameObject;
						copy.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
						copy.transform.parent = s.transform;
						copy.transform.position = new Vector3(copy.transform.position.x, copy.transform.position.y, -2);
						copy.transform.rotation = s.gameObject.transform.rotation;
						DontDestroyOnLoad(copy);
						
						SlotComponent sc = copy.GetComponent<SlotComponent>();
						sc.mySlot = s;
						s.SetSlotComponent(sc);
					}
				}	
			}		
		}	
	}
}
