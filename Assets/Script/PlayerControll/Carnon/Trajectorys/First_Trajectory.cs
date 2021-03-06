﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class First_Trajectory : MonoBehaviour {

    public LineRenderer lineRenderer;
    public float rotationSpeed = 4.04f;

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private float AimInterPol = 0.0f;

    private bool FireReady = false;
    private float FireTimer;

    private ViewControllMode ViewMode;
    private GameState Gamestate;
    private CannonNumber MyCannonNumber;
    private CannonWeapon MyCannonMode;

    private AudioSource Audio;
    public AudioClip FireSound;

    private Vector3 targetPoint = Vector3.zero;
    private Vector3 center = Vector3.zero;
    private Vector3 theArc = Vector3.zero;

    public GameObject AimTarget;
    public GameObject[] CannonBalls;
    public GameObject[] BuffCannonBalls;
    public ParticleSystem Spark;

    private RaycastHit hit;

    private float CannonCost;
    private int NowCannonIdx;
    private int NowBuffCannonIdx;

    void Awake()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetColors(Color.red, Color.black);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(25);

        AimInterPol = 0.0f;

        FireTimer = 0.0f;

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;
        MyCannonNumber = GameManager.CannonControl_Number;
        MyCannonMode = GameManager.CannonWeapon_Toggle;

        if(Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = FireSound;
        }

        for(int i = 0; i<CannonBalls.Length; i++)
        {
            CannonBalls[i].SetActive(false);
        }

        for (int i = 0; i < BuffCannonBalls.Length; i++)
        {
            BuffCannonBalls[i].SetActive(false);
        }

        CannonCost = 2.0f;//CannonBalls.Length;
        NowCannonIdx = 0;
        NowBuffCannonIdx = 0;

        Spark.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetColors(Color.red, Color.black);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(25);

        AimInterPol = 0.0f;

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        MyCannonNumber = GameManager.CannonControl_Number;
        MyCannonMode = GameManager.CannonWeapon_Toggle;

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = FireSound;
        }

        FireTimer = 0.0f;

        Spark.gameObject.SetActive(false);
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

        MyCannonNumber = GameManager.CannonControl_Number;
        MyCannonMode = GameManager.CannonWeapon_Toggle;
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
                                            lineRenderer.enabled = true;
                                            Spark.gameObject.SetActive(true);

                                            // 마우스 작업

                                            // 발사 주기 체크
                                            if (FireTimer >= 1.25f)
                                            {
                                                FireReady = true;
                                                FireTimer = 0.0f;
                                            }
                                            else
                                            {
                                                FireTimer += Time.deltaTime;
                                            }

                                            //print("NowCannonIdx : " + NowCannonIdx);


                                            
                                            
                                            //Debug.DrawLine(AimLight.transform.position, AimTarget.transform.position, Color.blue);


                                            switch (MyCannonMode)
                                            {
                                                case CannonWeapon.Normal:
                                                    {
                                                        if (Input.GetKeyDown(KeyCode.Space) && FireReady && (GameManager.Soul_MP_Parameter >= CannonCost))
                                                        {
                                                            FireReady = false;

                                                            Audio.Play();

                                                            if (CannonBalls[NowCannonIdx].activeSelf == false)
                                                            {
                                                                //print("Cannon Go!!");

                                                                Spark.Play();
                                                                CannonBalls[NowCannonIdx].gameObject.transform.position = this.transform.position;
                                                                CannonBalls[NowCannonIdx].SetActive(true);
                                                            }

                                                            if (NowCannonIdx >= (CannonBalls.Length - 1))
                                                            {
                                                                NowCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowCannonIdx++;
                                                                //CannonCost--;
                                                            }

                                                            if (GameManager.Soul_MP_Parameter <= 0.0f)
                                                            {
                                                                GameManager.Soul_MP_Parameter = 0.0f;
                                                            }
                                                            else
                                                            {
                                                                GameManager.Soul_MP_Parameter -= CannonCost;
                                                            }
                                                            //private RaycastHit hit;





                                                            //print("CannonBalls.Length : " + CannonBalls.Length);
                                                            //print("CannonCost : " + CannonCost);
                                                            //Instantiate(CannonBall, this.transform.position, Quaternion.identity);
                                                        }
                                                    }
                                                    break;

                                                case CannonWeapon.Buff:
                                                    {
                                                        if (Input.GetKeyDown(KeyCode.Space) && FireReady && GameManager.BuffCannonStack > 0)
                                                        {
                                                            FireReady = false;

                                                            Audio.Play();


                                                            if (BuffCannonBalls[NowBuffCannonIdx].activeSelf == false)
                                                            {
                                                                //print("Cannon Go!!");

                                                                Spark.Play();
                                                                BuffCannonBalls[NowBuffCannonIdx].gameObject.transform.position = this.transform.position;
                                                                BuffCannonBalls[NowBuffCannonIdx].SetActive(true);
                                                            }

                                                            if (NowBuffCannonIdx >= (BuffCannonBalls.Length - 1))
                                                            {
                                                                NowBuffCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowBuffCannonIdx++;
                                                                //CannonCost--;
                                                            }

                                                            GameManager.BuffCannonStack -= 1;
                                                        }
                                                        
                                                    }
                                                    break;
                                            }
                                            

                                            //if (null == Camera.main)
                                            //    return;

                                            // Generate a plane that intersects the transform's position with an upwards normal.
                                            //Plane playerPlane = new Plane(Vector3.up, transform.position);

                                            // Generate a ray from the cursor position
                                            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                            //Ray ray = Camera.main.scree(AimTarget.transform.position);

                                            // Determine the point where the cursor ray intersects the plane.
                                            // This will be the point that the object must look towards to be looking at the mouse.
                                            // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
                                            //   then find the point along that ray that meets that distance.  This will be the point
                                            //   to look at.
                                            //float hitdist = 0.0f;

                                            // If the ray is parallel to the plane, Raycast will return false.
                                            //Vector3 targetPoint = Vector3.zero;

                                            //if (playerPlane.Raycast(ray, out hitdist))
                                            //{

                                            //    // Get the point along the ray that hits the calculated distance.			
                                            //    targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                            //    // Draw the arc trajectory		
                                            //    center = (transform.position + targetPoint) * 0.5f;
                                            //    center.y -= 70.0f;

                                            //    // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                            //    Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                            //    // Smoothly rotate towards the target point.
                                            //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                            //    // shorten the ray if it's hitting some obstacle:
                                            //    //RaycastHit hitInfo;

                                            //    targetPoint = transform.position;

                                            //    //if (Physics.Linecast(transform.position, targetPoint, out hitInfo))
                                            //    //{
                                            //    //    targetPoint = hitInfo.point;

                                            //    //}
                                            //}
                                            //else
                                            //{
                                            //    targetPoint = transform.position;
                                            //}

                                            // Get the point along the ray that hits the calculated distance.			
                                            targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                            // Draw the arc trajectory		
                                            center = (transform.position + targetPoint) * 0.5f;
                                            center.y -= 50.0f + AimInterPol;//18.0f + AimInterPol;

                                            if (Input.GetKey(KeyCode.DownArrow))
                                            {
                                                if (center.y <= -0.2)
                                                    AimInterPol += 1.0f * Time.deltaTime;
                                            }
                                            if (Input.GetKey(KeyCode.UpArrow))
                                            {
                                                if (center.y <= 1)
                                                    AimInterPol -= 1.0f * Time.deltaTime;

                                            }

                                            //print(" center.y  : " + center.y);


                                            // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                            //Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                            // Smoothly rotate towards the target point.

                                            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                            // shorten the ray if it's hitting some obstacle:
                                            //RaycastHit hitInfo;

                                            //targetPoint = transform.position;

                                            Vector3 RelCenter = transform.position - center;
                                            Vector3 aimRelCenter = targetPoint - center;




                                            // Draw the arc line starting from the launcher
                                            for (float index = 0.0f, interval = -0.0417f; interval < 1.0f; )
                                            {
                                                theArc = Vector3.Slerp(RelCenter, aimRelCenter, interval += 0.0417f);
                                                lineRenderer.SetPosition((int)index++, theArc + center);
                                            }
                                        }
                                        break;

                                    case CannonNumber.Second:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    case CannonNumber.Third:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    case CannonNumber.Fourth:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    default:
                                        {
                                            lineRenderer.enabled = false;
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
                                            Spark.gameObject.SetActive(true);
                                            lineRenderer.enabled = true;

                                            // 게임 패드 작업

                                            // 발사 주기 체크
                                            if (FireTimer >= 1.25f)
                                            {
                                                FireReady = true;
                                                FireTimer = 0.0f;
                                            }
                                            else
                                            {
                                                FireTimer += Time.deltaTime;
                                            }

                                            switch (MyCannonMode)
                                            {
                                                case CannonWeapon.Normal:
                                                    {
                                                        if ((Input.GetAxis("P2_360_Trigger") > 0.001 || Input.GetKeyDown(KeyCode.Space)) && FireReady && (GameManager.Soul_MP_Parameter >= CannonCost))
                                                        {

                                                            Debug.Log("Right Trigger!");

                                                            FireReady = false;

                                                            Audio.Play();

                                                            if (CannonBalls[NowCannonIdx].activeSelf == false)
                                                            {
                                                                //print("Cannon Go!!");

                                                                Spark.Play();

                                                                CannonBalls[NowCannonIdx].gameObject.transform.position = this.transform.position;
                                                                CannonBalls[NowCannonIdx].SetActive(true);
                                                            }

                                                            if (NowCannonIdx >= (CannonBalls.Length - 1))
                                                            {
                                                                NowCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowCannonIdx++;
                                                                //CannonCost--;
                                                            }

                                                            if (GameManager.Soul_MP_Parameter <= 0.0f)
                                                            {
                                                                GameManager.Soul_MP_Parameter = 0.0f;
                                                            }
                                                            else
                                                            {
                                                                GameManager.Soul_MP_Parameter -= CannonCost;
                                                            }
                                                            //Instantiate(CannonBall, this.transform.position, Quaternion.identity);
                                                        }

                                                        if ((Input.GetAxis("P2_360_Trigger") < 0 || Input.GetKeyDown(KeyCode.Space)) && FireReady && (GameManager.Soul_MP_Parameter >= CannonCost))
                                                        {

                                                            Debug.Log("Left Trigger!");

                                                            FireReady = false;

                                                            Audio.Play();

                                                            if (CannonBalls[NowCannonIdx].activeSelf == false)
                                                            {
                                                                print("Cannon Go!!");

                                                                CannonBalls[NowCannonIdx].gameObject.transform.position = this.transform.position;
                                                                CannonBalls[NowCannonIdx].SetActive(true);
                                                            }

                                                            if (NowCannonIdx >= (CannonBalls.Length - 1))
                                                            {
                                                                NowCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowCannonIdx++;
                                                                //CannonCost--;
                                                            }
                                                            //Instantiate(CannonBall, this.transform.position, Quaternion.identity);

                                                            if (GameManager.Soul_MP_Parameter <= 0.0f)
                                                            {
                                                                GameManager.Soul_MP_Parameter = 0.0f;
                                                            }
                                                            else
                                                            {
                                                                GameManager.Soul_MP_Parameter -= CannonCost;
                                                            }

                                                        }

                                                    }
                                                    break;

                                                case CannonWeapon.Buff:
                                                    {
                                                        if ((Input.GetAxis("P2_360_Trigger") > 0.001 || Input.GetKeyDown(KeyCode.Space)) && FireReady && (GameManager.BuffCannonStack > 0))
                                                        {

                                                            Debug.Log("Right Trigger!");

                                                            FireReady = false;

                                                            Audio.Play();


                                                            if (BuffCannonBalls[NowBuffCannonIdx].activeSelf == false)
                                                            {
                                                                //print("Cannon Go!!");

                                                                Spark.Play();
                                                                BuffCannonBalls[NowBuffCannonIdx].gameObject.transform.position = this.transform.position;
                                                                BuffCannonBalls[NowBuffCannonIdx].SetActive(true);
                                                            }

                                                            if (NowBuffCannonIdx >= (BuffCannonBalls.Length - 1))
                                                            {
                                                                NowBuffCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowBuffCannonIdx++;
                                                                //CannonCost--;
                                                            }

                                                            GameManager.BuffCannonStack -= 1;
                                                        }

                                                        if ((Input.GetAxis("P2_360_Trigger") < 0 || Input.GetKeyDown(KeyCode.Space)) && FireReady && (GameManager.BuffCannonStack > 0))
                                                        {

                                                            Debug.Log("Left Trigger!");

                                                            FireReady = false;

                                                            Audio.Play();


                                                            if (BuffCannonBalls[NowBuffCannonIdx].activeSelf == false)
                                                            {
                                                                //print("Cannon Go!!");

                                                                Spark.Play();
                                                                BuffCannonBalls[NowBuffCannonIdx].gameObject.transform.position = this.transform.position;
                                                                BuffCannonBalls[NowBuffCannonIdx].SetActive(true);
                                                            }

                                                            if (NowBuffCannonIdx >= (BuffCannonBalls.Length - 1))
                                                            {
                                                                NowBuffCannonIdx = 0;
                                                            }
                                                            else
                                                            {

                                                                NowBuffCannonIdx++;
                                                                //CannonCost--;
                                                            }

                                                            GameManager.BuffCannonStack -= 1;
                                                        }

                                                    }
                                                    break;
                                            }
                                     

                                            // Get the point along the ray that hits the calculated distance.			
                                            targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                            // Draw the arc trajectory		
                                            center = (transform.position + targetPoint) * 0.5f;
                                            center.y -= 50.0f + AimInterPol;

                                            if (Input.GetAxisRaw("P2_360_L_UpStick") >= 0.5f || Input.GetKey(KeyCode.DownArrow))
                                            {

                                                if (center.y <= -0.2)
                                                    AimInterPol += 1.0f * Time.deltaTime;
                                            }

                                            if (Input.GetAxisRaw("P2_360_L_UpStick") <= -0.5f || Input.GetKey(KeyCode.UpArrow))
                                            {

                                                if (center.y <= 1)
                                                    AimInterPol -= 1.0f * Time.deltaTime;
                                            }


                                            //print(" center.y  : " + center.y);


                                            // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                            //Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                            Vector3 RelCenter = transform.position - center;
                                            Vector3 aimRelCenter = targetPoint - center;





                                            // Draw the arc line starting from the launcher
                                            for (float index = 0.0f, interval = -0.0417f; interval < 1.0f; )
                                            {
                                                theArc = Vector3.Slerp(RelCenter, aimRelCenter, interval += 0.0417f);
                                                lineRenderer.SetPosition((int)index++, theArc + center);
                                            }

                                        }
                                        break;

                                    case CannonNumber.Second:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    case CannonNumber.Third:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    case CannonNumber.Fourth:
                                        {
                                            lineRenderer.enabled = false;
                                        }
                                        break;

                                    default:
                                        {
                                            lineRenderer.enabled = false;
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
