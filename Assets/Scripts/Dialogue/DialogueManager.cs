using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// IMPORTANT
// ** Topics that grant player item can only be used once, else inventory could be filled with dups
		// OR you could check if inventory has item then not give it...
// ** Every Choice MUST HAVE A JUMP

public class DialogueManager : MonoBehaviour
{
	private enum DialogueState
	{
		INACTIVE,
		PICK_TOPIC,
		IN_TOPIC
	}
	
	private DialogueUI dui;
	private Inventory inv;
	private Database db;
	
	private DialogueState State;
	private List<DialogueNode> NodeList;
	private int NodeCurrentId;
	private DialogueNode NodeCurrent;
	
	private CharacterData ConversationPartner;
	
	public DialogueManager()
	{
		NodeCurrent = null;
		
		State = DialogueState.INACTIVE;
	}
	
	public void Start()
	{
		dui = GlobalVars.dialogue_ui; // gameObject.GetComponent<DialogueManager>();
		inv = GlobalVars.inventory;
		db = GlobalVars.database;
	}
	
	public void StartConversation(int charId) // new, not complete
	{
		ConversationPartner = db.GetCharacter(charId);
		if(ConversationPartner != null)
		{
			State = DialogueState.PICK_TOPIC;
			dui.Show ();
			UpdateGUI();
		}
	}
	
	private void StartTopic(int id) // new, not complete
	{
		NodeList = ConversationPartner.GetTopic(id).GetNodeList();
		State = DialogueState.IN_TOPIC;
		NodeCurrentId = 0;
		GotoNode();
	}
	
	public void StartConvo(List<DialogueNode> nl) // IN USE but will be phased out
	{
		NodeList = nl;
		State = DialogueState.IN_TOPIC;
		dui.Show();
		NodeCurrentId = 0;
		GotoNode();
	}
	
	
	public void Next()
	{
		if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrent.GetType() != DialogueNode.NodeType.Choice)
			{
				NodeCurrentId++;
				GotoNode();
			}
		}
	}
	
	public void PickChoice(int c)
	{
		if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrent.GetType() == DialogueNode.NodeType.Choice)
			{
				// execute action appropriate to choice
				DialogueChoice dc = (DialogueChoice)NodeCurrent;
				
				List<Choice> lc = dc.GetChoices();
				Choice ch = lc[c]; // i have no idea what array out of bounds does here, oh well it wont be possible
				List<ChoiceOutcome> lco = ch.GetOutcomes();
				bool jumped = false;
				foreach(ChoiceOutcome co in lco)
				{
					switch(co.GetType())
					{
						case ChoiceOutcome.OutcomeType.JumpToNode:
							NodeCurrentId = ((OutcomeJump)co).GetNodeId();
							GotoNode();
							jumped = true;
						break;
						case ChoiceOutcome.OutcomeType.AddToInventory:
							if(inv)
								inv.Add(((OutcomeItem)co).GetItemId());
						break;
						case ChoiceOutcome.OutcomeType.MoodMod:
							OutcomeMood om = (OutcomeMood)co;
							db.GetCharacter(om.GetCharacterId()).MoodMod(om.GetMoodMod());
						break;
						case ChoiceOutcome.OutcomeType.EndConversation:
							EndConvo();
						break;
					}
				}
				
				if(!jumped) // Move to next line if there was no Jump outcome
				{
					NodeCurrentId++;
					GotoNode();
				}
			}
		}
	}
	
	private void GotoNode()
	{
		if(NodeCurrentId < NodeList.Count)
		{
			NodeCurrent = NodeList[NodeCurrentId];
			UpdateGUI();
		}
		else
			EndConvo ();
	}
	
	private void EndConvo()
	{
		dui.Hide();
		State = DialogueState.INACTIVE;
		
		// Check if ANY character is in bad mood, if true then GAMEOVER NOOB
	}
	
	private void UpdateGUI()
	{
		if(State == DialogueState.PICK_TOPIC)
		{
			
		}
		else if(State == DialogueState.IN_TOPIC)
		{
			if(NodeCurrent.GetType() == DialogueNode.NodeType.Line)
			{
				dui.DisplayLine((DialogueLine)NodeCurrent);
			}
			else if(NodeCurrent.GetType() == DialogueNode.NodeType.Choice)
			{
				dui.DisplayChoice((DialogueChoice)NodeCurrent);
			}
		}
	}
}
