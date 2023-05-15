using System;
using System.Text;
using UnityEngine;




[Serializable]
public class PartColor
{
    [SerializeField] public Color color;
    [SerializeField] public string colorName;
    [SerializeField] public float price;
}


[Serializable]
public class CarPart
{
    [SerializeField] public string DisplayPartName;
    [SerializeField] public PartColor[] colors;
    public PartColor activeColor;
    [SerializeField] public MeshRenderer PartMesh;

    private MaterialPropertyBlock m_MaterialPropertyBlock;


    public void ChangeColor(Color color)
    {
        m_MaterialPropertyBlock= new MaterialPropertyBlock();
        m_MaterialPropertyBlock.SetColor("_BaseColor", color);
        PartMesh.SetPropertyBlock(m_MaterialPropertyBlock);
    }

    public void SetActive(PartColor color)
    {
        activeColor = color;
    }

}

[Serializable]
public struct CarSpecifications
{

    public string EngineType;
    public string Displacement;
    public string Horsepower;
    public string Torque;


    public string PrintSpecifications()
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("Engine type: " + EngineType + "<br>");
        sb.Append("Displacement: " + Displacement + "<br>");
        sb.Append("Horsepower: " + Horsepower + "<br>");
        sb.Append("Torque: " + Torque + "<br>");

        return sb.ToString();
    }

}


public class Car : MonoBehaviour
{

    [SerializeField] public string CarDisplayName;
    [SerializeField] public CarPart[] parts;

    [SerializeField] public CarSpecifications specifications;

}
