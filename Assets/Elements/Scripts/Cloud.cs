using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cloud
{
    public GameObject visibleCloud { get; private set; }
    public int positiveCharge { get; set; }
    public int negativeCharge { get; set; }
    public List<Material> materials { get; private set; }
    public Cloud(GameObject cloud, int pos, int neg, List<Material> factoryMaterials)
    {
        visibleCloud = cloud;
        positiveCharge = pos;
        negativeCharge = neg;
        materials = factoryMaterials;
    }

    public void SetMaterial(int index)
    {
        visibleCloud.GetComponent<Renderer>().material = materials[index];
    }
}