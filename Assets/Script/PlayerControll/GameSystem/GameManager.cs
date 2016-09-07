using UnityEngine;
using System.Collections;

public enum GameState
{
    GameIntro = 0,
    GameStart,
    GameEnd
}

// 시점 조작 모드 선택
public enum ViewControllMode
{
    GamePad,
    Mouse
};

public class GameManager : MonoBehaviour {

    public Transform[] RespawnPoint;
    public GameObject[] Enemies;

    private bool SpawnOn;

    private int NowLevel;
    private float RespawnTimer;

    static public GameState Gamestate;
    static public ViewControllMode ViewMode;

    // 공포도 수치 (게임 승패 조건)
    static public int Fear_Parameter;


    void Awake()
    {
        SpawnOn = false;
        NowLevel = 1;

        RespawnTimer = 1.0f;

        Gamestate = GameState.GameIntro;
        ViewMode = ViewControllMode.Mouse;
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

        switch(Gamestate)
        {
            case GameState.GameIntro:
                {
                    if(RespawnTimer <= 0.0f)
                    {
                        RespawnTimer = 1.0f;

                        SpawnOn = true;
                    }
                    else
                    {
                        RespawnTimer -= Time.deltaTime;
                    }
                    
                    if(SpawnOn)
                    {
                        SpawnOn = false;

                        int EnemyType = Random.Range(0, 4);
                        int EnemyLocation = Random.Range(0, 4);

                        StartCoroutine(RespawnEnemy(NowLevel, EnemyType, EnemyLocation));
                    }
                    
                }
                break;

            case GameState.GameStart:
                {

                }
                break;

            case GameState.GameEnd:
                {

                }
                break;
        }
	}

    IEnumerator RespawnEnemy(int Level, int Type, int Location)
    {
        switch(Level)
        {
            case 1:
                {
                    int Respawn_LerpX = Random.Range(-5, 6);
                    int Respawn_LerpZ = Random.Range(-5, 6);

                    Instantiate(Enemies[Type], new Vector3(RespawnPoint[Location].position.x + Respawn_LerpX, RespawnPoint[Location].position.y, RespawnPoint[Location].position.z + Respawn_LerpZ), Quaternion.identity);
                }
                break;

            case 2:
                {

                }
                break;

            case 3:
                {

                }
                break;
        }
        yield return null;
    }
}
