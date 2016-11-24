using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Main_Exit_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public AudioClip TestAudio;
    public SoundManager SDManager;
    
    //public static string webplayerQuitURL = "http://google.com";
    #if UNITY_WEBPLAYER
        public static string webplayerQuitURL = "http://kr.battle.net/heroes/ko/";
    #endif


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
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
         #else
            Application.Quit();
         #endif
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
