using UnityEngine;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private bool ElizabatDecentOn;
    private Vector3 BackupPos;
    private Vector3 BackupDir;
    private Vector3 TargetPos;

    private Vector3 FowardAttack;
    private Vector3 BackAttack;

    
    private Vector3 targetPosOnScreen;
    private RaycastHit hit;
    private Ray ray;

    public GameObject CameraChecker;
    public GameObject Target;
    public Camera MainCamera;

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

    private int NowSkillChecking;
    private bool CommandOn;
    private bool CommandStartOn;

    private int Inputcommand;
    private int currentNum;
    //private RaycastHit hit;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        CommandOn = true;
        CommandStartOn = false;
        NowSkillChecking = 0;
        ElizabatDecentOn = true;

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

        BackupPos = MainCamera.transform.position;
        BackupDir = CameraChecker.transform.position;
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

                                if (!GameManager.Elizabat_CommandStart)
                                {
                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetKey(KeyCode.F))
                                        {
                                            transform.position += (new Vector3(-2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetKey(KeyCode.H))
                                        {
                                            transform.position += (new Vector3(2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetKey(KeyCode.G))
                                        {
                                            transform.position += (new Vector3(0, 0, -2.5f) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {
                                        if (Input.GetKey(KeyCode.T))
                                        {
                                            transform.position += (new Vector3(0, 0, 2.5f) * normalMoveSpeed * Time.deltaTime);
                                            
                                        }
                                    }


                                    if (Input.GetKeyDown(KeyCode.C))
                                    {
                                        Debug.Log("Command Start!");

                                        CommandInitilization();

                                        //if (Input.GetKeyDown(KeyCode.LeftArrow))
                                        //{
                                        //    CommandInitilization(2);
                                        //}
                                        //else if (Input.GetKeyDown(KeyCode.RightArrow))
                                        //{

                                        //    CommandInitilization(4);
                                        //}
                                        //else if (Input.GetKeyDown(KeyCode.DownArrow))
                                        //{

                                        //    CommandInitilization(3);
                                        //}
                                        //else if (Input.GetKeyDown(KeyCode.UpArrow))
                                        //{

                                        //    CommandInitilization(1);
                                        //}
                                        
                                    }

                                    if(Input.GetKeyDown(KeyCode.V))
                                    {
                                        Debug.Log("Decent Start!");

                                        GameManager.Elizabat_SkillStart = true;
                                        
                                    }
                                }

                                if (GameManager.Elizabat_SkillStart)
                                {

                                    // 강하 공격 판정
                                    // hit는 레이가 부딪힌 물체의 정보를 가지고 있다. (위치, 판정 선 등등..)
                                    //if (Physics.Raycast(MainCamera.transform.position, CameraChecker.transform.position, out hit, 30.0f))
                                    //{


                                    //}

                                    ElizabatDecentAttack();

                                    Debug.DrawLine(MainCamera.transform.position, CameraChecker.transform.position, Color.red, 5f);
                                    //print(hit.transform.position);

                                    //StartCoroutine(MoveDown(MainCamera.transform, 1.0f, 10.0f));
                                }

                                //print(targetPosOnScreen);


                                if (GameManager.Elizabat_CommandStart)
                                {

                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                                        {
                                            NowSkillChecking = 2;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                                        {

                                            NowSkillChecking = 4;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetKeyDown(KeyCode.DownArrow))
                                        {
                                            NowSkillChecking = 3;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                                        {

                                            NowSkillChecking = 1;
                                            CommandStartOn = true;
                                        }
                                    }


                                    CommandInputStart(NowSkillChecking);
                                }
 
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                if (!GameManager.Elizabat_CommandStart)
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
                                        CommandInitilization();
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


                                if (GameManager.Elizabat_CommandStart)
                                {

                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                        {
                                            NowSkillChecking = 2;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                        {

                                            NowSkillChecking = 4;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                        {
                                            NowSkillChecking = 3;
                                            CommandStartOn = true;
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                        {

                                            NowSkillChecking = 1;
                                            CommandStartOn = true;
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

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            Vector3 pos = thisTransform.position;
            pos.y = Mathf.Lerp(startPos, endPos, t);
            thisTransform.position = pos;

            yield return 0;
        }
    }

    void ElizabatDecentAttack()
    {


        if (ElizabatDecentOn)
        {
            //GameManager.Elizabat_SkillStart = true;
            ElizabatDecentOn = false;

            FowardAttack = (CameraChecker.transform.localPosition - MainCamera.transform.position).normalized;
            BackAttack = (MainCamera.transform.position - CameraChecker.transform.localPosition).normalized;

            BackupPos = MainCamera.transform.position;

            TargetPos = CameraChecker.transform.position;

            //ray = MainCamera.toray

        }

        print("TargetPos : " + TargetPos);
        print("FowardAttack : " + FowardAttack);
        print("BackAttack : " + BackAttack);
        print("BackupPos : " + BackupPos);

        if (MainCamera.transform.position.y >= TargetPos.y)
        {
            // 강하 공격 판정
            // hit는 레이가 부딪힌 물체의 정보를 가지고 있다. (위치, 판정 선 등등..)
            if(!Physics.Linecast(MainCamera.transform.position, Target.transform.position, out hit))
            {
                MainCamera.transform.position += (FowardAttack * 50.0f * Time.deltaTime);
            }

        }
        else
        {
            if (MainCamera.transform.position.y >= BackupPos.y)
            {
                GameManager.Elizabat_SkillStart = false;
            }
            else
            {
                MainCamera.transform.position -= (BackAttack * 50.0f * Time.deltaTime);
            }
        }

            //if (hit.collider.tag == "Enemy")
            //{
            //    MainCamera.transform.Translate(BackAttack * 100.0f * Time.deltaTime);
            //}
            //else if(hit.collider.tag == "Walls")
            //{
            //    MainCamera.transform.Translate(BackAttack * 100.0f * Time.deltaTime);
            //}
            //else
            //{
            //    MainCamera.transform.Translate(hit.transform.position * 100.0f * Time.deltaTime);
            //}
    }

    void CommandInitilization()
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {
                    // 소닉 웨이브
                    int NowCommand = 0;

                    SonicMaxCount = Random.Range(2, 5);
                    EclipseMaxCount = Random.Range(2, 7);
                    SwarmMaxCount = Random.Range(2, 7);
                    DecentMaxCount = Random.Range(2, 3);

                    for (int i = 1; i < SonicMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        SonicWaveCommand[i] = NowCommand;
                    
                    }

                    for (int i = 1; i < EclipseMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        EclipseCommand[i] = NowCommand;
                    }

                    for (int i = 1; i < SwarmMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        SwarmCommand[i] = NowCommand;
                    }
                    for (int i = 1; i < DecentMaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        DecentCommand[i] = NowCommand;
                    }

                    currentNum = 0;
                    NowSkillChecking = 0;

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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;
                                                    
                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SonicMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                EclipseMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SwarmMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SonicMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        SonicWaveCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SonicMaxCount) && (SonicMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SonicMaxCount = 0;

                                                for (int i = 0; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    EclipseMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        EclipseCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= EclipseMaxCount) && (EclipseMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                EclipseMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    DecentMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        DecentCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= DecentMaxCount) && (DecentMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                DecentMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
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

                                                    GameManager.Elizabat_CommandStart = false;
                                                    CommandStartOn = false;
                                                    NowSkillChecking = 0;
                                                    SwarmMaxCount = 0;

                                                    for (int i = 1; i < 7; i++)
                                                    {
                                                        SwarmCommand[i] = 0;
                                                    }
                                                }
                                            }

                                            if ((currentNum >= SwarmMaxCount) && (SwarmMaxCount != 0))
                                            {
                                                print("Command Success!");

                                                GameManager.Elizabat_CommandStart = false;
                                                CommandStartOn = false;
                                                NowSkillChecking = 0;
                                                SwarmMaxCount = 0;

                                                for (int i = 1; i < 7; i++)
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
