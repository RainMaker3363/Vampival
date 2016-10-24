using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private SphereCollider SphereCol;

    //private Vector3 StartBackUp = Vector3.zero;
    private Vector3 StartPoint = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private float _angle = 23.0f;
    private float dist;

    private Vector3 localVelocity;
    private Vector3 globalVelocity;

    private AudioSource Audio;
    public AudioClip ExplosionSound;

    private bool IsFire;
    private bool _rotate;

    ParticleSystem.Particle[] unused = new ParticleSystem.Particle[1];

    public GameObject AimTarget;
    public GameObject CannonPoint;
    public ParticleSystem Explode_Particle;
    
	
    void Awake()
    {
        StopCoroutine("DeadOrAliveRoutin");

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        //Destroy(gameObject, 3.0f);

        if (AimTarget == null)
        {
            AimTarget = GameObject.FindWithTag("Cannon_CrossHair");
        }

        if (CannonPoint == null)
        {
            CannonPoint = GameObject.FindWithTag("Cannon");
        }

        if (SphereCol == null)
        {
            SphereCol = GetComponent<SphereCollider>();
        }

        if (SphereCol != null)
        {
            SphereCol.enabled = true;
        }



        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = ExplosionSound;
        }

        IsFire = false;
        _rotate = true;

        //Explode_Particle.gameObject.SetActive(false);
        Explode_Particle.Stop();

        GetComponent<ParticleSystemRenderer>().enabled = false;

        StartCoroutine("DeadOrAliveRoutin");
    }

    void OnEnable()
    {
        StopCoroutine("DeadOrAliveRoutin");

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        //Destroy(gameObject, 3.0f);

        if(AimTarget == null)
        {
            AimTarget = GameObject.FindWithTag("Cannon_CrossHair");
        }

        if(CannonPoint == null)
        {
            CannonPoint = GameObject.FindWithTag("Cannon");
        }

        if (SphereCol == null)
        {
            SphereCol = GetComponent<SphereCollider>();
        }

        if(SphereCol != null)
        {
            SphereCol.enabled = true;
        }

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = ExplosionSound;
        }


        IsFire = false;
        _rotate = true;

        //Explode_Particle.gameObject.SetActive(false);
        Explode_Particle.Stop();

        GetComponent<ParticleSystemRenderer>().enabled = false;

        StartCoroutine("DeadOrAliveRoutin");
    }

    IEnumerator DeadOrAliveRoutin()
    {
        yield return new WaitForSeconds(3.25f);

        this.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        GetComponent<ParticleSystemRenderer>().enabled = GetComponent<ParticleSystem>().GetParticles(unused) > 0;
    }

	// Update is called once per frame
	void FixedUpdate() 
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
                                    StartPoint = CannonPoint.transform.position;
                                    targetPoint = AimTarget.transform.position;

                                    this.transform.parent = null;

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
                                GetComponent<Rigidbody>().velocity = (globalVelocity * 2.0f);

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

                                    this.transform.parent = null;

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
                                GetComponent<Rigidbody>().velocity = (globalVelocity * 2.0f);

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
            //Destroy(this.gameObject, 0.2f);
            Explode_Particle.gameObject.SetActive(true);

            Explode_Particle.Play();

            Audio.Play();

            //Explode_Particle.transform.position = collision.transform.position;

            if (SphereCol != null)
            {
                SphereCol.enabled = false;
            }

            if (Explode_Particle.isStopped == true)
            {
                this.gameObject.SetActive(false);
            }
        }

        if (collision.transform.tag.Equals("Ground") == true)
        {
            //_rotate = false;
            //Destroy(this.gameObject, 0.2f);

            Explode_Particle.gameObject.SetActive(true);

            Explode_Particle.Play();

            Audio.Play();

            //Explode_Particle.transform.position = collision.transform.position;

            if (SphereCol != null)
            {
                SphereCol.enabled = false;
            }

            if(Explode_Particle.isStopped == true)
            {
                this.gameObject.SetActive(false);
            }
                
        }

    }
}
