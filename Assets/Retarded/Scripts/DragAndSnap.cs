using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class DragAndSnap : MonoBehaviour {
	private GameModel gameModel;
	private GameObject componentInstances;
	private Vector3 snap = Vector3.zero;
	private Vector3 screenPoint;
	private Vector3 offset;

	void Start () {
		gameModel = FindObjectOfType<GameModel>();
		componentInstances = GameObject.FindWithTag("ComponentInstances");
		if(componentInstances) Debug.Log("FOUND");
		else Debug.Log("NOT FOUND");	
	}
	
	void Update () {
		if(snap != Vector3.zero) {
			Debug.Log("Snapping");
			transform.position = snap;
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Slot") {
			Debug.Log("SNAP");
			snap = collision.gameObject.transform.position;
			
			
		}
	}
	
	void OnMouseDown()
	{
		Debug.Log ("DOWN");
		snap = Vector3.zero;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		snap = Vector3.zero;
	}
}
