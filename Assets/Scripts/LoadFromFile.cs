/// Class	LoadFromFile
/// Desc	Loads data from files and sends it to database
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;

public class WorldObjectData
{
	public string Name;
	public string Desc;
	public bool isCollectable;
	public string IconFileName;
	public bool isCombinable;
	public int CombinesWith;
	
	public WorldObjectData()
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

public class LoadFromFile : MonoBehaviour
{
	private Database db;
	
	void Start()
	{
		db = GlobalVars.database;
		
		List<string[]> contents = Load ("Assets/text/PickUpSuccessMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpSuccessMessage(contents[i][0]);
		
		contents = Load ("Assets/text/PickUpFailMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpFailMessage(contents[i][0]);
		
		
		LoadObjects("Assets/text/ObjectsList.txt");
		
		LoadCharacters("Assets/text/CharacterList.txt");
		
		/* OLD
		contents = Load ("Assets/text/Objects.txt");
		for(int i = 0; i < contents.Count; i++)
		{
			if(contents[i].Length < 2)
				db.AddObject(contents[i][0], "This object is beyond description.");
			else
				db.AddObject(contents[i][0], contents[i][1]);
		}
		*/
	}

 
	private List<string[]> Load(string fileName) // Returns each line as an array
	{
		List<string[]> data = new List<string[]>();
	    try
	    {
	        string line;
	        StreamReader file = new StreamReader(fileName, Encoding.Default);
			
	        
	        using (file)
	        {
	            do
	            {
	                line = file.ReadLine();
	 
	                if (line != null)
						data.Add (line.Split(';'));
	            }
	            while (line != null);
	 
	            file.Close();
	            return data; // success
	        }
		}
		
        catch (System.Exception e)
        {
            print( e.Message);
            return data; // fail
        }
	}
	
	
	void LoadObjects(string fileName)
	{
        string line;
        StreamReader file = new StreamReader(fileName, Encoding.Default);
        
        if(file != null)
        {
			WorldObjectData objectTemp = new WorldObjectData();
			
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
									ProcessObjectFileLine(elements, ref objectTemp);
							}
						}
            			while (!done);
						
						Base_Object b;
						if(objectTemp.isCollectable)
						{
							Texture2D texture = (Texture2D)Resources.Load("textures/items/" + objectTemp.IconFileName);
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
						db.AddObject(b);
					}
				}
            }
            while (line != null);
 
            file.Close();
        }
	}
	
	private void ProcessObjectFileLine(string[] elements, ref WorldObjectData objectTemp)
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
	
	void LoadCharacters(string fileName)
	{
        string line;
        StreamReader file = new StreamReader(fileName, Encoding.Default);
		
		CharacterData charTemp;
        
        if(file != null)
        {
            do
            {
                line = file.ReadLine();
 
                if (line != null)
				{
					if(line[0] == '{')
					{
						charTemp = new CharacterData();
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
									ProcessCharacterFileLine(elements, ref charTemp);
							}
						}
            			while (!done);
						db.AddCharacter(charTemp);
					}
				}
			} while (line != null);
 
            file.Close();
		}
	}
	
	private void ProcessCharacterFileLine(string[] elements, ref CharacterData charTemp)
	{
		elements[0] = elements[0].Trim(); // gets rid of leading whitespace
										
		switch(elements[0])
		{
		case "name":
			charTemp.SetName(elements[1]);
			break;
		case "bio":
			charTemp.SetBio(elements[1]);
			break;
		case "mood":
			charTemp.SetMood(int.Parse(elements[1]));
			break;
		//case "sprite":
			//charTemp.AddSprite(); // (Texture2D)Resource.Load("textures/characters/"+elements[1])
			//break;
		}
	}
}