using UnityEngine;
using System.Collections;


public class ThrusterController2 : ShipComponentController {
	
	public float 	thrusterStrength;
	private Vector3	thrusterPushDirection;
	
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		if (componentEnabled == true) {
			thrusterPushDirection = -transform.forward;
			rigidbody.AddForce(thrusterPushDirection * thrusterStrength);
		}
	}
}
