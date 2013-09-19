/// Class	LoadFromFile
/// Desc	Loads data from files and sends it to database
/// Author	Cameron A. Gardner
/// Date	11/09/2013

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.IO;  

public class LoadFromFile : MonoBehaviour
{
	void Start()
	{
		Database db = (Database)(gameObject.GetComponent("Database"));
		
		List<string[]> contents = Load ("Assets/text/PickUpSuccessMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpSuccessMessage(contents[i][0]);
		
		contents = Load ("Assets/text/PickUpFailMessages.txt");
		for(int i = 0; i < contents.Count; i++)
			db.AddPickUpFailMessage(contents[i][0]);
		
		contents = Load ("Assets/text/Objects.txt");
		for(int i = 0; i < contents.Count; i++)
		{
			if(contents[i].Length < 2)
				db.AddObject(contents[i][0], "This object is beyond description.");
			else
				db.AddObject(contents[i][0], contents[i][1]);
		}
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

}