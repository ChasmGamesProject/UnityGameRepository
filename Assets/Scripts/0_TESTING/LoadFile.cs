using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class ObjectData
{
	public string Name;
	public string Desc;
	public bool isCollectable;
	public string IconFileName;
	public bool isCombinable;
	public int CombinesWith;
	
	public ObjectData()
	{
		Reset ();
	}	
	
	public void Reset()
	{
		Name = "Unknown";
		Desc = "Unknown";
		isCollectable = false;
		IconFileName = "";
		isCombinable = false;
		CombinesWith = -1;
	}
}

public class LoadFile : MonoBehaviour
{
	void Start ()
	{
		LoadObjects ("Assets/text/Objects2.txt");
	}
	
	void LoadObjects(string fileName)
	{
		List<Base_Object> data = new List<Base_Object>();
	    try
	    {
	        string line;
	        StreamReader file = new StreamReader(fileName, Encoding.Default);
			
			ObjectData objectTemp = new ObjectData();
			
	        
	        using (file)
	        {
	            do
	            {
	                line = file.ReadLine();
	 
	                if (line != null)
					{
						if(line[0] == '{')
						{
							objectTemp.Reset();
							bool done = false;
							//string[] elements;
							do
							{
								line = file.ReadLine();
								if(line == null)
									done = true;
								else if(line[0] == '}')
									done = true;
								else
								{
									string[] elements = line.Split('=');
									if(elements.Length < 2)
										done = true;
									else
									{
										elements[0] = elements[0].Trim(); // gets rid of leading whitespace
										
										switch(elements[0])
										{
										case "name":
											objectTemp.Name = elements[1];
											break;
										case "desc":
											objectTemp.Desc = elements[1];
											break;
										case "collectable":
											string t = elements[1].ToLower();
											if(t.CompareTo("true") == 0 || t.CompareTo("1") == 0)
												objectTemp.isCollectable = true;
											break;
										case "icon":
											objectTemp.IconFileName = elements[1];
											break;
										case "combinable":
											string s = elements[1].ToLower();
											if(s.CompareTo("true") == 0 || s.CompareTo("1") == 0)
												objectTemp.isCombinable = true;
											break;
										case "combines_with":
											objectTemp.CombinesWith = int.Parse(elements[1]);
											break;
										}
									}
								}
							}
	            			while (!done);
							
							Base_Object b;
							if(objectTemp.isCollectable)
							{
								Texture2D texture = null; //Resources.Load(objectTemp.IconFileName)
								b = new Collectable(objectTemp.Name, objectTemp.Desc, texture);
								if(objectTemp.isCombinable)
								{
									((Collectable)b).combinable = true;
									((Collectable)b).combines_with = objectTemp.CombinesWith;
								}
							}
							else
							{
								b = new Base_Object(objectTemp.Name, objectTemp.Desc);
							}
							data.Add(b);
						}
					}
	            }
	            while (line != null);
	 
	            file.Close();
				
				// PRINT TEST
				string msg = "";
				foreach(Base_Object obj in data)
				{
					msg = obj.name + "; " + obj.desc;
					if(obj.type == Object_Type.OBJ_COLLECT)
					{
						msg += " [COLLECTABLE]";
						if(((Collectable)obj).combinable)
							msg += " [COMBINABLE]";
					}
					print(msg);
				}
	        }
		}
		
        catch (System.Exception e)
        {
            print( e.Message);
        }
	}
}
