using UnityEngine;
using System.Collections;

public class FlyingCam : MonoBehaviour {

    public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;

    public GameObject MainCameraRoot;
    public GameObject MainCameraChecker;
    //public GameObject CameraDir;

    private Animator animator;
    private Vector3 CameraCheck;
    private float Direction;

    private float ZoomInOut;
    private float StartYPos;
    private Vector3 StartZoomPos;

    private bool JoyPadZoomInOut;

    //private float rotationX = 0.0f;
    //private float rotationY = 0.0f;

    //private float CameraMoveMinX = -5.0f;
    //private float CameraMoveMinY = -5.0f;
    //private float CameraMoveMaxX = 5.0f;
    //private float CameraMoveMaxY = 5.0f;

    //private float CameraMovingPoint = 1.0f;
    //private float NowCurrentCoordX = 0.0f;
    //private float NowCurrentCoordY = 0.0f;

    private GameState Gamestate;
    private ViewControllMode ViewMode;

    void Awake()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        Gamestate = GameManager.Gamestate;
        ViewMode = GameManager.ViewMode;


        StartYPos = MainCameraRoot.transform.position.y;
        StartZoomPos = MainCameraRoot.transform.position;
        ZoomInOut = 0.0f;

        JoyPadZoomInOut = false;
        
        //Screen.lockCursor = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void GameStartUp()
    {
        
        GameManager.GameStartUp();

        animator.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        Gamestate = GameManager.Gamestate;
        ViewMode = GameManager.ViewMode;

        switch(Gamestate)
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
                    if ((!GameManager.Elizabat_CommandStart) && !GameManager.Elizabat_SkillStart)
                    {

                        CameraCheck = Camera.main.WorldToScreenPoint(MainCameraChecker.transform.position);

                        switch (ViewMode)
                        {
                            case ViewControllMode.Mouse:
                                {
                                    if ((CameraCheck.x >= 0))
                                    {
                                        if (Input.GetKey(KeyCode.A))
                                        {
                                            MainCameraRoot.transform.Translate(new Vector3(3.3f, 0, 0) * normalMoveSpeed * Time.deltaTime);



                                            MainCameraChecker.transform.Translate(new Vector3(1.7f, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if ((CameraCheck.x <= Screen.width))
                                    {
                                        if (Input.GetKey(KeyCode.D))
                                        {
                                            MainCameraRoot.transform.Translate(new Vector3(-3.3f, 0, 0) * normalMoveSpeed * Time.deltaTime);




                                            MainCameraChecker.transform.Translate(new Vector3(-1.7f, 0, 0) * normalMoveSpeed * Time.deltaTime);


                                        }
                                    }

                                    if ((CameraCheck.y <= Screen.height))
                                    {
                                        if (Input.GetKey(KeyCode.W))
                                        {

                                            MainCameraRoot.transform.Translate(new Vector3(0, -4.2f, 0) * normalMoveSpeed * Time.deltaTime);

                                            MainCameraChecker.transform.Translate(new Vector3(0, -0.7f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if ((CameraCheck.y >= 0))
                                    {
                                        if (Input.GetKey(KeyCode.S))
                                        {

                                            MainCameraRoot.transform.Translate(new Vector3(0, 4.2f, 0) * normalMoveSpeed * Time.deltaTime);

                                            MainCameraChecker.transform.Translate(new Vector3(0, 0.7f, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    if (Input.GetKey(KeyCode.Q))
                                    {
                                        //if (MainCameraRoot.transform.localEulerAngles.y > 96.0f && MainCameraRoot.transform.localEulerAngles.y < 360.0f)
                                        //{
                                        //    MainCameraRoot.transform.localEulerAngles = new Vector3(90.0f, 96.0f, 0.0f);
                                        //}
                                        //else
                                        //{
                                        //    MainCameraRoot.transform.Rotate(new Vector3(0, 0, -1), 50 * Time.deltaTime);
                                        //}

                                        MainCameraRoot.transform.Rotate(new Vector3(0, 0, -1), 50 * Time.deltaTime);
                                    }

                                    if (Input.GetKey(KeyCode.E))
                                    {
                                        //if (MainCameraRoot.transform.localEulerAngles.y < 360.0f && MainCameraRoot.transform.localEulerAngles.y >= 340.0f)
                                        //{
                                        //    MainCameraRoot.transform.localEulerAngles = new Vector3(90.0f, 360.0f, 0.0f);
                                        //}
                                        //else
                                        //{

                                        //}

                                        MainCameraRoot.transform.Rotate(new Vector3(0, 0, 1), 50 * Time.deltaTime);
                                    }

                                    // 줌 인 & 아웃 기능 구현
                                    if (Input.GetKey(KeyCode.R))
                                    {
                                        if (ZoomInOut <= 2.9f)
                                        {
                                            ZoomInOut += 2.8f * Time.deltaTime;

                                            
                                            MainCameraRoot.transform.Translate(new Vector3(0, 0, 48f) * Time.deltaTime);
                                        }

                                        
                                        //StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    }

                                    if (Input.GetKey(KeyCode.F))
                                    {
                                        if (ZoomInOut >= -2.9f)
                                        {
                                            ZoomInOut -= 2.8f * Time.deltaTime;

                                            MainCameraRoot.transform.Translate(new Vector3(0, 0, -48f) * Time.deltaTime);
                                        }
                                        
                                        //StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    }

                                    //if (Input.GetKey(KeyCode.R))
                                    //{
                                    //    if (ZoomInOut <= 2.5f)
                                    //    {
                                    //        ZoomInOut += 2.8f * Time.deltaTime;

                                    //        MainCameraRoot.transform.Translate(new Vector3(0, 0, -ZoomInOut));
                                    //    }

                                    //    StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    //}
                                    //else if (Input.GetKeyUp(KeyCode.R))
                                    //{
                                    //    MainCameraRoot.transform.localPosition = StartZoomPos;
                                    //    ZoomInOut = 0.0f;
                                    //}

                                    //if (Input.GetKey(KeyCode.F))
                                    //{
                                    //    if (ZoomInOut <= 2.5f)
                                    //    {
                                    //        ZoomInOut += 2.8f * Time.deltaTime;

                                    //        MainCameraRoot.transform.Translate(new Vector3(0, 0, ZoomInOut));
                                    //    }

                                    //    StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    //}
                                    //else if (Input.GetKeyUp(KeyCode.F))
                                    //{
                                    //    MainCameraRoot.transform.localPosition = StartZoomPos;
                                    //    ZoomInOut = 0.0f;
                                    //}
                                }
                                break;

                            case ViewControllMode.GamePad:
                                {
                                    if ((CameraCheck.x >= 0))
                                    {
                                        if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("P1_360_L_RightStick") <= -0.5f)
                                        {
                                            MainCameraRoot.transform.Translate(new Vector3(3.3f, 0, 0) * normalMoveSpeed * Time.deltaTime);



                                            MainCameraChecker.transform.Translate(new Vector3(1.7f, 0, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if ((CameraCheck.x <= Screen.width))
                                    {
                                        if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("P1_360_L_RightStick") >= 0.5f)
                                        {
                                            MainCameraRoot.transform.Translate(new Vector3(-3.3f, 0, 0) * normalMoveSpeed * Time.deltaTime);




                                            MainCameraChecker.transform.Translate(new Vector3(-1.7f, 0, 0) * normalMoveSpeed * Time.deltaTime);


                                        }
                                    }

                                    if ((CameraCheck.y <= Screen.height))
                                    {
                                        if (Input.GetKey(KeyCode.W) || Input.GetAxisRaw("P1_360_L_UpStick") <= -0.5f)
                                        {

                                            MainCameraRoot.transform.Translate(new Vector3(0, -4.0f, 0) * normalMoveSpeed * Time.deltaTime);

                                            MainCameraChecker.transform.Translate(new Vector3(0, -0.7f, 0) * normalMoveSpeed * Time.deltaTime);

                                        }
                                    }

                                    if ((CameraCheck.y >= 0))
                                    {
                                        if (Input.GetKey(KeyCode.S) || Input.GetAxisRaw("P1_360_L_UpStick") >= 0.5f)
                                        {

                                            MainCameraRoot.transform.Translate(new Vector3(0, 4.0f, 0) * normalMoveSpeed * Time.deltaTime);

                                            MainCameraChecker.transform.Translate(new Vector3(0, 0.7f, 0) * normalMoveSpeed * Time.deltaTime);
                                        }
                                    }

                                    // 회전 기능
                                    if (Input.GetButton("P1_360_LeftBumper") || Input.GetKey(KeyCode.Q))
                                    {
                                        //if (MainCameraRoot.transform.localEulerAngles.y > 96.0f && MainCameraRoot.transform.localEulerAngles.y < 360.0f)
                                        //{
                                        //    MainCameraRoot.transform.localEulerAngles = new Vector3(90.0f, 96.0f, 0.0f);
                                        //}
                                        //else
                                        //{
                                        //    MainCameraRoot.transform.Rotate(new Vector3(0, 0, -1), 50 * Time.deltaTime);
                                        //}

                                        MainCameraRoot.transform.Rotate(new Vector3(0, 0, -1), 50 * Time.deltaTime);
                                    }

                                    if (Input.GetButton("P1_360_RightBumper") || Input.GetKey(KeyCode.E))
                                    {
                                        //if (MainCameraRoot.transform.localEulerAngles.y < 360.0f && MainCameraRoot.transform.localEulerAngles.y >= 340.0f)
                                        //{
                                        //    MainCameraRoot.transform.localEulerAngles = new Vector3(90.0f, 360.0f, 0.0f);
                                        //}
                                        //else
                                        //{

                                        //}

                                        MainCameraRoot.transform.Rotate(new Vector3(0, 0, 1), 50 * Time.deltaTime);
                                    }

                                    // 줌 인 & 줌 아웃 기능
                                    if (Input.GetAxis("P1_360_Trigger") > 0.001 || Input.GetKey(KeyCode.R))
                                    {
                                        if (ZoomInOut <= 2.9f)
                                        {
                                            ZoomInOut += 2.8f * Time.deltaTime;

                                            MainCameraRoot.transform.Translate(new Vector3(0, 0, 48.0f) * Time.deltaTime);
                                        }

                                        //StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    }

                                    if (Input.GetAxis("P1_360_Trigger") < 0 || Input.GetKey(KeyCode.F))
                                    {
                                        if (ZoomInOut >= -2.9f)
                                        {
                                            ZoomInOut -= 2.8f * Time.deltaTime;

                                            MainCameraRoot.transform.Translate(new Vector3(0, 0, -48.0f) * Time.deltaTime);
                                        }

                                        //StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);
                                    }
                                    //if (Input.GetAxis("P1_360_Trigger") > 0.001)
                                    //{

                                    //    Debug.Log("Right Trigger!");

                                    //    if (ZoomInOut <= 2.5f)
                                    //    {
                                    //        ZoomInOut += 2.8f * Time.deltaTime;

                                    //        MainCameraRoot.transform.Translate(new Vector3(0, 0, -ZoomInOut));
                                    //    }

                                    //    StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);

                                    //    JoyPadZoomInOut = true;
                                    //}

                                    //if (Input.GetAxis("P1_360_Trigger") < 0)
                                    //{

                                    //    Debug.Log("Left Trigger!");

                                    //    if (ZoomInOut <= 2.5f)
                                    //    {
                                    //        ZoomInOut += 2.8f * Time.deltaTime;

                                    //        MainCameraRoot.transform.Translate(new Vector3(0, 0, ZoomInOut));
                                    //    }

                                    //    StartZoomPos = new Vector3(MainCameraRoot.transform.localPosition.x, StartYPos, MainCameraRoot.transform.localPosition.z);

                                    //    JoyPadZoomInOut = true;
                                        
                                    //}

                                    //if (Input.GetAxis("P1_360_Trigger") == 0 && JoyPadZoomInOut == true)
                                    //{
                                    //    MainCameraRoot.transform.localPosition = StartZoomPos;
                                    //    ZoomInOut = 0.0f;
                                    //    JoyPadZoomInOut = false;
                                    //}

                                    
                                    
                                }
                                break;
                        }

                    }
                    //switch (ViewMode)
                    //{
                    //    case ViewControllMode.Mouse:
                    //        {
                    //            // 마우스 작업

                    //            //print("Mouse");
                    //            //Direction = Vector3.Distance(this.gameObject.transform.position, CameraDir.transform.position);



                    //            //print(MainCameraRoot.gameObject.transform.localRotation);
                    //            //print(MainCameraRoot.transform.localRotation.eulerAngles);
                    //            //print(MainCameraRoot.transform.eulerAngles);
                    //            //print(CameraCheck);

                    //            //print("X : " + NowCurrentCoordX);
                    //            //print("Y : " + NowCurrentCoordY);

                    //            //if(Input.GetKey(KeyCode.A))
                    //            //{
                    //            //    transform.position += (-1 * transform.right) * normalMoveSpeed * Time.deltaTime;
                    //            //}
                    //            //if (Input.GetKey(KeyCode.D))
                    //            //{
                    //            //    transform.position += transform.right * normalMoveSpeed * Time.deltaTime;
                    //            //}
                    //            //if (Input.GetKey(KeyCode.S))
                    //            //{
                    //            //    transform.position += new Vector3(0, 0, -1) * normalMoveSpeed * Time.deltaTime;

                    //            //    //transform.position += (-1 * transform.forward) * normalMoveSpeed * Time.deltaTime;
                    //            //}
                    //            //if (Input.GetKey(KeyCode.W))
                    //            //{
                    //            //    transform.position += new Vector3(0, 0, 1) * normalMoveSpeed * Time.deltaTime;
                    //            //    //transform.position += transform.forward * normalMoveSpeed * Time.deltaTime;
                    //            //}

                    //            //rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                    //            //rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
                    //            //rotationY = Mathf.Clamp(rotationY, -90, 90);

                    //            //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                    //            //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

                    //            //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    //            //{
                    //            //    transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                    //            //    transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //            //}
                    //            //else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    //            //{
                    //            //    transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                    //            //    transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //            //}
                    //            //else
                    //            //{
                    //            //    transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                    //            //    transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //            //}


                    //            //if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
                    //            //if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
                    //        }
                    //        break;

                    //    case ViewControllMode.GamePad:
                    //        {
                    //            // 게임 패드 작업

                    //            if ((!GameManager.Elizabat_CommandStart) && !GameManager.Elizabat_SkillStart)
                    //            {
                    //                CameraCheck = Camera.main.WorldToScreenPoint(MainCameraChecker.transform.position);

                                    
                    //            }

                    //            //rotationY = Mathf.Clamp(rotationY, -90, 90);

                    //            //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                    //            //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

                    //        }
                    //        break;
                    //}
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }


        //if (Input.GetKeyDown(KeyCode.End))
        //{
        //    switch (Cursor.lockState)
        //    {
        //        case CursorLockMode.Locked:
        //            {
        //                Cursor.lockState = CursorLockMode.None;
        //                Cursor.visible = true;
        //            }
        //            break;

        //        case CursorLockMode.None:
        //            {
        //                Cursor.lockState = CursorLockMode.Locked;
        //                Cursor.visible = false;
        //            }
        //            break;
        //    }


        //    //Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
        //}
    }
}
