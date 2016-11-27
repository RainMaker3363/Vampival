using UnityEngine;
using System.Collections;

public class WildFire : MonoBehaviour {

    private GameState Gamestate;

    private SphereCollider col;

    public GameObject TargetPos;
    public GameObject StartPos;
    public ParticleSystem Explode_Particle;
    private Vector3 Dir;

    private bool TargetChecker;

    // Use this for initialization
    void Start()
    {

        //StopCoroutine(DeadOrAliveRoutin(1));

        Gamestate = GameManager.Gamestate;

        if (col == null)
        {
            col = GetComponent<SphereCollider>();
            col.enabled = true;
        }
        else
        {
            col.enabled = true;
        }


        //TargetPos.transform.position = Vector3.zero;
        //Dir = (TargetPos.transform.position - this.transform.position).normalized;
        if (StartPos == null)
        {
            StartPos = GameObject.FindWithTag("Elizabat");
            this.transform.position = StartPos.transform.position;
        }
        else
        {
            this.transform.position = StartPos.transform.position;
        }

        if (TargetPos == null)
        {
            TargetPos = GameObject.FindWithTag("SkillTarget");
        }

        this.transform.parent = null;
        this.transform.localEulerAngles = new Vector3(0, 0, 0);

        TargetChecker = false;

        Explode_Particle.gameObject.SetActive(false);
        Explode_Particle.Stop();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerSetObject"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("CarryPlayer"), LayerMask.NameToLayer("SkillParticle"), true);

        print("TargetPos : " + TargetPos.transform.position);
        print("StartPos : " + StartPos.transform.position);

        //StartCoroutine(DeadOrAliveRoutin(1));
    }

    void OnEnable()
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        Gamestate = GameManager.Gamestate;

        if (col == null)
        {
            col = GetComponent<SphereCollider>();
            col.enabled = true;
        }
        else
        {
            col.enabled = true;
        }

        //TargetPos.transform.position = Vector3.zero;
        //Dir = (TargetPos.transform.position - this.transform.position).normalized;
        if (StartPos == null)
        {
            StartPos = GameObject.FindWithTag("Elizabat");
            this.transform.position = StartPos.transform.position;
        }
        else
        {
            this.transform.position = StartPos.transform.position;
        }

        if(TargetPos == null)
        {
            TargetPos = GameObject.FindWithTag("SkillTarget");
        }

        this.transform.parent = null;
        this.transform.localEulerAngles = new Vector3(0, 0, 0);
        TargetChecker = false;

        Explode_Particle.gameObject.SetActive(false);
        Explode_Particle.Stop();

        print("TargetPos : " + TargetPos.transform.position);
        print("StartPos : " + StartPos.transform.position);

        StartCoroutine(DeadOrAliveRoutin(1));
    }

    public void SetTargetPos(GameObject Pos)
    {
        TargetPos.transform.position = Pos.gameObject.transform.position;

        Dir = (TargetPos.transform.position - this.transform.position).normalized;
    }

    IEnumerator DeadOrAliveRoutin(int temp)
    {
        yield return new WaitForSeconds(4.0f);

        this.gameObject.SetActive(false);
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

                }
                break;

            case GameState.GameStart:
                {
                    if (TargetChecker == false)
                    {
                        TargetChecker = true;
                        this.transform.position = StartPos.transform.position;
                        this.transform.localEulerAngles = new Vector3(0, 0, 0);
                        Dir = (TargetPos.transform.position - StartPos.transform.position).normalized;
                    }

                    //Debug.DrawRay(this.transform.position, Dir * 200.0f, Color.green, 100.0f);
                    this.transform.Translate((Dir * 75.0f) * Time.deltaTime);
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
            //print("WildFire Hit");

            if (col != null)
            {
                col.enabled = false;
            }

            Explode_Particle.gameObject.SetActive(true);
            Explode_Particle.Play();

            if (Explode_Particle.isStopped == true)
            {
                this.gameObject.SetActive(false);

            }
            

            
        }

        if (collision.transform.tag.Equals("Ground") == true)
        {
            //print("WildFire Hit");

            if (col != null)
            {
                col.enabled = false;
            }

            Explode_Particle.gameObject.SetActive(true);
            Explode_Particle.Play();

            if (Explode_Particle.isStopped == true)
            {
                this.gameObject.SetActive(false);

            }

            
        }

    }
}
