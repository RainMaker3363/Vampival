using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Lobby_Tutorial_Skill_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IPointerDownHandler, IDeselectHandler
{
    private ViewControllMode ViewMode;

    public AudioClip SceneChange;
    public SoundManager SDManager;

    //public Image Select_BG;

    // Use this for initialization
    void Start()
    {
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

                }
                break;

            case ViewControllMode.GamePad:
                {
                    //Select_BG.color = new Color32(255, 255, 255, 255);
                }
                break;
        }

        //UnityEngine.SceneManagement.SceneManager.LoadScene("MapTest");
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
                    //Select_BG.color = new Color32(255, 255, 255, 0);
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

                    //AutoFade.LoadLevel("Stage01", 0.1f, 0.1f, Color.black);
                    SDManager.PlaySfx(SceneChange);

                    LobbyManager.Skill_Button_On = true;
                    LobbyManager.Controll_Button_On = false;
                }
                break;

            case ViewControllMode.GamePad:
                {

                }
                break;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
        //Select_BG.color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
        //Select_BG.color = new Color32(255, 255, 255, 0);
    }
}
