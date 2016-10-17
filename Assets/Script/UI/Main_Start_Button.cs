using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Main_Start_Button : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public AudioClip TestAudio;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene2");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //SDManager.PlaySfx(TestAudio);
    }
}
