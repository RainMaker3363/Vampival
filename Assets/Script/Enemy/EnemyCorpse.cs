using UnityEngine;
using System.Collections;

public class EnemyCorpse : MonoBehaviour {

    private CapsuleCollider col;
    private Rigidbody rigid;

    private Vector3 StartPos;

	// Use this for initialization
    void Awake()
    {
        if(col == null)
        {
            col = GetComponent<CapsuleCollider>();
        }

        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        StartPos = this.transform.position;

        this.gameObject.SetActive(false);

        
        //col.height = 0.1f;
        //col.radius = 0.1f;
	}
	
    void OnEnable()
    {
        if (col == null)
        {
            col = GetComponent<CapsuleCollider>();
        }

        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        col.enabled = true;
        rigid.useGravity = true;

        this.transform.position = StartPos;

        DeadOrAliveRoutin();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Enemy")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    IEnumerator DeadOrAliveRoutin()
    {
        yield return new WaitForSeconds(6.0f);

        this.gameObject.SetActive(false);
    }

    void DeadOrAlive()
    {
        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy") == true)
        {
            //Destroy(this.gameObject, 2.0f);

            
            //this.gameObject.tag = "Untagged";
            col.enabled = false;
            rigid.useGravity = false;

            DeadOrAlive();
        }
    }
}
