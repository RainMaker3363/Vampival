using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_Menu_Resume_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public AudioClip SceneChange;
    public SoundManager SDManager;

    public GameObject Menu_BG;

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

        GameManager.GamePauseOn = false;
        GameManager.Gamestate = GameState.GameStart;

        Menu_BG.SetActive(false);
        //AutoFade.LoadLevel("StageLobbyScene", 2.0f, 1.0f, Color.black);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MapTest");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
