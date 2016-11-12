using UnityEngine;
using System.Collections;

public class Spidas_WebTrap : MonoBehaviour {

    private GameState Gamestate;

    private Rigidbody rigid;
    private SphereCollider col;

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
        
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("PlayerSetObject"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("PlayerSetObject"), true);
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
            
            this.gameObject.SetActive(false);


        }
    }
}
