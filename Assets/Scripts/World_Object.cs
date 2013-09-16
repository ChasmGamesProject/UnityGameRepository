/// Class	World_Object
/// Desc	Interactive world object
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class World_Object : MonoBehaviour
{
	public int object_id = 0; // zero is default, it means unknown?
	public bool collectable = false;
	
	//public GameObject _logic;
	
	private Database db;
	private InteractMode im;
	
	private DialogueBox di;
	
	private Inventory inv;
	
	private Material mat;
	
	void Start()
	{
		mat = renderer.material;
		
		GameObject _logic = GameObject.Find ("_LOGIC");
		
		db = (Database)(_logic.GetComponent("Database"));
		im = (InteractMode)(_logic.GetComponent("InteractMode"));
		
		di = (DialogueBox)(GameObject.Find("Main Camera").GetComponent("DialogueBox"));
		
		if(collectable)
			inv = (Inventory)(_logic.GetComponent("Inventory"));
		else
			inv = null;
	}
	
	void OnMouseEnter()
	{
		// start glowing
		mat.SetColor ("_Emission", Color.green);
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
				//print(o.name + ": " + o.desc);
			}
			
			if(im.GetMode() == InteractMode.iMode.MODE_USE)
			{
				Base_Object o = db.GetObject(object_id);
				
				if(collectable)
				{
					//print(o.name + " was added to inventory");
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
					//print (db.GetPickUpFailMessage());
				}
			}
		}
	}
}
