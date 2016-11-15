using UnityEngine;
using System.Collections;

public class UI_CaptureParameter : MonoBehaviour {

    private GameState Gamestate;
    private Animator ani;

    private float IsChangeMeter;

    // Use this for initialization
    void Start()
    {

        if(ani == null)
        {
            ani = GetComponent<Animator>();
        }
        
        Gamestate = GameManager.Gamestate;

        IsChangeMeter = GameManager.Capture_Parameter;
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
                    if (IsChangeMeter != GameManager.Capture_Parameter)
                    {
                        IsChangeMeter = GameManager.Capture_Parameter;
                        ani.SetTrigger("Capture_Spark");
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
