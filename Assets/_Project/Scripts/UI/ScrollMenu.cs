using System.Collections.Generic;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{


    [SerializeField] MenuItem MenuItem;
    [SerializeField] Transform menuRoot;

    [SerializeField] TMPro.TextMeshProUGUI titleText;
    [SerializeField] TMPro.TextMeshProUGUI priceText;
    [SerializeField] TMPro.TextMeshProUGUI specificationText;


    private List<MenuItem> menuItems = new List<MenuItem>();

    private Car activeCar;

    private void Awake()
    {
        CarChanger.onCarChange += LoadCarData;
    }

    private void OnDisable()
    {
        CarChanger.onCarChange -= LoadCarData;
    }


    private void LoadCarData(Car car)
    {
        if (menuItems != null) { ClearMenuItems(); }
        activeCar = car;

        titleText.text = car.CarDisplayName;

        foreach (CarPart part in car.parts)
        {
            MenuItem item = Instantiate(MenuItem);
            item.ParseData(part, this);
            item.transform.SetParent(menuRoot);
            menuItems.Add(item);
        }
        UpdatePrice();
        specificationText.text = car.specifications.PrintSpecifications();

    }

    public void UpdatePrice()
    {
        float total = 0.0f;
        foreach (CarPart part in activeCar.parts)
        {
            total += part.activeColor.price;
        }
        priceText.text = total.ToString();
    }

    private void ClearMenuItems()
    {
        foreach(MenuItem item in menuItems)
        {
            Destroy(item.gameObject);
        }
        menuItems.Clear();
    }

}
