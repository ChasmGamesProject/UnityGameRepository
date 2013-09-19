/// Class	RoomManager
/// Desc	Keeps track of the room player is in and changing rooms
/// Author	Cameron A. Gardner
/// Date	19/09/2013

using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{
	public int room_cur = 0;
	
	public GameObject[] rooms;
	
	private Camera[] room_cams;
	private AudioListener[] room_lis; // wiretaps
	
	
	private DialogueBox dia;
	
	void Start ()
	{
		GameObject mc = GameObject.Find ("Main Camera");
		if(mc)
			dia = (DialogueBox)(mc.GetComponent("DialogueBox"));
		else
			dia = null;
		
		room_cams = new Camera[rooms.Length];
		room_lis = new AudioListener[rooms.Length];
		for(int i = 0; i < rooms.Length; i++)
		{
			Camera c = (Camera)(rooms[i].GetComponentInChildren<Camera>());
			c.enabled = false; // turn off camera
			room_cams[i] = c;
			
			AudioListener al = (AudioListener)(rooms[i].GetComponentInChildren<AudioListener>());
			al.enabled = false;
			room_lis[i] = al;
		}
		
		EnterRoom (room_cur);
	}
	
	public void ChangeRoom(int id)
	{
		if(dia)
			dia.Hide(); // hides dialogue box messages
		
		ExitRoom(room_cur);
		EnterRoom(id);
	}
	
	private void ExitRoom(int id)
	{
		if(room_cams[id])
			room_cams[id].enabled = false;
		if(room_lis[id])
			room_lis[id].enabled = false;
	}
	
	private void EnterRoom(int id)
	{
		//enable room render components?
		
		if(room_cams[id])
			room_cams[id].enabled = true; // enable the rooms camera
		if(room_lis[id])
			room_lis[id].enabled = true;
		
		//teleport player
		
		//Set hud to show current room name?
		
		room_cur = id;
	}
}
