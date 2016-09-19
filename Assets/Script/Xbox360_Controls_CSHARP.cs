using UnityEngine;
using System.Collections;

public class Xbox360_Controls_CSHARP : MonoBehaviour {

    public float PlayerMovementSpeed = 10;

    public float PlayerRotationSpeed = 90;

    private bool XaxisInUse = false;
    private bool YaxisInUse = false;

    void Update()
    {

        Movement();

        UserInputs();

    }

    void Movement()
    {

        //transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * PlayerMovementSpeed);

        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PlayerMovementSpeed, 0, 0);

        //transform.Rotate(0, Input.GetAxis("RightStick") * Time.deltaTime * PlayerRotationSpeed, 0);

    }



    void UserInputs()
    {

        if (Input.GetButtonDown("P1_360_AButton"))
        {

            Vector3 vc = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, this.transform.position.z);

            this.gameObject.transform.position = vc;

            Debug.Log("A Button!");

        }

        if (Input.GetButtonDown("P1_360_BButton"))
        {

            Vector3 vc = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, this.transform.position.z);

            this.gameObject.transform.position = vc;

            Debug.Log("B Button!");

        }

        if (Input.GetButtonDown("P1_360_XButton"))
        {

            Debug.Log("X Button!");

        }

        if (Input.GetButtonDown("P1_360_YButton"))
        {

            Debug.Log("Y Button!");

        }

        if (Input.GetButtonDown("P1_360_LeftBumper"))
        {

            Debug.Log("Left Bumper!");

        }

        if (Input.GetButtonDown("P1_360_RightBumper"))
        {

            Debug.Log("Right Bumper!");

        }

        if (Input.GetButtonDown("P1_360_BackButton"))
        {

            Debug.Log("Back Button!");

        }

        if (Input.GetButtonDown("P1_360_StartButton"))
        {

            Debug.Log("Start Button!");

        }

        if (Input.GetButtonDown("P1_360_LeftThumbStickButton"))
        {

            Debug.Log("Left Thumbstick Button!");

        }

        if (Input.GetButtonDown("P1_360_RightThumbStickButton"))
        {

            Debug.Log("Right Thumbstick Button!");

        }




        if (Input.GetAxis("P1_360_Trigger") > 0.001)
        {

            Debug.Log("Right Trigger!");

        }

        if (Input.GetAxis("P1_360_Trigger") < 0)
        {

            Debug.Log("Left Trigger!");

        }

        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 1)
        {

            if (XaxisInUse == false)
            {
                XaxisInUse = true;

                Debug.Log("Right D-PAD Button!");
            }

        }

        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == -1)
        {
            if (XaxisInUse == false)
            {
                XaxisInUse = true;

                Debug.Log("Left D-PAD Button!");
            }

        }

        if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 1)
        {
            if (YaxisInUse == false)
            {
                YaxisInUse = true;

                Debug.Log("Up D-PAD Button!");
            }
        }

        if (Input.GetAxisRaw("P1_360_VerticalDPAD") == -1)
        {
            if (YaxisInUse == false)
            {
                YaxisInUse = true;

                Debug.Log("Down D-PAD Button!");
            }

        }


        if (Input.GetAxisRaw("P1_360_VerticalDPAD") == 0)
        {
            YaxisInUse = false;
        }

        if (Input.GetAxisRaw("P1_360_HorizontalDPAD") == 0)
        {
            XaxisInUse = false;
        }

        if (Input.GetAxisRaw("P1_360_LeftThumbStick") == 1)
        {
            
            print("Right");
        }

        if (Input.GetAxisRaw("P1_360_LeftThumbStick") == -1)
        {
            print("Left");
        }

        if (Input.GetAxisRaw("P1_360_UpThumbStick") == 1)
        {
            print("down");
        }

        if (Input.GetAxisRaw("P1_360_UpThumbStick") == -1)
        {
            print("Up");
            
        }


        if (Input.GetAxis("P1_360_RightStick") > 0.19)
        {

            Debug.Log("RightStick!");

            this.gameObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);

        }

        if (Input.GetAxis("P1_360_RightStick") < 0)
        {

            Debug.Log("LeftStick!");

            this.gameObject.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);

        }

        if (Input.GetAxis("P1_360_UpStick") < 0)
        {

            Debug.Log("UpStick!");

            this.gameObject.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);

        }

        if (Input.GetAxis("P1_360_UpStick") > 0.19)
        {

            Debug.Log("DownStick!");

            this.gameObject.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);

        }

    }
}
