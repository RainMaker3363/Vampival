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
    public GameObject[] Documents_Icon;

    public GameObject Main_BG;


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
                    if(GameManager.GamePauseOn == false)
                    {
                        audioSource.enabled = true;

                        switch (Doc_Num)
                        {
                            case 0:
                                {
                                    GameManager.Elizabat_SonicWave_Unlock = true;

                                    Document_BG.gameObject.SetActive(true);
                                    Documents[0].gameObject.SetActive(true);
                                }
                                break;

                            case 1:
                                {
                                    GameManager.Elizabat_Eclipse_Unlock = true;

                                    Document_BG.gameObject.SetActive(true);
                                    Documents[1].gameObject.SetActive(true);
                                }
                                break;

                            case 2:
                                {
                                    GameManager.Elizabat_Decent_Unlock = true;

                                    Document_BG.gameObject.SetActive(true);
                                    Documents[2].gameObject.SetActive(true);
                                }
                                break;

                            case 3:
                                {
                                    GameManager.Elizabat_Swarm_Unlock = true;

                                    Document_BG.gameObject.SetActive(true);
                                    Documents[3].gameObject.SetActive(true);
                                }
                                break;

                            default:
                                {

                                }
                                break;
                        }

                        //Document_BG.gameObject.SetActive(true);
                        //Documents[Doc_Num].gameObject.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("P1_360_AButton"))
                        {
                            GameManager.Gamestate = GameState.GameStart;

                            audioSource.enabled = false;

                            Document_BG.gameObject.SetActive(false);
                            Documents[Doc_Num].gameObject.SetActive(false);
                            Documents_Icon[Doc_Num].gameObject.SetActive(false);


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
                    
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
	}
}
