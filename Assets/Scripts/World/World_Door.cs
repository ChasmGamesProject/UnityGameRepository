﻿/// Class	Door
/// Desc	A door that can change the room
/// Author	Cameron A. Gardner
/// Date	19/09/2013

using UnityEngine;
using System.Collections;

public class World_Door : MonoBehaviour
{
	public int room_opens_to = 0;
	
	private RoomManager rm;
	
	void Start ()
	{
		rm = GlobalVars.room_manager;
	}
	
	void OnMouseDown()
	{
		if(rm)
		{
			rm.ChangeRoom(room_opens_to);
		}
	}
}
