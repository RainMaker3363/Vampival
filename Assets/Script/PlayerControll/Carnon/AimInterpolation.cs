using UnityEngine;
using System.Collections;

public class AimInterpolation : MonoBehaviour {

    private GameState Gamestate;
    
    public GameObject AimTarget;
    public GameObject AimInterpol;

    private int layermask;
    private float Leng = 0f;

    private RaycastHit hit;

    void Start()
    {
        layermask = (1 << LayerMask.NameToLayer("LayCastIn")); //(-1) - (1 << 15) | (1 << 12) | (1 << 10) | (1 << 9);
        Gamestate = GameManager.Gamestate;
    }

    void OnEnable()
    {
        Gamestate = GameManager.Gamestate;
    }
	
	// Update is called once per frame
    void Update()
    {
        Gamestate = GameManager.Gamestate;

        switch (Gamestate)
        {
            case GameState.GameIntro:
                {

                }
                break;

            case GameState.GamePause:
                {

                }
                break;

            case GameState.GameStart:
                {
                    if (Physics.Raycast(this.transform.position, (AimInterpol.transform.position - this.transform.position).normalized * 200.0f, out hit, Mathf.Infinity))
                    {
                        if (hit.collider.tag.Equals("Ground") == true)
                        {
                            Leng = this.transform.localPosition.z + Vector3.Distance(this.transform.position, hit.point);

                            AimTarget.transform.localPosition = new Vector3(AimTarget.transform.localPosition.x, AimTarget.transform.localPosition.y, Leng);

                        }
                    }
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }



    }
}