using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI partTextDisplay;
    public TMPro.TextMeshProUGUI cost;
    public TMPro.TMP_Dropdown dropDown;


    private CarPartStruct _carData;

    private List<string> GetColorData()
    {
        if (_carData.colors == null) { return null; }


        List<string> colorNames = new List<string>();
        foreach (var c in _carData.colors) 
        {
            colorNames.Add(c.colorName);
        }
        return colorNames;
    }

    private Color getColorFromString(string colorName)
    {
        Debug.Log(colorName);
        foreach (var c in _carData.colors)
        {
            if (colorName == c.colorName) { return c.color; }
        }
        //Error color
        return Color.black;
    }


    public void ParseData(CarPartStruct carPart)
    {
        _carData = carPart;

        partTextDisplay.text = carPart.DisplayPartName;
        cost.text = carPart.price.ToString();

        dropDown.ClearOptions();
        dropDown.AddOptions(GetColorData());

        dropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropDown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMPro.TMP_Dropdown change)
    {
        Color color = getColorFromString(change.options[change.value].text);
        _carData.ChangeColor(color);
    }



}
