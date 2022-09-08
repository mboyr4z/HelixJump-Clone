using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoSingleton<MaterialManager>
{

    [SerializeField] private Material goodMaterial;
    [SerializeField] private Material badMaterial;

    public Material GetGoodMaterial()
    {
        return goodMaterial;
    }


    public Material GetBadMaterial()
    {
        return badMaterial;
    }

    public void SetColorsOfMaterials(Color goodMaterialcolor, Color badMaterialColor)
    {
        goodMaterial.color = goodMaterialcolor;
        badMaterial.color = badMaterialColor;
    }
}
