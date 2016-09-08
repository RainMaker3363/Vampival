using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 StartPoint = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private float _angle = 18.0f;
    private float dist;

    private Vector3 localVelocity;
    private Vector3 globalVelocity;

    private bool IsFire;

    public GameObject AimTarget;
    public GameObject CannonPoint;

	// Use this for initialization
	void Start () {

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        IsFire = false;

        Destroy(gameObject, 3.0f);

        AimTarget = GameObject.FindWithTag("Cannon_CrossHair");
        CannonPoint = GameObject.FindWithTag("Cannon");

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

                                // 마우스 작업

                                // Get the point along the ray that hits the calculated distance.			
                                //ray.GetPoint(hitdist);
                                if (!IsFire)
                                {
                                    targetPoint = AimTarget.transform.position;
                                    StartPoint = CannonPoint.transform.position;

                                    IsFire ^= true;
                                }

                                if (Input.GetKey(KeyCode.X))
                                {
                                    _angle -= 2.0f;
                                }
                                if (Input.GetKey(KeyCode.C))
                                {
                                    _angle += 2.0f;
                                }

                                print(_angle);
                                // distance between target and source
                                dist = Vector3.Distance(StartPoint, targetPoint);

                                // rotate the object to face the target
                                // 해당 방향으로 회전하여 바라본다.
                                transform.LookAt(targetPoint);

                                // calculate initival velocity required to land the cube on target using the formula (9)
                                float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
                                float Vy, Vz;   // y,z components of the initial velocity

                                Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
                                Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

                                // create the velocity vector in local space
                                localVelocity = new Vector3(0f, Vy, Vz);

                                // transform it to global vector
                                globalVelocity = transform.TransformVector(localVelocity);

                                // launch the cube by setting its initial velocity
                                GetComponent<Rigidbody>().velocity = globalVelocity;

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
        if (collision.transform.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

    }
}
