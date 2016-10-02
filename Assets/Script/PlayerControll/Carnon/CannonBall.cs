using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 StartPoint = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private float _angle = 23.0f;
    private float dist;

    private Vector3 localVelocity;
    private Vector3 globalVelocity;

    private bool IsFire;
    private bool _rotate;

    public GameObject AimTarget;
    public GameObject CannonPoint;

	// Use this for initialization
	void Start () {

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        Destroy(gameObject, 3.0f);

        AimTarget = GameObject.FindWithTag("Cannon_CrossHair");
        CannonPoint = GameObject.FindWithTag("Cannon");

        IsFire = false;
        _rotate = true;

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



                                    IsFire = true;
                                }

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

                                if (_rotate)
                                    transform.rotation = Quaternion.LookRotation(globalVelocity);



                                //if (Input.GetKey(KeyCode.X))
                                //{
                                //    _angle -= 2.0f;
                                //}
                                //if (Input.GetKey(KeyCode.C))
                                //{
                                //    _angle += 2.0f;
                                //}

                                //print(_angle);
                                

                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                if (!IsFire)
                                {


                                    targetPoint = AimTarget.transform.position;
                                    StartPoint = CannonPoint.transform.position;



                                    IsFire = true;
                                }

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

                                if (_rotate)
                                    transform.rotation = Quaternion.LookRotation(globalVelocity);

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
        if (collision.transform.tag.Equals("Enemy") == true)
        {
            //_rotate = false;
            Destroy(this.gameObject);
        }

        if (collision.transform.tag.Equals("Walls") == true)
        {
            //_rotate = false;
            //Destroy(this.gameObject, 0.5f);
        }

    }
}
