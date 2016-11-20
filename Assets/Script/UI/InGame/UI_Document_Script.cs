using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Document_Script : MonoBehaviour {

    private GameState Gamestate;

	// Use this for initialization
	void Start () {
        Gamestate = GameManager.Gamestate;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("InGameItem"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyCorpse"), LayerMask.NameToLayer("InGameItem"), true);
	}
	
	// Update is called once per frame
	void Update () {
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


                    this.transform.Rotate(new Vector3(0, 0, 180), 180 * Time.deltaTime);
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Spidas") == true)
        {
            other.gameObject.SetActive(false);

            GameManager.Gamestate = GameState.GamePause;
        }
    }

}
