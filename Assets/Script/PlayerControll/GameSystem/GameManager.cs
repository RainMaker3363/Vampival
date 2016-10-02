﻿using UnityEngine;
using System.Collections;

// 게임 상태 값
public enum GameState
{
    GameIntro = 0,
    GamePause,
    GameStart,
    GameEnd
}

// 시점 조작 모드 선택
public enum ViewControllMode
{
    GamePad,
    Mouse
};

// 조작할 대포 번호
public enum CannonNumber : short
{
    First = 1,
    Second,
    Third,
    Fourth
}

public class GameManager : MonoBehaviour {

    public Transform[] RespawnPoint;
    public GameObject[] Enemies;

    /// <summary>
    // 공포도의 영향을 줄 빛
    /// </summary>
    public Light light;

    private bool SpawnOn;

    private int NowLevel;
    private float RespawnTimer;

    // 게임이 진행됨에 따라 빛의 농도를 바꿔준다.
    private float GameTimer;

    //===============================================================================================================================
    // 1P 엘리자벳의 조작
    //===============================================================================================================================
    
    // 커맨드 입력 On / OFF 여부
    static public bool Elizabat_CommandStart;
    static public bool Elizabat_SkillStart;

    static public bool Elizabat_Decent_On;
    static public bool Elizabat_Eclipse_On;
    static public bool Elizabat_Swarm_On;
    static public bool Elizabat_SonicWave_On;

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

    // 공포도 수치 (게임 승패 조건)
    static public int Fear_Parameter;
    
    // 함락도 수치 (게임 패배 조건)
    static public int Capture_Parameter;
    private float Capture_Meter;


    void Awake()
    {
        SpawnOn = false;
        NowLevel = 1;
        Fear_Parameter = 0;
        Capture_Parameter = 0;

        RespawnTimer = 1.0f;
        GameTimer = 0.0f;

        light.intensity = 0.35f;
        Elizabat_CommandStart = false;
        Elizabat_SkillStart = false;

        Elizabat_SonicWave_On = false;
        Elizabat_Eclipse_On = false;
        Elizabat_Decent_On = false;
        Elizabat_Swarm_On = false;

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
                    
                }
                break;

            case GameState.GameStart:
                {
                    
                    // 빛 타이머
                    if (GameTimer >= 5.0f)
                    {
                        GameTimer = 0.0f;
                        light.intensity += 0.036f;
                    }
                    else
                    {
                        if(Elizabat_Eclipse_On == false)
                        {
                            GameTimer += Time.deltaTime;
                        }

                    }

                    // 점령도 계산
                    Capture_Meter = (Capture_Parameter * 0.5f);

                    // 리스폰 타이머
                    if (RespawnTimer <= 0.0f)
                    {
                        RespawnTimer = 2.5f;

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

                        int EnemyType = Random.Range(0, 4);
                        int EnemyLocation = Random.Range(0, 4);

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

            case GameState.GameEnd:
                {

                }
                break;
        }
	}

    IEnumerator RespawnEnemy(int Level, int Type, int Location)
    {
        switch(Level)
        {
            case 1:
                {
                    int Respawn_LerpX = Random.Range(-5, 6);
                    int Respawn_LerpZ = Random.Range(-5, 6);

                    Instantiate(Enemies[Type], new Vector3(RespawnPoint[Location].position.x + Respawn_LerpX, RespawnPoint[Location].position.y, RespawnPoint[Location].position.z + Respawn_LerpZ), Quaternion.identity);
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
        yield return null;
    }
}
