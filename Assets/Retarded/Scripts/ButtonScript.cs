using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	public Material buttonDownMaterial;
	public Material buttonUpMaterial;
	private bool buttonDown = false;
	public GameModel gameModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		renderer.material = buttonDownMaterial;
		buttonDown = true;
	}
	
	void OnMouseUp() {
		renderer.material = buttonUpMaterial;
		if(buttonDown) {
			gameModel.StartGame();
		}
		buttonDown = false;
	}
	
	void OnMouseExit() {
		renderer.material = buttonUpMaterial;
		buttonDown = false;
	}
}
