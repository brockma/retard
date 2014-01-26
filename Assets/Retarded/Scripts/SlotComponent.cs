using UnityEngine;
using System.Collections;

[System.Serializable]
public enum ComponentType {
	CANNON,
	MISSILE,
	THRUSTER,
	SHIELD
}

[RequireComponent(typeof(BoxCollider))]
public class SlotComponent : MonoBehaviour {
	public ComponentType componentType;
	private GameModel gameModel;
	private GameObject componentInstances;
	private Vector3 snapPosition = Vector3.zero;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 initialPosition;
	private bool fixedComponent = false;
	public Slot mySlot = null;
	
	void Start () {
		initialPosition = transform.position;
		gameModel = FindObjectOfType<GameModel>();
		componentInstances = GameObject.FindWithTag("ComponentInstances");
	}
	
	void Update () {
		if(snapPosition != Vector3.zero) {
			transform.position = snapPosition;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Slot") {
			Slot slot = other.gameObject.GetComponent<Slot>();
			if(slot.getSlotComponent() == null) {
				slot.SetSlotComponent(this);
				snapPosition = other.gameObject.transform.position;
				transform.parent = slot.transform;
				mySlot = slot;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Slot") {
			Slot slot = other.gameObject.GetComponent<Slot>();
			slot.SetSlotComponent(null);
			transform.parent = null;
			mySlot = null;
		}
	}
	
	void OnMouseDown()
	{
		if(fixedComponent) return;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
		if(mySlot == null) {
			GameObject copy = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
			copy.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);		
			DontDestroyOnLoad(gameObject);
		}
		snapPosition = Vector3.zero;
	}
	
	void OnMouseDrag()
	{
		if(fixedComponent) return;
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		if(fixedComponent) return;
		if(snapPosition == Vector3.zero) {
			DestroyObject (gameObject);
			return;
		}
		snapPosition = Vector3.zero;
	}
}
