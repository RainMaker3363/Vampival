using UnityEngine;
using System.Collections;

public class Third_Cannon : MonoBehaviour 
{
    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private float RotationPossibleMax;
    private float RotationPossibleMin;
    private float RotationPossibleValue;
    private float TargetDis;
    private CannonNumber MyCannonNumber;

    //private RaycastHit hit;

    public GameObject Cannon_Indicator;
    public GameObject Dummy;
    public GameObject CrossHair_Icon;
    public GameObject[] Cannons = new GameObject[1];

    public Light SelectLight;

    // Use this for initialization
    void Awake()
    {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        MyCannonNumber = GameManager.CannonControl_Number;

        RotationPossibleMax = 54.0f;
        RotationPossibleMin = -54.0f;
        RotationPossibleValue = 0.0f;

        SelectLight.enabled = false;
        CrossHair_Icon.SetActive(false);
        Cannon_Indicator.SetActive(false);
        //Cannons[0].transform.LookAt(new Vector3(Dummy.transform.position.x, Cannons[0].transform.position.y, Dummy.transform.position.z));
    }

    void OnEnable()
    {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        MyCannonNumber = GameManager.CannonControl_Number;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        MyCannonNumber = GameManager.CannonControl_Number;
        Gamestate = GameManager.Gamestate;
        ViewMode = GameManager.ViewMode;

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

            case GameState.GameVictory:
                {

                }
                break;

            case GameState.GameStart:
                {
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                switch (MyCannonNumber)
                                {
                                    case CannonNumber.First:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                            Cannon_Indicator.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Second:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                            Cannon_Indicator.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Third:
                                        {
                                            SelectLight.enabled = true;
                                            CrossHair_Icon.gameObject.SetActive(true);
                                            Cannon_Indicator.SetActive(true);

                                            //Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 100);
                                            //Vector3 direction = new Vector3(hit.normal.x, hit.normal.y * 1, hit.normal.z);


                                            // 마우스 작업

                                            TargetDis = Vector3.Distance(CrossHair_Icon.transform.position, Cannons[0].transform.position);

                                            //this.transform.LookAt(new Vector3(CrossHair_Icon.transform.position.x, 0, CrossHair_Icon.transform.position.z));

                                            if (Input.GetKey(KeyCode.LeftArrow))
                                            {
                                                if (RotationPossibleValue <= RotationPossibleMax)
                                                {
                                                    RotationPossibleValue += 42.0f * Time.deltaTime;
                                                    this.transform.Rotate(new Vector3(0, 0, -90), 60 * Time.deltaTime);
                                                }

                                                //Cannons[0].transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);

                                                //CrossHair_Icon.transform.position += new Vector3(-1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                                //CrossHair_Icon.transform.position += new Vector3(-1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                            }
                                            if (Input.GetKey(KeyCode.RightArrow))
                                            {
                                                if (RotationPossibleValue >= RotationPossibleMin)
                                                {
                                                    RotationPossibleValue -= 42.0f * Time.deltaTime;
                                                    this.transform.Rotate(new Vector3(0, 0, 90), 60 * Time.deltaTime);
                                                }

                                                //Cannons[0].transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);

                                                //CrossHair_Icon.transform.position += new Vector3(1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                                //CrossHair_Icon.transform.position += new Vector3(1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                            }

                                            if (Input.GetKey(KeyCode.DownArrow))
                                            {
                                                if (TargetDis <= 150.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 7 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                }

                                                // CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, 1) * normalMoveSpeed * Time.deltaTime;
                                            }
                                            if (Input.GetKey(KeyCode.UpArrow))
                                            {
                                                if (TargetDis >= 36.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 7 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                    //CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, -1) * normalMoveSpeed * Time.deltaTime;
                                                }

                                            }



                                            //print(this.transform.rotation.eulerAngles);
                                            //print(TargetDis);
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

                                    case CannonNumber.Fourth:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                            Cannon_Indicator.SetActive(false);
                                        }
                                        break;

                                    default:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                            Cannon_Indicator.SetActive(false);
                                        }
                                        break;
                                }
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                switch (MyCannonNumber)
                                {
                                    case CannonNumber.First:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Second:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Third:
                                        {
                                            SelectLight.enabled = true;
                                            CrossHair_Icon.gameObject.SetActive(true);

                                            // 게임 패드 작업

                                            if (Input.GetAxisRaw("P2_360_R_RightStick") >= 0.5f || Input.GetKeyDown(KeyCode.RightArrow))
                                            {

                                                Debug.Log("RightStick!");

                                                if (RotationPossibleValue <= RotationPossibleMax)
                                                {
                                                    RotationPossibleValue -= 42.0f * Time.deltaTime;
                                                    this.transform.Rotate(new Vector3(0, 0, -90), 60 * Time.deltaTime);
                                                }

                                            }

                                            if (Input.GetAxisRaw("P2_360_R_RightStick") <= -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                                            {

                                                Debug.Log("LeftStick!");

                                                if (RotationPossibleValue >= RotationPossibleMin)
                                                {
                                                    RotationPossibleValue += 42.0f * Time.deltaTime;
                                                    this.transform.Rotate(new Vector3(0, 0, 90), 60 * Time.deltaTime);
                                                }
                                            }

                                            if (Input.GetAxisRaw("P2_360_L_UpStick") <= -0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                                            {

                                                //Debug.Log("UpStick!");

                                                if (TargetDis >= 36.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 7 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                    //CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, -1) * normalMoveSpeed * Time.deltaTime;
                                                }

                                            }

                                            if (Input.GetAxisRaw("P2_360_L_UpStick") >= 0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                                            {

                                                //Debug.Log("DownStick!");

                                                if (TargetDis <= 150.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 7 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                }
                                            }

                                            //rotationY = Mathf.Clamp(rotationY, -90, 90);

                                            //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                                            //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

                                        }
                                        break;

                                    case CannonNumber.Fourth:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    default:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;
                                }
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
