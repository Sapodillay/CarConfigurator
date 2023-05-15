using System;
using UnityEngine;

[Serializable]
public enum CarPartType
{
    Colorable,
    Swappable
}

[Serializable]
public struct PartColor
{
    [SerializeField] public Color color;
    [SerializeField] public string colorName;
}


[Serializable]
public struct CarPartStruct
{
    [SerializeField] public string DisplayPartName;
    [SerializeField] public float price;
    [SerializeField] public PartColor[] colors;
    [SerializeField] public MeshRenderer PartMesh;

    public void ChangeColor(Color color)
    {
        PartMesh.sharedMaterial.color = color;
    }

}

/// <summary>
/// Object to map data to the car
/// </summary>
[CreateAssetMenu(menuName = "Car object")]
public class CarObject : ScriptableObject
{

    [SerializeField] public string CarDisplayName;
    [SerializeField] public GameObject Car;
    [SerializeField] public CarPartStruct[] parts;



}
