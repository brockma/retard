using UnityEngine;
using System.Collections;

public class TargetReceiver : MonoBehaviour {

	public GameObject currentTarget;

	public void setTarget(GameObject obj) {
		currentTarget = obj;
	}
}

