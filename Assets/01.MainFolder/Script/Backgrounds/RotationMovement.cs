using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_fSpeed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, m_fSpeed, 0);
    }
}
