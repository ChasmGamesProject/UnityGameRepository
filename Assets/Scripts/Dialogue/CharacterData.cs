using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterData
{
	private string Name;
	private string Bio; // a breif desc of character ?
	private int Mood;
	
	//private Texture2D[] Sprites; //array of sprites eg sally_happy
	
	private const int MOOD_MIN = 0;
	private const int MOOD_MAX = 100;
	
	private List<TopicData> TopicList;
	private List<int> AvaliableTopicList;
	
	
	public CharacterData()
	{
		Name = "Unknown";
		Bio = "[Classified]";
		Mood = 100; // does not need to be the case though
		TopicList = new List<TopicData>();
		AvaliableTopicList = new List<int>();
	}
	
	public CharacterData(string n)
	{
		SetName(n);
		Bio = "[Classified]";
		Mood = 100;
		TopicList = new List<TopicData>();
		AvaliableTopicList = new List<int>();
	}
	
	public void SetName(string n)
	{
		Name = n;
	}
	
	public string GetName()
	{
		return Name;
	}
	
	public void SetBio(string b)
	{
		Bio = b;
	}
	
	public void SetMood(int m)
	{
		Mood = m;
	}
	
	public bool InGoodMood()
	{
		return Mood > 0;
	}
	
	public void MoodMod(int mod)
	{
		Mood += mod;
		
		if(Mood > MOOD_MAX)
			Mood = MOOD_MAX;
		else if(Mood < MOOD_MIN)
			Mood = MOOD_MIN;
	}
	
	public void AddTopic(TopicData td)
	{
		TopicList.Add (td);
	}
	
	public void AddAvaliableTopic(int id)
	{
		if(!AvaliableTopicList.Contains(id)) // don't add duplicates
			AvaliableTopicList.Add(id);
	}
	
	public void RemoveAvaliableTopic(int id)
	{
		AvaliableTopicList.Remove(id);
	}
	
	public List<int> GetAvaliableTopics()
	{
		return AvaliableTopicList;
	}
	
	public TopicData GetTopic(int id)
	{
		if(id < TopicList.Count && id > -1)
			return TopicList[id];
		else
			return null;
	}
}
