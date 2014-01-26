using UnityEngine;
using System.Collections;

public class MissileTargetSelection : MonoBehaviour {
	
	public HomingController homingController;
	public float			targetConeWidth = 30;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (homingController == null) {
			return;
		}
		GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject [] friends = GameObject.FindGameObjectsWithTag("Player");
		
		GameObject closestTargetInFront = null;
		float closestDistance = -1;
		foreach (GameObject obj in enemies) {
			float distance = calculateFrontDistance(obj);
			if (distance < 0 || (closestDistance != -1 && distance > closestDistance)) {
				continue;
			}
			closestDistance = distance;
			closestTargetInFront = obj;
		}
		if (closestTargetInFront != null) {
			homingController.wayPoint = closestTargetInFront;
		}
	}
	
	float calculateFrontDistance(GameObject target) {
		Vector3 targetVec = transform.position - target.transform.position;
		float angle = Vector3.Angle(targetVec, transform.forward);
		if (angle > targetConeWidth) {
			return -1;
		}
		if (Vector3.Dot(targetVec, transform.forward) < 0) {
			return -1;
		}
		return targetVec.magnitude;
	}
}
