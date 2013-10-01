using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopicData
{
	private string Name;
	
	private List<DialogueNode> Nodes;
	
	public TopicData()
	{
		Name = "Unknown";
		Nodes = null;
	}
	
	public TopicData(string n, List<DialogueNode> ldn)
	{
		Name = n;
		Nodes = ldn;
	}
	
	public string GetName()
	{
		return Name;	
	}
	
	public List<DialogueNode> GetNodeList()
	{
		return Nodes;
	}
}
