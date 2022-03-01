using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        transform.TryGetComponent(out mesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Breach()
    {
        Vector3[] vertices = mesh.vertices;
        
    }
}
