using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour
{

    public Texture2D Inventoryicon;

    enum UITypes { MainGUI, InventoryGUI, MenuGUI, MenuOptionGUI };

    UITypes CurrentUI = UITypes.MenuGUI;

    private float vSliderValue = 0.0f;
    private Rect Icon50;
    private Rect IconInventoryScalable;
    private Rect LabelInventoryScalable;
    private Rect Icon45;
    private Rect MenuButton;
    private Rect InventoryBackground;

    private Inventory inv;
    private Database db;

    void Start()
    {
        GameObject logic = GameObject.Find("_LOGIC");
        inv = (Inventory)(logic.GetComponent("Inventory"));
        db = (Database)(logic.GetComponent("Database"));

        Icon50 = new Rect(0, 0, 50, 50); // Create a default rectangle of 50x50 pixels, simply specify later where to locate it
        Icon45 = new Rect(0, 0, 45, 30); //Create a default rectangle of 45x30 pixels
        IconInventoryScalable = new Rect(0, 0, (Screen.width / 14), (Screen.width / 14)); //create an inventory box of scalable size depending on resolution
        LabelInventoryScalable = new Rect(0, 0, 256, 100); //create an inventory label of scalable size depending on resolution 
        MenuButton = new Rect(0, 0, 200, 50); //Create a default menu button size
        InventoryBackground = new Rect(Screen.width / 10, Screen.height / 10, Screen.width - (Screen.width / 10)*2, Screen.height - (Screen.height / 10)*2);
    }

    void OnGUI()
    {
        if (CurrentUI == UITypes.MainGUI)
        {
            Icon50.x = 25;
            Icon50.y = Screen.height - 75;
            if (GUI.Button(Icon50, Inventoryicon))
            {          
                CurrentUI = UITypes.InventoryGUI;
            }
            Icon50.y = 10;
            if (GUI.Button(Icon50, "Menu"))
            {
                //Put code here to switch back to the menu view
                CurrentUI = UITypes.MenuGUI;
            }
            //If any other GUI options need to go on the main game overlay they go here
        }
        else if (CurrentUI == UITypes.InventoryGUI)
        {

            GUI.Box(InventoryBackground, "Inventory");
            Icon45.x = (Screen.width / 10) + 5;
            Icon45.y = (Screen.height / 10) + 5;
            if (GUI.Button(Icon45, "Exit"))
            {
                CurrentUI = UITypes.MainGUI;
            }
            int j = 0;
            for (int i = 0; i < Inventory.INVENTORY_SIZE; i++)
            {
                if (i % 5 == 0)
                {
                    j++;
                }
                LabelInventoryScalable.x = (Screen.width / 14) * ((i % 5) + 1) * 2;
                LabelInventoryScalable.y = (Screen.height / 14) * 3 * j -30;
                if (inv.GetObjectInSlot(i) == -1)
                {
                    GUI.Label(LabelInventoryScalable, "Empty");
                }
                else
                {
                    GUI.Label(LabelInventoryScalable, db.GetObject(inv.GetObjectInSlot(i)).name);
                }
                IconInventoryScalable.x = (Screen.width / 14) * ((i % 5) + 1) * 2;
                IconInventoryScalable.y = (Screen.height / 14) * 3 * j;
                GUI.Box(IconInventoryScalable, Inventoryicon); //change later to fetch appropriate inventory icon
            }
        }
        else if (CurrentUI == UITypes.MenuGUI)
        {
            MenuButton.x = Screen.width / 2 -100;
            MenuButton.y = (Screen.height / 10) * 5;
            if (GUI.Button(MenuButton, "Play Game"))
            {
                //Put code here to switch to the game view
                CurrentUI = UITypes.MainGUI;
            }
            MenuButton.y = (Screen.height / 10) * 6;
            if (GUI.Button(MenuButton, "Options"))
            {
                //Put code here to switch to the menu options view
                CurrentUI = UITypes.MenuOptionGUI;
            }
            //Any additional menu options can be added in here
        }
        else if (CurrentUI == UITypes.MenuOptionGUI)
        {
            //Test menu options slider
            MenuButton.x = Screen.width / 10;
            MenuButton.y = Screen.height / 10;
            GUI.Box(MenuButton, "Master Volume");

            //Removed for now, when menu choices are finalised proper size and position of sliders will be determined.
            //vSliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 14 * 3, Screen.height / 10+10, 100, 50), vSliderValue, 10.0f, 0.0f);
            //Fill out the rest of this when we know what options we want to give the players
        }
    }

}