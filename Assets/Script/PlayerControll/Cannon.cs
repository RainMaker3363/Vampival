using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private float TargetDis;

    //private RaycastHit hit;

    public GameObject CrossHair_Icon;
    public GameObject[] Cannons = new GameObject[1];

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;
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

                                //Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 100);
                                //Vector3 direction = new Vector3(hit.normal.x, hit.normal.y * 1, hit.normal.z);


                                // 마우스 작업

                                TargetDis = Vector3.Distance(CrossHair_Icon.transform.position, Cannons[0].transform.position);

                                if (Input.GetKey(KeyCode.LeftArrow))
                                {

                                    this.transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);
                                    Cannons[0].transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);

                                    //CrossHair_Icon.transform.position += new Vector3(-1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                    //CrossHair_Icon.transform.position += new Vector3(-1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                }
                                if (Input.GetKey(KeyCode.RightArrow))
                                {
                                    this.transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);
                                    Cannons[0].transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);
                                    
                                    //CrossHair_Icon.transform.position += new Vector3(1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                    //CrossHair_Icon.transform.position += new Vector3(1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                }
                                if (Input.GetKey(KeyCode.DownArrow))
                                {
                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 30 * Time.deltaTime);

                                    CrossHair_Icon.transform.position += new Vector3(0, 0, 2.5f) * normalMoveSpeed * Time.deltaTime;
                                   // CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, 1) * normalMoveSpeed * Time.deltaTime;
                                }
                                if (Input.GetKey(KeyCode.UpArrow))
                                {
                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 30 * Time.deltaTime);

                                    CrossHair_Icon.transform.position += new Vector3(0, 0, -2.5f) * normalMoveSpeed * Time.deltaTime;
                                    //CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, -1) * normalMoveSpeed * Time.deltaTime;
                                }

                                //print(this.transform.rotation.eulerAngles);
                                print(TargetDis);
                               // print("Cannons[0].rotation.x : " + Cannons[0].transform.eulerAngles.x);
                                //print("Cannons[0].rotation.y : " + Cannons[0].transform.eulerAngles.y);
                                //print("Cannons[0].rotation.z : " + Cannons[0].transform.eulerAngles.z);

                                //if (Input.GetKey(KeyCode.DownArrow))
                                //{
                                //    transform.position += new Vector3(0, 0, -1.5f) * normalMoveSpeed * Time.deltaTime;

                                //}
                                //if (Input.GetKey(KeyCode.UpArrow))
                                //{
                                //    transform.position += new Vector3(0, 0, 1.5f) * normalMoveSpeed * Time.deltaTime;
                                //}

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

                                if (Input.GetAxisRaw("P2_360_RightStick") == 1)
                                {

                                    Debug.Log("RightStick!");

                                    this.transform.Rotate(new Vector3(0, -90, 0), 50 * Time.deltaTime);
                                    CrossHair_Icon.transform.position += new Vector3(1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P2_360_RightStick") == -1)
                                {

                                    Debug.Log("LeftStick!");

                                    this.transform.Rotate(new Vector3(0, 90, 0), 50 * Time.deltaTime);
                                    CrossHair_Icon.transform.position += new Vector3(-1.5f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P2_360_UpStick") == -1)
                                {

                                    Debug.Log("UpStick!");

                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 30 * Time.deltaTime);

                                    CrossHair_Icon.transform.position += new Vector3(0, 0, -1.5f) * normalMoveSpeed * Time.deltaTime;

                                }

                                if (Input.GetAxisRaw("P2_360_UpStick") == 1)
                                {

                                    Debug.Log("DownStick!");

                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 30 * Time.deltaTime);

                                    CrossHair_Icon.transform.position += new Vector3(0, 0, 1.5f) * normalMoveSpeed * Time.deltaTime;
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
