using UnityEngine;
using System.Collections;

public class ShipComponentController : MonoBehaviour {

	public bool		componentEnabled;

	// Use this for initialization
	void Start () {
		componentEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void launch() {
		componentEnabled = true;
	}

}
