using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ******************************************
// lists could be arrays, file loader puts
// data into list than gives to these classes
// AS ARRAYS
// ******************************************

public class DialogueNode
{
	public enum NodeType
	{
		Unknown,
		Line,
		Choice
	};
	
	protected NodeType Type;
	
	public DialogueNode()
	{
		Type = NodeType.Unknown;
	}
	
	public NodeType GetType()
	{
		return Type;
	}
}

public class DialogueLine : DialogueNode
{
	private int SpeakerId;
	//private int SpriteId; // to show emotion eg sally_happy // may be subject to change
	private string Text;
	
	public DialogueLine()
	{
		Type = DialogueNode.NodeType.Line;
		Set(0, "...");
	}
	
	public DialogueLine(int id, string t)
	{
		Type = DialogueNode.NodeType.Line;
		Set(id, t);
	}
	
	public void Set(int id, string t)
	{
		SpeakerId = id;
		Text = t;
	}
	
	public int GetSpeakerId()
	{
		return SpeakerId;
	}
	
	public string GetText()
	{
		return Text;
	}
}

public class DialogueChoice : DialogueNode
{
	private List<Choice> ChoiceList;
	
	public DialogueChoice()
	{
		Type = DialogueNode.NodeType.Choice;
		ChoiceList = new List<Choice>();
	}
	
	public void AddChoice(Choice c)
	{
		ChoiceList.Add(c);
	}
	
	public List<Choice> GetChoices()
	{
		return ChoiceList;
	}
}

public class Choice
{
	private string Text;
	private List<ChoiceOutcome> OutcomeList;
	
	public Choice()
	{
		SetText("...");
		OutcomeList = new List<ChoiceOutcome>();
	}
	
	public void SetText(string t)
	{
		Text = t;
	}
	
	public void AddOutcome(ChoiceOutcome co)
	{
		OutcomeList.Add(co);
	}
	
	public string GetText()
	{
		return Text;
	}
	
	public List<ChoiceOutcome> GetOutcomes()
	{
		return OutcomeList;
	}
}

public class ChoiceOutcome
{
	public enum OutcomeType
	{
		Unknown,
		JumpToNode,
		AddToInventory,
		MoodMod,
		EndConversation
	}
	
	protected OutcomeType Type;
	
	public ChoiceOutcome()
	{
		Type = OutcomeType.Unknown;
	}
	
	public OutcomeType GetType()
	{
		return Type;
	}
}

public class OutcomeJump : ChoiceOutcome
{
	private int NodeId;
	
	public OutcomeJump()
	{
		NodeId = 0;
		Type = OutcomeType.JumpToNode;
	}
	
	public OutcomeJump(int id)
	{
		NodeId = id;
		Type = OutcomeType.JumpToNode;
	}
	
	public int GetNodeId()
	{
		return NodeId;
	}
}

public class OutcomeItem : ChoiceOutcome
{
	private int ItemId;
	
	public OutcomeItem()
	{
		Type = OutcomeType.AddToInventory;
		ItemId = -1;
	}
	
	public OutcomeItem(int id)
	{
		Type = OutcomeType.AddToInventory;
		ItemId = id;
	}
	
	public int GetItemId()
	{
		return ItemId;
	}
}

public class OutcomeMood : ChoiceOutcome
{
	private int CharacterId;
	private int MoodMod;
	
	public OutcomeMood()
	{
		Type = OutcomeType.MoodMod;
		CharacterId = 0;
		MoodMod = 0;
	}
	
	public OutcomeMood(int id, int mod)
	{
		Type = OutcomeType.MoodMod;
		CharacterId = id;
		MoodMod = mod;
	}
	
	public int GetCharacterId()
	{
		return CharacterId;
	}
	
	public int GetMoodMod()
	{
		return MoodMod;
	}
}

public class OutcomeEnd : ChoiceOutcome
{
	public OutcomeEnd()
	{
		Type = OutcomeType.EndConversation;
	}
}