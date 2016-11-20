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
                    //IsChangeMeter = GameManager.Soul_MP_Parameter;
                    if (IsChangeMeter < GameManager.Soul_MP_Parameter)
                    {
                        IsChangeMeter = GameManager.Soul_MP_Parameter;
                        //ani.SetTrigger("Soul_Decrease");
                        ani.SetBool("Soul_Increase", true);
                    }
                    else
                    {
                        ani.SetBool("Soul_Increase", false);
                    }
                    
                    if (IsChangeMeter > GameManager.Soul_MP_Parameter)
                    {
                        IsChangeMeter = GameManager.Soul_MP_Parameter;
                        ani.SetBool("Soul_Decrease", true);

                        //ani.SetTrigger("Soul_Spark");
                    }
                    else
                    {

                        ani.SetBool("Soul_Decrease", false);
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
