using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Document_Script : MonoBehaviour {

    private GameState Gamestate;

	// Use this for initialization
	void Start () {
        Gamestate = GameManager.Gamestate;
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

}
