using UnityEngine;
using System.Collections;

public class Resource_Souls : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    public GameObject Consume_Particle_Object;
    public ParticleSystem Consume_Particle;

    private bool Consume_On;

	// Use this for initialization
	void Start () 
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("InGameItem"), true);

        Consume_Particle_Object.gameObject.SetActive(false);
        Consume_On = false;
        //Consume_Particle.Stop();

        StartCoroutine(DeadOrAliveRoutin(1));
	}

    void OnEnable()
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        Consume_Particle_Object.gameObject.SetActive(false);
        Consume_On = false;
        //Consume_Particle.Stop();

        StartCoroutine(DeadOrAliveRoutin(1));
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

        switch (ViewMode)
        {
            case ViewControllMode.Mouse:
                {

                    // 마우스 작업
                }
                break;

            case ViewControllMode.GamePad:
                {
                    // 게임 패드 작업
                }
                break;
        }
	}

    IEnumerator DeadOrAliveRoutin(int i)
    {
        yield return new WaitForSeconds(60.0f);

        this.gameObject.SetActive(false);
    }

    IEnumerator ConsumeOnRoutine(int i)
    {
        yield return new WaitForSeconds(0.6f);

        Consume_Particle_Object.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Spidas") == true)
        {
            if (Consume_On == false)
            {
                print("Soul Recharging");

                Consume_Particle_Object.gameObject.SetActive(true);

                StopCoroutine(ConsumeOnRoutine(1));

                Consume_On = true;

                GameManager.Soul_MP_Parameter += 3.0f;

                StartCoroutine(ConsumeOnRoutine(1));


            }
            //if (Input.GetKeyDown(KeyCode.H))
            //{
            //    //print("Soul Recharging");

            //    if(Consume_On == false)
            //    {
            //        print("Soul Recharging");

            //        Consume_Particle_Object.gameObject.SetActive(true);

            //        StopCoroutine(ConsumeOnRoutine(1));

            //        Consume_On = true;

            //        GameManager.Soul_MP_Parameter += 3.0f;

            //        StartCoroutine(ConsumeOnRoutine(1));


            //    }




            //}


            //if (Input.GetButtonDown("P3_360_AButton"))
            //{


            //    if (Consume_On == false)
            //    {
            //        print("Soul Recharging");
            //        Consume_Particle_Object.gameObject.SetActive(true);


            //        StopCoroutine(ConsumeOnRoutine(1));

            //        Consume_On = true;
            //        GameManager.Soul_MP_Parameter += 3.0f;

            //        StartCoroutine(ConsumeOnRoutine(1));




            //    }

            //}

        }
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag.Equals("Spidas") == true)
    //    {
    //        print("Soul Recharging");

    //        this.gameObject.SetActive(false);

    //        GameManager.Soul_MP_Parameter += 2.0f;
    //    }
    //}
}
