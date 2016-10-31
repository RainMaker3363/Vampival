using UnityEngine;
using System.Collections;
using System.Text;

public class EnemyCorpse : MonoBehaviour {

    private CapsuleCollider col;
    private Rigidbody rigid;

    // 랜덤으로 튕겨 나가게 한다.
    private float Explode_Random_X;
    private float Explode_Random_Z;
    //private Vector3 StartPos;

	// Use this for initialization
    void Awake()
    {
        StopCoroutine("DeadOrAliveRoutin");


        //StartPos = this.transform.position;

        Explode_Random_X = Random.Range(6.0f, 10.0f);
        Explode_Random_Z = Random.Range(6.0f, 10.0f);

        if(col == null)
        {
            col = GetComponent<CapsuleCollider>();
        }

        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
            rigid.AddForce(new Vector3(Explode_Random_X, 8.0f, -Explode_Random_Z), ForceMode.Impulse);
        }


        //this.gameObject.SetActive(false);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("AttackPoint"), true);
        
        //col.height = 0.1f;
        //col.radius = 0.1f;
        StartCoroutine("DeadOrAliveRoutin");
	}
	
    void OnEnable()
    {
        StopCoroutine("DeadOrAliveRoutin");

        //this.transform.position = StartPos;

        Explode_Random_X = Random.Range(6.0f, 10.0f);
        Explode_Random_Z = Random.Range(6.0f, 10.0f);


        if (col == null)
        {
            col = GetComponent<CapsuleCollider>();
        }

        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
        else
        {
            rigid.AddForce(new Vector3(Explode_Random_X, 8.0f, -Explode_Random_Z), ForceMode.Impulse);
        }
        

        col.enabled = true;
        rigid.useGravity = true;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("AttackPoint"), true);

        //this.transform.position = StartPos;

        //if (StringBuilder.Equals(this.gameObject.tag, "Untagged"))
        //{
        //    this.gameObject.tag = "EnemyCorpse";
        //}
        

        StartCoroutine("DeadOrAliveRoutin");
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
        yield return new WaitForSeconds(8.0f);

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
            
            //col.enabled = false;
            //rigid.useGravity = false;

            this.gameObject.SetActive(false);
            //DeadOrAlive();
        }
    }
}
