using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour {

    private ViewControllMode ViewMode;

    public EventSystem ES;
    private GameObject storeSelect;

	// Use this for initialization
	void Start () {
        storeSelect = ES.firstSelectedGameObject;
        ViewMode = ViewControllMode.Mouse;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        ViewMode = GameManager.ViewMode;

        switch(ViewMode)
        {
            case ViewControllMode.Mouse:
                {

                }
                break;

            case ViewControllMode.GamePad:
                {
                    if (Input.GetButtonDown("P1_360_AButton"))
                    {
                        print("Button_A");
                    }

                    if (Input.GetButtonDown("P1_360_BButton"))
                    {
                        print("Button_B");
                    }

                    if (Input.GetButtonDown("P1_360_XButton"))
                    {
                        print("Button_X");
                    }

                    if (Input.GetButtonDown("P1_360_YButton"))
                    {
                        print("Button_Y");
                    }
                }
                break;
        }

        if(ES.currentSelectedGameObject != storeSelect)
        {
            if(ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(storeSelect);
            }
            else
            {
                storeSelect = ES.currentSelectedGameObject;
            }
        }
	}
}
