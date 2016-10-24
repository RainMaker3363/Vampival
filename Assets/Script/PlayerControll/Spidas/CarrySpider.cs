using UnityEngine;
using System.Collections;

public class CarrySpider : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Quaternion Rotate;
    private Vector3 Dir;
    private int layermask;
    private RaycastHit hit;

    public float normalMoveSpeed = 7;
    public float FastMoveSpeed = 40;

    public GameObject SpiderRayChecker;
    public GameObject SpiderSpot;
    public GameObject Look;

	// Use this for initialization
	void Start () {
        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        SpiderSpot.SetActive(true);

        layermask = (1<<LayerMask.NameToLayer("LayCastIn"));//(-1) - (1 << 9) | (1 << 10) | (1 << 12) | (1 << 15);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("CarryPlayer"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("CarryPlayer"), true);
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

                                    }
                                }

                                if (Input.GetKey(KeyCode.I))
                                {

                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position += Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(-0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    //this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
                                    transform.Translate(new Vector3(0, 0, 3.25f) * normalMoveSpeed * Time.deltaTime);
                                    
                                }
                                if (Input.GetKey(KeyCode.K))
                                {
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position -= Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                    transform.Translate(new Vector3(0, 0, -3.25f) * normalMoveSpeed * Time.deltaTime);
                                    
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

                                    }
                                }

                                //if (Input.GetAxisRaw("P1_360_RightStick") == 1)
                                //{

                                //    Debug.Log("RightStick!");

                                //    //rotationX +=  cameraSensitivity * Time.deltaTime;
                                //    transform.position += transform.right * FastMoveSpeed * Time.deltaTime;
                                //}

                                //if (Input.GetAxisRaw("P1_360_RightStick") == -1)
                                //{


                                //    Debug.Log("LeftStick!");


                                //    //rotationX -= cameraSensitivity * Time.deltaTime;
                                //    //transform.position -= transform.forward * normalMoveSpeed * Time.deltaTime;
                                //    transform.position -= transform.right * FastMoveSpeed * Time.deltaTime;
                                //}

                                if (Input.GetAxisRaw("P3_360_UpStick") == -1)
                                {

                                    Debug.Log("UpStick!");

                                    transform.Translate(new Vector3(0, 0, 2.5f) * normalMoveSpeed * Time.deltaTime);
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position += Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(-0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;

                                }

                                if (Input.GetAxisRaw("P3_360_UpStick") == 1)
                                {

                                    Debug.Log("DownStick!");

                                    transform.Translate(new Vector3(0, 0, -2.5f) * normalMoveSpeed * Time.deltaTime);
                                    //Dir = (Look.transform.position - this.transform.position).normalized;
                                    //transform.position -= Dir * normalMoveSpeed * Time.deltaTime;

                                    //transform.position += new Vector3(0.05f, hit.normal.y, 0) * normalMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P3_360_LeftThumbStick") == 1)
                                {

                                    print("Right");

                                    this.transform.Rotate(new Vector3(0, -180, 0), -140 * Time.deltaTime);
                                    //this.transform.Rotate(new Vector3(0, -180, 0), -110 * Time.deltaTime);
                                    
                                }

                                if (Input.GetAxisRaw("P3_360_LeftThumbStick") == -1)
                                {
                                    print("Left");
                                    this.transform.Rotate(new Vector3(0, -180, 0), 140 * Time.deltaTime);
                                    //this.transform.Rotate(new Vector3(0, -180, 0), 110 * Time.deltaTime);
                                }

                                //if (Input.GetAxisRaw("P1_360_UpThumbStick") == 1)
                                //{
                                //    print("down");
                                //}

                                //if (Input.GetAxisRaw("P1_360_UpThumbStick") == -1)
                                //{
                                //    print("Up");

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CannonBall")
        {
            print("Friendly Fire!!");
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
}
