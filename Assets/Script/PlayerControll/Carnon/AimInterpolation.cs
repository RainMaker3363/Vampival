using UnityEngine;
using System.Collections;

public class AimInterpolation : MonoBehaviour {

    
    public GameObject AimTarget;
    public GameObject AimInterpol;
    

    private RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //if (Physics.Raycast(this.transform.position, (AimInterpol.transform.position - this.transform.position).normalized * 100.0f, out hit, Mathf.Infinity))
        //{
        //    //print("Hit");



        //    if (hit.collider.tag.Equals("Ground") == true)
        //    {

        //        print(hit.distance);


        //        ///AimTarget.transform.position = hit.transform.position;
        //    }
        //}

        //print(AimTarget.transform.position);
        //Debug.DrawRay(this.transform.position, (AimInterpol.transform.position - this.transform.position).normalized * 80.0f, Color.green, 5.0f);
	}
}
