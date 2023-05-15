using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{


    [SerializeField] MenuItem MenuItem;
    [SerializeField] Transform menuRoot;

    [SerializeField] CarObject carData;

    private void Awake()
    {
        foreach(CarPartStruct part in carData.parts)
        {
            MenuItem item = Instantiate(MenuItem);
            item.ParseData(part);
            item.transform.SetParent(menuRoot);
        }
    }
}
