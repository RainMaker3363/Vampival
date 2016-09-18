using UnityEngine;
using System.Collections;

public enum GameState
{
    GameIntro = 0,
    GamePause,
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

    /// <summary>
    // 공포도의 영향을 줄 빛
    /// </summary>
    public Light light;

    private bool SpawnOn;

    private int NowLevel;
    private float RespawnTimer;

    // 게임이 진행됨에 따라 빛의 농도를 바꿔준다.
    private float GameTimer;

    // 커맨드 입력 On / OFF 여부
    static public bool CommandStart;

    static public GameState Gamestate;
    static public ViewControllMode ViewMode;

    // 공포도 수치 (게임 승패 조건)
    static public int Fear_Parameter;


    void Awake()
    {
        SpawnOn = false;
        NowLevel = 1;
        Fear_Parameter = 0;

        RespawnTimer = 1.0f;
        GameTimer = 0.0f;

        light.intensity = 0.35f;
        CommandStart = false;

        Gamestate = GameState.GameStart;
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
                    
                }
                break;

            case GameState.GameStart:
                {
                    
                    // 빛 타이머
                    if (GameTimer >= 5.0f)
                    {
                        GameTimer = 0.0f;
                        light.intensity += 0.036f;
                    }
                    else
                    {
                        GameTimer += Time.deltaTime;
                    }

                    // 리스폰 타이머
                    if (RespawnTimer <= 0.0f)
                    {
                        RespawnTimer = 3.5f;

                        SpawnOn = true;
                    }
                    else
                    {
                        RespawnTimer -= Time.deltaTime;
                    }

                    // 리스폰을 한번씩 해준다.
                    if (SpawnOn)
                    {
                        SpawnOn = false;

                        int EnemyType = Random.Range(0, 4);
                        int EnemyLocation = Random.Range(0, 4);

                        StartCoroutine(RespawnEnemy(NowLevel, EnemyType, EnemyLocation));
                    }
                    
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
