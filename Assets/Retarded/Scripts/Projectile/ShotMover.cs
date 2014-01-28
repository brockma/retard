using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public float shotSpeed = 1.0f;
	private float lifeTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime += Time.deltaTime;
	}
	
	void FixedUpdate() {
		rigidbody.velocity = transform.forward * shotSpeed;
		
		// Snap the object to the plane		
//		transform.position.Set(transform.position.x, -5, transform.position.z);
	}
	
	void OnTriggerEnter(Collider other) {
		if (lifeTime < 0.05f) {
			return;
		}
	
		if (other.tag == "Boundary") {
			return;
		}
		
		if (other.tag == "Player") {
			if(playerExplosion != null) Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		} else if(other.tag == "Enemy") {
			if(explosion != null) Instantiate(explosion, other.transform.position, other.transform.rotation);
		}
		
		if(other.tag == "Enemy" || other.tag == "Player") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
