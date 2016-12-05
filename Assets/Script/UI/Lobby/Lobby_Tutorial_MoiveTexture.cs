using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lobby_Tutorial_MoiveTexture : MonoBehaviour {

    private LobbySelectChar SelectChar;
    public RawImage MainMovie;

    public MovieTexture[] Elizabat_Movie;
    public MovieTexture[] Spidas_Movie;
    public MovieTexture[] Caron_Movie;

    private int NowPlay_Elizabat_Movie;
    private int NowPlay_Spidas_Movie;
    private int NowPlay_Caron_Movie;

	// Use this for initialization
	void Start () {

        NowPlay_Elizabat_Movie = Elizabat_Movie.Length;
        NowPlay_Caron_Movie = Caron_Movie.Length;
        NowPlay_Spidas_Movie = Spidas_Movie.Length;

        SelectChar = LobbyManager.SelectChar;

        switch (SelectChar)
        {
            case LobbySelectChar.CARON:
                {
                    MainMovie.texture = Caron_Movie[0];
                    Caron_Movie[0].Play();
                }
                break;

            case LobbySelectChar.ELIZABAT:
                {
                    MainMovie.texture = Elizabat_Movie[0];
                    Elizabat_Movie[0].Play();
                }
                break;

            case LobbySelectChar.SPIDAS:
                {
                    MainMovie.texture = Spidas_Movie[0];
                    Spidas_Movie[0].Play();
                }
                break;
        }

        
        //MainMovie.Play();
        
	}
	
	// Update is called once per frame
	void Update () {
        SelectChar = LobbyManager.SelectChar;

        //switch(SelectChar)
        //{
        //    case LobbySelectChar.CARON:
        //        {


        //            if(MainMovie.isPlaying == false)
        //            {
        //                MainMovie.Stop();

        //                if (NowPlay_Caron_Movie < Caron_Movie.Length)
        //                {
        //                    NowPlay_Caron_Movie++;
        //                }
        //                else
        //                {
        //                    NowPlay_Caron_Movie = 0;
        //                }

        //                MainMovie.texture = Caron_Movie[NowPlay_Caron_Movie];
        //                MainMovie.Play();
        //            }
        //        }
        //        break;

        //    case LobbySelectChar.ELIZABAT:
        //        {

        //            if (MainMovie.isPlaying == false)
        //            {
        //                MainMovie.Stop();

        //                if (NowPlay_Elizabat_Movie < Elizabat_Movie.Length)
        //                {
        //                    NowPlay_Elizabat_Movie++;
        //                }
        //                else
        //                {
        //                    NowPlay_Elizabat_Movie = 0;
        //                }

        //                MainMovie.texture = Elizabat_Movie[NowPlay_Elizabat_Movie];
        //                MainMovie.Play();
        //            }
        //        }
        //        break;

        //    case LobbySelectChar.SPIDAS:
        //        {

        //            if (MainMovie.isPlaying == false)
        //            {
        //                MainMovie.Stop();

        //                if (NowPlay_Spidas_Movie < Spidas_Movie.Length)
        //                {
        //                    NowPlay_Spidas_Movie++;
        //                }
        //                else
        //                {
        //                    NowPlay_Spidas_Movie = 0;
        //                }

        //                MainMovie.texture = Spidas_Movie[NowPlay_Spidas_Movie];
        //                MainMovie.Play();
        //            }
        //        }
        //        break;
        //}
	}
}
