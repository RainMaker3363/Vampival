using UnityEngine;
using System.Collections;

public class EnemySpawnChecker : MonoBehaviour {

    // 현재 적군의 활성화 상태 카운트
    private int EnemyCounter;
    // 적군의 최대 수를 세고있다.
    private int EnemyMaxCount;

    // 적군의 정보들
    public GameObject[] Enemies;

	// Use this for initialization
	void Awake () {

        EnemyCounter = 0;
        EnemyMaxCount = Enemies.Length;
	}

    void OnEnable()
    {
        EnemyCounter = 0;
        EnemyMaxCount = Enemies.Length;

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].SetActive(true);
        }

        print("Enemy Respawn!");
    }
	
	// Update is called once per frame
	void Update () {
	
        //for(int i =0; i<Enemies.Length; i++)
        //{
        //    if(Enemies[i].activeSelf == false)
        //    {
        //        EnemyCounter++;
        //    }
        //}

        //if(EnemyCounter >= EnemyMaxCount)
        //{
        //    //print("EnemyCountOver");
        //    //this.gameObject.SetActive(false);
        //}
        //else
        //{
        //    EnemyCounter = 0;
        //}
	}

    public void Revive()
    {
        EnemyCounter = 0;
        EnemyMaxCount = Enemies.Length;

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].SetActive(true);
        }

        print("Enemy Respawn!");
    }
}
