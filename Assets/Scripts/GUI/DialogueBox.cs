using UnityEngine;
using System.Collections;

public class DialogueBox : MonoBehaviour
{

	private bool show = false;
	
	private string name;
	private string desc;
	
	/*
	private Rect box_rect;
	private Rect label_rect;
	
	private Rect button_rect;
	*/
	
	private Rect world_label;
	private float show_timeout;
	private float show_timeout_max;
	private float show_timeout_fade;
	private Color label_color;
	
	public Font font_base;
	public Font font_outline;
	
	GUIStyle guiText;
	
	void Start ()
	{
		/*
		int box_buffer = 32; // gap between bottom of screen and box
		int box_height = 128;
		int box_width = 512;
		box_rect = new Rect((Screen.width - box_width)/2, Screen.height - box_height - box_buffer, box_width, box_height);
		
		
		int button_width = 128;
		int button_height = 32;
		button_rect = new Rect((Screen.width - button_width)/2, Screen.height - button_height - box_buffer, button_width, button_height);
		
		
		label_rect = new Rect(Screen.width/2 - box_width/2, Screen.height - box_height - box_buffer - 32, button_width, button_height);
		*/
		
		
		world_label = new Rect(0, 0, 256, 64);
		show_timeout = 0.0f;
		show_timeout_max = 3.0f;
		show_timeout_fade = 1.0f;
		label_color = Color.black;
		
		guiText = new GUIStyle();
		guiText.fontSize = 26;
		guiText.font = font_base;
	}
	
	void Update()
	{
		if(show_timeout > 0)
		{
			show_timeout -= Time.deltaTime;
			if(show_timeout <= 0)
			{
				show_timeout = 0.0f;
				show = false;
			}
		}
	}
	
	public void SetText(string n, string d)
	{
		name = n;
		desc = d;
		
		show = true;
		
		world_label.x = Input.mousePosition.x;
		world_label.y = Screen.height - Input.mousePosition.y;
		show_timeout = show_timeout_max;
	}
	
	void OnGUI()
	{
		if(show)
		{
			if(show_timeout < show_timeout_fade)
				label_color.a = show_timeout;
			label_color.r = label_color.g = label_color.b = 0.0f;
			guiText.normal.textColor = label_color;
			
			//guiText.font = font_base;
			
			//GUI.Label(world_label, desc, guiText);
			world_label.x += 2;
			GUI.Label(world_label, desc, guiText);
			world_label.x -= 4;
			GUI.Label(world_label, desc, guiText);
			world_label.y += 2;
			GUI.Label(world_label, desc, guiText);
			world_label.y -= 4;
			GUI.Label(world_label, desc, guiText);
			
			
			world_label.x += 2;
			world_label.y += 2;
			
			//guiText.font = font_outline;
			label_color.r = label_color.g = label_color.b = 1.0f;
			guiText.normal.textColor = label_color;
			GUI.Label(world_label, desc, guiText);
			
			label_color.a = 1.0f;
		}
		
		// BOX AT BOTTOM OF SCREEn
		/*
		if(show)
		{
			GUI.Box(label_rect, name);
			GUI.Box(box_rect, desc);
			
			if(GUI.Button(button_rect, "Close"))
				show = false;
		}
		*/
	}
}
