using UnityEngine;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 targetPosOnScreen;

    public GameObject CameraChecker;

    // 소닉 웨이브 커맨드 5개 (고정1 + 랜덤 4)
    private int[] SonicWaveCommand = new int[7];
    // 강하 공격 커맨드 3개 (고정1 + 랜덤 2)
    private int[] DecentCommand = new int[7];
    // 스웜 공격 커맨드 7개 (고정1 + 랜덤 6)
    private int[] SwarmCommand = new int[7];
    // 일식 스킬 커맨드 7개 (고정1 + 랜덤 6)
    private int[] EclipseCommand = new int[7];

    private int EclipseMaxCount;
    private int SwarmMaxCount;
    private int DecentMaxCount;
    private int SonicMaxCount;

    private bool CommandOn;
    private bool CommandInit;

    private int Inputcommand;
    private int currentNum;
    //private RaycastHit hit;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        CommandOn = true;
        CommandInit = false;

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

        Inputcommand = 0;
        currentNum = 0;
        
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

                                targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

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

                                if (!GameManager.CommandStart)
                                {
                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetKey(KeyCode.F))
                                        {
                                            transform.Translate(new Vector3(-2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetKey(KeyCode.H))
                                        {
                                            transform.Translate(new Vector3(2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetKey(KeyCode.G))
                                        {
                                            transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {
                                        if (Input.GetKey(KeyCode.T))
                                        {
                                            transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }


                                    if (Input.GetKeyDown(KeyCode.C))
                                    {
                                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                                        {
                                            CommandInitilization(2);
                                        }
                                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                                        {

                                            CommandInitilization(4);
                                        }
                                        else if (Input.GetKeyDown(KeyCode.DownArrow))
                                        {

                                            CommandInitilization(3);
                                        }
                                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                                        {

                                            CommandInitilization(1);
                                        }
                                        
                                    }
                                }
                                

                                //print(targetPosOnScreen);


                                if (GameManager.CommandStart)
                                {

                                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                                    {
                                        CommandInputStart(2);
                                    }
                                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                                    {

                                        CommandInputStart(4);
                                    }
                                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                                    {

                                        CommandInputStart(3);
                                    }
                                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                                    {

                                        CommandInputStart(1);
                                    }
                                }
 
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                if (!GameManager.CommandStart)
                                {
                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_RightStick") == -1)
                                        {

                                            Debug.Log("LeftStick!");
                                            transform.Translate(new Vector3(-2, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetAxisRaw("P1_360_RightStick") == 1)
                                        {

                                            Debug.Log("RightStick!");
                                            transform.Translate(new Vector3(2, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_UpStick") == 1)
                                        {

                                            Debug.Log("DownStick!");

                                            transform.Translate(new Vector3(0, -2, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {

                                        if (Input.GetAxisRaw("P1_360_UpStick") == -1)
                                        {

                                            Debug.Log("UpStick!");

                                            transform.Translate(new Vector3(0, 2, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (Input.GetButtonDown("P1_360_AButton"))
                                    {
                                        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                        {
                                            CommandInitilization(4);
                                        }
                                        else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                        {


                                            CommandInitilization(2);
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                        {

                                            CommandInitilization(1);
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                        {


                                            CommandInitilization(3);
                                        }

                                    }
                                }


                                if (GameManager.CommandStart)
                                {
                                    if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                    {
                                        CommandInputStart(4);
                                    }
                                    else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                    {


                                        CommandInputStart(2);
                                    }
                                    else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                    {

                                        CommandInputStart(1);
                                    }
                                    else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                    {


                                        CommandInputStart(3);
                                    }

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

    void CommandInitilization(int SkillNumber)
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {

                    switch (SkillNumber)
                    {
                        // 소닉 웨이브
                        case 1:
                            {
                                int NowCommand = 0;

                                SonicMaxCount = Random.Range(2, 5);

                                for (int i = 1; i < SonicMaxCount; i++)
                                {
                                    NowCommand = Random.Range(1, 5);

                                    SonicWaveCommand[i] = NowCommand;
                                }

                                CommandInit = true;
                                currentNum = 0;

                                //print("MaxCount : " + MaxCount);

                                for (int i = 1; i < SonicMaxCount; i++)
                                {
                                    Debug.Log(" SonicWaveCommand : " + SonicWaveCommand[i]);
                                }

                                GameManager.CommandStart = true;
                            }
                            break;

                        // 일식
                        case 2:
                            {
                                int NowCommand = 0;

                                EclipseMaxCount = Random.Range(2, 7);

                                for (int i = 1; i < EclipseMaxCount; i++)
                                {
                                    NowCommand = Random.Range(1, 5);

                                    EclipseCommand[i] = NowCommand;
                                }

                                CommandInit = true;
                                currentNum = 0;

                                //print("MaxCount : " + MaxCount);

                                for (int i = 1; i < EclipseMaxCount; i++)
                                {
                                    Debug.Log(" SonicWaveCommand : " + EclipseCommand[i]);
                                }

                                GameManager.CommandStart = true;
                            }
                            break;

                        // 강하 공격
                        case 3:
                            {
                                int NowCommand = 0;

                                DecentMaxCount = Random.Range(2, 3);

                                for (int i = 1; i < DecentMaxCount; i++)
                                {
                                    NowCommand = Random.Range(1, 5);

                                    DecentCommand[i] = NowCommand;
                                }

                                CommandInit = true;
                                currentNum = 0;

                                //print("MaxCount : " + MaxCount);

                                for (int i = 1; i < DecentMaxCount; i++)
                                {
                                    Debug.Log(" SonicWaveCommand : " + DecentCommand[i]);
                                }

                                GameManager.CommandStart = true;
                            }
                            break;
                        // 스웜 공격
                        case 4:
                            {
                                int NowCommand = 0;

                                SwarmMaxCount = Random.Range(2, 7);

                                for (int i = 1; i < SwarmMaxCount; i++)
                                {
                                    NowCommand = Random.Range(1, 5);

                                    SwarmCommand[i] = NowCommand;
                                }

                                CommandInit = true;
                                currentNum = 0;

                                //print("MaxCount : " + MaxCount);

                                for (int i = 1; i < SwarmMaxCount; i++)
                                {
                                    Debug.Log(" SonicWaveCommand : " + SwarmCommand[i]);
                                }

                                GameManager.CommandStart = true;
                            }
                            break;
                    }


                }
                break;
        }
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
                                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                SonicMaxCount = 0;

                                                for (int i = 0; i < 6; i++)
                                                {
                                                    SonicWaveCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;

                                        // 일식
                                    case 2:
                                        {
                                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                EclipseMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    EclipseCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;

                                        // 강하 공격
                                    case 3:
                                        {
                                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                DecentMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    DecentCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;
                                        // 스웜 공격
                                    case 4:
                                        {
                                            if (Input.GetKeyDown(KeyCode.LeftArrow))
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.RightArrow))
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.DownArrow))
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetKeyDown(KeyCode.UpArrow))
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                SwarmMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    SwarmCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;
                                }
                               
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {

                                switch (SkillNumber)
                                {
                                    // 소닉 웨이브
                                    case 1:
                                        {


                                            if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SonicMaxCount);

                                                if (SonicWaveCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                SonicMaxCount = 0;

                                                for (int i = 0; i < 6; i++)
                                                {
                                                    SonicWaveCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;

                                    // 일식
                                    case 2:
                                        {
                                            if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + EclipseMaxCount);

                                                if (EclipseCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                EclipseMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    EclipseCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;

                                    // 강하 공격
                                    case 3:
                                        {
                                            if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + DecentMaxCount);

                                                if (DecentCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                DecentMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    DecentCommand[i] = 0;
                                                }
                                            }
                                        }
                                        break;
                                    // 스웜 공격
                                    case 4:
                                        {
                                            if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                            {
                                                Inputcommand = 2;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                            {
                                                Inputcommand = 4;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                            {
                                                Inputcommand = 3;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }
                                            else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                            {
                                                Inputcommand = 1;

                                                print("currentNum : " + currentNum);
                                                print("MaxCount : " + SwarmMaxCount);

                                                if (SwarmCommand[currentNum] == Inputcommand)
                                                {
                                                    currentNum++;

                                                    Inputcommand = 0;
                                                }
                                                else
                                                {
                                                    print("Command Failed!");

                                                    GameManager.CommandStart = false;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 6; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.CommandStart = false;
                                                SwarmMaxCount = 0;

                                                for (int i = 1; i < 6; i++)
                                                {
                                                    SwarmCommand[i] = 0;
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
