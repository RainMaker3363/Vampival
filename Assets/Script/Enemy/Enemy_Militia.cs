using UnityEngine;
using System.Collections;

public class Enemy_Militia : MonoBehaviour {

    NavMeshAgent Agent;

    //private Rigidbody rigid;

    public GameObject[] Corpse;
    public GameObject Target;
    public GameObject Spidas;

    public GameObject EnemySpot;
    public GameObject EnemyArrow;

    private EnemyState enemystate;
    private GameState Gamestate;

    private Vector3 StartPos = Vector3.zero;
    private Vector3 targetPosOnScreen;
    private Vector3 center;
    private float angle;
    private float coef;
    private int edgeLine;
    private float degreeRange;

    // 현재 남아있는 시체 수
    private int NowCorpseStack;
    
    // 공포도 체크
    private bool FearMeterCheck;

    // 사망 체크
    private bool DeathCheck;
    
    // 스피다스 체크
    private float SpidasDistance;

    private bool CameraMarkingOn;

    public int HP;
    public int AttackPoint;
    public float Speed;
    

    void Awake()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("Projectile"), true);
        
        //rigid = GetComponent<Rigidbody>();

        EnemySpot.gameObject.SetActive(false);
        EnemyArrow.gameObject.SetActive(false);

        if (Target == null || Spidas == null)
        {
            Target = GameObject.FindWithTag("AttackPoint");
            Spidas = GameObject.FindWithTag("Spidas");
        }



        enemystate = EnemyState.Run;
        Gamestate = GameManager.Gamestate;

        HP = 10;
        AttackPoint = 10;
        NowCorpseStack = 0;
        DeathCheck = false;
        FearMeterCheck = false;
        
        CameraMarkingOn = false;
        //Instantiate(Corpse, this.transform.position, Quaternion.identity);

        //Agent.enabled = false;

        //Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);

        if(StartPos == Vector3.zero)
        {
            // 현재 시작 값
            StartPos = this.transform.position;
        }


        if (Agent == null)
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Agent.enabled = true;

            Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
        }


        //print("StartPos : " + StartPos);
        //print("Agent.destination : " + Agent.destination);
        //print("Active : " + this.enabled);

        for (int i = 0; i < Corpse.Length; i++ )
        {
            Corpse[i].SetActive(false);
        }
            
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(Target == null || Spidas == null)
        {
            Target = GameObject.FindWithTag("AttackPoint");
            Spidas = GameObject.FindWithTag("Spidas");
        }


        HP = 10;
        AttackPoint = 10;
        DeathCheck = false;
        FearMeterCheck = false;
        CameraMarkingOn = false;

        EnemySpot.gameObject.SetActive(false);
        EnemyArrow.gameObject.SetActive(false);

        enemystate = EnemyState.Run;
        Gamestate = GameManager.Gamestate;

        this.transform.position = StartPos;
        

        if (Agent == null)
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Agent.enabled = true;

            Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
        }


        //print("StartPos : " + StartPos);
        //print("Agent.destination : " + Agent.destination);
        //print("Active : " + this.enabled);

        for (int i = 0; i < Corpse.Length; i++)
        {
            if (Corpse[i].gameObject.activeSelf == false)
            {
                Corpse[i].SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Gamestate = GameManager.Gamestate;

        switch (Gamestate)
        {
            case GameState.GameIntro:
                {

                }
                break;

            case GameState.GamePause:
                {
                    Agent.enabled = false;

                    EnemySpot.gameObject.SetActive(false);
                    EnemyArrow.gameObject.SetActive(false);
                }
                break;

            case GameState.GameStart:
                {
                    switch(enemystate)
                    {
                        case EnemyState.Run:
                            {

                                //SpidasDistance = Vector3.Distance(this.transform.position, Spidas.transform.position);

                                //if (SpidasDistance <= 210.0f)
                                //{
                                //    Agent.destination = new Vector3(Spidas.transform.position.x, Spidas.transform.position.y, Spidas.transform.position.z);
                                //    enemystate = EnemyState.Detect;

                                //}


                                //print("Distance : " + SpidasDistance);

                                if (HP <= 0 && !DeathCheck)
                                {
                                    DeathCheck = true;
                                    //Instantiate(Corpse, this.transform.position, Quaternion.identity);
                                    Agent.enabled = false;

                                    if (Corpse[NowCorpseStack].gameObject.activeSelf == false)
                                    {
                                        Corpse[NowCorpseStack].transform.position = this.transform.position;
                                        Corpse[NowCorpseStack].gameObject.SetActive(true);

                                    }

                                    if (NowCorpseStack >= (Corpse.Length - 1))
                                    {
                                        NowCorpseStack = 0;
                                    }
                                    else
                                    {
                                        NowCorpseStack++;
                                    }

                                    this.gameObject.SetActive(false);
                                    
                                    //Destroy(this.gameObject);
                                }

                                if(Agent.enabled == false && this.gameObject.activeSelf == true)
                                {
                                    Agent.enabled = true;
                                    Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
                                }

                                //print(Controller.isGrounded);
                                targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

                                // 화면 밖 처리
                                if ((targetPosOnScreen.x > Screen.width || targetPosOnScreen.x < 0 || targetPosOnScreen.y > Screen.height || targetPosOnScreen.y < 0))
                                {
                                    if (GameManager.Elizabat_SonicWave_On == false)
                                    {
                                        EnemySpot.gameObject.SetActive(false);
                                        EnemyArrow.gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        if (CameraMarkingOn)
                                        {
                                            EnemySpot.gameObject.SetActive(true);
                                        }
                                            

                                        EnemyArrow.gameObject.SetActive(false);
                                    }

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



                                    switch(edgeLine)
                                    {
                                        case 0:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-30, -30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 1:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(30, -30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 2:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(40, 30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 3:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-40, 30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;
                                    }

                                    //if (targetPosOnScreen.x > Screen.width)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-25, 0, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.x < 0)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(25, 0, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.y > Screen.height)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, -25, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.y < 0)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 25, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                }
                                else
                                {
                                    CameraMarkingOn = true;

                                    if (GameManager.Elizabat_SonicWave_On == false)
                                    {
                                        EnemySpot.gameObject.SetActive(true);
                                        EnemyArrow.gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        if (CameraMarkingOn)
                                        {
                                            EnemySpot.gameObject.SetActive(true);
                                        }
                                            
                                        EnemyArrow.gameObject.SetActive(false);
                                    }

                                }

                                //Controller.Move(new Vector3(1, 0, 1) * Time.deltaTime);
                            }
                            break;

                        case EnemyState.Stun:
                            {

                            }
                            break;

                        case EnemyState.Detect:
                            {

                                if (SpidasDistance > 230.0f)
                                {
                                    Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
                                    enemystate = EnemyState.Run;
                                }

                                if (HP <= 0)
                                {
                                    //Instantiate(Corpse, this.transform.position, Quaternion.identity);

                                    Corpse[NowCorpseStack].transform.position = this.transform.position;
                                    Corpse[NowCorpseStack].gameObject.SetActive(true);

                                    Agent.enabled = false;
                                    //Destroy(this.gameObject);
                                }

                                //print(Controller.isGrounded);
                                //SpidasDistance = Vector3.Distance(this.transform.position, Spidas.transform.position);

                                Agent.destination = new Vector3(Spidas.transform.position.x, Spidas.transform.position.y, Spidas.transform.position.z);

                                //print("Distance : " + SpidasDistance);

                                targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

                                // 화면 밖 처리
                                if ((targetPosOnScreen.x > Screen.width || targetPosOnScreen.x < 0 || targetPosOnScreen.y > Screen.height || targetPosOnScreen.y < 0))
                                {
                                    if (GameManager.Elizabat_SonicWave_On == false)
                                    {
                                        EnemySpot.gameObject.SetActive(false);
                                        EnemyArrow.gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        EnemySpot.gameObject.SetActive(true);
                                        EnemyArrow.gameObject.SetActive(false);
                                    }

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
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-30, -30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 1:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(30, -30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 2:
                                            {
                                               // print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(40, 30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;

                                        case 3:
                                            {
                                                //print("edgeLine = " + edgeLine);
                                                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-40, 30, 10));
                                                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                            }
                                            break;
                                    }

                                    //if (targetPosOnScreen.x > Screen.width)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-25, 0, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.x < 0)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(25, 0, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.y > Screen.height)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, -25, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                    //else if (targetPosOnScreen.y < 0)
                                    //{
                                    //    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 25, 10));
                                    //    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
                                    //}
                                }
                                else
                                {
                                    if (GameManager.Elizabat_SonicWave_On == false)
                                    {
                                        EnemySpot.gameObject.SetActive(true);
                                        EnemyArrow.gameObject.SetActive(false);
                                    }
                                    else
                                    {
                                        EnemySpot.gameObject.SetActive(true);
                                        EnemyArrow.gameObject.SetActive(false);
                                    }

                                }

                                

                            }
                            break;
                    }

                  
                }
                break;

            case GameState.GameVictory:
                {
                    Agent.enabled = false;

                    EnemySpot.gameObject.SetActive(false);
                    EnemyArrow.gameObject.SetActive(false);
                }
                break;

            case GameState.GameEnd:
                {
                    Agent.enabled = false;

                    EnemySpot.gameObject.SetActive(false);
                    EnemyArrow.gameObject.SetActive(false);
                }
                break;
        }

    }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("AttackPoint") == true)
        {

            GameManager.Capture_Parameter += 1;
            Agent.enabled = false;

            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);

        }

        if (other.transform.tag.Equals("Elizabat") == true)
        {
            print("Elizabat Attack!!");
        }

        if(other.transform.tag.Equals("CorpseChecker") == true)
        {
            //if(FearMeterCheck == false)
            //{
            //    FearMeterCheck = true;

                

            //    GameManager.Fear_Parameter += 1;

            //    print("FearMeter : " + GameManager.Fear_Parameter);
            //}


            GameManager.Fear_Parameter += 3.0f;

            print("FearMeter : " + GameManager.Fear_Parameter);
        }

        if(other.transform.tag.Equals("SpidasChecker") == true)
        {
            

            GameManager.Fear_Parameter += 0.1f;

            print("FearMeter : " + GameManager.Fear_Parameter);
        }

    }

    // 트리거를 빠져 나갔을때..
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("SpidasChecker") == true)
        {

            //GameManager.Fear_Parameter -= 1;

            //print("FearMeter : " + GameManager.Fear_Parameter);
        }

        if (other.transform.tag.Equals("CorpseChecker") == true)
        {
            //if (FearMeterCheck == false)
            //{
            //    FearMeterCheck = true;



            //    GameManager.Fear_Parameter += 1;

            //    print("FearMeter : " + GameManager.Fear_Parameter);
            //}

            //if(GameManager.Fear_Parameter <= 1)
            //{
            //    GameManager.Fear_Parameter -= 0;
            //}
            //else
            //{
            //    GameManager.Fear_Parameter -= 1;
            //}

            //print("FearMeter : " + GameManager.Fear_Parameter);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag.Equals("CannonBall") == true)
        {
            //print("Hit !!");

            if (HP > 0)
            {
                HP -= 10;
            }
            else if (HP <= 0)
            {
                GameManager.Fear_Parameter += 1;
                Agent.enabled = false;

                //Instantiate(Corpse, this.transform.position, Quaternion.identity);
                if (Corpse[NowCorpseStack].gameObject.activeSelf == false)
                {
                    Corpse[NowCorpseStack].transform.position = this.transform.position;
                    Corpse[NowCorpseStack].gameObject.SetActive(true);

                }

                if (NowCorpseStack >= (Corpse.Length - 1))
                {
                    NowCorpseStack = 0;
                }
                else
                {
                    NowCorpseStack++;
                }

                this.gameObject.SetActive(false);

                //Destroy(this.gameObject);
            }

        }

        if (collision.transform.tag.Equals("EnemyCorpse") == true)
        {
            print("Balling !!");

            if (HP > 0)
            {
                HP -= 10;
            }
            else if (HP <= 0)
            {
                //Instantiate(Corpse, this.transform.position, Quaternion.identity);

                Agent.enabled = false;

                if (Corpse[NowCorpseStack].gameObject.activeSelf == false)
                {
                    Corpse[NowCorpseStack].transform.position = this.transform.position;
                    Corpse[NowCorpseStack].gameObject.SetActive(true);

                }

                if (NowCorpseStack >= (Corpse.Length - 1))
                {
                    NowCorpseStack = 0;
                }
                else
                {
                    NowCorpseStack++;
                }

                this.gameObject.SetActive(false);

                //Destroy(this.gameObject);
            }
        }
    }
}
