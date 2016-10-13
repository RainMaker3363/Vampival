using UnityEngine;
using System.Collections;

public class AimInterpolation : MonoBehaviour {

    
    public GameObject AimTarget;
    public GameObject AimInterpol;

    private float Leng = 0f;

    private RaycastHit hit;
	
	// Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(this.transform.position, (AimInterpol.transform.position - this.transform.position).normalized * 80.0f, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag.Equals("Ground") == true)
            {
                Leng = this.transform.localPosition.z + Vector3.Distance(this.transform.position, hit.point);

                AimTarget.transform.localPosition = new Vector3(AimTarget.transform.localPosition.x, AimTarget.transform.localPosition.y, Leng);

            }
        }

    }
}