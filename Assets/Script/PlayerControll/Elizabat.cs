using UnityEngine;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 targetPosOnScreen;

    public GameObject CameraChecker;

    private int[] CommandChart = new int[6];
    private int MaxCount;
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

        for (int i = 0; i < 6; i++)
        {
            CommandChart[i] = 0;
        }

        Inputcommand = 0;
        currentNum = 0;
        MaxCount = 0;
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
                                            transform.Translate(new Vector3(-2, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.x < Screen.width)
                                    {
                                        if (Input.GetKey(KeyCode.H))
                                        {
                                            transform.Translate(new Vector3(2, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.y > 0)
                                    {
                                        if (Input.GetKey(KeyCode.G))
                                        {
                                            transform.Translate(new Vector3(0, -2, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if (targetPosOnScreen.y < Screen.height)
                                    {
                                        if (Input.GetKey(KeyCode.T))
                                        {
                                            transform.Translate(new Vector3(0, 2, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }


                                    if (Input.GetKeyDown(KeyCode.C))
                                    {
                                        CommandInitilization();
                                    }
                                }
                                

                                //print(targetPosOnScreen);


                                if (GameManager.CommandStart)
                                {
                                    
                                    CommandInputStart();
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
                                        CommandInitilization();

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

    void CommandInitilization()
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {
                    int NowCommand = 0;
                    MaxCount = Random.Range(2, 7);

                    for (int i = 0; i < MaxCount; i++)
                    {
                        NowCommand = Random.Range(1, 5);

                        CommandChart[i] = NowCommand;

                    }

                    CommandInit = true;
                    currentNum = 0;

                    //print("MaxCount : " + MaxCount);

                    for (int i = 0; i < MaxCount; i++)
                    {
                        Debug.Log(" CommandChart : " + CommandChart[i]);
                    }

                    GameManager.CommandStart = true;

                }
                break;
        }
    }

    void CommandInputStart()
    {
        switch (Gamestate)
        {
            case GameState.GameStart:
                {
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {

                                if (Input.GetKeyDown(KeyCode.LeftArrow))
                                {
                                    Inputcommand = 2;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }
                                else if (Input.GetKeyDown(KeyCode.RightArrow))
                                {
                                    Inputcommand = 4;
                                    
                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }
                                else if (Input.GetKeyDown(KeyCode.DownArrow))
                                {
                                    Inputcommand = 3;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }
                                else if (Input.GetKeyDown(KeyCode.UpArrow))
                                {
                                    Inputcommand = 1;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }

                                if ((currentNum >= MaxCount) && (MaxCount != 0))
                                {
                                    print("Command Success!");

                                    GameManager.CommandStart = false;
                                    MaxCount = 0;

                                    for (int i = 0; i < 6; i++)
                                    {
                                        CommandChart[i] = 0;
                                    }
                                }
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {




                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
                                {
                                    Inputcommand = 4;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }

                                }

                                if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
                                {
                                    Inputcommand = 2;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);


                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }

                                }

                                if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
                                {
                                    Inputcommand = 1;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }

                                if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
                                {
                                    Inputcommand = 3;

                                    print("currentNum : " + currentNum);
                                    print("MaxCount : " + MaxCount);

                                    if (CommandChart[currentNum] == Inputcommand)
                                    {
                                        currentNum++;

                                        Inputcommand = 0;
                                    }
                                    else
                                    {
                                        print("Command Failed!");

                                        GameManager.CommandStart = false;
                                        MaxCount = 0;

                                        for (int i = 0; i < 6; i++)
                                        {
                                            CommandChart[i] = 0;
                                        }
                                    }
                                }

                                if ((currentNum >= MaxCount) && (MaxCount != 0))
                                {
                                    print("Command Success!");

                                    GameManager.CommandStart = false;
                                    MaxCount = 0;

                                    for (int i = 0; i < 6; i++)
                                    {
                                        CommandChart[i] = 0;
                                    }
                                }
                            }
                            break;
                    }

                    
                }
                break;
        }
    }
}
