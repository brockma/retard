using UnityEngine;
using System.Collections;

public class KnobShipController : MonoBehaviour {

	private Slot[] slots;
	private int slotCount;

	// Use this for initialization
	void Start () {
		slots = GetComponentsInChildren<Slot>();
		slotCount = slots.GetLength(0);
	}

	public Slot[] getSlots() {
		return slots;
	}
			
	// Update is called once per frame
	void Update () {
		
	}
	
	public void launch() {
		foreach(Slot slot in slots) {
			ShipComponentController [] controllers = slot.GetComponentsInChildren<ShipComponentController>();
			foreach(ShipComponentController controller in controllers) {
				if (controller != null) {
					controller.launch();
				} else {
					Debug.Log("Component controller not set");
				}
			}
		}
	}
}
