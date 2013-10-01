using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
	private DialogueManager dm;
	private Database db;
	
	private bool show;
	//private DialogueNode.NodeType Type;
	private DialogueNode CurrentNode;
	
	private Rect TextBox;
	private Rect NextBox;
	
	private Rect ChoiceBox;
	private int ChoiceStartY;
	
	void Awake()
	{
		GlobalVars.dialogue_ui = this;
	}

	void Start ()
	{
		dm = gameObject.GetComponent<DialogueManager>();
		db = GlobalVars.database;
		
		show = false;
		CurrentNode = null;
		
		TextBox = new Rect(0, Screen.height / 4, Screen.width, Screen.height / 2);
		NextBox = new Rect(0, Screen.height / 4 + 64, 64, 32);
		ChoiceBox = new Rect(0, Screen.height / 4 + 32, 256, 32);
		ChoiceStartY = Screen.height / 4;
	}
	
	void OnGUI()
	{
		if(show)
		{
			if(CurrentNode != null) // Make sure its not null
			{
				GUI.skin.label.fontSize = 18;
				GUI.skin.label.alignment = TextAnchor.UpperLeft;
				
				if(CurrentNode.GetType() == DialogueNode.NodeType.Line)
				{
					DialogueLine dl = (DialogueLine)CurrentNode; 
					GUI.Label (TextBox, db.GetCharacter(dl.GetSpeakerId()).GetName() + ":\n\"" + dl.GetText() + "\"");
					if(GUI.Button (NextBox, "Next"))
					{
						dm.Next();
					}
				}
				else if(CurrentNode.GetType() == DialogueNode.NodeType.Choice)
				{
					List<Choice> lc = ((DialogueChoice)CurrentNode).GetChoices();
					for(int i = 0; i < lc.Count; i++)
					{
						ChoiceBox.y = ChoiceStartY + 48 * i;
						if(GUI.Button(ChoiceBox, lc[i].GetText()))
						{
							dm.PickChoice(i);
						}
					}
				}
			}
			DrawCharacters();
		}
	}
	
	private void DrawCharacters()
	{
		
	}
	
	public void Show()
	{
		show = true;
	}
	
	public void Hide()
	{
		CurrentNode = null;
		show = false;
	}
	
	
	public void DisplayLine(DialogueLine dl)
	{
		CurrentNode = dl;
		
		//show = true;
	}
	
	public void DisplayChoice(DialogueChoice dc)
	{
		CurrentNode = dc;
		//show = true;
	}
}
