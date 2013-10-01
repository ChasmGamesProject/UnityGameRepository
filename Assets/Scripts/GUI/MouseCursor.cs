/// Class	MouseCursor
/// Desc	Displays mouse cursor
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour
{
	public Texture2D[] cursorTextures;
	
	private int cursor_index;
	
	int cursor_x = 64; // size
	int cursor_y = 64;
	
	void Start()
	{
		Screen.showCursor = false;
	}
	
	public void ChangeCursor(int index)
	{
		if(cursorTextures.Length > index) // Make sure mode exists
			cursor_index = index;
	}
	
	void OnGUI()
	{
		GUI.depth = 1;
		GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursor_x, cursor_y), cursorTextures[cursor_index]);
	}
}
