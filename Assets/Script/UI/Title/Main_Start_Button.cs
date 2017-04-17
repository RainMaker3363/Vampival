using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Main_Start_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public AudioClip SceneChange;
    public SoundManager SDManager;

    private bool ButtonChecker;

    //public GameObject MissionInfoPanel;

    // Use this for initialization
    void Start()
    {
        ButtonChecker = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (ButtonChecker == false)
            {
                ButtonChecker = true;

                SDManager.PlaySfx(SceneChange);

                AutoFade.LoadLevel("StageLobbyScene", 2.0f, 1.0f, Color.black);
            }
        }

    }

    public void OnSelect(BaseEventData eventData)
    {
        if(ButtonChecker == false)
        {
            ButtonChecker = true;

            SDManager.PlaySfx(SceneChange);

            AutoFade.LoadLevel("StageLobbyScene", 2.0f, 1.0f, Color.black);
        }

        //UnityEngine.SceneManagement.SceneManager.LoadScene("MapTest");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
