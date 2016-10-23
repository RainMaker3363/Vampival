using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private GameState Gamestate;

    // 읽은 문서의 고유 번호
    private int Doc_Num;

    public AudioClip Diary_BGM;
    private AudioSource audioSource;

    public Image Document_BG;
    public Image[] Documents;


	// Use this for initialization
	void Start () {
        Gamestate = GameManager.Gamestate;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Diary_BGM;
        audioSource.enabled = false;

        Doc_Num = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Gamestate = GameManager.Gamestate;

        switch (Gamestate)
        {
            case GameState.GameIntro:
                {

                }
                break;

            case GameState.GamePause:
                {
                    audioSource.enabled = true;

                    Document_BG.gameObject.SetActive(true);
                    Documents[Doc_Num].gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("P1_360_AButton"))
                    {
                        GameManager.Gamestate = GameState.GameStart;

                        audioSource.enabled = false;

                        Document_BG.gameObject.SetActive(false);
                        Documents[Doc_Num].gameObject.SetActive(false);


                        if (Doc_Num <= Documents.Length)
                        {
                            Doc_Num++;
                        }
                        else
                        {
                            Doc_Num = 0;
                        }
                    }
                }
                break;

            case GameState.GameStart:
                {
                    
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
	}
}
