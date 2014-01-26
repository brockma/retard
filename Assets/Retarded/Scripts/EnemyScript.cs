using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private GameObject target;
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("enemytarget");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target.transform.position);
		rigidbody.AddForce(0.1f*rigidbody.transform.forward);
//		Vector3 p = transform.position;
//		p.y = -5;
//		transform.position = p;
	}
}
