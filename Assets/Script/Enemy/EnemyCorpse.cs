using UnityEngine;
using System.Collections;

public class EnemyCorpse : MonoBehaviour {

    private CapsuleCollider col;
    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        col = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        //col.height = 0.1f;
        //col.radius = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Enemy")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy") == true)
        {
            Destroy(this.gameObject, 2.0f);


            this.gameObject.tag = "Untagged";
            col.enabled = false;
            rigid.useGravity = false;
        }
    }
}
