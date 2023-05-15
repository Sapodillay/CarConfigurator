using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;

public class CarChanger : MonoBehaviour
{
    /// <summary>
    /// List of cars
    /// </summary>
    [SerializeField] Car[] cars;
    int carIndex;

    Transform carSpawn;

    private Car carGameObject;

    static public Action<Car> onCarChange;



    private void Start()
    {
        carIndex = 0;
        LoadCar(cars[carIndex]); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PrintOutAllCars();
        }
    }


    public void PreviousCar()
    {
        if (carIndex == 0)
        {
            carIndex = cars.Length - 1;
        }
        else
        {
            carIndex--;
        }
        LoadCar(cars[carIndex]);
    }

    public void NextCar()
    {
        if (carIndex == cars.Length - 1)
        {
            carIndex = 0;
        }
        else
        {
            carIndex++;
        }
        LoadCar(cars[carIndex]);
    }



    private void LoadCar(Car carPrefab)
    {

        if (carGameObject != null) { Destroy(carGameObject.gameObject); }


        carGameObject = Instantiate(carPrefab);
        carGameObject.transform.SetParent(carSpawn);
        carGameObject.transform.position = Vector3.zero;
        onCarChange?.Invoke(carGameObject);
    }

    //this is a function so can easily change how this information is displayed in the future.
    private void CarPrint(string text)
    {
        Debug.Log("<color=orange>" + text + "</color>");
    }


    private void PrintOutAllCars()
    {

        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < cars.Length; i++)
        {
            Car car = cars[i];
            CarPrint("Car model: " + car.name);
            CarPrint("===Parts===");

            foreach(CarPart part in car.parts)
            {
                CarPrint(part.DisplayPartName);
                CarPrint("   Options:");

                foreach(PartColor partColor in part.colors)
                {
                    CarPrint("      Name: " + partColor.colorName);
                    CarPrint("     Price: " + partColor.price.ToString());
                }
            }
        }
    }
}
