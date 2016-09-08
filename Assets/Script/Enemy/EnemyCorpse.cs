﻿using UnityEngine;
using System.Collections;

public class EnemyCorpse : MonoBehaviour {

    private CapsuleCollider col;

	// Use this for initialization
	void Start () {
        col = GetComponent<CapsuleCollider>();

        col.height = 0.1f;
        col.radius = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(this.gameObject, 5.0f);
        }
    }
}
