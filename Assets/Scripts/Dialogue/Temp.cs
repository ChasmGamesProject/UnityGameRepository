using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Temp : MonoBehaviour
{
	private DialogueManager dm;
	
	private List<DialogueNode> topic;
	
	private Database db;
	
	Temp()
	{
		topic = new List<DialogueNode>();
		
		topic.Add(new DialogueLine(0, "Hello"));
		topic.Add(new DialogueLine(1, "Mornin'"));
		topic.Add(new DialogueLine(0, "Who stole the cookie from the cookie jar?"));
		topic.Add(new DialogueLine(1, "You stole the cookie from the cookie jar.")); // id = 3
		topic.Add(new DialogueLine(0, "Who me?"));
		topic.Add(new DialogueLine(1, "Yes, you!"));
		topic.Add(new DialogueLine(0, "Couldn't be!"));
		topic.Add(new DialogueLine(1, "Then who?"));
		
		
		DialogueChoice dc = new DialogueChoice();
		Choice c = new Choice();
		c.SetText("No idea.");
		c.AddOutcome(new OutcomeJump(3));
		dc.AddChoice(c);
		c = new Choice();
		c.AddOutcome(new OutcomeMood(0, -10));
		c.SetText("I lied, it was me actually."); // Choices CAN have no outcome, dialogue continues from next line
		dc.AddChoice(c);
		c = new Choice();
		c.AddOutcome(new OutcomeEnd());
		c.SetText("*Run Away*");
		dc.AddChoice(c);
		
		topic.Add(dc);
		
		topic.Add(new DialogueLine(1, "As expected, I'll be a master detective in no time."));
		topic.Add(new DialogueLine(0, "A master without cookies that is."));
	}
	
	void Start()
	{
		db = GlobalVars.database;
		
		
		db.AddCharacter(new CharacterData("You"));
		
		
		CharacterData cd = new CharacterData("Sally");
		cd.AddTopic(new TopicData("Cookies",topic));
		cd.AddAvaliableTopic(0);
		db.AddCharacter(cd);
	}

	// Use this for initialization
	void StartDialogueTest ()
	{
		dm = gameObject.GetComponent<DialogueManager>();
		dm.StartConvo(topic);
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
			StartDialogueTest();
		
		if(Input.GetKeyDown(KeyCode.Space))
			dm.Next();
		
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("menu");
	}
}
