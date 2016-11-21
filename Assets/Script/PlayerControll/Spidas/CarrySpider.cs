using UnityEngine;
using System.Collections;

public class CarrySpider : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Quaternion Rotate;
    private Vector3 Dir;
    private Vector3 MyScale;
    private Vector3 PowerUpScale;
    private int layermask;
    private int ObjectLayerMask;
    private RaycastHit hit;

    public float normalMoveSpeed = 7;
    public float FastMoveSpeed = 40;

    private bool TrapSetOn;

    public GameObject SpiderRayChecker;
    public GameObject SpiderSpot;
    //public GameObject Look;

    public GameObject ObjectChecker;
    private RaycastHit Objecthit;

    public GameObject[] Spider_Web_Trap;
    private int NowWebTrapStack;
    private float WebTrapCost;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        SpiderSpot.SetActive(true);

        layermask = (1<<LayerMask.NameToLayer("LayCastIn"));//(-1) - (1 << 9) | (1 << 10) | (1 << 12) | (1 << 15);
        ObjectLayerMask = (1 << LayerMask.NameToLayer("LayObjectCheck"));
        
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("CarryPlayer"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("CarryPlayer"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("CarryPlayer"), true);

        MyScale = Vector3.zero;
        PowerUpScale = Vector3.zero;

        MyScale = this.gameObject.transform.localScale;
        PowerUpScale = new Vector3(MyScale.x + 3.0f, MyScale.y + 3.0f, MyScale.z + 3.0f);

        // 함정 설치 가능 여부
        TrapSetOn = true;

        // 현재 함정의 수
        NowWebTrapStack = 0;
        WebTrapCost = 5.0f;

        for (int i = 0; i < Spider_Web_Trap.Length; i++)
        {
            Spider_Web_Trap[i].SetActive(false);
        }

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

            case GameState.GameStart:
                {
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {

                                // 마우스 작업
                                //print("Mouse");

                                //if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layermask))
                                //{
                                //    print(hit.collider.tag);
                                //}
                                if (Physics.Raycast(SpiderRayChecker.transform.position, (this.transform.position - SpiderRayChecker.transform.position).normalized * 150.0f, out hit, Mathf.Infinity, layermask))
                                {

                                    if (hit.collider.tag.Equals("Ground") == true)
                                    {
                                        //print(hit.point.y);
                                        this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 3.0f, this.transform.position.z);

                                        if (Input.GetKeyDown(KeyCode.Y) && GameManager.Soul_MP_Parameter >= WebTrapCost && TrapSetOn == true)
                                        {
                                            if (Spider_Web_Trap[NowWebTrapStack].gameObject.activeSelf == false)
                                            {
                                                Spider_Web_Trap[NowWebTrapStack].transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);

                                                Spider_Web_Trap[NowWebTrapStack].transform.parent = null;

                                                Spider_Web_Trap[NowWebTrapStack].gameObject.SetActive(true);

                                                GameManager.Soul_MP_Parameter -= WebTrapCost;
                                            }

                                            if (NowWebTrapStack >= (Spider_Web_Trap.Length - 1))
                                            {
                                                NowWebTrapStack = 0;
                                            }
                                            else
                                            {
                                                NowWebTrapStack++;
                                            }
                                        }
                                    }
                                }


                                //if (GameManager.Spidas_PowerUp_On == true)
                                //{
                                //    this.gameObject.transform.localScale = PowerUpScale;
                                //    normalMoveSpeed = 12.0f;
                                //}
                                //else
                                //{
                                //    this.gameObject.transform.localScale = MyScale;
                                //    normalMoveSpeed = 7.0f;
                                //}

                                //Debug.DrawRay(this.transform.position, (ObjectChecker.transform.position - this.transform.position).normalized * 4.5f, Color.cyan);

                                if (Input.GetKey(KeyCode.I))
                                {

                                    if (!Physics.Raycast(this.transform.position, (ObjectChecker.transform.position - this.transform.position).normalized, out Objecthit, 5.0f, ObjectLayerMask))
                                    {
                                        transform.Translate(new Vector3(0, 0, 4.0f) * normalMoveSpeed * Time.deltaTime);
                                    }
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position += Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(-0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    //this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);

                                    
                                }
                                if (Input.GetKey(KeyCode.K))
                                {
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position -= Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    transform.Translate(new Vector3(0, 0, -4.0f) * normalMoveSpeed * Time.deltaTime);
                                    
                                }
                                if (Input.GetKey(KeyCode.J))
                                {
                                    this.transform.Rotate(new Vector3(0, -180, 0), 140 * Time.deltaTime);
                                    //transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                }
                                if (Input.GetKey(KeyCode.L))
                                {
                                    this.transform.Rotate(new Vector3(0, -180, 0), -140 * Time.deltaTime);
                                    //transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                }


                                //if (Input.GetKeyDown(KeyCode.H))
                                //{
                                //    ConsumeOn = true;
                                //}



                                
                                //else if (Input.GetKeyUp(KeyCode.H))
                                //{
                                //    ConsumeOn = false;
                                //}

                                //Rotate = Quaternion.LookRotation(Dir);
                                //this.transform.Rotate(new Vector3(0, -90, 0), 50 * Time.deltaTime);// = //Quaternion.Slerp(this.transform.rotation, Quaternion.EulerAngles(0, 5.0f, 0), 6.0f * Time.deltaTime);
                                //transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);

                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                //print("GamePad 3 On");
                                if (Physics.Raycast(SpiderRayChecker.transform.position, (this.transform.position - SpiderRayChecker.transform.position).normalized * 150.0f, out hit, Mathf.Infinity, layermask))
                                {

                                    if (hit.collider.tag.Equals("Ground") == true)
                                    {
                                        //print(hit.point.y);
                                        this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 3.0f, this.transform.position.z);

                                        if (Input.GetButtonDown("P3_360_BButton") && GameManager.Soul_MP_Parameter >= WebTrapCost && TrapSetOn == true)
                                        {
                                            if (Spider_Web_Trap[NowWebTrapStack].gameObject.activeSelf == false)
                                            {
                                                Spider_Web_Trap[NowWebTrapStack].transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);

                                                Spider_Web_Trap[NowWebTrapStack].transform.parent = null;

                                                Spider_Web_Trap[NowWebTrapStack].gameObject.SetActive(true);

                                                GameManager.Soul_MP_Parameter -= WebTrapCost;
                                            }

                                            if (NowWebTrapStack >= (Spider_Web_Trap.Length - 1))
                                            {
                                                NowWebTrapStack = 0;
                                            }
                                            else
                                            {
                                                NowWebTrapStack++;
                                            }
                                        }
                                    }
                                }

                                if (Input.GetAxisRaw("P3_360_L_RightStick") >= 0.5f)
                                {

                                    Debug.Log("RightStick!");

                                    this.transform.Rotate(new Vector3(0, 180, 0), 140 * Time.deltaTime);
                                }

                                if (Input.GetAxisRaw("P3_360_L_RightStick") <= -0.5f)
                                {

                                    Debug.Log("LeftStick!");

                                    this.transform.Rotate(new Vector3(0, -180, 0), 140 * Time.deltaTime);
                                }

                                if (Input.GetAxisRaw("P3_360_R_UpStick") <= -0.5f)
                                {

                                    Debug.Log("UpStick!");

                                    transform.Translate(new Vector3(0, 0, 2.5f) * normalMoveSpeed * Time.deltaTime);

                                }

                                if (Input.GetAxisRaw("P3_360_R_UpStick") >= 0.5f)
                                {

                                    Debug.Log("DownStick!");

                                    transform.Translate(new Vector3(0, 0, -2.5f) * normalMoveSpeed * Time.deltaTime);
                                }

                                //if (Input.GetButtonDown("P3_360_AButton"))
                                //{
                                //    ConsumeOn = true;
                                //}
                                //else if (Input.GetButtonUp("P3_360_AButton"))
                                //{
                                //    ConsumeOn = false;
                                //}
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

    // 스피다스 버프 효과
    IEnumerator PowerUp(int idx)
    {
        GameManager.Spidas_PowerUp_On = true;

        this.gameObject.transform.localScale = PowerUpScale;
        normalMoveSpeed = 12.0f;

        //if (GameManager.Spidas_PowerUp_On == true)
        //{
        //    this.gameObject.transform.localScale = PowerUpScale;
        //    normalMoveSpeed = 12.0f;
        //}
        //else
        //{
        //    this.gameObject.transform.localScale = MyScale;
        //    normalMoveSpeed = 7.0f;
        //}

        yield return new WaitForSeconds(8.0f);

        GameManager.Spidas_PowerUp_On = false;

        this.gameObject.transform.localScale = MyScale;
        normalMoveSpeed = 7.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("BuffCannonBall") == true)
        {
            print("Power Up!!");

            StopCoroutine(PowerUp(1));

            StartCoroutine(PowerUp(1));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("UI_Document") == true)
        {
            other.gameObject.SetActive(false);

            GameManager.Gamestate = GameState.GamePause;
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("PlayerTrap") == true)
        {
            TrapSetOn = true;

            
        }
    }

    void OnTriggerStay(Collider other)
    {
        //if (other.transform.tag.Equals("Souls") == true)
        //{

        //}

        if (other.transform.tag.Equals("PlayerTrap") == true)
        {
            TrapSetOn = false;

            
        }
    }
}
