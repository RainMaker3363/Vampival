using UnityEngine;
using System.Collections;

public class Enemy_Health_Bar : MonoBehaviour {

    public GameObject MainCamera;

    private GameState Gamestate;

	// Use this for initialization
	void Start () {

        if(MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

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
                    this.transform.LookAt(this.transform.position + MainCamera.transform.rotation * Vector3.back,
                        MainCamera.transform.rotation * Vector3.down);
                }
                break;

            case GameState.GameEnd:
                {

                }
                break;

            case GameState.GameVictory:
                {

                }
                break;
        }

	}
}
