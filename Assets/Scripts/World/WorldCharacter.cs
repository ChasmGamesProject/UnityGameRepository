/// Class	WorldCharacter
/// Desc	An interactive NPC (can start convos)
/// Author	Cameron A. Gardner
/// Date	1/10/2013

using UnityEngine;
using System.Collections;

public class WorldCharacter : MonoBehaviour
{
	public int CharacterId = 0;
	
	private InteractMode im;
	private DialogueManager dm;
	
	public void Start()
	{
		im = GlobalVars.interact_mode;
		
		dm = GameObject.Find ("Main Camera").GetComponent<DialogueManager>();
	}
	
	public void OnMouseDown()
	{
		if(dm != null)
		{
			//if(im != null)
			//{
				//if(im.GetMode() == InteractMode.iMode.MODE_USE)
				//{
					dm.StartConversation(CharacterId);
					// Either above function or here needs to tell InteractionMode class that it is in dialogue mode
				//}
			//}
		}
	}
}
