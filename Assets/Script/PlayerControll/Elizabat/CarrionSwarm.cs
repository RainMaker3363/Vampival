using UnityEngine;
using System.Collections;

public class CarrionSwarm : MonoBehaviour {

    private GameState Gamestate;

    private SphereCollider col;

	// Use this for initialization
	void Start () {

        StopCoroutine(DeadOrAliveRoutin(1));

        Gamestate = GameManager.Gamestate;

        if (col == null)
        {
            col = GetComponent<SphereCollider>();
        }


        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerSetObject"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("SkillParticle"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("CarryPlayer"), LayerMask.NameToLayer("SkillParticle"), true);

        StartCoroutine(DeadOrAliveRoutin(1));
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

        StartCoroutine(DeadOrAliveRoutin(1));
    }

    IEnumerator DeadOrAliveRoutin(int temp)
    {
        yield return new WaitForSeconds(4.0f);

        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
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

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.tag.Equals("Enemy") == true)
    //    {
    //        print("Enemy in the Swarm!");
    //    }
    //}
}
