using UnityEngine;
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
    GamePad = 0,
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

// 대포 모드
public enum CannonWeapon
{
    Normal = 0,
    Buff
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
    public GameObject[] Militia_Enemies;

    public GameObject[] Respawns_East_Enemies;
    public GameObject[] Respawn_South_Enemies;
    public GameObject[] Respawns_West_Enemies;
    public GameObject[] Respawns_North_Enemies;

    // 현재 어느 적이 리스폰이 되어야할지의 여부를 체크해준다.
    private float LevelChecker;
    private bool RespawnCheck;
    private int Level;

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
    private float Soul_MP_Max;

    //===============================================================================================================================
    // 2P 카론의 조작
    //===============================================================================================================================

    // 현재 조작하는 대포의 번호
    static public CannonNumber CannonControl_Number;
    static public CannonWeapon CannonWeapon_Toggle;

    static public int BuffCannonStack;

    //===============================================================================================================================
    // 3P 스피다스의 조작
    //===============================================================================================================================

    // 스피다스의 파워업 여부
    static public bool Spidas_PowerUp_On;
    
    // 스피다스의 사망 여부
    static public bool Spidas_Death_On;

    // 스피다스의 부활 여부
    static public bool Spidas_Revive_On;

    //===============================================================================================================================
    // 게임 설정 값
    //===============================================================================================================================
    static public GameState Gamestate;
    static public ViewControllMode ViewMode;
    static public bool GamePauseOn;

    // Stage01 시연 기믹 용
    static public bool FirstBloodCheck;
    static public Vector3 FirstBloodPos;

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

    public GameObject Menu_UI;
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
        //===========================================================
        // 게임 내부 조정 값
        //===========================================================
        SpawnOn = false;
        NowLevel = 1;
        Fear_Parameter = 0;
        Capture_Parameter = 0;
        

        RespawnTimer = 1.0f;
        GameTimer = 0.0f;

        light.intensity = 0.35f;

        CaptureTimer = 0.0f;
        Capture_Max = 250.0f;
        Capture_Meter = Capture_Max;

        Fear_Max = 50.0f;
        
        Soul_MP_Max = 50.0f;
        Soul_MP_Parameter = Soul_MP_Max;

        

        //light.intensity = 1f;

        //===========================================================
        // 엘리자벳 스킬 여부
        //===========================================================
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

        Elizabat_Eclipse_Unlock = false;
        Elizabat_Decent_Unlock = false;
        Elizabat_SonicWave_Unlock = false;
        Elizabat_Swarm_Unlock = false;

        //===========================================================
        // 스피다스 조작 부분
        //===========================================================
        Spidas_PowerUp_On = true;
        Spidas_Death_On = false;
        Spidas_Revive_On = true;

        //===========================================================
        // 카로 조작 부분
        //===========================================================

        BuffCannonStack = 50;

        //===========================================================
        // 적들 리스폰 조작 (Stage01)
        //===========================================================
        
        //for (int i = 0; i < Respawns_East_Enemies.Length; i++)
        //{
        //    Respawns_East_Enemies[i].SetActive(false);
        //}

        //for (int i = 0; i < Respawn_South_Enemies.Length; i++)
        //{
        //    Respawn_South_Enemies[i].SetActive(false);
        //}

        //for (int i = 0; i < Respawns_West_Enemies.Length; i++)
        //{
        //    Respawns_West_Enemies[i].SetActive(false);
        //}

        //for (int i = 0; i < Respawns_North_Enemies.Length; i++)
        //{
        //    Respawns_North_Enemies[i].SetActive(false);
        //}

        //Respawns_East_Enemies[0].SetActive(true);
        //Respawn_South_Enemies[0].SetActive(true);
        //Respawns_North_Enemies[0].SetActive(true);

        LevelChecker = 0.0f;
        Level = 0;
        RespawnCheck = false;

        //===========================================================
        // ETC...
        //===========================================================

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = InGameBGM;
        }


        SoundChecker = false;

        Menu_UI.SetActive(false);
        Main_UI.SetActive(false);
        MiniMap_UI.SetActive(false);

        GamePauseOn = false;
        FirstBloodCheck = false;
        FirstBloodPos = Vector3.zero;

        //Gamestate = GameState.GameIntro;
        Gamestate = GameState.GameStart;
        ViewMode = ViewControllMode.Mouse;
        CannonControl_Number = CannonNumber.First;
        CannonWeapon_Toggle = CannonWeapon.Normal;

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

        //print(Gamestate);

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
                            if (Elizabat_Eclipse_On)
                            {
                                Capture_Meter -= ((Capture_Parameter * 0.5f) * 0.8f);
                            }
                            else
                            {
                                Capture_Parameter_Gage.gameObject.SetActive(true);

                                Capture_Meter -= (Capture_Parameter * 0.5f);
                            }
                            
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
                    else if (Soul_MP_Parameter >= Soul_MP_Max)
                    {
                        Soul_MP_Parameter = Soul_MP_Max;
                        SoulMP_Parameter_Gage.fillAmount = 1.0f;
                    }
                    
                    // 대포 스킬 스택 관리
                    if(BuffCannonStack <= 0)
                    {
                        BuffCannonStack = 0;
                    }
                    

                    // 리스폰 타이머
                    //LevelChecker += Time.deltaTime;

                    //if(((LevelChecker / 14.0f) >= 1 ||
                    //    (LevelChecker / 22.0f) >= 1) &&
                    //    RespawnCheck == false)
                    //{
                    //    Stage01RespawnEnemies((int)(LevelChecker / 14),
                    //        (int)(LevelChecker / 14),
                    //        (int)(LevelChecker / 14),
                    //        (int)(LevelChecker / 22));
                    //}

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

                    switch(ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
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

                                // 대포 모드 변경
                                if(Input.GetKeyDown(KeyCode.LeftControl))
                                {
                                    if(CannonWeapon_Toggle == CannonWeapon.Normal)
                                    {
                                        CannonWeapon_Toggle = CannonWeapon.Buff;
                                    }
                                    else
                                    {
                                        CannonWeapon_Toggle = CannonWeapon.Normal;
                                    }
                                    
                                }

                                if (Input.GetKeyDown(KeyCode.P))
                                {
                                    if (Gamestate == GameState.GamePause)
                                    {
                                        GamePauseOn = false;
                                        Gamestate = GameState.GameStart;
                                    }
                                    else
                                    {
                                        GamePauseOn = true;
                                        Gamestate = GameState.GamePause;
                                    }
                                }
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                if (Input.GetButtonDown("P2_360_LeftBumper"))
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
                                else if (Input.GetButtonDown("P2_360_RightBumper"))
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

                                if (Input.GetButtonDown("P2_360_YButton"))
                                {
                                    if (CannonWeapon_Toggle == CannonWeapon.Normal)
                                    {
                                        CannonWeapon_Toggle = CannonWeapon.Buff;
                                    }
                                    else
                                    {
                                        CannonWeapon_Toggle = CannonWeapon.Normal;
                                    }

                                }

                                if (Input.GetButtonDown("P1_360_StartButton") ||
                                     Input.GetButtonDown("P2_360_StartButton") ||
                                     Input.GetButtonDown("P3_360_StartButton"))
                                {
                                    if (Gamestate == GameState.GamePause)
                                    {
                                        GamePauseOn = false;
                                        Gamestate = GameState.GameStart;
                                    }
                                    else
                                    {
                                        GamePauseOn = true;
                                        Gamestate = GameState.GamePause;
                                    }
                                }
                            }
                            break;
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
                    Menu_UI.SetActive(true);
                }
                break;

            case GameState.GamePause:
                {
                    audioSource.Pause();

                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                if (Input.GetKeyDown(KeyCode.P))
                                {
                                    if (Gamestate == GameState.GamePause)
                                    {
                                        GamePauseOn = false;
                                        Gamestate = GameState.GameStart;
                                    }
                                    else
                                    {
                                        GamePauseOn = true;
                                        Gamestate = GameState.GamePause;
                                    }
                                }
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                if (Input.GetButtonDown("P1_360_StartButton") ||
                                    Input.GetButtonDown("P2_360_StartButton") ||
                                    Input.GetButtonDown("P3_360_StartButton"))
                                {
                                    if(Gamestate == GameState.GamePause)
                                    {
                                        GamePauseOn = false;
                                        Gamestate = GameState.GameStart;
                                    }
                                    else
                                    {
                                        GamePauseOn = true;
                                        Gamestate = GameState.GamePause;
                                    }
                                }
                            }
                            break;
                    }
                }
                break;

        }
	}

    // 스테이지 01의 리스폰이다.
    // 각각 4방향에서 적들에 대한 설정
    void Stage01RespawnEnemies(int EastLevel, int SouthLevel, int WestLevel, int NorthLevel)
    {
        // 동쪽 적들 리스폰 설정
        switch(EastLevel)
        {
            case 1:
                {
                    Respawns_East_Enemies[1].SetActive(true);
                }
                break;

            case 2:
                {
                    Respawns_East_Enemies[2].SetActive(true);
                }
                break;

            case 3:
                {
                    
                }
                break;

            case 4:
                {
                    Respawns_East_Enemies[3].SetActive(true);
                }
                break;

            case 5:
                {

                }
                break;

            case 6:
                {
                    Respawns_East_Enemies[4].SetActive(true);
                    Respawns_East_Enemies[5].SetActive(true);
                }
                break;

            case 7:
                {
                    Respawns_East_Enemies[6].SetActive(true);
                    Respawns_East_Enemies[7].SetActive(true);
                }
                break;

            case 8:
                {

                }
                break;

            case 9:
                {
                    Respawns_East_Enemies[8].SetActive(true);
                    Respawns_East_Enemies[9].SetActive(true);
                }
                break;

            case 10:
                {

                }
                break;
        }

        // 남쪽 
        switch (SouthLevel)
        {
            case 1:
                {
                    Respawn_South_Enemies[1].SetActive(true);
                }
                break;

            case 2:
                {
                    Respawn_South_Enemies[2].SetActive(true);
                }
                break;

            case 3:
                {
                    Respawn_South_Enemies[3].SetActive(true);
                    Respawn_South_Enemies[4].SetActive(true);
                }
                break;

            case 4:
                {
                    Respawn_South_Enemies[5].SetActive(true);
                }
                break;

            case 5:
                {
                    Respawn_South_Enemies[6].SetActive(true);
                    Respawn_South_Enemies[7].SetActive(true);
                }
                break;

            case 6:
                {
                    Respawn_South_Enemies[8].SetActive(true);
                    Respawn_South_Enemies[9].SetActive(true);
                }
                break;

            case 7:
                {
                    Respawn_South_Enemies[10].SetActive(true);
                    Respawn_South_Enemies[11].SetActive(true);
                }
                break;

            case 8:
                {
                    Respawn_South_Enemies[12].SetActive(true);
                    Respawn_South_Enemies[13].SetActive(true);
                }
                break;

            case 9:
                {
                    Respawn_South_Enemies[14].SetActive(true);
                    Respawn_South_Enemies[15].SetActive(true);
                }
                break;

            case 10:
                {

                }
                break;
        }

        // 서쪽
        switch (WestLevel)
        {
            case 1:
                {
                    Respawns_West_Enemies[0].SetActive(true);
                }
                break;

            case 2:
                {
                    Respawns_West_Enemies[1].SetActive(true);
                }
                break;

            case 3:
                {
                    Respawns_West_Enemies[2].SetActive(true);
                }
                break;

            case 4:
                {

                }
                break;

            case 5:
                {
                    Respawns_West_Enemies[3].SetActive(true);
                    Respawns_West_Enemies[4].SetActive(true);
                }
                break;

            case 6:
                {
                    Respawns_West_Enemies[5].SetActive(true);
                }
                break;

            case 7:
                {

                }
                break;

            case 8:
                {
                    Respawns_West_Enemies[6].SetActive(true);
                    Respawns_West_Enemies[7].SetActive(true);
                }
                break;

            case 9:
                {
                    Respawns_West_Enemies[8].SetActive(true);
                    Respawns_West_Enemies[9].SetActive(true);
                }
                break;

            case 10:
                {

                }
                break;
        }

        // 북쪽
        switch (NorthLevel)
        {
            case 1:
                {

                }
                break;

            case 2:
                {
                    Respawns_North_Enemies[1].SetActive(true);
                }
                break;

            case 3:
                {
                    Respawns_North_Enemies[2].SetActive(true);
                }
                break;

            case 4:
                {
                    Respawns_North_Enemies[3].SetActive(true);
                }
                break;

            case 5:
                {

                }
                break;

            case 6:
                {

                }
                break;

            case 7:
                {
                    Respawns_North_Enemies[4].SetActive(true);
                    Respawns_North_Enemies[5].SetActive(true);
                }
                break;

            case 8:
                {
                    Respawns_North_Enemies[6].SetActive(true);
                    Respawns_North_Enemies[7].SetActive(true);
                }
                break;

            case 9:
                {
                    Respawns_North_Enemies[8].SetActive(true);
                    Respawns_North_Enemies[9].SetActive(true);
                }
                break;

            case 10:
                {

                }
                break;
        }

        // 리스폰 체크를 풀어준다.
        RespawnCheck = false;
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
