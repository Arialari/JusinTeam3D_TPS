using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Menu : MonoBehaviour
{
    public bool _bCloseToMissionMenu;

    // Start is called before the first frame update
    void Start()
    {
        _bCloseToMissionMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseToMissionMenu()
    {
        _bCloseToMissionMenu = false;
    }
}
