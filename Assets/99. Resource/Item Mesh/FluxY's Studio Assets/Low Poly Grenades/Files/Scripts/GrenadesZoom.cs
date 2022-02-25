using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadesZoom : MonoBehaviour {

    public float zoomChangeAmount = 80f;
    public float zoom = 60f;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = zoom;
    }

    private void Update()
    {
        HandleZoom();
    }

    void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, 25, 60);

        cam.fieldOfView = zoom;
    }
}
