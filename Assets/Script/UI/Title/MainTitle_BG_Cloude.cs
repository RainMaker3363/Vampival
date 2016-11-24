using UnityEngine;
using System.Collections;

public class MainTitle_BG_Cloude : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(new Vector3(0, 0, -10.0f) * Time.deltaTime);
	}
}
