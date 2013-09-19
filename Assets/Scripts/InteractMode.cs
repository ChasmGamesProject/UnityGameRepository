/// Class	InteractMode
/// Desc	Tracks interation mode
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class InteractMode : MonoBehaviour
{
	public enum iMode
	{
		MODE_IDENTIFY = 0,
		MODE_USE = 1
		//MODE_DIALOGUE, /// BAD IDEA  right now///
		//MODE_PUZZLE
	};
	
	private int mode_count = 2;
	
	private iMode cur_mode;
	
	private MouseCursor scr_mouse;
	
	void Start ()
	{
		cur_mode = iMode.MODE_IDENTIFY;
		
		scr_mouse = (MouseCursor)(GameObject.Find("Main Camera").GetComponent("MouseCursor"));
		
		UpdateCursor();
	}
	
	public void SetMode(iMode mode)
	{
		cur_mode = mode;
		UpdateCursor();
	}
	
	public void CycleMode()
	{
		cur_mode++;
		if((int)cur_mode == mode_count)
			cur_mode = 0;
		UpdateCursor();
	}
	
	public iMode GetMode()
	{
		return(cur_mode);
	}
	
	private void UpdateCursor()
	{
		if(scr_mouse != null)
			scr_mouse.ChangeCursor((int)cur_mode);
	}
}
