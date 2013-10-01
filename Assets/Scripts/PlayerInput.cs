/// Class	PlayerInput
/// Desc	Handles inputs
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private InteractMode im;
	
	//private DrawInventory di;
	
	private RoomManager rm;
	
	private DialogueBox dia;
	
	void Start ()
	{
		im = GlobalVars.interact_mode;
		rm = GlobalVars.room_manager;
		dia = GlobalVars.dialogue_box;
		
		/*
		GameObject camera = GameObject.Find ("Main Camera");
		if(camera)
			di = camera.GetComponent<DrawInventory>();
		else
			di = null;
			*/
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(1)) // Right Mouse
		{
			if(im)
				im.CycleMode();
		}
		
		/*
		if(Input.GetKeyDown(KeyCode.I))
		{
			ToggleInventory();
		}
		*/
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("menu");
		}
	}
	
	/*
	public void ToggleInventory()
	{
		if(di) // Check pointer is not null
		{
			di.ToggleVisibility();
			if(rm)
				rm.ToggleRoom();
			if(dia)
				dia.Hide ();
		}
	}
	*/
}
			
