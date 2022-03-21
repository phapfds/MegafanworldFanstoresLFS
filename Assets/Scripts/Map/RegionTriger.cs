using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RegionType
{
    Left,
    Right
}
public class RegionTriger : MonoBehaviour
{
    [SerializeField] GameObject centerRegion;
    [SerializeField] GameObject rightRegion;
    [SerializeField] GameObject leftRegion;
    [SerializeField] RegionType regionType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            centerRegion.SetActive(true);
            if (leftRegion != null)
                leftRegion.SetActive(regionType == RegionType.Left);
            if (rightRegion != null)
                rightRegion.SetActive(regionType == RegionType.Right);
        }
    }
}
