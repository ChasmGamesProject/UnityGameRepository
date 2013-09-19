/// Class	DrawInventory
/// Desc	Draws inventory on screen
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;

public class DrawInventory : MonoBehaviour
{
	private Inventory inv;
	private Database db;
	
	private string text;
	
	private Rect textBox;

	void Start ()
	{
		GameObject logic = GameObject.Find("_LOGIC");
		inv = (Inventory)(logic.GetComponent("Inventory"));
		db = (Database)(logic.GetComponent("Database"));
		
		textBox = new Rect(0, 0, 256, Screen.height);
	}
	
	void Update()
	{
		text = "[Inventory]";
		for(int i = 0; i < Inventory.INVENTORY_SIZE; i++)
		{
			text += "\nSlot #" + i + ": ";
			if(inv.GetObjectInSlot(i) == -1)
				text += "Empty";
			else
				text += db.GetObject(inv.GetObjectInSlot(i)).name;
		}
	}
	
	
	void OnGUI()
	{
		GUI.Label(textBox, text);
	}
}
