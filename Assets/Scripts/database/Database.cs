/// Class	Database
/// Desc	Keeps track of information on objects
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Object_Type
{
	OBJ_BASE = 0,
	OBJ_COLLECT = 1
}

public class Base_Object
{
	public Object_Type type;
	public string name;
	public string desc;
	
	public Base_Object()
	{
		type = Object_Type.OBJ_BASE;
		name = "Unknown";
		desc = "Unknown";
	}
	
	public Base_Object(string n, string d)
	{
		type = Object_Type.OBJ_BASE;
		name = n;
		desc = d;
	}
}

public class Collectable : Base_Object
{
	public Texture2D icon;
	
	public Collectable()
	{
		type = Object_Type.OBJ_COLLECT;
		icon = null;
	}
	
	public Collectable(string n, string d, Texture2D t) : base(n, d)
	{
		type = Object_Type.OBJ_COLLECT;
		icon = t;
	}
}


public class Database : MonoBehaviour
{	
	List<Base_Object> objects;
	
	List<string> pu_success_msgs;
	List<string> pu_fail_msgs;
	
	Database()
	{
		objects = new List<Base_Object>();
		
		pu_fail_msgs = new List<string>();
		
		pu_success_msgs = new List<string>();
		
		//AddObject("Test", "Test.", null);
	}

	public void AddObject(string name, string desc)
	{
		Base_Object o = new Base_Object(name, desc);
		objects.Add(o);
	}
	
	public void AddObject(string name, string desc, Texture2D tex)
	{
		Collectable o = new Collectable(name, desc, tex);
		objects.Add(o);
	}
	
	public Base_Object GetObject(int id)
	{
		return(objects[id]);
	}
	
	public void AddPickUpSuccessMessage(string msg)
	{
		pu_success_msgs.Add (msg);
	}
	
	public string GetPickUpSuccessMessage()
	{
		if(pu_success_msgs.Count > 0)
		{
			int i = Random.Range(0, pu_success_msgs.Count);
			
			return(pu_success_msgs[i]);
		}
		else
			return("");
	}
	
	public void AddPickUpFailMessage(string msg)
	{
		pu_fail_msgs.Add (msg);
	}
	
	public string GetPickUpFailMessage()
	{
		if(pu_fail_msgs.Count > 0)
		{
			int i = Random.Range(0, pu_fail_msgs.Count);
			
			return(pu_fail_msgs[i]);
		}
		else
			return("");
	}
}
