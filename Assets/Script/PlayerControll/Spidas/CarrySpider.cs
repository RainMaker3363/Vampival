using UnityEngine;
using System.Collections;

public class CarrySpider : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Animator ani;

    private Quaternion Rotate;
    private Vector3 Dir;
    private Vector3 MyScale;
    private Vector3 PowerUpScale;
    private int layermask;
    private int ObjectLayerMask;
    private RaycastHit hit;

    public float normalMoveSpeed = 7;
    public float FastMoveSpeed = 40;

    // 인디케이터 설정 부분
    public GameObject Spider_Indicator;
    private Vector3 targetPosOnScreen;
    private Vector3 center;
    private float angle;
    private float coef;
    private int edgeLine;
    private float degreeRange;

    private bool TrapSetOn;

    public GameObject SpiderRayChecker;
    public GameObject SpiderSpot;
    //public GameObject Look;

    public GameObject Berserk_Particle;

    public GameObject ObjectCheckers;
    public GameObject StartObjectChecker;
    public GameObject EndObjectChecker;

    private bool FrontRotationOn;
    private bool BackRotationOn;

    private RaycastHit Objecthit;

    public GameObject[] Spider_Web_Trap;
    private int NowWebTrapStack;
    private float WebTrapCost;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        if (ani == null)
        {
            ani = GetComponent<Animator>();
        }

        SpiderSpot.SetActive(true);

        layermask = (1 << LayerMask.NameToLayer("LayCastIn"));//(-1) - (1 << 9) | (1 << 10) | (1 << 12) | (1 << 15);
        ObjectLayerMask = (((1 << LayerMask.NameToLayer("LayCastIn")) | (1 << LayerMask.NameToLayer("LayObjectCheck"))));
        
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("CarryPlayer"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("CarryPlayer"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("CarryPlayer"), true);

        MyScale = Vector3.zero;
        PowerUpScale = Vector3.zero;

        MyScale = this.gameObject.transform.localScale;
        PowerUpScale = new Vector3(MyScale.x + 1.0f, MyScale.y + 1.0f, MyScale.z + 1.0f);
        
        // 광폭화 이펙트 활성화 여부
        Berserk_Particle.gameObject.SetActive(false);

        // 함정 설치 가능 여부
        TrapSetOn = true;

        // 현재 함정의 수
        NowWebTrapStack = 0;
        WebTrapCost = 5.0f;

        for (int i = 0; i < Spider_Web_Trap.Length; i++)
        {
            Spider_Web_Trap[i].SetActive(false);
        }

        FrontRotationOn = true;
        BackRotationOn = false;

        Spider_Indicator.SetActive(false);
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

                                //Debug.DrawRay(EndObjectChecker.transform.position, (StartObjectChecker.transform.position - EndObjectChecker.transform.position).normalized * 6.0f, Color.cyan);

                                if (Input.GetKey(KeyCode.I))
                                {

                                    if (FrontRotationOn == false)
                                    {
                                        ObjectCheckers.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
                                        BackRotationOn = false;
                                        FrontRotationOn = true;
                                    }

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    if (!Physics.Raycast(EndObjectChecker.transform.position, (StartObjectChecker.transform.position - EndObjectChecker.transform.position).normalized, out Objecthit, 6.0f, ObjectLayerMask))
                                    {
  

                                        transform.Translate(new Vector3(0, 0, 5.0f) * normalMoveSpeed * Time.deltaTime);
                                    }
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position += Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(-0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    //this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);

                                    
                                }
                                else if (Input.GetKeyUp(KeyCode.I))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", false);
                                    }
                                }

                                if (Input.GetKey(KeyCode.K))
                                {
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position -= Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    if (BackRotationOn == false)
                                    {
                                        ObjectCheckers.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
                                        BackRotationOn = true;
                                        FrontRotationOn = false;
                                    }

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    if (!Physics.Raycast(EndObjectChecker.transform.position, (StartObjectChecker.transform.position - EndObjectChecker.transform.position).normalized, out Objecthit, 10.0f, ObjectLayerMask))
                                    {


                                        transform.Translate(new Vector3(0, 0, -5.0f) * normalMoveSpeed * Time.deltaTime);
                                    }
                                    
                                    
                                }
                                else if(Input.GetKeyUp(KeyCode.K))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", false);
                                    }
                                }

                                if (Input.GetKey(KeyCode.J))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    this.transform.Rotate(new Vector3(0, -180, 0), 160 * Time.deltaTime);
                                    //transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                }
                                else if(Input.GetKeyUp(KeyCode.J))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", false);
                                    }
                                }

                                if (Input.GetKey(KeyCode.L))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }
                                    this.transform.Rotate(new Vector3(0, -180, 0), -160 * Time.deltaTime);
                                    //transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
                                }
                                else if (Input.GetKeyUp(KeyCode.L))
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", false);
                                    }
                                }
                                //if (Input.GetKeyDown(KeyCode.H))
                                //{
                                //    ConsumeOn = true;
                                //}
                                targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

                                // 화면 밖 처리
                                if ((targetPosOnScreen.x > Screen.width || targetPosOnScreen.x < 0 || targetPosOnScreen.y > Screen.height || targetPosOnScreen.y < 0))
                                {

                                    Spider_Indicator.gameObject.SetActive(true);


                                    center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

                                    angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;

                                    if (Screen.width > Screen.height)
                                        coef = Screen.width / Screen.height;
                                    else
                                        coef = Screen.height / Screen.width;

                                    degreeRange = 360f / (coef + 1);


                                    if (angle < 0)
                                        angle = angle + 360;

                                    if (angle < degreeRange / 4f)
                                        edgeLine = 0;
                                    else if (angle < 180 - degreeRange / 4f)
                                        edgeLine = 1;
                                    else if (angle < 180 + degreeRange / 4f)
                                        edgeLine = 2;
                                    else if (angle < 360 - degreeRange / 4f)
                                        edgeLine = 3;
                                    else
                                        edgeLine = 0;



                                    switch (edgeLine)
                                    {
                                        case 0:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-30, -30, 10));
                                                //Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 1:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(30, -30, 10));
                                                //Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 2:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(40, 30, 10));
                                                //Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 3:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-40, 30, 10));
                                                //Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;
                                    }


                                }
                                else
                                {
                                    Spider_Indicator.gameObject.SetActive(false);
                                }
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

                                if (Input.GetAxisRaw("P3_360_L_RightStick") == 0 && Input.GetAxisRaw("P3_360_R_UpStick") == 0)
                                {
                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", false);
                                    }
                                }


                                if (Input.GetAxisRaw("P3_360_L_RightStick") >= 0.5f)
                                {

                                    //Debug.Log("RightStick!");

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    this.transform.Rotate(new Vector3(0, 180, 0), 160 * Time.deltaTime);
                                }

                                if (Input.GetAxisRaw("P3_360_L_RightStick") <= -0.5f)
                                {

                                    //Debug.Log("LeftStick!");

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    this.transform.Rotate(new Vector3(0, -180, 0), 160 * Time.deltaTime);
                                }
                                
                                if (Input.GetAxisRaw("P3_360_R_UpStick") <= -0.5f)
                                {

                                    //Debug.Log("UpStick!");

                                    if (FrontRotationOn == false)
                                    {
                                        ObjectCheckers.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
                                        BackRotationOn = false;
                                        FrontRotationOn = true;
                                    }

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    if (!Physics.Raycast(EndObjectChecker.transform.position, (StartObjectChecker.transform.position - EndObjectChecker.transform.position).normalized, out Objecthit, 10.0f, ObjectLayerMask))
                                    {
                                        transform.Translate(new Vector3(0, 0, 5.0f) * normalMoveSpeed * Time.deltaTime);
                                    }

                                }

                                if (Input.GetAxisRaw("P3_360_R_UpStick") >= 0.5f)
                                {

                                    //Debug.Log("DownStick!");

                                    if (BackRotationOn == false)
                                    {
                                        ObjectCheckers.transform.Rotate(new Vector3(180.0f, 0.0f, 0.0f));
                                        BackRotationOn = true;
                                        FrontRotationOn = false;
                                    }

                                    if (ani != null)
                                    {
                                        ani.SetBool("Walk", true);
                                    }

                                    if (!Physics.Raycast(EndObjectChecker.transform.position, (StartObjectChecker.transform.position - EndObjectChecker.transform.position).normalized, out Objecthit, 10.0f, ObjectLayerMask))
                                    {
                                        transform.Translate(new Vector3(0, 0, -5.0f) * normalMoveSpeed * Time.deltaTime);
                                    }
                                }


                                targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

                                // 화면 밖 처리
                                if ((targetPosOnScreen.x > Screen.width || targetPosOnScreen.x < 0 || targetPosOnScreen.y > Screen.height || targetPosOnScreen.y < 0))
                                {

                                    Spider_Indicator.gameObject.SetActive(true);


                                    center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

                                    angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;

                                    if (Screen.width > Screen.height)
                                        coef = Screen.width / Screen.height;
                                    else
                                        coef = Screen.height / Screen.width;

                                    degreeRange = 360f / (coef + 1);


                                    if (angle < 0)
                                        angle = angle + 360;

                                    if (angle < degreeRange / 4f)
                                        edgeLine = 0;
                                    else if (angle < 180 - degreeRange / 4f)
                                        edgeLine = 1;
                                    else if (angle < 180 + degreeRange / 4f)
                                        edgeLine = 2;
                                    else if (angle < 360 - degreeRange / 4f)
                                        edgeLine = 3;
                                    else
                                        edgeLine = 0;



                                    switch (edgeLine)
                                    {
                                        case 0:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-30, -30, 10));
                                                Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 1:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(30, -30, 10));
                                                Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 2:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(40, 30, 10));
                                                Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 3:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                Spider_Indicator.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-40, 30, 10));
                                                Spider_Indicator.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;
                                    }


                                }
                                else
                                {
                                    Spider_Indicator.gameObject.SetActive(false);
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

    // 인디케이터 연산 부분
    Vector3 intersect(int edgeLine, Vector3 line2point1, Vector3 line2point2)
    {
        float[] A1 = { -Screen.height, 0, Screen.height, 0 };
        float[] B1 = { 0, -Screen.width, 0, Screen.width };
        float[] C1 = { -Screen.width * Screen.height, -Screen.width * Screen.height, 0, 0 };

        float A2 = line2point2.y - line2point1.y;
        float B2 = line2point1.x - line2point2.x;
        float C2 = A2 * line2point1.x + B2 * line2point1.y;

        float det = A1[edgeLine] * B2 - A2 * B1[edgeLine];

        return new Vector3((B2 * C1[edgeLine] - B1[edgeLine] * C2) / det, (A1[edgeLine] * C2 - A2 * C1[edgeLine]) / det, 0);
    }

    // 스피다스 버프 효과
    IEnumerator PowerUp(int idx)
    {
        GameManager.Spidas_PowerUp_On = true;

        Berserk_Particle.gameObject.SetActive(true);
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

        yield return new WaitForSeconds(9.0f);

        GameManager.Spidas_PowerUp_On = false;

        Berserk_Particle.gameObject.SetActive(false);
        this.gameObject.transform.localScale = MyScale;
        normalMoveSpeed = 7.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("BuffCannonBall") == true)
        {
            //print("Power Up!!");

            GameManager.Spidas_PowerUp_On = false;
            StopCoroutine(PowerUp(1));

            StartCoroutine(PowerUp(1));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("UI_Document") == true)
        {
            other.gameObject.SetActive(false);

            if(other.gameObject.name.Equals("Document_SonicWave"))
            {
                GameManager.Elizabat_SonicWave_Unlock = true;
                UIManager.Document_Number = 0;
            }
            else if(other.gameObject.name.Equals("Document_Eclipse"))
            {
                GameManager.Elizabat_Eclipse_Unlock = true;
                UIManager.Document_Number = 1;
            }
            else if (other.gameObject.name.Equals("Document_WildFire"))
            {
                GameManager.Elizabat_Decent_Unlock = true;
                UIManager.Document_Number = 2;
            }
            else if (other.gameObject.name.Equals("Document_Swarm"))
            {
                GameManager.Elizabat_Swarm_Unlock = true;
                UIManager.Document_Number = 3;
            }


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
