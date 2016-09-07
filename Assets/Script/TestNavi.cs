using UnityEngine;
using System.Collections;

public class TestNavi : MonoBehaviour {

    NavMeshAgent Agent;
    public Transform Target;

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();

        Agent.destination = new Vector3(Target.transform.position.x, 0, Target.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
