using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {
	private SlotComponent component = null;
	
	public bool isEmpty() {
		return component == null;
	}

	public SlotComponent getSlotComponent() 
	{
		return component;
	}

	public void SetSlotComponent(SlotComponent o)
	{
		component = o;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
	
	}
}
