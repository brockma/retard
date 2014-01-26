using UnityEngine;
using System.Collections;

public class PlanetHitScript : MonoBehaviour {

	public GameObject populationQuad;
	public PlatformControl platformControl;
	public GameObject explosion;
	public GameObject playerExplosion;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.tag == "Enemy") {
			Instantiate(explosion, other.transform.position, other.transform.rotation);		
			Destroy(other.gameObject);
			
			Vector3 scale = populationQuad.transform.localScale;
			if(scale.x > 1.0f) scale.x -= 1.0f;
			else scale.x = 0.0f;
			populationQuad.transform.localScale = scale;
			populationQuad.transform.position = new Vector3(
				populationQuad.transform.position.x - 0.5f, 
				populationQuad.transform.position.y, 
				populationQuad.transform.position.z);
				
			if(scale.x <= 0.0f) {
				platformControl.setGameOver();
			}	
		} else if(other.gameObject.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
		}
	}
}
