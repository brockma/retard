using UnityEngine;
using System.Collections;

public class PeriodicShooter : ShipComponentController {

	public float timeBetweenShots = 0.5f;
	public GameObject shotObject;
	public GameObject muzzleObject;

	private float nextShootTime = 0.0f;

	void Start() {
	
	}

	void Update() {
	
	}
	
	void FixedUpdate() {
		if (componentEnabled == false) {
			return;
		}
		
		if (Time.time > nextShootTime) {
			nextShootTime = Time.time + timeBetweenShots;
			GameObject newshot = Instantiate(shotObject, muzzleObject.transform.position, muzzleObject.transform.rotation) as GameObject;
			newshot.rigidbody.velocity = rigidbody.velocity;
			newshot.SetActive(true);
			foreach (MonoBehaviour script in newshot.GetComponentsInChildren<MonoBehaviour>()) {
				script.enabled = true;
			}
		}
	}
}
