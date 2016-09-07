using UnityEngine;
using System.Collections;

public class UICameraSetting : MonoBehaviour {

    public RenderTexture Render_Target;
    private Camera UICamera;

    void Awake()
    {
        UICamera = GetComponent<Camera>();

        UICamera.targetTexture = Render_Target;
    }

}
