/// Class	World_Object
/// Desc	Interactive world object
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class World_Object : MonoBehaviour
{
	public int object_id = 0; // zero is default, it means unknown?	
	
	private Database db;
	private InteractMode im;
	
	private DialogueBox di;
	
	private Inventory inv;
	
	private Material mat;
	
	void Start()
	{
		mat = renderer.material;
		
		db = GlobalVars.database;
		im = GlobalVars.interact_mode;
		
		di = GlobalVars.dialogue_box;
		
		inv = GlobalVars.inventory;
	}
	
	void OnMouseEnter()
	{
		// start glowing
		mat.SetColor ("_Emission", Color.green);
		
		// Should change cursor icon
	}
	
	void OnMouseExit()
	{
		// stop glowing
		mat.SetColor ("_Emission", Color.black);
	}
	
	void OnMouseDown()
	{
		if(db != null)
		{
			if(im.GetMode() == InteractMode.iMode.MODE_IDENTIFY)
			{
				Base_Object o = db.GetObject(object_id);
				di.SetText(o.name, o.desc);
			}
			
			if(im.GetMode() == InteractMode.iMode.MODE_USE)
			{
				Base_Object o = db.GetObject(object_id);
				
				if(o.type == Object_Type.OBJ_COLLECT)
				{
					if(inv != null)
						if(inv.Add(object_id))
						{
							di.SetText(o.name, db.GetPickUpSuccessMessage());
							Destroy(gameObject);
						}
				}
				else
				{
					di.SetText(o.name, db.GetPickUpFailMessage());
				}
			}
		}
	}
}
