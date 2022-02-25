using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrenades : MonoBehaviour {

    public float rotSpeed = 1;

    private void OnMouseDrag()
    {
        float y = -Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.right, y);
        float x = -Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, x);
    }
}
