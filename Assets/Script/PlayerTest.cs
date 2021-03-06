﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTest : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent Agent;
    CharacterController Controller;
    public Transform Target;

    public GameObject EnemySpot;
    public GameObject EnemyArrow;
    
    private Vector3 targetPosOnScreen;
    private Vector3 center;
    private float angle;
    private float coef;
    private int edgeLine;
    private float degreeRange;

    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        EnemySpot.gameObject.SetActive(true);
        EnemyArrow.gameObject.SetActive(false);

        Agent.destination = new Vector3(Target.transform.position.x,0,Target.transform.position.z);


    }
	
	// Update is called once per frame
	void Update () {
        //print(Controller.isGrounded);
        targetPosOnScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        // 화면 밖 처리
        if (targetPosOnScreen.x > Screen.width || targetPosOnScreen.x < 0 || targetPosOnScreen.y > Screen.height || targetPosOnScreen.y < 0)
        {
            EnemySpot.gameObject.SetActive(false);
            EnemyArrow.gameObject.SetActive(true);

            center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;

            if (Screen.width > Screen.height)
                coef = Screen.width / Screen.height;
            else
                coef = Screen.height / Screen.width;

            degreeRange = 360f / (coef + 1);


            if (angle < 0)
                angle = angle + 360;

            if (angle < degreeRange / 4f)
                edgeLine = 0;
            else if (angle < 180 - degreeRange / 4f)
                edgeLine = 1;
            else if (angle < 180 + degreeRange / 4f)
                edgeLine = 2;
            else if (angle < 360 - degreeRange / 4f)
                edgeLine = 3;
            else
                edgeLine = 0;

            if (targetPosOnScreen.x > Screen.width)
            {
                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(-40, 0, 45));
                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
            }
            else if (targetPosOnScreen.x < 0)
            {
                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(40, 0, 45));
                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
            }
            else if (targetPosOnScreen.y > Screen.height)
            {
                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, -40, 45));
                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
            }
            else if (targetPosOnScreen.y < 0)
            {
                EnemyArrow.gameObject.transform.position = Camera.main.ScreenToWorldPoint(intersect(edgeLine, center, targetPosOnScreen) + new Vector3(0, 40, 45));
                EnemyArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, angle);
            }
        }
        else
        {
            
            EnemySpot.gameObject.SetActive(true);
            EnemyArrow.gameObject.SetActive(false);
        }

 
        //Controller.Move(new Vector3(1, 0, 1) * Time.deltaTime);
	}

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "TestTag")
        {
            print("FSAHSFSEFSWF");
        }
    }

    Vector3 intersect(int edgeLine, Vector3 line2point1, Vector3 line2point2)
    {
        float[] A1 = { -Screen.height, 0, Screen.height, 0 };
        float[] B1 = { 0, -Screen.width, 0, Screen.width };
        float[] C1 = { -Screen.width * Screen.height, -Screen.width * Screen.height, 0, 0 };

        float A2 = line2point2.y - line2point1.y;
        float B2 = line2point1.x - line2point2.x;
        float C2 = A2 * line2point1.x + B2 * line2point1.y;

        float det = A1[edgeLine] * B2 - A2 * B1[edgeLine];

        return new Vector3((B2 * C1[edgeLine] - B1[edgeLine] * C2) / det, (A1[edgeLine] * C2 - A2 * C1[edgeLine]) / det, 0);
    }
}
