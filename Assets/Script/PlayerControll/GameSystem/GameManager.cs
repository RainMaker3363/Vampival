﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// 게임 상태 값
public enum GameState
{
    GameIntro = 0,
    GamePause,
    GameStart,
    GameEnd,
    GameVictory
}

// 시점 조작 모드 선택
public enum ViewControllMode
{
    GamePad,
    Mouse
};

// 조작할 대포 번호
public enum CannonNumber
{
    First = 1,
    Second,
    Third,
    Fourth
}

// 적들 상태 값
public enum EnemyState
{
    Idle = 0,
    Run,
    Dead,
    Stun,
    Detect
}

public class GameManager : MonoBehaviour {

    //===============================================================================================================================
    // 게임 레벨 디자인 설정 값
    //===============================================================================================================================

    //public Transform[] RespawnPoint;
    public GameObject[] Enemies;
    public GameObject[] Militia_Enemies;

    //===============================================================================================================================
    // 1P 엘리자벳의 조작
    //===============================================================================================================================
    
    // 커맨드 입력 On / OFF 여부
    static public bool Elizabat_CommandStart;
    static public bool Elizabat_SkillStart;

    // 효과 지속 여부
    static public bool Elizabat_Decent_On;
    static public bool Elizabat_Eclipse_On;
    static public bool Elizabat_Swarm_On;
    static public bool Elizabat_SonicWave_On;

    // 쿨타임이 전부 끝나서 준비됬는지의 여부
    static public bool Elizabat_Decent_Ready;
    static public bool Elizabat_Eclipse_Ready;
    static public bool Elizabat_Swarm_Ready;
    static public bool Elizabat_SonicWave_Ready;

    // 스킬 언락 여부
    static public bool Elizabat_Decent_Unlock;
    static public bool Elizabat_Eclipse_Unlock;
    static public bool Elizabat_Swarm_Unlock;
    static public bool Elizabat_SonicWave_Unlock;

    // 마나 게이지
    static public float Soul_MP_Parameter;
    float Soul_MP_Max;

    //===============================================================================================================================
    // 2P 카론의 조작
    //===============================================================================================================================

    // 현재 조작하는 대포의 번호
    static public CannonNumber CannonControl_Number;


    //===============================================================================================================================
    // 3P 스피다스의 조작
    //===============================================================================================================================

    //===============================================================================================================================
    // 게임 설정 값
    //===============================================================================================================================
    static public GameState Gamestate;
    static public ViewControllMode ViewMode;

    /// <summary>
    // 공포도의 영향을 줄 빛
    /// </summary>
    public Light light;

    // 사운드
    private AudioSource audioSource;
    public AudioClip InGameBGM;
    //public AudioClip GameOverBGM;

    // 게임 내의 UI들
    public Image SoulMP_Parameter_Gage;
    public Image Capture_Parameter_Gage;
    public Image GameOver_BG;

    public GameObject Main_UI;
    public GameObject MiniMap_UI;

    private bool SpawnOn;

    private int NowLevel;
    private float RespawnTimer;

    // 게임이 진행됨에 따라 빛의 농도를 바꿔준다.
    private float GameTimer;

    // 공포도 수치 (게임 승패 조건)
    static public float Fear_Parameter;
    private float Fear_Max;
    
    // 함락도 수치 (게임 패배 조건)
    static public float Capture_Parameter;
    private float CaptureTimer;

    private float Capture_Meter;
    private float Capture_Max;

    private bool SoundChecker;

    void Awake()
    {
        SpawnOn = false;
        NowLevel = 1;
        Fear_Parameter = 0;
        Capture_Parameter = 0;
        

        RespawnTimer = 1.0f;
        GameTimer = 0.0f;

        light.intensity = 0.35f;

        CaptureTimer = 0.0f;
        Capture_Max = 100.0f;
        
        Fear_Max = 50.0f;
        
        Soul_MP_Max = 100.0f;
        Soul_MP_Parameter = 100.0f;

        Capture_Meter = Capture_Max;

        //light.intensity = 1f;
        Elizabat_CommandStart = false;
        Elizabat_SkillStart = false;

        Elizabat_SonicWave_On = false;
        Elizabat_Eclipse_On = false;
        Elizabat_Decent_On = false;
        Elizabat_Swarm_On = false;

        Elizabat_Eclipse_Ready = true;
        Elizabat_Decent_Ready = true;
        Elizabat_SonicWave_Ready = true;
        Elizabat_Swarm_Ready = true;

        Elizabat_Eclipse_Unlock = true;
        Elizabat_Decent_Unlock = true;
        Elizabat_SonicWave_Unlock = true;
        Elizabat_Swarm_Unlock = true;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = InGameBGM;
        }


        SoundChecker = false;

        Main_UI.SetActive(false);
        MiniMap_UI.SetActive(false);

        //Gamestate = GameState.GameIntro;
        Gamestate = GameState.GameStart;
        ViewMode = ViewControllMode.Mouse;
        CannonControl_Number = CannonNumber.First;


        //ViewMode = ViewControllMode.Mouse;
        //CannonControl_Number = CannonNumber.First;
        //Gamestate = GameState.GameIntro;
    }

    public static void GameStartUp()
    {
        Gamestate = GameState.GameStart;
        ViewMode = ViewControllMode.Mouse;
        CannonControl_Number = CannonNumber.First;
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        switch(Gamestate)
        {
            case GameState.GameIntro:
                {
                    if (InGameBGM != null)
                    {
                        if (audioSource.isPlaying == false)
                        {
                            audioSource.volume -= 0.2f;
                            audioSource.Play();
                        }
                    }
                }
                break;

            case GameState.GameStart:
                {
                    if (Main_UI.activeSelf == false)
                    {
                        Main_UI.SetActive(true);
                    }

                    if(MiniMap_UI.activeSelf == false)
                    {
                        MiniMap_UI.SetActive(true);
                    }

                    if (SoundChecker == false)
                    {
                        SoundChecker = true;
                        audioSource.volume += 0.2f;
                    }
                    //if (InGameBGM != null)
                    //{
                    //    if (audioSource.isPlaying == false)
                    //    {

                    //        audioSource.Play();
                    //    }
                    //}

                    // 빛 타이머
                    //if (GameTimer >= 5.0f)
                    //{
                    //    GameTimer = 0.0f;
                    //    light.intensity += 0.036f;
                    //}
                    //else
                    //{
                    //    if (Elizabat_Eclipse_On == false)
                    //    {
                    //        GameTimer += Time.deltaTime;
                    //    }

                    //}

                    // 점령도 및 공포도 계산
                    if (CaptureTimer >= 1.0f)
                    {
                        CaptureTimer = 0.0f;

                        //if (Fear_Parameter_Gage.fillAmount < 1.0f)
                        //{
                        //    // 점령도를 계산해준다.
                        //    Fear_Parameter_Gage.fillAmount = (Fear_Parameter / Fear_Max);
                        //}
                        //else
                        //{
                        //    // 이겼을 경우...
                        //    Gamestate = GameState.GameVictory;
                        //}

                        if(Capture_Parameter_Gage.fillAmount <= 0.0f)
                        {
                            Gamestate = GameState.GameEnd;

                            GameOver_BG.gameObject.SetActive(true);
                            
                            //audioSource.clip = GameOverBGM;
                            audioSource.Stop();
                        }
                        else
                        {
                            Capture_Meter -= (Capture_Parameter * 0.5f);
                            Capture_Parameter_Gage.fillAmount = (Capture_Meter / Capture_Max);
                        }

                    }
                    else
                    {
                        CaptureTimer += Time.deltaTime;
                    }

                    // 마나 게이지 관리
                    if(Soul_MP_Parameter < Soul_MP_Max)
                    {
                        SoulMP_Parameter_Gage.fillAmount = (Soul_MP_Parameter / Soul_MP_Max);
                    }
                    else
                    {
                        Soul_MP_Parameter = Soul_MP_Max;
                        SoulMP_Parameter_Gage.fillAmount = 1.0f;
                    }
                    
                    

                    // 리스폰 타이머
                    if (RespawnTimer <= 0.0f)
                    {
                        RespawnTimer = 3.0f;

                        SpawnOn = true;
                    }
                    else
                    {
                        RespawnTimer -= Time.deltaTime;
                    }

                     // 리스폰을 한번씩 해준다.
                    if (SpawnOn)
                    {
                        SpawnOn = false;

                        int EnemyType = Random.Range(0, 1);
                        int EnemyLocation = Random.Range(0, 4);

                        RespawnEnemy(NowLevel, EnemyType, EnemyLocation);
                        //StartCoroutine(RespawnEnemy(NowLevel, EnemyType, EnemyLocation));
                    }



                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (CannonControl_Number > CannonNumber.First)
                        {
                            CannonControl_Number--;

                        }
                        else
                        {
                            CannonControl_Number = CannonNumber.Fourth;
                        }

                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (CannonControl_Number < CannonNumber.Fourth)
                        {
                            CannonControl_Number++;
                        }
                        else
                        {
                            CannonControl_Number = CannonNumber.First;
                        }
                    }
                }
                break;

            case GameState.GameVictory:
                {

                }
                break;

            case GameState.GameEnd:
                {
                    //audioSource.Play();
                }
                break;

            case GameState.GamePause:
                {
                    audioSource.Pause();
                }
                break;

        }
	}

    void RespawnEnemy(int Level, int Type, int Location)
    {
        switch(Level)
        {
            case 1:
                {
                    //int Respawn_LerpX = Random.Range(-5, 6);
                    //int Respawn_LerpZ = Random.Range(-5, 6);

                    //Instantiate(Enemies[Type], new Vector3(RespawnPoint[Location].position.x + Respawn_LerpX, RespawnPoint[Location].position.y, RespawnPoint[Location].position.z + Respawn_LerpZ), Quaternion.identity);

                    int RespawnNumber = Random.Range(0, Militia_Enemies.Length);
                    int SpawnCount = Level;

                    //print("RspawnNumber : " + RespawnNumber);
                    

                    if(SpawnCount > 0)
                    {
                        for (int i = 0; i < (Level * 3); i++)
                        {
                            if (Militia_Enemies[RespawnNumber].activeSelf == true)
                            {
                                RespawnNumber = Random.Range(0, Militia_Enemies.Length);

                                // print("Respawn Wait...");
                            }
                            else
                            {
                                Militia_Enemies[RespawnNumber].SetActive(true);

                                if(SpawnCount > 0)
                                {
                                    SpawnCount -= 1;    
                                }
                                else
                                {
                                    break;
                                }
                                

                                //  print("Respawning...");
                                //  print("SpawnCount : " + SpawnCount);
                            }
                        }
                    }


                    //for (int i = 0; i < Militia_Enemies.Length; i++)
                    //{
                    //    if(Militia_Enemies[i].GetComponent<Enemy_Militia>().enabled)
                    //    {
                    //        Militia_Enemies[i].SetActive(true);

                    //        //break;
                    //    }
                    //}
                    
                }
                break;

            case 2:
                {

                }
                break;

            case 3:
                {

                }
                break;
        }
//        yield return null;
    }
}
