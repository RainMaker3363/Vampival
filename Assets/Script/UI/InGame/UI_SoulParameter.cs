using UnityEngine;
using System.Collections;

public class UI_SoulParameter : MonoBehaviour {

    private GameState Gamestate;
    private Animator ani;

    private float IsChangeMeter;

    // Use this for initialization
    void Start()
    {

        if (ani == null)
        {
            ani = GetComponent<Animator>();
        }

        Gamestate = GameManager.Gamestate;

        IsChangeMeter = GameManager.Soul_MP_Parameter;
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
                    if (IsChangeMeter != GameManager.Soul_MP_Parameter)
                    {
                        IsChangeMeter = GameManager.Soul_MP_Parameter;
                        ani.SetTrigger("Soul_Spark");
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
