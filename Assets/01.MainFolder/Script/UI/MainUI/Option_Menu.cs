using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Menu : MonoBehaviour
{
    public bool _bCloseToOptionMenu;

    // Start is called before the first frame update
    void Start()
    {
        _bCloseToOptionMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseToOptionMenu()
    {
        _bCloseToOptionMenu = false;
    }
}
