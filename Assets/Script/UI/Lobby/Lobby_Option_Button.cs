﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Lobby_Option_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IPointerDownHandler, IDeselectHandler
{
    private ViewControllMode ViewMode;

    public AudioClip SceneChange;
    public SoundManager SDManager;

    public Image Select_BG;

    //public GameObject MissionInfoPanel;

    // Use this for initialization
    void Start()
    {
        Select_BG.color = new Color32(255, 255, 255, 0);
        ViewMode = ViewControllMode.Mouse;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {

        switch (ViewMode)
        {
            case ViewControllMode.Mouse:
                {
                    //SDManager.PlaySfx(SceneChange);

                    //AutoFade.LoadLevel("Stage01", 0.1f, 0.1f, Color.black);

                }
                break;

            case ViewControllMode.GamePad:
                {
                    Select_BG.color = new Color32(255, 255, 255, 255);
                }
                break;
        }
    }

    public void OnDeselect(BaseEventData data)
    {
        switch (ViewMode)
        {
            case ViewControllMode.Mouse:
                {
                    //SDManager.PlaySfx(SceneChange);

                    //AutoFade.LoadLevel("Stage01", 0.1f, 0.1f, Color.black);

                }
                break;

            case ViewControllMode.GamePad:
                {
                    Select_BG.color = new Color32(255, 255, 255, 0);
                }
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (ViewMode)
        {
            case ViewControllMode.Mouse:
                {
                    //SDManager.PlaySfx(SceneChange);
                    SDManager.PlaySfx(SceneChange);
                    //AutoFade.LoadLevel("Stage01", 0.1f, 0.1f, Color.black);
                }
                break;

            case ViewControllMode.GamePad:
                {
                    Select_BG.color = new Color32(255, 255, 255, 255);
                }
                break;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
        Select_BG.color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
        Select_BG.color = new Color32(255, 255, 255, 0);
    }
}
