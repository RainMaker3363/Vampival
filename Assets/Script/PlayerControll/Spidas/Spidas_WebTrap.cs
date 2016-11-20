using UnityEngine;
using System.Collections;

public class Spidas_WebTrap : MonoBehaviour {

    private GameState Gamestate;

    private Rigidbody rigid;
    private SphereCollider col;

    public ParticleSystem WebSetUp;
    public ParticleSystem WebActive;

    private float ParticleTimer;
    private bool ParticleTimerOn;

    // Use this for initialization
    void Start()
    {
        Gamestate = GameManager.Gamestate;

        if(col == null)
        {
            col = GetComponent<SphereCollider>();
        }

        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        if(WebActive != null)
        {
            WebActive.Stop();
            //WebActive.gameObject.SetActive(false);
        }

        if (WebSetUp != null)
        {
            WebSetUp.Play();
        }

        ParticleTimer = 0.0f;
        ParticleTimerOn = false;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerSetObject"), LayerMask.NameToLayer("PlayerSetObject"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("PlayerSetObject"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("PlayerSetObject"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("CarryPlayer"), LayerMask.NameToLayer("PlayerSetObject"), true);
    }

    void OnEnable()
    {
        if (col == null)
        {
            col = GetComponent<SphereCollider>();
            col.enabled = true;
        }
        else
        {
            col.enabled = true;
        }

        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
        }
        else
        {
            rigid.useGravity = false;
        }

        if (WebActive != null)
        {
            WebActive.Stop();
            //WebActive.gameObject.SetActive(false);
        }
        
        if(WebSetUp != null)
        {
            WebSetUp.Play();
        }

        ParticleTimer = 0.0f;
        ParticleTimerOn = false;
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
                    if(Input.GetKeyDown(KeyCode.Y))
                    {
                        if(this.gameObject.activeSelf == true)
                            this.transform.parent = null;
                    }

                    if(ParticleTimerOn == true)
                    {
                        if(ParticleTimer <= 0.5f)
                        {
                            ParticleTimer += Time.deltaTime;
                        }
                        else
                        {
                            ParticleTimer = 0.0f;
                            ParticleTimerOn = false;

                            this.gameObject.SetActive(false);
                        }
                    }

                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Enemy") == true)
        {
            print("Enemy Touch Trap!");

            if(col != null)
            {
                col.enabled = false;
            }
                

            rigid.useGravity = false;

            //WebActive.gameObject.SetActive(true);

            WebActive.Play();

            ParticleTimerOn = true;
        }
    }
}
