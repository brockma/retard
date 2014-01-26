using UnityEngine;
using System.Collections;

public class MachinegunShooter : ShipComponentController {

	public float timeBetweenShots = 0.5f;
	public GameObject shotObject;
	public GameObject muzzleObject;

	private float nextShootTime = 0.0f;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	void FixedUpdate() {
		if (componentEnabled == false) {
			return;
		}
		
		if (Time.time > nextShootTime) {
			nextShootTime = Time.time + timeBetweenShots;
			GameObject newshot = Instantiate(shotObject, muzzleObject.transform.position, muzzleObject.transform.rotation) as GameObject;
			newshot.SetActive(true);
		}
	}
}
