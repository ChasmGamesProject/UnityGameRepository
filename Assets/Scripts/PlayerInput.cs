/// Class	PlayerInput
/// Desc	Handles inputs
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private InteractMode im;
	
	void Start ()
	{		
		im = (InteractMode)(gameObject.GetComponent("InteractMode"));
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(1))
		{
			im.CycleMode();
		}
	}
}
			
