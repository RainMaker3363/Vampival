using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_Menu_Exit_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
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

        Menu_BG.SetActive(false);

        AutoFade.LoadLevel("Title", 0.2f, 0.2f, Color.black);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MapTest");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
