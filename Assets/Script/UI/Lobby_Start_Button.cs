﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Lobby_Start_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public AudioClip SceneChange;
    public SoundManager SDManager;

    //public GameObject MissionInfoPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect(BaseEventData eventData)
    {
        //SDManager.PlaySfx(SceneChange);

        AutoFade.LoadLevel("MapTest", 0.1f, 0.1f, Color.black);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MapTest");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
