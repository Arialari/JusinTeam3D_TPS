using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Menu : MonoBehaviour
{
    public bool _bCloseToEquipMenu;
    // Start is called before the first frame update
    void Start()
    {
        _bCloseToEquipMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseToEquipMenu()
    {
        _bCloseToEquipMenu = false;
    }
}
