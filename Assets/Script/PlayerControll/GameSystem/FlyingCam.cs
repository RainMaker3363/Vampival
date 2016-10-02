using UnityEngine;
using System.Collections;

public class FlyingCam : MonoBehaviour {

    public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;


    public GameObject MainCameraChecker;
    //public GameObject CameraDir;

    private Vector3 CameraCheck;
    private float Direction;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    //private float CameraMoveMinX = -5.0f;
    //private float CameraMoveMinY = -5.0f;
    //private float CameraMoveMaxX = 5.0f;
    //private float CameraMoveMaxY = 5.0f;

    //private float CameraMovingPoint = 1.0f;
    //private float NowCurrentCoordX = 0.0f;
    //private float NowCurrentCoordY = 0.0f;

    private ViewControllMode ViewMode;

    void Start()
    {
        ViewMode = GameManager.ViewMode;
        
        //Screen.lockCursor = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        switch(ViewMode)
        {
            case ViewControllMode.Mouse:
                {
                    // 마우스 작업

                    CameraCheck = Camera.main.WorldToScreenPoint(MainCameraChecker.transform.position);
                    //Direction = Vector3.Distance(this.gameObject.transform.position, CameraDir.transform.position);

                    if ((!GameManager.Elizabat_CommandStart) && !GameManager.Elizabat_SkillStart)
                    {
                        if ((CameraCheck.x >= 0))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                transform.Translate(new Vector3(-2, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(-1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                //NowCurrentCoordX -= (CameraMovingPoint * Time.deltaTime);
                            }
                        }

                        if ((CameraCheck.x <= Screen.width))
                        {
                            if (Input.GetKey(KeyCode.D))
                            {
                                transform.Translate(new Vector3(2, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                //NowCurrentCoordX += (CameraMovingPoint * Time.deltaTime);
                                //transform.position += (-1 * transform.forward) * normalMoveSpeed * Time.deltaTime;
                            }
                        }

                        if ((CameraCheck.y <= Screen.height))
                        {
                            if (Input.GetKey(KeyCode.W))
                            {
                                //transform.position += new Vector3(0, ForwardDir.y, ForwardDir.z) * normalMoveSpeed * Time.deltaTime;
                                transform.Translate(new Vector3(0, 1, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(0, 0.75f, 0) * normalMoveSpeed * Time.deltaTime);
                                //NowCurrentCoordY += (CameraMovingPoint * Time.deltaTime);
                            }
                        }

                        if ((CameraCheck.y >= 0))
                        {
                            if (Input.GetKey(KeyCode.S))
                            {
                                //transform.position -= new Vector3(0, ForwardDir.y, ForwardDir.z) * normalMoveSpeed * Time.deltaTime;
                                transform.Translate(new Vector3(0, -1, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(0, -1, 0) * normalMoveSpeed * Time.deltaTime);
                                //NowCurrentCoordY -= (CameraMovingPoint * Time.deltaTime);
                                //transform.position += transform.forward * normalMoveSpeed * Time.deltaTime;
                            }
                        }
                    }

                    //print(CameraCheck);

                    //print("X : " + NowCurrentCoordX);
                    //print("Y : " + NowCurrentCoordY);

                    //if(Input.GetKey(KeyCode.A))
                    //{
                    //    transform.position += (-1 * transform.right) * normalMoveSpeed * Time.deltaTime;
                    //}
                    //if (Input.GetKey(KeyCode.D))
                    //{
                    //    transform.position += transform.right * normalMoveSpeed * Time.deltaTime;
                    //}
                    //if (Input.GetKey(KeyCode.S))
                    //{
                    //    transform.position += new Vector3(0, 0, -1) * normalMoveSpeed * Time.deltaTime;
                        
                    //    //transform.position += (-1 * transform.forward) * normalMoveSpeed * Time.deltaTime;
                    //}
                    //if (Input.GetKey(KeyCode.W))
                    //{
                    //    transform.position += new Vector3(0, 0, 1) * normalMoveSpeed * Time.deltaTime;
                    //    //transform.position += transform.forward * normalMoveSpeed * Time.deltaTime;
                    //}

                    //rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                    //rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
                    //rotationY = Mathf.Clamp(rotationY, -90, 90);

                    //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                    //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

                    //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    //{
                    //    transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                    //    transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //}
                    //else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    //{
                    //    transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                    //    transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //}
                    //else
                    //{
                    //    transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                    //    transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
                    //}


                    //if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
                    //if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
                }
                break;

            case ViewControllMode.GamePad:
                {
                    // 게임 패드 작업

                    CameraCheck = Camera.main.WorldToScreenPoint(MainCameraChecker.transform.position);

                    if ((!GameManager.Elizabat_CommandStart) && (!GameManager.Elizabat_SkillStart))
                    {
                        if ((CameraCheck.x >= 0))
                        {
                            if (Input.GetAxisRaw("P1_360_LeftThumbStick") == -1)
                            {

                                Debug.Log("LeftStick!");


                                transform.Translate(new Vector3(-1, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(-1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                            }
                        }

                        if ((CameraCheck.x <= Screen.width))
                        {
                            if (Input.GetAxisRaw("P1_360_LeftThumbStick") == 1)
                            {

                                Debug.Log("RightStick!");

                                transform.Translate(new Vector3(1, 0, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime);
                            }
                        }

                        if ((CameraCheck.y <= Screen.height))
                        {
                            if (Input.GetAxisRaw("P1_360_UpThumbStick") == -1)
                            {

                                Debug.Log("UpStick!");

                                transform.Translate(new Vector3(0, 1, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(0, 0, -2.5f) * normalMoveSpeed * Time.deltaTime);

                            }
                        }

                        if ((CameraCheck.y >= 0))
                        {
                            if (Input.GetAxisRaw("P1_360_UpThumbStick") == 1)
                            {

                                Debug.Log("DownStick!");

                                transform.Translate(new Vector3(0, -1, 0) * normalMoveSpeed * Time.deltaTime);
                                MainCameraChecker.transform.Translate(new Vector3(0, 0, 2.5f) * normalMoveSpeed * Time.deltaTime);
                            }
                        }
                    }

                    //rotationY = Mathf.Clamp(rotationY, -90, 90);

                    //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                    //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

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
