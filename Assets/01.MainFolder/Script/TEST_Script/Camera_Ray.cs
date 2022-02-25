using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ray : MonoBehaviour
{
    private Camera _TestCamera;

    private RaycastHit _RayHit;
    private Ray _Ray;

    public float _fMax_RayDis = 500.0f;

    public GameObject _RayHitObject;

    public string _WeapName;

    private InGameUIMgr _gameUIManager;

    LayerMask _Mask;

    // Start is called before the first frame update
    void Start()
    {
        _TestCamera = Camera.main.GetComponent<Camera>();

        _Mask = LayerMask.GetMask("StaticInteractOBJ") |
            LayerMask.GetMask("DynamicInteractOBJ") |
            LayerMask.GetMask("StaticOBJ") |
            LayerMask.GetMask("TakeWeapOBJ") |
            LayerMask.GetMask("ShopOBJ") |
            LayerMask.GetMask("CanTakeOBJ") |
            LayerMask.GetMask("RideOBJ");

        _gameUIManager = GameObject.Find("GameUI Manager").GetComponent<InGameUIMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        if(null != _RayHit.transform)
            _RayHitObject = _RayHit.transform.gameObject;
        else
            _RayHitObject = null;
    }

    void LateUpdate()
    {
        _Ray = _TestCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(_Ray, out _RayHit, _fMax_RayDis, _Mask))
        {
            Debug.DrawLine(_Ray.origin, _RayHit.point, Color.green);
            //Debug.Log("레이충돌 된 오브젝트 : " + _RayHit.transform.name);
            
            _WeapName = _RayHit.transform.name;
        }
        else
        {
            Debug.DrawRay(_Ray.origin, _Ray.direction * 100.0f, Color.red);
            _gameUIManager.m_InteractionUI.SetActive(false);
        }
    }
}
