using UnityEngine;
using System.Collections;

public class Resource_Souls : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("InGameItem"), true);

        StartCoroutine(DeadOrAliveRoutin(1));
	}

    void OnEnable()
    {
        StopCoroutine(DeadOrAliveRoutin(1));

        StartCoroutine(DeadOrAliveRoutin(1));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator DeadOrAliveRoutin(int i)
    {
        yield return new WaitForSeconds(60.0f);

        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Spidas") == true)
        {
            print("Soul Recharging");

            this.gameObject.SetActive(false);

            GameManager.Soul_MP_Parameter += 2.0f;
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
