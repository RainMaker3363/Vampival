using UnityEngine;
using System.Collections;

public class Enemy_Militia : MonoBehaviour {

    NavMeshAgent Agent;

    //private Rigidbody rigid;

    public GameObject Corpse;
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

    private float SpidasDistance;

    public int HP;
    public int AttackPoint;
    public float Speed;

    void Awake()
    {

        
        //rigid = GetComponent<Rigidbody>();

        EnemySpot.gameObject.SetActive(false);
        EnemyArrow.gameObject.SetActive(false);

        if (Target == null || Spidas == null)
        {
            Target = GameObject.FindWithTag("AttackPoint");
            Spidas = GameObject.FindWithTag("Spidas");
        }

        Corpse.SetActive(false);

        enemystate = EnemyState.Run;
        Gamestate = GameManager.Gamestate;

        HP = 10;
        AttackPoint = 10;
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


        print("StartPos : " + StartPos);
        print("Agent.destination : " + Agent.destination);
        print("Active : " + this.enabled);

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

        EnemySpot.gameObject.SetActive(false);
        EnemyArrow.gameObject.SetActive(false);

        enemystate = EnemyState.Run;
        Gamestate = GameManager.Gamestate;

        this.transform.position = StartPos;
        Corpse.SetActive(false);

        if (Agent == null)
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Agent.enabled = true;

            Agent.destination = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
        }


        print("StartPos : " + StartPos);
        print("Agent.destination : " + Agent.destination);
        print("Active : " + this.enabled);

    }

    // Update is called once per frame
    void Update()
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

                                if (HP <= 0)
                                {
                                    //Instantiate(Corpse, this.transform.position, Quaternion.identity);
                                    Agent.enabled = false;
                                    
                                    Corpse.gameObject.SetActive(true);
                                    this.gameObject.SetActive(false);

                                    
                                    //Destroy(this.gameObject);
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
                                        EnemySpot.gameObject.SetActive(true);
                                        EnemyArrow.gameObject.SetActive(true);
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

                                    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 0, 10));
                                    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);

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
                                        EnemyArrow.gameObject.SetActive(true);
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
                                    Instantiate(Corpse, this.transform.position, Quaternion.identity);

                                    Agent.enabled = false;
                                    Destroy(this.gameObject);
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
                                        EnemyArrow.gameObject.SetActive(true);
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

                                    EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 0, 10));
                                    EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);

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
                                        EnemyArrow.gameObject.SetActive(true);
                                    }

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
                Corpse.gameObject.SetActive(true);
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

                Corpse.gameObject.SetActive(true);
                this.gameObject.SetActive(false);

                //Destroy(this.gameObject);
            }
        }
    }
}
