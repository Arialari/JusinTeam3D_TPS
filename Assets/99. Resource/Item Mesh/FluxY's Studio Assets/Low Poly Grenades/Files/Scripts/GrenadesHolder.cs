using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadesHolder : MonoBehaviour
{
    public GameObject[] grenades;
    private int index;

    void Start()
    {
        grenades = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            grenades[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in grenades)
            go.SetActive(false);

        if (grenades[0])
            grenades[0].SetActive(true);
    }

    public void ToogleLeft()
    {
        grenades[index].SetActive(false);

        index--;
        if (index < 0)
            index = grenades.Length - 1;

        grenades[index].SetActive(true);
    }

    public void ToogleRight()
    {
        grenades[index].SetActive(false);

        index++;
        if (index == grenades.Length)
            index = 0;

        grenades[index].SetActive(true);
    }
}
