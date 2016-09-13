using UnityEngine;
using System.Collections;

public class Tracing : MonoBehaviour {

    public LineRenderer lineRenderer;
    public float rotationSpeed = 4.04f;

    public float normalMoveSpeed = 10;
    public float FastMoveSpeed = 40;

    private float AimInterPol = 0.0f;

    private ViewControllMode ViewMode;
    private GameState Gamestate;

    private Vector3 targetPoint = Vector3.zero;
    private Vector3 center = Vector3.zero;
    private Vector3 theArc = Vector3.zero;


    public GameObject AimTarget;

    public GameObject CannonBall;

    void Start()
    {
        lineRenderer.SetColors(Color.red, Color.black);
        lineRenderer.SetWidth(0.3f, 0.3f);
        lineRenderer.SetVertexCount(25);

        AimInterPol = 0.0f;

        ViewMode = GameManager.ViewMode;
        Gamestate = GameManager.Gamestate;
    }

    void Update()
    {
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
                    switch (ViewMode)
                    {
                        case ViewControllMode.Mouse:
                            {

                                // 마우스 작업

                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    Instantiate(CannonBall, this.transform.position, Quaternion.identity);
                                }

                                //if (null == Camera.main)
                                //    return;

                                // Generate a plane that intersects the transform's position with an upwards normal.
                                //Plane playerPlane = new Plane(Vector3.up, transform.position);

                                // Generate a ray from the cursor position
                                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                //Ray ray = Camera.main.scree(AimTarget.transform.position);

                                // Determine the point where the cursor ray intersects the plane.
                                // This will be the point that the object must look towards to be looking at the mouse.
                                // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
                                //   then find the point along that ray that meets that distance.  This will be the point
                                //   to look at.
                                //float hitdist = 0.0f;

                                // If the ray is parallel to the plane, Raycast will return false.
                                //Vector3 targetPoint = Vector3.zero;

                                //if (playerPlane.Raycast(ray, out hitdist))
                                //{

                                //    // Get the point along the ray that hits the calculated distance.			
                                //    targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                //    // Draw the arc trajectory		
                                //    center = (transform.position + targetPoint) * 0.5f;
                                //    center.y -= 70.0f;

                                //    // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                //    Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                //    // Smoothly rotate towards the target point.
                                //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                //    // shorten the ray if it's hitting some obstacle:
                                //    //RaycastHit hitInfo;

                                //    targetPoint = transform.position;

                                //    //if (Physics.Linecast(transform.position, targetPoint, out hitInfo))
                                //    //{
                                //    //    targetPoint = hitInfo.point;

                                //    //}
                                //}
                                //else
                                //{
                                //    targetPoint = transform.position;
                                //}

                                // Get the point along the ray that hits the calculated distance.			
                                targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                // Draw the arc trajectory		
                                center = (transform.position + targetPoint) * 0.5f;
                                center.y -= 18.0f + AimInterPol;

                                if (Input.GetKey(KeyCode.DownArrow))
                                {
                                    if (center.y <= -0.2)
                                        AimInterPol += 1.0f * Time.deltaTime;
                                }
                                if (Input.GetKey(KeyCode.UpArrow))
                                {
                                    if (center.y <= 1) 
                                        AimInterPol -= 1.0f * Time.deltaTime;

                                }

                                //print(" center.y  : " + center.y);


                                // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                // Smoothly rotate towards the target point.

                                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                // shorten the ray if it's hitting some obstacle:
                                //RaycastHit hitInfo;

                                //targetPoint = transform.position;

                                Vector3 RelCenter = transform.position - center;
                                Vector3 aimRelCenter = targetPoint - center;




                                // Draw the arc line starting from the launcher
                                for (float index = 0.0f, interval = -0.0417f; interval < 1.0f; )
                                {
                                    theArc = Vector3.Slerp(RelCenter, aimRelCenter, interval += 0.0417f);
                                    lineRenderer.SetPosition((int)index++, theArc + center);
                                }
                            }
                            break;

                        case ViewControllMode.GamePad:
                            {
                                // 게임 패드 작업

                                if (Input.GetAxisRaw("P1_360_RightStick") == 1)
                                {

                                    Debug.Log("RightStick!");

                                    //rotationX +=  cameraSensitivity * Time.deltaTime;
                                    transform.position += transform.right * FastMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P1_360_RightStick") == -1)
                                {

                                    Debug.Log("LeftStick!");


                                    //rotationX -= cameraSensitivity * Time.deltaTime;
                                    //transform.position -= transform.forward * normalMoveSpeed * Time.deltaTime;
                                    transform.position -= transform.right * FastMoveSpeed * Time.deltaTime;
                                }

                                if (Input.GetAxisRaw("P1_360_UpStick") == -1)
                                {

                                    Debug.Log("UpStick!");

                                    //rotationY += cameraSensitivity * Time.deltaTime;
                                    transform.position += new Vector3(0, 0, 1) * FastMoveSpeed * Time.deltaTime;

                                }

                                if (Input.GetAxisRaw("P1_360_UpStick") == 1)
                                {

                                    Debug.Log("DownStick!");

                                    //rotationY -= cameraSensitivity * Time.deltaTime;
                                    transform.position -= new Vector3(0, 0, 1) * FastMoveSpeed * Time.deltaTime;
                                }

                                //if (null == Camera.main)
                                //    return;

                                // Generate a plane that intersects the transform's position with an upwards normal.
                                //Plane playerPlane = new Plane(Vector3.up, transform.position);

                                // Generate a ray from the cursor position
                                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                //Ray ray = Camera.main.scree(AimTarget.transform.position);

                                // Determine the point where the cursor ray intersects the plane.
                                // This will be the point that the object must look towards to be looking at the mouse.
                                // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
                                //   then find the point along that ray that meets that distance.  This will be the point
                                //   to look at.
                                //float hitdist = 0.0f;

                                // If the ray is parallel to the plane, Raycast will return false.
                                //Vector3 targetPoint = Vector3.zero;

                                //if (playerPlane.Raycast(ray, out hitdist))
                                //{

                                //    // Get the point along the ray that hits the calculated distance.			
                                //    targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                //    // Draw the arc trajectory		
                                //    center = (transform.position + targetPoint) * 0.5f;
                                //    center.y -= 70.0f;

                                //    // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                //    Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                //    // Smoothly rotate towards the target point.
                                //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                //    // shorten the ray if it's hitting some obstacle:
                                //    //RaycastHit hitInfo;

                                //    targetPoint = transform.position;

                                //    //if (Physics.Linecast(transform.position, targetPoint, out hitInfo))
                                //    //{
                                //    //    targetPoint = hitInfo.point;

                                //    //}
                                //}
                                //else
                                //{
                                //    targetPoint = transform.position;
                                //}

                                // Get the point along the ray that hits the calculated distance.			
                                targetPoint = AimTarget.transform.position;//ray.GetPoint(hitdist);

                                // Draw the arc trajectory		
                                center = (transform.position + targetPoint) * 0.5f;
                                center.y -= 70.0f;

                                // Determine the target rotation.  This is the rotation if the transform looks at the middle between the target object and your object.

                                Quaternion targetRotation = Quaternion.LookRotation(center - transform.position);

                                // Smoothly rotate towards the target point.
                                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                                // shorten the ray if it's hitting some obstacle:
                                //RaycastHit hitInfo;

                                //targetPoint = transform.position;

                                Vector3 RelCenter = transform.position - center;
                                Vector3 aimRelCenter = targetPoint - center;

                                // Draw the arc line starting from the launcher
                                for (float index = 0.0f, interval = -0.0417f; interval < 1.0f; )
                                {
                                    theArc = Vector3.Slerp(RelCenter, aimRelCenter, interval += 0.0417f);
                                    lineRenderer.SetPosition((int)index++, theArc + center);
                                }

                            }
                            break;
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
