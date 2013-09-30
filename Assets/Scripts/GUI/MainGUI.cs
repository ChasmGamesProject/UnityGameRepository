using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour
{

    public Texture2D Inventoryicon;

    enum UITypes { MainGUI, InventoryGUI, MenuGUI, MenuOptionGUI };

    UITypes CurrentUI = UITypes.MenuGUI;

    private float vSliderValue = 0.0f;

    private Inventory inv;
    private Database db;

    void Start()
    {
        GameObject logic = GameObject.Find("_LOGIC");
        inv = (Inventory)(logic.GetComponent("Inventory"));
        db = (Database)(logic.GetComponent("Database"));
    }

    void OnGUI()
    {
        if (CurrentUI == UITypes.MainGUI)
        {
            if (GUI.Button(new Rect(25, Screen.height - 75, 50, 50), Inventoryicon))
            {          
                CurrentUI = UITypes.InventoryGUI;
            }

            if (GUI.Button(new Rect(25, 10, 50, 50), "Menu"))
            {
                //Put code here to switch back to the menu view
                CurrentUI = UITypes.MenuGUI;
            }
            //If any other GUI options need to go on the main game overlay they go here
        }
        else if (CurrentUI == UITypes.InventoryGUI)
        {

            GUI.Box(new Rect(Screen.width / 10, Screen.height / 10, Screen.width - (Screen.width / 10)*2, Screen.height - (Screen.height / 10)*2), "Inventory");
            if (GUI.Button(new Rect((Screen.width / 10) + 5, (Screen.height / 10) + 5, 45, 30), "Exit"))
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
                if (inv.GetObjectInSlot(i) == -1)
                {
                    GUI.Label(new Rect((Screen.width / 14) * ((i%5)+1) * 2, (Screen.height / 14) * 3 *j - 30, 256, 100), "Empty");
                }
                else
                {
                    GUI.Label(new Rect((Screen.width / 14) * ((i % 5) + 1) * 2, (Screen.height / 14) * 3 * j - 30, 256, 100), db.GetObject(inv.GetObjectInSlot(i)).name);
                }
                GUI.Box(new Rect((Screen.width / 14) * ((i % 5) + 1) * 2, (Screen.height / 14) * 3 * j, (Screen.width / 14), (Screen.width / 14)), Inventoryicon); //change later to fetch appropriate inventory icon
            }
        }
        else if (CurrentUI == UITypes.MenuGUI)
        {
            if (GUI.Button(new Rect(Screen.width / 2 -100, (Screen.height / 10) * 5, 200, 50), "Play Game"))
            {
                //Put code here to switch to the game view
                CurrentUI = UITypes.MainGUI;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, (Screen.height / 10) * 6, 200, 50), "Options"))
            {
                //Put code here to switch to the menu options view
                CurrentUI = UITypes.MenuOptionGUI;
            }
            //Any additional menu options can be added in here
        }
        else if (CurrentUI == UITypes.MenuOptionGUI)
        {
            //Test menu options slider
            GUI.Box(new Rect(Screen.width / 10, Screen.height / 10, 200, 30), "Master Volume");
            vSliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 14 * 3, Screen.height / 10+10, 100, 50), vSliderValue, 10.0f, 0.0f);
            //Fill out the rest of this when we know what options we want to give the players
        }
    }

}