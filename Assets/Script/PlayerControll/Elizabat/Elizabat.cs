using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private AudioSource Audio;
    public AudioClip SonicWaveSound;
    public AudioClip EclipseSound;
    public AudioClip SonicWave_Command_Correct_Sound;
    public AudioClip Eclipse_Command_Correct_Sound;
    public AudioClip Swarm_Command_Correct_Sound;
    public AudioClip CommandStart_Sound;

    private bool ElizabatDecentOn;

    private Vector3 CheckerStartPos = Vector3.zero;
    private Vector3 targetPosOnScreen;
    private RaycastHit hit;
    private Light light;
    //private Ray ray;

    public Image Skill_Not_Yangpigi;
    public Image Skill_True_Yangpigi;
    public Image Skill_Button;

    public GameObject Skill_SonicWave_Icon_Block;
    public GameObject Skill_SonicWave_Icon;
    public GameObject Skill_SonicWave_Command_Chart;
    public Image Skill_SonicWave_Cool_Icon;

    public GameObject Skill_Eclipse_Icon_Block;
    public GameObject Skill_Eclipse_Icon;
    public GameObject Skill_Eclipse_Command_Chart;
    public Image Skill_Eclipse_Cool_Icon;

    public GameObject Skill_Decent_Icon_Block;
    public GameObject Skill_Decent_Icon;
    public GameObject Skill_Decent_Command_Chart;
    public Image Skill_Decent_Cool_Icon;

    public GameObject Skill_Swarm_Icon_Block;
    public GameObject Skill_Swarm_Icon;
    public GameObject Skill_Swarm_Command_Chart;
    public Image Skill_Swarm_Cool_Icon;

    public Image Skill_SonicWave_Logo;
    public Image Skill_Eclipse_Logo;
    public Image Skill_Decent_Logo;
    public Image Skill_Swarm_Logo;

    public Image[] Skill_SonicWave_Commands;
    public Image[] Skill_Eclipse_Commands;
    public Image[] Skill_Decent_Commands;
    public Image[] Skill_Swarm_Commands;

    public Sprite[] Command_Arrows;

    public GameObject CameraChecker;
    public GameObject SkillTarget;
    public GameObject Elizabat_Interpol;

    public Camera MainCamera;

    // 소닉 웨이브 커맨드 5개 (고정1 + 랜덤 4)
    private int[] SonicWaveCommand = new int[7];
    // 강하 공격 커맨드 3개 (고정1 + 랜덤 2)
    private int[] DecentCommand = new int[7];
    // 스웜 공격 커맨드 7개 (고정1 + 랜덤 6)
    private int[] SwarmCommand = new int[7];
    // 일식 스킬 커맨드 7개 (고정1 + 랜덤 6)
    private int[] EclipseCommand = new int[7];

    // 스킬 쿨 타임
    private float SonicCoolTime;
    private float EclipseCoolTime;
    private float DecentCoolTime;
    private float SwarmCoolTime;

    // 스킬 제한 시간
    private float SonicTimeLimit;
    private float EclipseTimeLimit;
    private float DecentTimeLimit;
    private float SwarmTimeLimit;

    private float CommandCheckTimer;

    // 스킬 마나 관련...
    private float SonicWaveCost;
    private float EclipseCost;
    private float DecentCost;
    private float SwarmCost;

    // 스킬 쿨 타임 체커
    private float SkillCoolTimer;

    // 스킬 입력 시스템 관련...
    private int EclipseMaxCount;
    private int SwarmMaxCount;
    private int DecentMaxCount;
    private int SonicMaxCount;

    private int NowSkillChecking;
    private bool CommandOn;
    private bool CommandStartOn;

    private int Inputcommand;
    private int currentNum;

    private float Leng;
    private int layermask;


    private bool KeyDownEnable = false;
    //private RaycastHit hit;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        CommandOn = true;
        CommandStartOn = false;
        NowSkillChecking = 0;
        ElizabatDecentOn = true;


        // 스킬 언락 여부 설정

        Skill_SonicWave_Icon_Block.SetActive(true);
        Skill_Eclipse_Icon_Block.SetActive(true);
        Skill_Decent_Icon_Block.SetActive(true);
        Skill_Swarm_Icon_Block.SetActive(true);

        Skill_SonicWave_Icon.gameObject.SetActive(false);
        Skill_Decent_Icon.gameObject.SetActive(false);
        Skill_Eclipse_Icon.gameObject.SetActive(false);
        Skill_Swarm_Icon.gameObject.SetActive(false);

        // 스킬 커맨드 관련...

        SonicWaveCommand[0] = 1;
        DecentCommand[0] = 3;
        SwarmCommand[0] = 4;
        EclipseCommand[0] = 2;

        
        for (int i = 1; i < 7; i++)
        {
            SonicWaveCommand[i] = 0;
            DecentCommand[i] = 0;
            SwarmCommand[i] = 0;
            EclipseCommand[i] = 0;
        }

        Skill_SonicWave_Commands[0].sprite = Command_Arrows[1];
        Skill_Eclipse_Commands[0].sprite = Command_Arrows[2];
        Skill_Decent_Commands[0].sprite = Command_Arrows[3];
        Skill_Swarm_Commands[0].sprite = Command_Arrows[4];

        for (int j = 1; j < Skill_Eclipse_Commands.Length; j++ )
        {
            Skill_SonicWave_Commands[j].sprite = Command_Arrows[0];
            Skill_Eclipse_Commands[j].sprite = Command_Arrows[0];
            Skill_Decent_Commands[j].sprite = Command_Arrows[0];
            Skill_Swarm_Commands[j].sprite = Command_Arrows[0];
        }

        //Skill_SonicWave_Command_Chart.gameObject.SetActive(GameManager.Elizabat_SonicWave_Unlock);
        //Skill_Eclipse_Command_Chart.gameObject.SetActive(GameManager.Elizabat_Eclipse_Unlock);
        //Skill_Decent_Command_Chart.gameObject.SetActive(GameManager.Elizabat_Decent_Unlock);
        //Skill_Swarm_Command_Chart.gameObject.SetActive(GameManager.Elizabat_Swarm_Unlock);

        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
        Skill_Decent_Command_Chart.gameObject.SetActive(false);
        Skill_Swarm_Command_Chart.gameObject.SetActive(false);

        // 스킬 쿨 타임 지정
        SkillCoolTimer = 0.0f;

        SonicCoolTime = 1.0f / 5.0f;
        DecentCoolTime = 1.0f / 3.0f;
        EclipseCoolTime = 1.0f / 20.0f;
        SwarmCoolTime = 1.0f / 7.0f;

        // 스킬 소모 마나 코스트 설정
        SonicWaveCost = 2.0f;
        EclipseCost = 2.0f;
        DecentCost = 2.0f;
        SwarmCost = 2.0f;

        // 스킬 커맨드 제한 시간
        SonicTimeLimit = 1.5f;
        EclipseTimeLimit = 2.0f;
        DecentTimeLimit = 2.5f;
        SwarmTimeLimit = 3.0f;

        CommandCheckTimer = 0.0f;

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
        }

        Inputcommand = 0;
        currentNum = 0;

        light = GetComponent<Light>();
        light.enabled = false;

        CheckerStartPos = CameraChecker.transform.position;

        Leng = 0.0f;
        layermask = (1 << LayerMask.NameToLayer("LayCastIn"));//(-1) - (1 << 9) | (1 << 10) | (1 << 12) | (1 << 15);

        SkillTarget.gameObject.SetActive(false);
	}

    
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        switch (Gamestate)
        {
            case GameState.GameIntro:
                {

                }
                break;

            case GameState.GamePause:
                {

                }
                break;

            case GameState.GameStart:
                {
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {


                                //pushObjectBackInFrustum(this.gameObject.transform);

                                //Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 100);

                                //Vector3 direction = new Vector3(hit.normal.x, hit.normal.y * 1, hit.normal.z);
                                
                                // 화면 안에 있을 경우 처리
                                //if (targetPosOnScreen.x < Screen.width || targetPosOnScreen.x > 0 || targetPosOnScreen.y < Screen.height || targetPosOnScreen.y > 0)
                                //{
                                //    Debug.Log("Camera In : " + targetPosOnScreen);
                                //}
                                //else
                                //{
                                //    Debug.Log("Camera Out : " + targetPosOnScreen);
                                //}

                                //Debug.Log("Screen.widht : " + Screen.width);
                                //Debug.Log("Screen.height : " + Screen.height);

                                
                                
                                //targetPosOnScreen.y += -1;

                                // 마우스 작업
                                //if ((targetPosOnScreen.x < Screen.width && targetPosOnScreen.x > 0) || (targetPosOnScreen.y > Screen.height && targetPosOnScreen.y < 0))
                                //{


                                //    CameraMove = true;
                                //    //Debug.Log("Camera In : X " + targetPosOnScreen.x + ", Y " + targetPosOnScreen.y);
                                //}
                                //else
                                //{
                                //    CameraMove = false;
                                //    //Debug.Log("Camera Out : X " + targetPosOnScreen.x + ", Y " + targetPosOnScreen.y);
                                //}

                                // 스킬 언락 여부 설정

                                if(GameManager.Elizabat_SonicWave_Unlock == true)
                                {
                                    Skill_SonicWave_Icon_Block.SetActive(false);
                                    Skill_SonicWave_Icon.gameObject.SetActive(true);
                                }
                                
                                if(GameManager.Elizabat_Eclipse_Unlock == true)
                                {
                                    Skill_Eclipse_Icon_Block.SetActive(false);
                                    Skill_Eclipse_Icon.gameObject.SetActive(true);
                                }

                                if(GameManager.Elizabat_Decent_Unlock == true)
                                {
                                    Skill_Decent_Icon_Block.SetActive(false);
                                    Skill_Decent_Icon.gameObject.SetActive(true);
                                }

                                if(GameManager.Elizabat_Swarm_Unlock == true)
                                {
                                    Skill_Swarm_Icon_Block.SetActive(false);
                                    Skill_Swarm_Icon.gameObject.SetActive(true);
                                }

                                // 스킬 쿨타임
                                if (SkillCoolTimer < 1.0f)
                                {
                                    SkillCoolTimer += Time.deltaTime;
                                }
                                else
                                {
                                    // 일식
                                    if (Skill_Eclipse_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Eclipse_Cool_Icon.fillAmount -= EclipseCoolTime;
                                    }
                                    else if (Skill_Eclipse_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Eclipse_Ready = true;
                                    }

                                    // 소닉 웨이브
                                    if (Skill_SonicWave_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_SonicWave_Cool_Icon.fillAmount -= SonicCoolTime;
                                    }
                                    else if (Skill_SonicWave_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_SonicWave_Ready = true;
                                    }

                                    // 스웜 공격
                                    if (Skill_Swarm_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Swarm_Cool_Icon.fillAmount -= SwarmCoolTime;
                                    }
                                    else if (Skill_Swarm_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Swarm_Ready = true;
                                    }

                                    // 강하 공격
                                    if (Skill_Decent_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Decent_Cool_Icon.fillAmount -= DecentCoolTime;
                                    }
                                    else if (Skill_Decent_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Decent_Ready = true;
                                    }

                                    SkillCoolTimer = 0.0f;
                                }
                                


                                if (!GameManager.Elizabat_CommandStart && !GameManager.Elizabat_SkillStart)
                                {
                                    light.enabled = false;
                                    SkillTarget.gameObject.SetActive(false);

                                    if (Input.GetKeyDown(KeyCode.C))
                                    {
                                        Debug.Log("Command Start!");

                                        CameraChecker.transform.position = CheckerStartPos;

                                        CommandInitilization();

                                        // Skill UI 활성화 사운드
                                        Audio.clip = CommandStart_Sound;
                                        Audio.Play();

                                        // Skill UI를 활성화 시킨다.
                                        //Skill_Not_Yangpigi.gameObject.SetActive(false);
                                        //Skill_True_Yangpigi.gameObject.SetActive(true);
                                        //Skill_Button.gameObject.SetActive(false);
                                    }
                                }


                                if (GameManager.Elizabat_CommandStart)
                                {
                                    light.enabled = true;
                                    SkillTarget.gameObject.SetActive(true);

                                    targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                    

                                    if (Physics.Raycast(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 300.0f, out hit, Mathf.Infinity, layermask))
                                    {
                                        //print("Ground");

                                        Debug.DrawRay(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 300.0f, Color.green);

                                        if (hit.collider.tag.Equals("Ground") == true)
                                        {
                                            Leng = Vector3.Distance(this.transform.position, hit.point);

                                            SkillTarget.transform.localPosition = new Vector3(SkillTarget.transform.localPosition.x, SkillTarget.transform.localPosition.y, Leng);
                                            CameraChecker.transform.localPosition = SkillTarget.transform.localPosition;
                                        }
                                    }

                                    // 스킬 캔슬 여부
                                    if (Input.GetKeyDown(KeyCode.Z))
                                    {
                                        CommandCancel();

                                        // Skill UI를 활성화 시킨다.
                                        //Skill_Not_Yangpigi.gameObject.SetActive(false);
                                        //Skill_True_Yangpigi.gameObject.SetActive(true);
                                        //Skill_Button.gameObject.SetActive(false);
                                    }

                                    // 스킬 시전 좌표 이동
                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetKey(KeyCode.F))
                                        {
                                            //print("Move Left");
                                            transform.Translate(new Vector3(-2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetKey(KeyCode.H))
                                        {
                                            //print("Move Right");
                                            transform.Translate(new Vector3(2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetKey(KeyCode.G))
                                        {
                                            //print("Move Down");
                                            transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {
                                        if (Input.GetKey(KeyCode.T))
                                        {
                                            //print("Move Up");
                                            transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    print("Soul_MP_Parameter : " + GameManager.Soul_MP_Parameter);
                                    print("Elizabat_Eclipse_Ready : " + GameManager.Elizabat_Eclipse_Ready);
                                    print("Elizabat_Eclipse_Unlock : " + GameManager.Elizabat_Eclipse_Unlock);
                                    print("Elizabat_Eclipse_On : " + GameManager.Elizabat_Eclipse_On);
                                    print("CommandStartOn : " + CommandStartOn);

                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                                        {
                                            // 일식
                                            if(GameManager.Elizabat_Eclipse_On == false && GameManager.Elizabat_Eclipse_Ready == true &&
                                                GameManager.Elizabat_Eclipse_Unlock == true &&  GameManager.Soul_MP_Parameter >= EclipseCost)
                                            {
                                                
                                                NowSkillChecking = 2;

                                                print("NowSkillChecking : " + NowSkillChecking);

                                                if((GameManager.Soul_MP_Parameter - EclipseCost) <= 0.0f)
                                                {
                                                    GameManager.Soul_MP_Parameter = 0.0f;
                                                }
                                                else
                                                {
                                                    GameManager.Soul_MP_Parameter -= EclipseCost;
                                                }
                                                
                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력

                                            }
                                        }
                                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                                        {
                                            // 스웜 공격
                                            if (GameManager.Elizabat_Swarm_On == false && GameManager.Elizabat_Swarm_Ready == true &&
                                                GameManager.Elizabat_Swarm_Unlock == true && GameManager.Soul_MP_Parameter >= SwarmCost)
                                            {
                                                NowSkillChecking = 4;

                                                if ((GameManager.Soul_MP_Parameter - SwarmCost) <= 0.0f)
                                                {
                                                    GameManager.Soul_MP_Parameter = 0.0f;
                                                }
                                                else
                                                {
                                                    GameManager.Soul_MP_Parameter -= SwarmCost;
                                                }

                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }

                                        }
                                        else if (Input.GetKeyDown(KeyCode.DownArrow))
                                        {
                                            // 강하 공격
                                            if (GameManager.Elizabat_Decent_On == false && GameManager.Elizabat_Decent_Ready == true &&
                                                GameManager.Elizabat_Decent_Unlock == true && GameManager.Soul_MP_Parameter >= DecentCost)
                                            {
                                                NowSkillChecking = 3;

                                                if ((GameManager.Soul_MP_Parameter - DecentCost) <= 0.0f)
                                                {
                                                    GameManager.Soul_MP_Parameter = 0.0f;
                                                }
                                                else
                                                {
                                                    GameManager.Soul_MP_Parameter -= DecentCost;
                                                }

                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                                        {
                                            // 소닉 웨이브
                                            if (GameManager.Elizabat_SonicWave_On == false && GameManager.Elizabat_SonicWave_Ready == true &&
                                                GameManager.Elizabat_SonicWave_Unlock == true && GameManager.Soul_MP_Parameter >= SonicWaveCost)
                                            {
                                                NowSkillChecking = 1;

                                                if ((GameManager.Soul_MP_Parameter - SonicWaveCost) <= 0.0f)
                                                {
                                                    GameManager.Soul_MP_Parameter = 0.0f;
                                                }
                                                else
                                                {
                                                    GameManager.Soul_MP_Parameter -= SonicWaveCost;
                                                }

                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }

                                        }
                                    }


                                    CommandInputStart(NowSkillChecking);
                                }
 
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                //print("GamePad 1 On");
                                //targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                // 스킬 언락 여부 설정
                                if (GameManager.Elizabat_SonicWave_Unlock == true)
                                {
                                    Skill_SonicWave_Icon_Block.SetActive(false);
                                    Skill_SonicWave_Icon.gameObject.SetActive(true);
                                }

                                if (GameManager.Elizabat_Eclipse_Unlock == true)
                                {
                                    Skill_Eclipse_Icon_Block.SetActive(false);
                                    Skill_Eclipse_Icon.gameObject.SetActive(true);
                                }

                                if (GameManager.Elizabat_Decent_Unlock == true)
                                {
                                    Skill_Decent_Icon_Block.SetActive(false);
                                    Skill_Decent_Icon.gameObject.SetActive(true);
                                }

                                if (GameManager.Elizabat_Swarm_Unlock == true)
                                {
                                    Skill_Swarm_Icon_Block.SetActive(false);
                                    Skill_Swarm_Icon.gameObject.SetActive(true);
                                }

                                // 스킬 쿨타임
                                if (SkillCoolTimer < 1.0f)
                                {
                                    SkillCoolTimer += Time.deltaTime;

                                }
                                else
                                {
                                    // 일식
                                    if (Skill_Eclipse_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Eclipse_Cool_Icon.fillAmount -= EclipseCoolTime;
                                    }
                                    else if (Skill_Eclipse_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Eclipse_Ready = true;
                                    }

                                    // 소닉 웨이브
                                    if (Skill_SonicWave_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_SonicWave_Cool_Icon.fillAmount -= SonicCoolTime;
                                    }
                                    else if (Skill_SonicWave_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_SonicWave_Ready = true;
                                    }

                                    // 스웜 공격
                                    if (Skill_Swarm_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Swarm_Cool_Icon.fillAmount -= SwarmCoolTime;
                                    }
                                    else if (Skill_Swarm_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Swarm_Ready = true;
                                    }

                                    // 강하 공격
                                    if (Skill_Decent_Cool_Icon.fillAmount > 0.0f)
                                    {
                                        Skill_Decent_Cool_Icon.fillAmount -= DecentCoolTime;
                                    }
                                    else if (Skill_Decent_Cool_Icon.fillAmount <= 0.0f)
                                    {
                                        GameManager.Elizabat_Decent_Ready = true;
                                    }

                                    SkillCoolTimer = 0.0f;
                                }

                                if (!GameManager.Elizabat_CommandStart && !GameManager.Elizabat_SkillStart)
                                {
                                    light.enabled = false;

                                    if (Input.GetButtonDown("P1_360_AButton"))
                                    {
                                        Debug.Log("Command Start!");

                                        CameraChecker.transform.position = CheckerStartPos;

                                        CommandInitilization();

                                        // Skill UI 활성화 사운드
                                        Audio.clip = CommandStart_Sound;
                                        Audio.Play();

                                        // 스킬 UI를 활성화 시킨다.
                                        //Skill_Not_Yangpigi.gameObject.SetActive(false);
                                        //Skill_True_Yangpigi.gameObject.SetActive(true);
                                        //Skill_Button.gameObject.SetActive(false);

                                        //if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                        //{
                                        //    CommandInitilization(4);
                                        //}
                                        //else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                        //{


                                        //    CommandInitilization(2);
                                        //}
                                        //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                        //{

                                        //    CommandInitilization(1);
                                        //}
                                        //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                        //{


                                        //    CommandInitilization(3);
                                        //}

                                    }
                                }

                                //print(Input.GetAxisRaw("P1_360_HorizontalDPAD"));



                                //if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                //{
                                //    if(!KeyDownEnable)
                                //    {
                                //        KeyDownEnable = true;

                                //        print("Left");
                                //    }
                                    
                                //}
                                //else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                //{
                                //    if (!KeyDownEnable)
                                //    {
                                //        KeyDownEnable = true;

                                //        print("Right");
                                //    }
                                //}
                                //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                //{
                                //    if (!KeyDownEnable)
                                //    {
                                //        KeyDownEnable = true;

                                //        print("Down");
                                //    }
                                    
                                //}
                                //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                //{
                                //    if (!KeyDownEnable)
                                //    {
                                //        KeyDownEnable = true;

                                //        print("Up");
                                //    }
                                    
                                //}


                                //if (Input.GetAxisRaw("P1_360_HorizontalDPAD") >= 0.1f &&
                                //    Input.GetAxisRaw("P1_360_HorizontalDPAD") <= -0.1f &&
                                //    Input.GetAxisRaw("P1_360_VerticalDPAD") <= -0.1f &&
                                //    Input.GetAxisRaw("P1_360_VerticalDPAD") >= 0.1f)
                                //{
                                //    KeyDownEnable = false;
                                //}

                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 0 &&
                                    Input.GetAxisRaw("P1_360_VerticalDPAD") == 0)
                                {
                                    KeyDownEnable = false;
                                }

                                print("P1_360_HorizontalDPAD : " + Input.GetAxisRaw("P1_360_HorizontalDPAD"));
                                print("P1_360_VerticalDPAD : " + Input.GetAxisRaw("P1_360_VerticalDPAD"));

                                if (GameManager.Elizabat_CommandStart)
                                {
                                    light.enabled = true;
                                    SkillTarget.gameObject.SetActive(true);
                                    targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);


                                    if (Physics.Raycast(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 300.0f, out hit, Mathf.Infinity, layermask))
                                    {
                                        //Debug.DrawRay(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 200.0f);

                                        if (hit.collider.tag.Equals("Ground") == true)
                                        {
                                            Leng = Vector3.Distance(this.transform.position, hit.point);

                                            SkillTarget.transform.localPosition = new Vector3(SkillTarget.transform.localPosition.x, SkillTarget.transform.localPosition.y, Leng);
                                            CameraChecker.transform.localPosition = SkillTarget.transform.localPosition;
                                        }
                                    }

                                    // 스킬 캔슬 여부
                                    if (Input.GetButtonDown("P1_360_BButton"))
                                    {
                                        CommandCancel();

                                        // Skill UI를 활성화 시킨다.
                                        //Skill_Not_Yangpigi.gameObject.SetActive(false);
                                        //Skill_True_Yangpigi.gameObject.SetActive(true);
                                        //Skill_Button.gameObject.SetActive(false);
                                    }

                                    // 스킬 시전 좌표 이동
                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_R_RightStick") <= -0.5f)
                                        {

                                            //Debug.Log("LeftStick!");
                                            transform.Translate(new Vector3(-2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetAxisRaw("P1_360_R_RightStick") >= 0.5f)
                                        {

                                            //Debug.Log("RightStick!");
                                            transform.Translate(new Vector3(2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_R_UpStick") >= 0.5f)
                                        {

                                            //Debug.Log("DownStick!");

                                            transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {

                                        if (Input.GetAxisRaw("P1_360_R_UpStick") <= -0.5f)
                                        {

                                            //Debug.Log("UpStick!");

                                            transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }


                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                        {
                                            // 일식
                                            if (GameManager.Elizabat_Eclipse_On == false && GameManager.Elizabat_Eclipse_Ready == true &&
                                                GameManager.Elizabat_Eclipse_Unlock == true && GameManager.Soul_MP_Parameter >= EclipseCost)
                                            {
                                                if (!KeyDownEnable)
                                                {
                                                    KeyDownEnable = true;

                                                    NowSkillChecking = 2;

                                                    if ((GameManager.Soul_MP_Parameter - EclipseCost) <= 0.0f)
                                                    {
                                                        GameManager.Soul_MP_Parameter = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        GameManager.Soul_MP_Parameter -= EclipseCost;
                                                    }

                                                    CommandStartOn = true;
                                                }

                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                        {

                                            // 스웜 공격
                                            if (GameManager.Elizabat_Swarm_On == false && GameManager.Elizabat_Swarm_Ready == true &&
                                                GameManager.Elizabat_Swarm_Unlock == true && GameManager.Soul_MP_Parameter >= SwarmCost)
                                            {
                                                if (!KeyDownEnable)
                                                {
                                                    KeyDownEnable = true;

                                                    NowSkillChecking = 4;

                                                    if ((GameManager.Soul_MP_Parameter - SwarmCost) <= 0.0f)
                                                    {
                                                        GameManager.Soul_MP_Parameter = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        GameManager.Soul_MP_Parameter -= SwarmCost;
                                                    }


                                                    CommandStartOn = true;
                                                }

                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                        {
                                            // 강하 공격
                                            if (GameManager.Elizabat_Decent_On == false && GameManager.Elizabat_Decent_Ready == true &&
                                                GameManager.Elizabat_Decent_Unlock == true && GameManager.Soul_MP_Parameter >= DecentCost)
                                            {
                                                if (!KeyDownEnable)
                                                {

                                                    KeyDownEnable = true;
                                                    NowSkillChecking = 3;


                                                    if ((GameManager.Soul_MP_Parameter - DecentCost) <= 0.0f)
                                                    {
                                                        GameManager.Soul_MP_Parameter = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        GameManager.Soul_MP_Parameter -= DecentCost;
                                                    }


                                                    CommandStartOn = true;
                                                }

                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                        {
                                            // 소닉 웨이브
                                            if (GameManager.Elizabat_SonicWave_On == false && GameManager.Elizabat_SonicWave_Ready == true &&
                                                GameManager.Elizabat_SonicWave_Unlock == true && GameManager.Soul_MP_Parameter >= SonicWaveCost)
                                            {
                                                if (!KeyDownEnable)
                                                {

                                                    KeyDownEnable = true;

                                                    NowSkillChecking = 1;


                                                    if ((GameManager.Soul_MP_Parameter - SonicWaveCost) <= 0.0f)
                                                    {
                                                        GameManager.Soul_MP_Parameter = 0.0f;
                                                    }
                                                    else
                                                    {
                                                        GameManager.Soul_MP_Parameter -= SonicWaveCost;
                                                    }


                                                    CommandStartOn = true;
                                                }

                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                    }

                                    CommandInputStart(NowSkillChecking);
                                }
                                //rotationY = Mathf.Clamp(rotationY, -90, 90);

                                //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                                //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

                            }
                            break;
                    }
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
	}

    public void pushObjectBackInFrustum(Transform obj)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.position);

        if (pos.x < 0f)
            pos.x = 0f;

        if (pos.x > 1f)
            pos.x = 1f;

        if (pos.y < 0f)
            pos.y = 0f;

        if (pos.y > 1f)
            pos.y = 1f;

        obj.position = Camera.main.ViewportToWorldPoint(pos);
    }

    IEnumerator MoveDown(Transform thisTransform, float distance, float speed)
    {
        float startPos = thisTransform.position.y;
        float endPos = startPos - distance;
        float rate = 1.0f / Mathf.Abs(startPos - endPos) * speed;
        float t = 0.0f;

        //this.transform.LookAt()

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            Vector3 pos = thisTransform.position;
            pos.y = Mathf.Lerp(startPos, endPos, t);
            
            thisTransform.position = pos;

            //yield return 0;
        }
        
        if(t >= 1.0f)
            t = 0.0f;


        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            Vector3 pos = thisTransform.position;
            pos.y = Mathf.Lerp(endPos, startPos, t);
            
            thisTransform.position = pos;

    
        }


        yield return 0;
        ElizabatDecentOn = true;
        GameManager.Elizabat_SkillStart = false;

        //yield return 1;

    }

    // 강하 공격
    IEnumerator DecentAttack(Transform thisTransform, GameObject Target, float speed)
    {
        GameManager.Elizabat_SkillStart = true;

        print("Decent Skill On");

        float startPos_Y = thisTransform.position.y;
        float endPos_Y = Target.transform.position.y;
        float startPos_Z = thisTransform.position.z;
        float endPos_Z = Target.transform.position.z;
        float startPos_X = thisTransform.position.x;
        float endPos_X = Target.transform.position.x;

        float rate = 1.0f / Mathf.Abs(startPos_Y - endPos_Y) * speed;
        float t = 0.0f;

        //thisTransform.LookAt(Target.transform.position);

        // 강하 공격 판정
        // hit는 레이가 부딪힌 물체의 정보를 가지고 있다. (위치, 판정 선 등등..)
        if(Physics.Linecast(MainCamera.transform.position, CameraChecker.transform.position, out hit))
        {
            if (hit.collider.tag.Equals("Walls"))
            {
                print("Ground");
                //hit.collider.gameObject.GetComponent<Enemy>().HP -= 10;
            }

            //if(hit.collider.tag.Equals("Enemy"))
            //{
            //    print("Enemy Hit");
            //    hit.collider.gameObject.GetComponent<Enemy>().HP -= 10;
            //}
        }

        light.range = 0;

        while (t < 1.0f)
        {
            if (Physics.Linecast(MainCamera.transform.position, CameraChecker.transform.position, out hit))
            {
                if (hit.collider.tag.Equals("Walls"))
                {
                    
                    break;
                }
            }

            t += Time.deltaTime * rate;
            Vector3 pos = thisTransform.position;
            pos.y = Mathf.Lerp(startPos_Y, endPos_Y, t);
            pos.z = Mathf.Lerp(startPos_Z, endPos_Z, t);
            pos.x = Mathf.Lerp(startPos_X, endPos_X, t);
            thisTransform.position = pos;


            //print("Pos : " + pos);

        }

        t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            Vector3 pos = thisTransform.position;
            pos.y = Mathf.Lerp(endPos_Y, startPos_Y, t);
            pos.z = Mathf.Lerp(endPos_Z, startPos_Z, t);
            pos.x = Mathf.Lerp(endPos_X, startPos_X, t);
            thisTransform.position = pos;


            yield return 0;
        }

        //ElizabatDecentOn = true;
        GameManager.Elizabat_SkillStart = false;
        light.range = 200;

        //// 스킬 UI를 원상복구 시킨다.
        //Skill_Not_Yangpigi.enabled = true;
        //Skill_True_Yangpigi.enabled = false;
        //Skill_Button.enabled = true;

        // 강하 공격 쿨 타임
        GameManager.Elizabat_Decent_On = true;

        Skill_Decent_Logo.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        print("Decent Skill Off");

        Skill_Decent_Logo.gameObject.SetActive(false);

        GameManager.Elizabat_Decent_On = false;


        Skill_Decent_Cool_Icon.fillAmount = 1.0f;
        //yield return 1;

    }

    // 스웜 공격
    IEnumerator SwarmSkill()
    {
        GameManager.Elizabat_Swarm_On = true;

        GameManager.Elizabat_SkillStart = false;

        Skill_Swarm_Cool_Icon.fillAmount = 1.0f;

        print("Swarm Skill On");

        Skill_Swarm_Logo.gameObject.SetActive(true);
        
        // 스웜 이펙트 출력


        yield return new WaitForSeconds(7.0f);

        print("Swarm Skill Off");

        Skill_Swarm_Logo.gameObject.SetActive(false);

        GameManager.Elizabat_Swarm_On = false;
    }

    // 일식 공격
    IEnumerator EclipseSkill()
    {
        GameManager.Elizabat_Eclipse_On = true;
        
        GameManager.Elizabat_SkillStart = false;

        Skill_Eclipse_Cool_Icon.fillAmount = 1.0f;

        // 일식 효과
        print("Eclipse Skill On");

        Audio.clip = EclipseSound;

        if(Audio.isPlaying == false)
        {
            Audio.Play();
        }
        


        Skill_Eclipse_Logo.gameObject.SetActive(true);

        yield return new WaitForSeconds(20.0f);

        print("Eclipse Skill Off");

        Skill_Eclipse_Logo.gameObject.SetActive(false);

        GameManager.Elizabat_Eclipse_On = false;
    }

    // 소닉 웨이브 공격
    IEnumerator SonicWaveSkill()
    {
        GameManager.Elizabat_SonicWave_On = true;

        GameManager.Elizabat_SkillStart = false;

        Skill_SonicWave_Cool_Icon.fillAmount = 1.0f;

        // 소닉 웨이브 효과
        print("Sonic Skill On");

        Audio.clip = SonicWaveSound;

        if (Audio.isPlaying == false)
        {
            Audio.Play();
        }

        Skill_SonicWave_Logo.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        print("Sonic Skill Off");

        Skill_SonicWave_Logo.gameObject.SetActive(false);

        GameManager.Elizabat_SonicWave_On = false;
    }

    void CommandInitilization()
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {
                    // 스킬 커맨드 표를 활성화 시켜준다.
                    Skill_SonicWave_Command_Chart.gameObject.SetActive(true);
                    Skill_Eclipse_Command_Chart.gameObject.SetActive(true);
                    Skill_Decent_Command_Chart.gameObject.SetActive(true);
                    Skill_Swarm_Command_Chart.gameObject.SetActive(true);

                    // 소닉 웨이브
                    int NowCommand = 0;

                    SonicWaveCommand[0] = 1;
                    DecentCommand[0] = 3;
                    SwarmCommand[0] = 4;
                    EclipseCommand[0] = 2;


                    for (int i = 1; i < 7; i++)
                    {
                        SonicWaveCommand[i] = 0;
                        DecentCommand[i] = 0;
                        SwarmCommand[i] = 0;
                        EclipseCommand[i] = 0;
                    }

                    Skill_SonicWave_Commands[0].sprite = Command_Arrows[1];
                    Skill_SonicWave_Commands[0].color = Color.green;

                    Skill_Eclipse_Commands[0].sprite = Command_Arrows[2];
                    Skill_Eclipse_Commands[0].color = Color.blue;

                    Skill_Decent_Commands[0].sprite = Command_Arrows[3];
                    Skill_Decent_Commands[0].color = Color.yellow;

                    Skill_Swarm_Commands[0].sprite = Command_Arrows[4];
                    Skill_Swarm_Commands[0].color = Color.red;

                    for (int j = 1; j < Skill_Eclipse_Commands.Length; j++)
                    {
                        Skill_SonicWave_Commands[j].sprite = Command_Arrows[0];
                        Skill_SonicWave_Commands[j].color = new Color(0, 0, 0, 0);
                        
                        Skill_Eclipse_Commands[j].sprite = Command_Arrows[0];
                        Skill_Eclipse_Commands[j].color = new Color(0, 0, 0, 0);
                        
                        Skill_Decent_Commands[j].sprite = Command_Arrows[0];
                        Skill_Decent_Commands[j].color = new Color(0, 0, 0, 0);
                        
                        Skill_Swarm_Commands[j].sprite = Command_Arrows[0];
                        Skill_Swarm_Commands[j].color = new Color(0, 0, 0, 0);
                    }

                    //SonicMaxCount = Random.Range(2, 5);
                    //EclipseMaxCount = Random.Range(2, 7);
                    //SwarmMaxCount = Random.Range(2, 7);
                    //DecentMaxCount = Random.Range(2, 3);

                    SonicMaxCount = 3;//Random.Range(2, 3);
                    EclipseMaxCount = 4;// Random.Range(2, 4);
                    DecentMaxCount = 5;// Random.Range(2, 5);
                    SwarmMaxCount = 6;// Random.Range(2, 6);
                    

                    for (int i = 1; i < SonicMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        SonicWaveCommand[i] = NowCommand;
                        
                        switch(NowCommand)
                        {
                            case 1:
                                {
                                    Skill_SonicWave_Commands[i].sprite = Command_Arrows[1];
                                    Skill_SonicWave_Commands[i].color = Color.green;
                                }
                                break;

                            case 2:
                                {
                                    Skill_SonicWave_Commands[i].sprite = Command_Arrows[2];
                                    Skill_SonicWave_Commands[i].color = Color.green;
                                }
                                break;

                            case 3:
                                {
                                    Skill_SonicWave_Commands[i].sprite = Command_Arrows[3];
                                    Skill_SonicWave_Commands[i].color = Color.green;
                                }
                                break;

                            case 4:
                                {
                                    Skill_SonicWave_Commands[i].sprite = Command_Arrows[4];
                                    Skill_SonicWave_Commands[i].color = Color.green;
                                }
                                break;

                            default:
                                {
                                    Skill_SonicWave_Commands[i].sprite = Command_Arrows[0];
                                    Skill_SonicWave_Commands[i].color = new Color(0, 0, 0, 0);
                                }
                                break;
                        }
                        
                    
                    }

                    for (int i = 1; i < EclipseMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        EclipseCommand[i] = NowCommand;

                        switch (NowCommand)
                        {
                            case 1:
                                {

                                    Skill_Eclipse_Commands[i].sprite = Command_Arrows[1];
                                    Skill_Eclipse_Commands[i].color = Color.blue;
                                }
                                break;

                            case 2:
                                {
                                    Skill_Eclipse_Commands[i].sprite = Command_Arrows[2];
                                    Skill_Eclipse_Commands[i].color = Color.blue;
                                }
                                break;

                            case 3:
                                {
                                    Skill_Eclipse_Commands[i].sprite = Command_Arrows[3];
                                    Skill_Eclipse_Commands[i].color = Color.blue;
                                }
                                break;

                            case 4:
                                {
                                    Skill_Eclipse_Commands[i].sprite = Command_Arrows[4];
                                    Skill_Eclipse_Commands[i].color = Color.blue;
                                }
                                break;

                            default:
                                {
                                    Skill_Eclipse_Commands[i].sprite = Command_Arrows[0];
                                    Skill_Eclipse_Commands[i].color = new Color(0, 0, 0, 0);
                                }
                                break;
                        }
                    }

                    for (int i = 1; i < SwarmMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        SwarmCommand[i] = NowCommand;

                        switch (NowCommand)
                        {
                            case 1:
                                {

                                    Skill_Swarm_Commands[i].sprite = Command_Arrows[1];
                                    Skill_Swarm_Commands[i].color = Color.red;
                                }
                                break;

                            case 2:
                                {
                                    Skill_Swarm_Commands[i].sprite = Command_Arrows[2];
                                    Skill_Swarm_Commands[i].color = Color.red;
                                }
                                break;

                            case 3:
                                {
                                    Skill_Swarm_Commands[i].sprite = Command_Arrows[3];
                                    Skill_Swarm_Commands[i].color = Color.red;
                                }
                                break;

                            case 4:
                                {
                                    Skill_Swarm_Commands[i].sprite = Command_Arrows[4];
                                    Skill_Swarm_Commands[i].color = Color.red;
                                }
                                break;

                            default:
                                {
                                    Skill_Swarm_Commands[i].sprite = Command_Arrows[0];
                                    Skill_Swarm_Commands[i].color = new Color(0, 0, 0, 0);
                                }
                                break;
                        }
                    }
                    for (int i = 1; i < DecentMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        DecentCommand[i] = NowCommand;

                        switch (NowCommand)
                        {
                            case 1:
                                {

                                    Skill_Decent_Commands[i].sprite = Command_Arrows[1];
                                    Skill_Decent_Commands[i].color = Color.yellow;
                                }
                                break;

                            case 2:
                                {
                                    Skill_Decent_Commands[i].sprite = Command_Arrows[2];
                                    Skill_Decent_Commands[i].color = Color.yellow;
                                }
                                break;

                            case 3:
                                {
                                    Skill_Decent_Commands[i].sprite = Command_Arrows[3];
                                    Skill_Decent_Commands[i].color = Color.yellow;
                                }
                                break;

                            case 4:
                                {
                                    Skill_Decent_Commands[i].sprite = Command_Arrows[4];
                                    Skill_Decent_Commands[i].color = Color.yellow;
                                }
                                break;

                            default:
                                {
                                    Skill_Decent_Commands[i].sprite = Command_Arrows[0];
                                    Skill_Decent_Commands[i].color = new Color(0, 0, 0, 0);
                                }
                                break;
                        }
                    }

                    currentNum = 0;
                    NowSkillChecking = 0;

                    CommandCheckTimer = 0.0f;
                    KeyDownEnable = false;

                    //print("MaxCount : " + MaxCount);

                    Debug.Log("[1] SonicWaveCommand : " + SonicWaveCommand[0] + SonicWaveCommand[1] + SonicWaveCommand[2] + SonicWaveCommand[3] + SonicWaveCommand[4] + SonicWaveCommand[5] + SonicWaveCommand[6]);
                    Debug.Log("[2] EclipseCommand : " + EclipseCommand[0] + EclipseCommand[1] + EclipseCommand[2] + EclipseCommand[3] + EclipseCommand[4] + EclipseCommand[5] + EclipseCommand[6]);
                    Debug.Log("[3] DecentCommand : " + DecentCommand[0] + DecentCommand[1] + DecentCommand[2] + DecentCommand[3] + DecentCommand[4] + DecentCommand[5] + DecentCommand[6]);
                    Debug.Log("[4] SwarmCommand : " + SwarmCommand[0] + SwarmCommand[1] + SwarmCommand[2] + SwarmCommand[3] + SwarmCommand[4] + SwarmCommand[5] + SwarmCommand[6]);
                    

                    GameManager.Elizabat_CommandStart = true;
                }
                break;
        }
    }

    void CommandCancel()
    {
        light.enabled = false;
        SkillTarget.gameObject.SetActive(false);

        GameManager.Elizabat_CommandStart = false;
        GameManager.Elizabat_SkillStart = false;
        CommandStartOn = false;
        NowSkillChecking = 0;

        EclipseMaxCount = 0;
        SwarmMaxCount = 0;
        DecentMaxCount = 0;
        SonicMaxCount = 0;
        
        currentNum = 0;
        CommandCheckTimer = 0.0f;
        KeyDownEnable = false;

        for (int i = 1; i < 7; i++)
        {
            SonicWaveCommand[i] = 0;
            DecentCommand[i] = 0;
            SwarmCommand[i] = 0;
            EclipseCommand[i] = 0;
        }

        for (int j = 1; j < Skill_Eclipse_Commands.Length; j++)
        {
            Skill_SonicWave_Commands[j].sprite = Command_Arrows[0];
            Skill_SonicWave_Commands[j].color = new Color(0, 0, 0, 0);

            Skill_Eclipse_Commands[j].sprite = Command_Arrows[0];
            Skill_Eclipse_Commands[j].color = new Color(0, 0, 0, 0);

            Skill_Decent_Commands[j].sprite = Command_Arrows[0];
            Skill_Decent_Commands[j].color = new Color(0, 0, 0, 0);

            Skill_Swarm_Commands[j].sprite = Command_Arrows[0];
            Skill_Swarm_Commands[j].color = new Color(0, 0, 0, 0);
        }

        // 스킬 커맨드 표를 활성화 시켜준다.
        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
        Skill_Decent_Command_Chart.gameObject.SetActive(false);
        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
    }

    void CommandInputStart(int SkillNumber)
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                switch(SkillNumber)
                                {
                                        // 소닉 웨이브
                                    case 1:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= SonicTimeLimit)
                                            {
                                                print("Time Over!");

                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SonicMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                // 타이머를 체크
                                                CommandCheckTimer += Time.deltaTime;
                                                print("CommandCheckTimer : " + CommandCheckTimer);


                                                if (Input.GetKeyDown(KeyCode.LeftArrow))
                                                {
                                                    Inputcommand = 2;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SonicMaxCount);

                                                    if (SonicWaveCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        // Skill UI 활성화 사운드
                                                        Audio.clip = SonicWave_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SonicMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SonicWaveCommand[i] = 0;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                                {
                                                    Inputcommand = 4;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SonicMaxCount);

                                                    if (SonicWaveCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = SonicWave_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SonicMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SonicWaveCommand[i] = 0;
                                                        //}

                                                        //for(int j =0; j<Skill_SonicWave_Commands.Length; j++)
                                                        //{
                                                        //    Skill_SonicWave_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.DownArrow))
                                                {
                                                    Inputcommand = 3;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SonicMaxCount);

                                                    if (SonicWaveCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = SonicWave_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SonicMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SonicWaveCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                        //{
                                                        //    Skill_SonicWave_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.UpArrow))
                                                {
                                                    Inputcommand = 1;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SonicMaxCount);

                                                    if (SonicWaveCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = SonicWave_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SonicMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SonicWaveCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                        //{
                                                        //    Skill_SonicWave_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }

                                                if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 소닉 웨이브 스킬 시작
                                                    StartCoroutine("SonicWaveSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    SonicWaveCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                    //{
                                                    //    Skill_SonicWave_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                           
                                        }
                                        break;

                                        // 일식
                                    case 2:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= EclipseTimeLimit)
                                            {
                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                EclipseMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetKeyDown(KeyCode.LeftArrow))
                                                {
                                                    Inputcommand = 2;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + EclipseMaxCount);

                                                    if (EclipseCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        EclipseMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    EclipseCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Eclipse_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                                {
                                                    Inputcommand = 4;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + EclipseMaxCount);

                                                    if (EclipseCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;


                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        EclipseMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    EclipseCommand[i] = 0;
                                                        //}


                                                        //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Eclipse_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.DownArrow))
                                                {
                                                    Inputcommand = 3;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + EclipseMaxCount);

                                                    if (EclipseCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        EclipseMaxCount = 0;
                                                        currentNum = 0;


                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    EclipseCommand[i] = 0;
                                                        //}


                                                        //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Eclipse_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.UpArrow))
                                                {
                                                    Inputcommand = 1;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + EclipseMaxCount);

                                                    if (EclipseCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        EclipseMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    EclipseCommand[i] = 0;
                                                        //}


                                                        //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Eclipse_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }

                                                if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 일식 스킬 시작
                                                    StartCoroutine("EclipseSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    EclipseCommand[i] = 0;
                                                    //}


                                                    //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Eclipse_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                            
                                        }
                                        break;

                                        // 강하 공격
                                    case 3:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= DecentTimeLimit)
                                            {
                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                DecentMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    DecentCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                //{
                                                //    Skill_Decent_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetKeyDown(KeyCode.LeftArrow))
                                                {
                                                    Inputcommand = 2;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + DecentMaxCount);

                                                    if (DecentCommand[currentNum] == Inputcommand)
                                                    {

                                                        Skill_Decent_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        DecentMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    DecentCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Decent_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                                {
                                                    Inputcommand = 4;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + DecentMaxCount);

                                                    if (DecentCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Decent_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        DecentMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    DecentCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Decent_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.DownArrow))
                                                {
                                                    Inputcommand = 3;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + DecentMaxCount);

                                                    if (DecentCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Decent_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        DecentMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    DecentCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Decent_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.UpArrow))
                                                {
                                                    Inputcommand = 1;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + DecentMaxCount);

                                                    if (DecentCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Decent_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Eclipse_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        DecentMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    DecentCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Decent_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }

                                                if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 강하 공격
                                                    StartCoroutine(DecentAttack(MainCamera.transform, CameraChecker, 10.0f));

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;

                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 6; i++)
                                                    //{
                                                    //    DecentCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Decent_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                            
                                        }
                                        break;
                                        // 스웜 공격
                                    case 4:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= SwarmTimeLimit)
                                            {
                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SwarmMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    SwarmCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                //{
                                                //    Skill_Swarm_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetKeyDown(KeyCode.LeftArrow))
                                                {
                                                    Inputcommand = 2;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SwarmMaxCount);

                                                    if (SwarmCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Swarm_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Swarm_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SwarmMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SwarmCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Swarm_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                                {
                                                    Inputcommand = 4;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SwarmMaxCount);

                                                    if (SwarmCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Swarm_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Swarm_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SwarmMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SwarmCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Swarm_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.DownArrow))
                                                {
                                                    Inputcommand = 3;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SwarmMaxCount);

                                                    if (SwarmCommand[currentNum] == Inputcommand)
                                                    {
                                                        Skill_Swarm_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Swarm_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SwarmMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SwarmCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Swarm_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }
                                                else if (Input.GetKeyDown(KeyCode.UpArrow))
                                                {
                                                    Inputcommand = 1;

                                                    print("currentNum : " + currentNum);
                                                    print("MaxCount : " + SwarmMaxCount);

                                                    if (SwarmCommand[currentNum] == Inputcommand)
                                                    {

                                                        Skill_Swarm_Commands[currentNum].color = Color.white;

                                                        currentNum++;

                                                        Inputcommand = 0;

                                                        Audio.clip = Swarm_Command_Correct_Sound;
                                                        Audio.Play();
                                                    }
                                                    else
                                                    {
                                                        print("Command Failed!");

                                                        GameManager.Elizabat_CommandStart = false;
                                                        GameManager.Elizabat_SkillStart = false;
                                                        CommandStartOn = false;
                                                        NowSkillChecking = 0;
                                                        SwarmMaxCount = 0;
                                                        currentNum = 0;

                                                        // 스킬 UI를 원상복구 시킨다.
                                                        //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                        //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                        //Skill_Button.gameObject.SetActive(true);

                                                        //for (int i = 1; i < 7; i++)
                                                        //{
                                                        //    SwarmCommand[i] = 0;
                                                        //}

                                                        //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                        //{
                                                        //    Skill_Swarm_Commands[j].color = Color.black;
                                                        //}

                                                        // 스킬 커맨드 표를 활성화 시켜준다.
                                                        Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                        Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                    }
                                                }

                                                if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 스웜 공격
                                                    StartCoroutine("SwarmSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;

                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    SwarmCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Swarm_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                           
                                        }
                                        break;
                                }
                               
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 0 &&
                                    Input.GetAxisRaw("P1_360_VerticalDPAD") == 0)
                                {
                                    KeyDownEnable = false;
                                }
                                
                                print(KeyDownEnable);

                                switch (SkillNumber)
                                {
                                    // 소닉 웨이브
                                    case 1:
                                        {
                                            //print("SkillNumber : " + SkillNumber);

                                            //if (Input.GetAxisRaw("P1_360_HorizontalDPAD") <= -0.1f)
                                            //{
                                            //    if (!KeyDownEnable)
                                            //    {
                                            //        KeyDownEnable = true;

                                            //        print("Left");
                                            //    }

                                            //}
                                            //else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") >= 0.1f)
                                            //{
                                            //    if (!KeyDownEnable)
                                            //    {
                                            //        KeyDownEnable = true;

                                            //        print("Right");
                                            //    }
                                            //}
                                            //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") <= -0.1f)
                                            //{
                                            //    if (!KeyDownEnable)
                                            //    {
                                            //        KeyDownEnable = true;

                                            //        print("Down");
                                            //    }

                                            //}
                                            //else if (Input.GetAxisRaw("P1_360_VerticalDPAD") >= 0.1f)
                                            //{
                                            //    if (!KeyDownEnable)
                                            //    {
                                            //        KeyDownEnable = true;

                                            //        print("Up");
                                            //    }

                                            //}
                                            KeyDownEnable = false;

                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= SonicTimeLimit)
                                            {
                                                print("Command Failed!");

                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SonicMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    SonicWaveCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                //{
                                                //    Skill_SonicWave_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 2;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SonicMaxCount);

                                                        if (SonicWaveCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = SonicWave_Command_Correct_Sound;
                                                            Audio.Play();

                                                            //KeyDownEnable = false;
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SonicMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SonicWaveCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                            //{
                                                            //    Skill_SonicWave_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }
                                                else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 4;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SonicMaxCount);

                                                        if (SonicWaveCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = SonicWave_Command_Correct_Sound;
                                                            Audio.Play();

                                                            //KeyDownEnable = false;
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SonicMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SonicWaveCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                            //{
                                                            //    Skill_SonicWave_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 3;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SonicMaxCount);

                                                        if (SonicWaveCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = SonicWave_Command_Correct_Sound;
                                                            Audio.Play();

                                                            //KeyDownEnable = false;
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SonicMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SonicWaveCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                            //{
                                                            //    Skill_SonicWave_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;


                                                        Inputcommand = 1;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SonicMaxCount);

                                                        if (SonicWaveCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_SonicWave_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = SonicWave_Command_Correct_Sound;
                                                            Audio.Play();

                                                            //KeyDownEnable = false;
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SonicMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SonicWaveCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                            //{
                                                            //    Skill_SonicWave_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }

                                                }

                                                if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 소닉 웨이브 스킬 시작
                                                    StartCoroutine("SonicWaveSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 0; i < 7; i++)
                                                    //{
                                                    //    SonicWaveCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_SonicWave_Commands.Length; j++)
                                                    //{
                                                    //    Skill_SonicWave_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                            
                                        }
                                        break;

                                    // 일식
                                    case 2:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= EclipseTimeLimit)
                                            {
                                                print("Command Failed!");

                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                EclipseMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    EclipseCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                //{
                                                //    Skill_Eclipse_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 2;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + EclipseMaxCount);

                                                        if (EclipseCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            EclipseMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    EclipseCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Eclipse_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 4;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + EclipseMaxCount);

                                                        if (EclipseCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            EclipseMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    EclipseCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Eclipse_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 3;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + EclipseMaxCount);

                                                        if (EclipseCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            EclipseMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    EclipseCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Eclipse_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 1;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + EclipseMaxCount);

                                                        if (EclipseCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Eclipse_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;
                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            EclipseMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    EclipseCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Eclipse_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }

                                                if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 일식 스킬 시작
                                                    StartCoroutine("EclipseSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;

                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    EclipseCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_Eclipse_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Eclipse_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                          
                                        }
                                        break;

                                    // 강하 공격
                                    case 3:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= DecentTimeLimit)
                                            {
                                                print("Command TimeOver!");

                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;

                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                DecentMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    DecentCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                //{
                                                //    Skill_Decent_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 2;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + DecentMaxCount);

                                                        if (DecentCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Decent_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            DecentMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    DecentCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Decent_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 4;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + DecentMaxCount);

                                                        if (DecentCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Decent_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            DecentMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    DecentCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Decent_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 3;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + DecentMaxCount);

                                                        if (DecentCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Decent_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            DecentMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    DecentCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Decent_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 1;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + DecentMaxCount);

                                                        if (DecentCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Decent_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Eclipse_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            DecentMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    DecentCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Decent_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }

                                                if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 강하 공격

                                                    StartCoroutine(DecentAttack(MainCamera.transform, CameraChecker, 10.0f));

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;

                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    DecentCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_Decent_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Decent_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }


                                        }
                                        break;
                                    // 스웜 공격
                                    case 4:
                                        {
                                            // 시간내에 입력하지 못하면 실패
                                            if (CommandCheckTimer >= SwarmTimeLimit)
                                            {
                                                print("Command Failed!");

                                                GameManager.Elizabat_CommandStart = false;
                                                GameManager.Elizabat_SkillStart = false;

                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SwarmMaxCount = 0;
                                                currentNum = 0;

                                                // 스킬 UI를 원상복구 시킨다.
                                                //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                //Skill_Button.gameObject.SetActive(true);

                                                //for (int i = 1; i < 7; i++)
                                                //{
                                                //    SwarmCommand[i] = 0;
                                                //}

                                                //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                //{
                                                //    Skill_Swarm_Commands[j].color = Color.black;
                                                //}

                                                // 스킬 커맨드 표를 활성화 시켜준다.
                                                Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                            }
                                            else
                                            {
                                                CommandCheckTimer += Time.deltaTime;

                                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 2;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SwarmMaxCount);

                                                        if (SwarmCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Swarm_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;

                                                            Audio.clip = Swarm_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SwarmMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SwarmCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Swarm_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }
                                                else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 4;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SwarmMaxCount);

                                                        if (SwarmCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Swarm_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;


                                                            Audio.clip = Swarm_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SwarmMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SwarmCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Swarm_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 3;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SwarmMaxCount);

                                                        if (SwarmCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Swarm_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;


                                                            Audio.clip = Swarm_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SwarmMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SwarmCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Swarm_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                    
                                                }
                                                else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                                {
                                                    if (!KeyDownEnable)
                                                    {
                                                        KeyDownEnable = true;

                                                        Inputcommand = 1;

                                                        print("currentNum : " + currentNum);
                                                        print("MaxCount : " + SwarmMaxCount);

                                                        if (SwarmCommand[currentNum] == Inputcommand)
                                                        {
                                                            Skill_Swarm_Commands[currentNum].color = Color.white;

                                                            currentNum++;

                                                            Inputcommand = 0;


                                                            Audio.clip = Swarm_Command_Correct_Sound;
                                                            Audio.Play();
                                                        }
                                                        else
                                                        {
                                                            print("Command Failed!");

                                                            GameManager.Elizabat_CommandStart = false;
                                                            GameManager.Elizabat_SkillStart = false;

                                                            CommandStartOn = false;
                                                            NowSkillChecking = 0;
                                                            SwarmMaxCount = 0;
                                                            currentNum = 0;

                                                            // 스킬 UI를 원상복구 시킨다.
                                                            //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                            //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                            //Skill_Button.gameObject.SetActive(true);

                                                            //for (int i = 1; i < 7; i++)
                                                            //{
                                                            //    SwarmCommand[i] = 0;
                                                            //}

                                                            //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                            //{
                                                            //    Skill_Swarm_Commands[j].color = Color.black;
                                                            //}

                                                            // 스킬 커맨드 표를 활성화 시켜준다.
                                                            Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                            Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                        }
                                                    }
                                                   
                                                }

                                                if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                                {
                                                    print("Command Success!");

                                                    // 스웜 공격
                                                    StartCoroutine("SwarmSkill");

                                                    GameManager.Elizabat_CommandStart = false;
                                                    GameManager.Elizabat_SkillStart = false;

                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;
                                                    currentNum = 0;

                                                    // 스킬 UI를 원상복구 시킨다.
                                                    //Skill_Not_Yangpigi.gameObject.SetActive(true);
                                                    //Skill_True_Yangpigi.gameObject.SetActive(false);
                                                    //Skill_Button.gameObject.SetActive(true);

                                                    //for (int i = 1; i < 7; i++)
                                                    //{
                                                    //    SwarmCommand[i] = 0;
                                                    //}

                                                    //for (int j = 0; j < Skill_Swarm_Commands.Length; j++)
                                                    //{
                                                    //    Skill_Swarm_Commands[j].color = Color.black;
                                                    //}

                                                    // 스킬 커맨드 표를 활성화 시켜준다.
                                                    Skill_SonicWave_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Eclipse_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Decent_Command_Chart.gameObject.SetActive(false);
                                                    Skill_Swarm_Command_Chart.gameObject.SetActive(false);
                                                }
                                            }

                                           
                                        }
                                        break;
                                }


                            }
                            break;
                    }

                    
                }
                break;
        }
    }

}
