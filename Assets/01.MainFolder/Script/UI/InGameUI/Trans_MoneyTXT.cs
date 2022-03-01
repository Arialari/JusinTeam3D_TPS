using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trans_MoneyTXT : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Text>().color.a <= 0f)
        {
            Destroy(gameObject);
        }

        GetComponent<RectTransform>().position += new Vector3(0f, 4f, 0f);

        GetComponent<Text>().color -= new Color(0f, 0f, 0f, Time.deltaTime * 3f);
    }
}
