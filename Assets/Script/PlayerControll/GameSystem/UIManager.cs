using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private GameState Gamestate;

    // 읽은 문서의 고유 번호
    //private int Doc_Num;

    public AudioClip Diary_BGM;
    private AudioSource audioSource;

    public GameObject Minimap;
    public GameObject Ok_Button;
    public Image Document_BG;
    public Image[] Documents;
    public GameObject[] Documents_Icon;

    public GameObject Main_BG;

    // 현재 몇번째 문서를 읽었는지의 정보
    static public int Document_Number;
    private bool DocumentRespawnChecker;

	// Use this for initialization
	void Start () {
        Gamestate = GameManager.Gamestate;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Diary_BGM;
        audioSource.enabled = false;

        Document_Number = -1;
        DocumentRespawnChecker = false;
        //Doc_Num = 0;

        if(GameManager.FirstBloodCheck == false)
        {
            Documents_Icon[1].SetActive(false);
        }
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


                    if(GameManager.GamePauseOn == false)
                    {
                        audioSource.enabled = true;

                        Minimap.SetActive(false);
                        Ok_Button.SetActive(true);
                        Document_BG.gameObject.SetActive(true);
                        Documents[Document_Number].gameObject.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("P1_360_AButton"))
                        {
                            GameManager.Gamestate = GameState.GameStart;


                            audioSource.enabled = false;

                            Ok_Button.SetActive(false);
                            Document_BG.gameObject.SetActive(false);
                            Documents[Document_Number].gameObject.SetActive(false);
                            Documents_Icon[Document_Number].gameObject.SetActive(false);


                            //if (Doc_Num <= Documents.Length)
                            //{
                            //    Doc_Num++;
                            //}
                            //else
                            //{
                            //    Doc_Num = 0;
                            //}
                        }
                    }
                    else
                    {
                        GameManager.Gamestate = GameState.GamePause;
                        GameManager.GamePauseOn = true;
                        
                        Main_BG.SetActive(true);
                    }
                    
                }
                break;

            case GameState.GameStart:
                {
                    if (GameManager.FirstBloodCheck == true && DocumentRespawnChecker == false)
                    {
                        Documents_Icon[1].SetActive(true);
                        Documents_Icon[1].transform.position = GameManager.FirstBloodPos;

                        print(GameManager.FirstBloodCheck);
                        print(GameManager.FirstBloodPos);
                        DocumentRespawnChecker = true;
                    }
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
	}
}
