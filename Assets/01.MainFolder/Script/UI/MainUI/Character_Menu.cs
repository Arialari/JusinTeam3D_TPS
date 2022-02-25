using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Menu : MonoBehaviour
{
    bool _bOnInfo;

    GameObject _CharInfoPanel;
    GameObject _LockedInfoPanel;

    GameObject _Info01;
    GameObject _Info02;
    GameObject _Info03;

    GameObject _Tab01;
    GameObject _Tab02;
    GameObject _Tab03;

    GameObject _SelectedText;
    GameObject _LockedText;
    
    bool _bOnInfo01;
    bool _bOnInfo02;
    bool _bOnInfo03;

    bool _bSelTo01;
    bool _bSelTo02;
    bool _bSelTo03;

    bool _bLockedInfo;

    public bool _bCharChooseCloseButton;


    // Start is called before the first frame update
    void Start()
    {
        _bOnInfo = false;
        
        _CharInfoPanel = GameObject.Find("Character Info Panel");
        _LockedInfoPanel = GameObject.Find("Character Lock Panel");

        _Info01 = GameObject.Find("Character Info Panel/Main BG/INFO_01").gameObject;
        _Info02 = GameObject.Find("Character Info Panel/Main BG/INFO_02").gameObject;
        _Info03 = GameObject.Find("Character Info Panel/Main BG/INFO_03").gameObject;

        _Tab01 = GameObject.Find("Character_Tab01").gameObject;
        _Tab02 = GameObject.Find("Character_Tab02").gameObject;
        _Tab03 = GameObject.Find("Character_Tab03").gameObject;

        _SelectedText = GetComponentInChildren<RectTransform>().GetChild(4).gameObject;
        _LockedText = GetComponentInChildren<RectTransform>().GetChild(5).gameObject;

        _bOnInfo01 = false;
        _bOnInfo02 = false;
        _bOnInfo03 = false;

        _bSelTo01 = true;
        _bSelTo02 = false;
        _bSelTo03 = false;

        _bLockedInfo = false;
        _bCharChooseCloseButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        { 
            if (!_bOnInfo)
                _bOnInfo = true;
            else
                _bOnInfo = false;
        }

        if (_bOnInfo)
            _CharInfoPanel.SetActive(true);
        else
            _CharInfoPanel.SetActive(false);

        if (_bLockedInfo)
            _LockedInfoPanel.SetActive(true);
        else
            _LockedInfoPanel.SetActive(false);
        
        TabAnimatorLogic();
    }

    public void ClickToTab()
    {
        if (!_bOnInfo)
        {
            _bOnInfo = true;
            _bOnInfo01 = true;
            _bOnInfo02 = false;
            _bOnInfo03 = false;
            _Info01.SetActive(true);
            _Info02.SetActive(false);
            _Info03.SetActive(false);
        }
    }

    public void ClickToTab02()
    {
        if (!_bOnInfo)
        {
            _bOnInfo = true;
            _bOnInfo01 = false;
            _bOnInfo02 = true;
            _bOnInfo03 = false;
            _Info01.SetActive(false);
            _Info02.SetActive(true);
            _Info03.SetActive(false);
        }
    }

    public void ClickToTab03()
    {
        if (!_bOnInfo)
        {
            _bOnInfo = true;
            _bOnInfo01 = false;
            _bOnInfo02 = false;
            _bOnInfo03 = true;
        }
    }

    public void ClickToSelectButton()
    {
        // 선택하기 버튼 눌렀을 때 함수
        if (_bOnInfo01)
        {
            _bSelTo01 = true;
            _bSelTo02 = false;
            _bSelTo03 = false;
            _SelectedText.GetComponent<RectTransform>().localPosition = new Vector3(0, -309, 0);
        }

        if (_bOnInfo02)
        {
            _bSelTo01 = false;
            _bSelTo02 = true;
            _bSelTo03 = false;
            _SelectedText.GetComponent<RectTransform>().localPosition = new Vector3(-500, -309, 0);
        }

        if (_bOnInfo03)
        {
            _bSelTo01 = false;
            _bSelTo02 = false;
            _bSelTo03 = true;
            _SelectedText.GetComponent<RectTransform>().localPosition = new Vector3(500, -309, 0);
        }

        _bOnInfo = false;
    }

    public void ClickToReturnButton()
    {
        _bOnInfo = false;
    }

    public void TabAnimatorLogic()
    {

        if (_bSelTo01)
        {
            _bSelTo02 = false;
            _bSelTo03 = false;
        }
            
        else if (_bSelTo02)
        {
            _bSelTo01 = false;
            _bSelTo03 = false;
        }

        else if (_bSelTo03)
        {
            _bSelTo02 = false;
            _bSelTo01 = false;
        }
    }

    public void ClickToLockedText()
    {
        if (!_bLockedInfo)
            _bLockedInfo = true;
    }

    public void ClickToLockedPanel()
    {
        _bLockedInfo = false;
    }

    public void ClickToCharChooseCloseButton()
    {
        _bCharChooseCloseButton = false;
    }
}
