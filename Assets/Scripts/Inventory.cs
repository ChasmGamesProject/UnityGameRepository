/// Class	Inventory
/// Desc	Keeps track of player inventory
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;


public class Inventory : MonoBehaviour
{
	public static readonly int INVENTORY_SIZE = 10;
	
	private int[] slots;
	
	Inventory()
	{
		slots = new int[INVENTORY_SIZE];
		for(int i = 0; i < INVENTORY_SIZE; i++)
			slots[i] = -1;
	}
	
	public bool Add(int obj_id)
	{
		int slot_index = -1;
		
		for(int i = 0; i < INVENTORY_SIZE; i++)
		{
			if(slots[i] == -1)
			{
				slot_index = i;
				break;
			}
		}
		
		if(slot_index == -1)
		{
			print("Inventory Full");
			return false;
		}
		else
		{
			slots[slot_index] = obj_id;
			return true;
		}
	}
	
	public bool Contains(int obj_id)
	{
		for(int i = 0; i < INVENTORY_SIZE; i++)
			if(slots[i] == obj_id)
				return true;
		return false;
	}
	
	public int GetObjectInSlot(int slot_num)
	{
		if(slot_num < 0 || slot_num > INVENTORY_SIZE)
			return -1;
		else
			return(slots[slot_num]);
	}
	
	public void Remove(int obj_id)
	{
		for(int i = 0; i < INVENTORY_SIZE; i++)
			if(slots[i] == obj_id)
				slots[i] = -1;
	}
}
