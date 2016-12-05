using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lobby_Tutorial_MoiveTexture : MonoBehaviour {

    private LobbySelectChar SelectChar;
    public RawImage MainMovie;

    public MovieTexture[] Elizabat_Movie;
    public MovieTexture[] Spidas_Movie;
    public MovieTexture[] Caron_Movie;

    private int NowPlayMovie;

	// Use this for initialization
	void Start () {

        NowPlayMovie = 0;

        SelectChar = LobbyManager.SelectChar;

        for (int i = 0; i < Elizabat_Movie.Length; i++ )
        {
            Elizabat_Movie[i].loop = false;
            Elizabat_Movie[i].Stop();
        }

        for (int i = 0; i < Spidas_Movie.Length; i++)
        {
            Spidas_Movie[i].loop = false;
            Spidas_Movie[i].Stop();
        }

        for (int i = 0; i < Caron_Movie.Length; i++)
        {
            Caron_Movie[i].loop = false;
            Caron_Movie[i].Stop();
        }

        //Spidas_Movie[0].loop = true;
        //Elizabat_Movie[0].loop = true;
        //Caron_Movie[0].loop = true;

            switch (SelectChar)
            {
                case LobbySelectChar.CARON:
                    {
                        MainMovie.texture = Caron_Movie[0];
                        //Caron_Movie[0].Play();
                    }
                    break;

                case LobbySelectChar.ELIZABAT:
                    {
                        MainMovie.texture = Elizabat_Movie[0];
                        //Elizabat_Movie[0].Play();
                    }
                    break;

                case LobbySelectChar.SPIDAS:
                    {
                        MainMovie.texture = Spidas_Movie[0];
                        //Spidas_Movie[0].Play();
                    }
                    break;
            }

        
        //MainMovie.Play();
        
	}
	
	// Update is called once per frame
	void Update () {
        SelectChar = LobbyManager.SelectChar;

        if(LobbyManager.Tutorial_On == true)
        {
            switch (SelectChar)
            {
                case LobbySelectChar.CARON:
                    {
                        
                        if(LobbyManager.MovieChangeOn == false)
                        {
                            NowPlayMovie = 0;
                            Caron_Movie[NowPlayMovie].Stop();
                            Caron_Movie[NowPlayMovie].Play();

                            LobbyManager.MovieChangeOn = true;
                        }

                        if (Caron_Movie[NowPlayMovie].isPlaying == false)
                        {
                            Caron_Movie[NowPlayMovie].Stop();

                            if (NowPlayMovie < Caron_Movie.Length - 1)
                            {
                                NowPlayMovie++;
                            }
                            else
                            {
                                NowPlayMovie = 0;
                            }

                            MainMovie.texture = Caron_Movie[NowPlayMovie];
                            Caron_Movie[NowPlayMovie].Play();
                        }
                    }
                    break;

                case LobbySelectChar.ELIZABAT:
                    {

                        //Elizabat_Movie[NowPlayMovie].Play();
                        if (LobbyManager.MovieChangeOn == false)
                        {
                            NowPlayMovie = 0;

                            Elizabat_Movie[NowPlayMovie].Stop();
                            Elizabat_Movie[NowPlayMovie].Play();

                            LobbyManager.MovieChangeOn = true;
                        }

                        if (Elizabat_Movie[NowPlayMovie].isPlaying == false)
                        {
                            Elizabat_Movie[NowPlayMovie].Stop();

                            if (NowPlayMovie < Elizabat_Movie.Length - 1)
                            {
                                NowPlayMovie++;
                            }
                            else
                            {
                                NowPlayMovie = 0;
                            }

                            MainMovie.texture = Elizabat_Movie[NowPlayMovie];
                            Elizabat_Movie[NowPlayMovie].Play();
                        }
                    }
                    break;

                case LobbySelectChar.SPIDAS:
                    {
                        //Spidas_Movie[NowPlayMovie].Play();
                        if (LobbyManager.MovieChangeOn == false)
                        {
                            NowPlayMovie = 0;

                            Spidas_Movie[NowPlayMovie].Stop();
                            Spidas_Movie[NowPlayMovie].Play();

                            LobbyManager.MovieChangeOn = true;
                        }

                        if (Spidas_Movie[NowPlayMovie].isPlaying == false)
                        {
                            Spidas_Movie[NowPlayMovie].Stop();

                            if (NowPlayMovie < Spidas_Movie.Length - 1)
                            {
                                NowPlayMovie++;
                            }
                            else
                            {
                                NowPlayMovie = 0;
                            }

                            MainMovie.texture = Spidas_Movie[NowPlayMovie];
                            Spidas_Movie[NowPlayMovie].Play();
                        }
                    }
                    break;
            }
        }
        
	}
}
