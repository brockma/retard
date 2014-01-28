using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	public Renderer renderer2;
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
		renderer2.material = buttonDownMaterial;
		buttonDown = true;
	}
	
	void OnMouseUp() {
		renderer2.material = buttonUpMaterial;
		if(buttonDown) {
			gameModel.StartGame();
		}
		buttonDown = false;
	}
	
	void OnMouseExit() {
		renderer2.material = buttonUpMaterial;
		buttonDown = false;
	}
}
