using UnityEngine;
using System.Collections;

public class EnemyCorpse : MonoBehaviour {

    private CapsuleCollider col;

	// Use this for initialization
	void Start () {
        col = GetComponent<CapsuleCollider>();

        col.height = 0.5f;
        col.radius = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(this.gameObject, 1.0f);
        }
    }
}
