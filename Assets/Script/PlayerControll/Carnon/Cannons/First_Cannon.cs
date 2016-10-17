﻿using UnityEngine;
using System.Collections;

public class First_Cannon : MonoBehaviour {
    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private float TargetDis;
    private CannonNumber MyCannonNumber;

    //private RaycastHit hit;

    public GameObject Dummy;
    public GameObject CrossHair_Icon;
    public GameObject[] Cannons = new GameObject[1];

    public Light SelectLight;

    // Use this for initialization
    void Start()
    {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        MyCannonNumber = GameManager.CannonControl_Number;

        SelectLight.enabled = false;
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
                                switch (MyCannonNumber)
                                {
                                    case CannonNumber.First:
                                        {
                                            SelectLight.enabled = true;
                                            CrossHair_Icon.gameObject.SetActive(true);

                                            //Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 100);
                                            //Vector3 direction = new Vector3(hit.normal.x, hit.normal.y * 1, hit.normal.z);

                                            // 마우스 작업

                                            TargetDis = Vector3.Distance(CrossHair_Icon.transform.position, Cannons[0].transform.position);

                                            //this.transform.LookAt(new Vector3(CrossHair_Icon.transform.position.x, 0, CrossHair_Icon.transform.position.z));

                                            if (Input.GetKey(KeyCode.LeftArrow))
                                            {

                                                this.transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);
                                                //Cannons[0].transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);

                                                //CrossHair_Icon.transform.position += new Vector3(-1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                                //CrossHair_Icon.transform.position += new Vector3(-1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                            }
                                            if (Input.GetKey(KeyCode.RightArrow))
                                            {
                                                this.transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);
                                                //Cannons[0].transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);

                                                //CrossHair_Icon.transform.position += new Vector3(1.8f, 0, 0) * normalMoveSpeed * Time.deltaTime;
                                                //CrossHair_Icon.transform.position += new Vector3(1, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                            }

                                            if (Input.GetKey(KeyCode.DownArrow))
                                            {

                                                // BackUp = 73.0f
                                                if (TargetDis <= 85.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 30 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                }

                                                // CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, 1) * normalMoveSpeed * Time.deltaTime;
                                            }
                                            if (Input.GetKey(KeyCode.UpArrow))
                                            {
                                                if (TargetDis >= 45.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 30 * Time.deltaTime);

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

                                    case CannonNumber.Second:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Third:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Fourth:
                                        {
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
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
                                            SelectLight.enabled = true;
                                            CrossHair_Icon.gameObject.SetActive(true);

                                            // 게임 패드 작업

                                            if (Input.GetAxisRaw("P2_360_RightStick") == 1)
                                            {

                                                //Debug.Log("RightStick!");

                                                this.transform.Rotate(new Vector3(0, 0, 90), 35 * Time.deltaTime);
                                            }

                                            if (Input.GetAxisRaw("P2_360_RightStick") == -1)
                                            {

                                                //Debug.Log("LeftStick!");

                                                this.transform.Rotate(new Vector3(0, 0, -90), 35 * Time.deltaTime);
                                            }

                                            if (Input.GetAxisRaw("P2_360_UpStick") == -1)
                                            {

                                                //Debug.Log("UpStick!");

                                                if (TargetDis >= 36.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(90, 0, 0), 30 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, -2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                    //CrossHair_Icon.transform.position += new Vector3(0, hit.normal.y, -1) * normalMoveSpeed * Time.deltaTime;
                                                }

                                            }

                                            if (Input.GetAxisRaw("P2_360_UpStick") == 1)
                                            {

                                                //Debug.Log("DownStick!");

                                                if (TargetDis <= 85.0f)
                                                {
                                                    Cannons[0].transform.Rotate(new Vector3(-90, 0, 0), 30 * Time.deltaTime);

                                                    CrossHair_Icon.transform.Translate(new Vector3(0, 2.5f, 0) * normalMoveSpeed * Time.deltaTime);
                                                }
                                            }

                                            //rotationY = Mathf.Clamp(rotationY, -90, 90);

                                            //transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                                            //transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

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
                                            SelectLight.enabled = false;
                                            CrossHair_Icon.gameObject.SetActive(false);
                                        }
                                        break;

                                    case CannonNumber.Fourth:
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
