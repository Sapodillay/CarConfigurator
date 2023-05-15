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


    private CarPart _carData;

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

    private PartColor getColorPartFromString(string colorName)
    {
        foreach (var c in _carData.colors)
        {
            if (colorName == c.colorName) { return c; }
        }
        return _carData.colors[0];
    }


    public void ParseData(CarPart carPart, ScrollMenu menu)
    {
        _carData = carPart;

        partTextDisplay.text = carPart.DisplayPartName;

        //Set starting display price to default color (first element)
        cost.text = carPart.colors[0].price.ToString();
        carPart.SetActive(carPart.colors[0]);

        dropDown.ClearOptions();
        dropDown.AddOptions(GetColorData());

        dropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropDown, carPart);
            menu.UpdatePrice();
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMPro.TMP_Dropdown change, CarPart carPart)
    {
        PartColor color = getColorPartFromString(change.options[change.value].text);
        _carData.ChangeColor(color.color);
        carPart.SetActive(color);
        cost.text = carPart.activeColor.price.ToString();
    }


}
