/// Class	GlobalVars
/// Desc	Stores global variables that are used by many objects for quick access
/// Author	Cameron A. Gardner
/// Date	27/09/2013

using UnityEngine;
using System.Collections;

public static class GlobalVars //: MonoBehaviour
{
	public static Database database = null;
	
	public static Inventory inventory = null;
	
	public static InteractMode interact_mode = null;
	
	public static RoomManager room_manager = null;
	
	
	// GUI Scripts
	
	public static DialogueBox dialogue_box = null;
	
	public static DialogueUI dialogue_ui = null;
}
