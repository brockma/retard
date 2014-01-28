using UnityEngine;
using System.Collections;

public class VisualTargetSelector : MonoBehaviour {
	public TargetReceiver 		targetReceiver;
	public float				targetConeWidth = 160;
	
	// Use this for initialization
	void Start () {
		if (targetReceiver == null) {
			Debug.LogWarning("Target selector not set");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (targetReceiver == null) {
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
			targetReceiver.setTarget(closestTargetInFront);
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
