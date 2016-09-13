using UnityEngine;
using System.Collections;

public class Elizabat : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    public Transform CameraChecker;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 targetPosOnScreen;

    //private RaycastHit hit;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

	}
	
	// Update is called once per frame
	void Update () 
    {
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
                                targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
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
                                
                                // 마우스 작업

                                if (Input.GetKey(KeyCode.F))
                                {
                                    transform.position += new Vector3(-1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime;

                                }
                                if (Input.GetKey(KeyCode.H))
                                {
                                    transform.position += new Vector3(1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime;

                                }
                                if (Input.GetKey(KeyCode.G))
                                {
                                    transform.position += new Vector3(0, 0, -1.5f) * normalMoveSpeed * Time.deltaTime;

                                }
                                if (Input.GetKey(KeyCode.T))
                                {
                                    transform.position += new Vector3(0, 0, 1.5f) * normalMoveSpeed * Time.deltaTime;
                                }

                                //if(Input.GetKey(KeyCode.LeftArrow))
                                //{
                                //    transform.position += new Vector3(-1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;
                                //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                                //    transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                //    //transform.rotation = Quaternion.LookRotation(Vector3.forward, hit.normal);
                                //}
                                //if (Input.GetKey(KeyCode.RightArrow))
                                //{
                                //    transform.position += new Vector3(1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;
                                //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                                //    transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                //   // transform.rotation = Quaternion.LookRotation(Vector3.forward, hit.normal);
                                //}
                                //if (Input.GetKey(KeyCode.DownArrow))
                                //{
                                //    transform.position += new Vector3(0, hit.normal.y, -1) * normalMoveSpeed * Time.deltaTime;
                                //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                                //    transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                //    //transform.rotation = Quaternion.LookRotation(Vector3.forward, hit.normal);
                                //    //transform.position += (-1 * transform.forward) * normalMoveSpeed * Time.deltaTime;
                                //}
                                //if (Input.GetKey(KeyCode.UpArrow))
                                //{
                                //    transform.position += new Vector3(0, hit.normal.y, 1) * normalMoveSpeed * Time.deltaTime;
                                //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                                //    transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                //    //transform.rotation = Quaternion.LookRotation(Vector3.forward, hit.normal);
                                //    //transform.position += transform.forward * normalMoveSpeed * Time.deltaTime;
                                //}
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                if (Input.GetAxisRaw("P1_360_RightStick") == 1)
                                {

                                    Debug.Log("RightStick!");

                                    //rotationX +=  cameraSensitivity * Time.deltaTime;
                                    transform.position += transform.right * FastMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P1_360_RightStick") == -1)
                                {

                                    Debug.Log("LeftStick!");


                                    //rotationX -= cameraSensitivity * Time.deltaTime;
                                    //transform.position -= transform.forward * normalMoveSpeed * Time.deltaTime;
                                    transform.position -= transform.right * FastMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P1_360_UpStick") == -1)
                                {

                                    Debug.Log("UpStick!");

                                    //rotationY += cameraSensitivity * Time.deltaTime;
                                    transform.position += new Vector3(0, 0, 1) * FastMoveSpeed * Time.deltaTime;

                                }

                                if (Input.GetAxisRaw("P1_360_UpStick") == 1)
                                {

                                    Debug.Log("DownStick!");

                                    //rotationY -= cameraSensitivity * Time.deltaTime;
                                    transform.position -= new Vector3(0, 0, 1) * FastMoveSpeed * Time.deltaTime;
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
}
