using UnityEngine;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private bool ElizabatDecentOn;

    private Vector3 CheckerStartPos = Vector3.zero;
    private Vector3 targetPosOnScreen;
    private RaycastHit hit;
    private Light light;
    //private Ray ray;

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

        light = GetComponent<Light>();
        light.enabled = false;

        CheckerStartPos = CameraChecker.transform.position;

        Leng = 0.0f;
        layermask = (1 << LayerMask.NameToLayer("LayCastIn"));//(-1) - (1 << 9) | (1 << 10) | (1 << 12) | (1 << 15);
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

                                

                                if (!GameManager.Elizabat_CommandStart && !GameManager.Elizabat_SkillStart)
                                {
                                    light.enabled = false;

                                    if (Input.GetKeyDown(KeyCode.C))
                                    {
                                        Debug.Log("Command Start!");

                                        CameraChecker.transform.position = CheckerStartPos;

                                        CommandInitilization();
                                        
                                    }

                                    //if(Input.GetKeyDown(KeyCode.V))
                                    //{
                                    //    StartCoroutine("SonicWaveSkill");
                                    //}

                                    //if(Input.GetKeyDown(KeyCode.V))
                                    //{
                                    //    Debug.Log("Decent Start!");

                                    //    GameManager.Elizabat_SkillStart = true;
                                        
                                    //}
                                }


                                if (GameManager.Elizabat_CommandStart)
                                {
                                    light.enabled = true;

                                    targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                    if (Physics.Raycast(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 80.0f, out hit, Mathf.Infinity, layermask))
                                    {
                                        //Debug.DrawRay(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 200.0f);

                                        if (hit.collider.tag.Equals("Ground") == true)
                                        {
                                            Leng = Vector3.Distance(this.transform.position, hit.point);

                                            SkillTarget.transform.localPosition = new Vector3(SkillTarget.transform.localPosition.x, SkillTarget.transform.localPosition.y, Leng);
                                            CameraChecker.transform.localPosition = SkillTarget.transform.localPosition;
                                        }
                                    }

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

                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                                        {
                                            // 일식
                                            if(GameManager.Elizabat_Eclipse_On == false)
                                            {
                                                NowSkillChecking = 2;
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
                                            if(GameManager.Elizabat_Swarm_On == false)
                                            {
                                                NowSkillChecking = 4;
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
                                            if(GameManager.Elizabat_Decent_On == false)
                                            {
                                                NowSkillChecking = 3;
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
                                            if(GameManager.Elizabat_SonicWave_On == false)
                                            {
                                                NowSkillChecking = 1;
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

                                //targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                if (!GameManager.Elizabat_CommandStart && !GameManager.Elizabat_SkillStart)
                                {
                                    light.enabled = false;


                                    if (Input.GetButtonDown("P1_360_AButton"))
                                    {
                                        Debug.Log("Command Start!");

                                        CameraChecker.transform.position = CheckerStartPos;

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
                                    light.enabled = true;

                                    targetPosOnScreen = Camera.main.WorldToScreenPoint(CameraChecker.transform.position);

                                    if (Physics.Raycast(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 80.0f, out hit, Mathf.Infinity, layermask))
                                    {
                                        //Debug.DrawRay(this.transform.position, (Elizabat_Interpol.transform.position - this.transform.position).normalized * 200.0f);

                                        if (hit.collider.tag.Equals("Ground") == true)
                                        {
                                            Leng = Vector3.Distance(this.transform.position, hit.point);

                                            SkillTarget.transform.localPosition = new Vector3(SkillTarget.transform.localPosition.x, SkillTarget.transform.localPosition.y, Leng);
                                            CameraChecker.transform.localPosition = SkillTarget.transform.localPosition;
                                        }
                                    }

                                    if (targetPosOnScreen.x > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_RightStick") == -1)
                                        {

                                            Debug.Log("LeftStick!");
                                            transform.Translate(new Vector3(-2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetAxisRaw("P1_360_RightStick") == 1)
                                        {

                                            Debug.Log("RightStick!");
                                            transform.Translate(new Vector3(2.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetAxisRaw("P1_360_UpStick") == 1)
                                        {

                                            Debug.Log("DownStick!");

                                            transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {

                                        if (Input.GetAxisRaw("P1_360_UpStick") == -1)
                                        {

                                            Debug.Log("UpStick!");

                                            transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (!CommandStartOn)
                                    {
                                        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                        {
                                            // 일식
                                            if (GameManager.Elizabat_Eclipse_On == false)
                                            {
                                                NowSkillChecking = 2;
                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                        {

                                            // 스웜 공격
                                            if (GameManager.Elizabat_Swarm_On == false)
                                            {
                                                NowSkillChecking = 4;
                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                        {
                                            // 강하 공격
                                            if (GameManager.Elizabat_Decent_On == false)
                                            {
                                                NowSkillChecking = 3;
                                                CommandStartOn = true;
                                            }
                                            else
                                            {
                                                // 실패 사운드 출력
                                            }
                                        }
                                        else if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                        {
                                            // 소닉 웨이브
                                            if (GameManager.Elizabat_SonicWave_On == false)
                                            {
                                                NowSkillChecking = 1;
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

        // 강하 공격 쿨 타임
        GameManager.Elizabat_Decent_On = true;

        yield return new WaitForSeconds(3.0f);

        print("Decent Skill Off"); 
        GameManager.Elizabat_Decent_On = false;
        //yield return 1;

    }

    // 스웜 공격
    IEnumerator SwarmSkill()
    {
        GameManager.Elizabat_Swarm_On = true;

        print("Swarm Skill On");
        // 스웜 이펙트 출력

        yield return new WaitForSeconds(7.0f);

        print("Swarm Skill Off");
        GameManager.Elizabat_Swarm_On = false;
    }

    // 일식 공격
    IEnumerator EclipseSkill()
    {
        GameManager.Elizabat_Eclipse_On = true;

        // 일식 효과
        print("Eclipse Skill On");
        yield return new WaitForSeconds(20.0f);

        print("Eclipse Skill Off");

        GameManager.Elizabat_Eclipse_On = false;
    }

    // 소닉 웨이브 공격
    IEnumerator SonicWaveSkill()
    {
        GameManager.Elizabat_SonicWave_On = true;

        // 소닉 웨이브 효과
        print("Sonic Skill On");

        yield return new WaitForSeconds(5.0f);

        print("Sonic Skill Off");

        GameManager.Elizabat_SonicWave_On = false;
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

                                                // 소닉 웨이브 스킬 시작
                                                StartCoroutine("SonicWave_Timer");

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

                                                // 일식 스킬 시작
                                                StartCoroutine("EclipseSkill");

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

                                                // 강하 공격
                                                StartCoroutine(DecentAttack(MainCamera.transform, CameraChecker, 10.0f));

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

                                                // 스웜 공격
                                                StartCoroutine("SwarmSkill");

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

                                                // 소닉 웨이브 스킬 시작
                                                StartCoroutine("SonicWave_Timer");

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

                                                // 일식 스킬 시작
                                                StartCoroutine("EclipseSkill");

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

                                                // 강하 공격

                                                StartCoroutine(DecentAttack(MainCamera.transform, CameraChecker, 10.0f));

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

                                                // 스웜 공격
                                                StartCoroutine("SwarmSkill");

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
