using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum LobbySelectChar
{
    CARON = 0,
    ELIZABAT,
    SPIDAS
}

public class LobbyManager : MonoBehaviour {


    static public LobbySelectChar SelectChar;
    private ViewControllMode ViewMode;

    static public bool Tutorial_On;
    static public bool Skill_Button_On;
    static public bool Controll_Button_On;
    static public int PlayMovieNumber;

    static public bool SkillMoiveChangeOn;
    static public bool ExplainMoiveChangeOn;
    
    //public Image Character_Portrait;
    public Text Character_Controll_Text;
    public Text Character_Skill_Text;
    public Text Character_Explain_Text;
    public Text Character_Explain_Name_Text;
    public Text Movie_Skill_Text;
    public Text Movie_Controll_Text;
    public Image Controll_Panel;

    public Sprite[] Portraits;
    public Sprite[] Keyboard_Controll_Panels;
    public Sprite[] GamePad_Controll_Panels;
    public Text[] Explain_Texts;
    public Text[] Skill_Texts;
    public Text[] Controll_Texts;
    public Text[] Movie_Skill_Texts;
    public Text[] Movie_Controll_Texts;
    public GameObject[] Portrait_BackLights;
    public Image[] Character_Portraits;

	void Awake()
    {
        SelectChar = LobbySelectChar.CARON;
        ViewMode = ViewControllMode.Mouse;

        Tutorial_On = false;

        Skill_Button_On = false;
        Controll_Button_On = true;

        ExplainMoiveChangeOn = true;
        SkillMoiveChangeOn = false;

        PlayMovieNumber = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.End))
        {
            if (ViewMode == ViewControllMode.GamePad)
                ViewMode = ViewControllMode.Mouse;
            else
                ViewMode = ViewControllMode.GamePad;
        }

        if (Tutorial_On == true)
        {
            switch (ViewMode)
            {
                case ViewControllMode.Mouse:
                    {

                    }
                    break;

                case ViewControllMode.GamePad:
                    {
                        if (Input.GetButton("P1_360_LeftBumper"))
                        {
                            if (SelectChar > LobbySelectChar.CARON)
                            {
                                SelectChar--;
                            }
                            else
                            {
                                SelectChar = LobbySelectChar.SPIDAS;
                            }
                        }

                        if (Input.GetButton("P1_360_RightBumper"))
                        {
                            if (SelectChar < LobbySelectChar.SPIDAS)
                            {
                                SelectChar++;
                            }
                            else
                            {
                                SelectChar = LobbySelectChar.CARON;
                            }
                        }
                    }
                    break;
            }
        }
  
        // 0 : 카론
        // 1 : 엘리자뱃
        // 2 : 스피다스

        switch(SelectChar)
        {
            case LobbySelectChar.CARON:
                {
                    Character_Portraits[0].sprite = Portraits[1];
                    Character_Portraits[1].sprite = Portraits[0];
                    Character_Portraits[2].sprite = Portraits[2];

                    Portrait_BackLights[0].SetActive(false);
                    Portrait_BackLights[1].SetActive(true);
                    Portrait_BackLights[2].SetActive(false);

                    if(Skill_Button_On == true)
                    {
                        Character_Skill_Text.text = Skill_Texts[0].text;
                        
                    }
                    else
                    {
                        Character_Skill_Text.text = "";
                        
                    }

                    if(Controll_Button_On == true)
                    {
                        Character_Controll_Text.text = Controll_Texts[0].text;

                        
                    }
                    else
                    {
                        Character_Controll_Text.text = "";
                        
                    }

                    Movie_Skill_Text.text = Movie_Skill_Texts[0].text;
                    Movie_Controll_Text.text = Movie_Controll_Texts[0].text;

                    Character_Explain_Text.text = Explain_Texts[0].text;
                    Character_Explain_Name_Text.text = "카론";

                    switch(ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                Controll_Panel.sprite = Keyboard_Controll_Panels[0];
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                Controll_Panel.sprite = GamePad_Controll_Panels[0];
                            }
                            break;
                    }
                    
                }
                break;

            case LobbySelectChar.ELIZABAT:
                {
                    Character_Portraits[0].sprite = Portraits[2];
                    Character_Portraits[1].sprite = Portraits[1];
                    Character_Portraits[2].sprite = Portraits[0];

                    Portrait_BackLights[0].SetActive(false);
                    Portrait_BackLights[1].SetActive(true);
                    Portrait_BackLights[2].SetActive(false);

                    if (Skill_Button_On == true)
                    {
                        Character_Skill_Text.text = Skill_Texts[1].text;
                        
                    }
                    else
                    {
                        Character_Skill_Text.text = "";
                        
                    }

                    if (Controll_Button_On == true)
                    {
                        Character_Controll_Text.text = Controll_Texts[1].text;

                        
                    }
                    else
                    {
                        Character_Controll_Text.text = "";
                        
                    }

                    Movie_Skill_Text.text = Movie_Skill_Texts[1].text;
                    Movie_Controll_Text.text = Movie_Controll_Texts[1].text;

                    Character_Explain_Text.text = Explain_Texts[1].text;
                    Character_Explain_Name_Text.text = "엘리자뱃";

                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                Controll_Panel.sprite = Keyboard_Controll_Panels[1];
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                Controll_Panel.sprite = GamePad_Controll_Panels[1];
                            }
                            break;
                    }
                }
                break;

            case LobbySelectChar.SPIDAS:
                {
                    Character_Portraits[0].sprite = Portraits[0];
                    Character_Portraits[1].sprite = Portraits[2];
                    Character_Portraits[2].sprite = Portraits[1];

                    Portrait_BackLights[0].SetActive(false);
                    Portrait_BackLights[1].SetActive(true);
                    Portrait_BackLights[2].SetActive(false);

                    if (Skill_Button_On == true)
                    {
                        Character_Skill_Text.text = Skill_Texts[2].text;
                        
                    }
                    else
                    {
                        Character_Skill_Text.text = "";
                        
                    }

                    if (Controll_Button_On == true)
                    {
                        Character_Controll_Text.text = Controll_Texts[2].text;

                        
                    }
                    else
                    {
                        Character_Controll_Text.text = "";
                    }

                    Movie_Skill_Text.text = Movie_Skill_Texts[2].text;
                    Movie_Controll_Text.text = Movie_Controll_Texts[2].text;

                    Character_Explain_Text.text = Explain_Texts[2].text;
                    Character_Explain_Name_Text.text = "스피다스";


                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {
                                Controll_Panel.sprite = Keyboard_Controll_Panels[2];
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                Controll_Panel.sprite = GamePad_Controll_Panels[2];
                            }
                            break;
                    }
                }
                break;
        }
	}

    public static void LeftArrow()
    {
        if (SelectChar > LobbySelectChar.CARON)
        {
            SelectChar--;

            //SkillMoiveChangeOn = false;
            //ExplainMoiveChangeOn = true;
        }
        else
        {
            SelectChar = LobbySelectChar.SPIDAS;

            //SkillMoiveChangeOn = false;
            //ExplainMoiveChangeOn = true;
        }
    }

    public static void RightArrow()
    {
        if (SelectChar < LobbySelectChar.SPIDAS)
        {
            SelectChar++;

            //SkillMoiveChangeOn = false;
            //ExplainMoiveChangeOn = true;
        }
        else
        {
            SelectChar = LobbySelectChar.CARON;

            //SkillMoiveChangeOn = false;
            //ExplainMoiveChangeOn = true;
        }
    }

    public static void ExplainMovieOn()
    {
        SkillMoiveChangeOn = false;
        ExplainMoiveChangeOn = true;
    }

    public static void SkillMovieOn()
    {
        SkillMoiveChangeOn = true;
        ExplainMoiveChangeOn = false;

    }
}
