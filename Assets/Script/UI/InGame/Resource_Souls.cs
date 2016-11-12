using UnityEngine;
using System.Collections;

public class Resource_Souls : MonoBehaviour {

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private bool ConsumeOn;

	// Use this for initialization
	void Start () 
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("InGameItem"), true);

        ConsumeOn = false;

        StartCoroutine(DeadOrAliveRoutin(1));
	}

    void OnEnable()
    {
        StopCoroutine(DeadOrAliveRoutin(1));

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

                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        ConsumeOn = true;
                    }
                    //else if(Input.GetKeyUp(KeyCode.H))
                    //{
                    //    ConsumeOn = false;
                    //}

                }
                break;

            case ViewControllMode.GamePad:
                {
                    // 게임 패드 작업

                    if (Input.GetButtonDown("P3_360_AButton"))
                    {
                        ConsumeOn = true;
                    }
                    //else if(Input.GetButtonUp("P3_360_AButton"))
                    //{
                    //    ConsumeOn = false;
                    //}


                }
                break;
        }
	}

    IEnumerator DeadOrAliveRoutin(int i)
    {
        yield return new WaitForSeconds(60.0f);

        this.gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Spidas") == true)
        {
            if (ConsumeOn)
            {
                print("Soul Recharging");

                this.gameObject.SetActive(false);

                GameManager.Soul_MP_Parameter += 2.0f;

                ConsumeOn = false;
            }


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
