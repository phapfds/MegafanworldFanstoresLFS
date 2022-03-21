using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USCityVersion : MonoBehaviour
{
    public GameObject nextCity;
    public GameObject previousCity;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            if (nextCity != null)
            {
                if (PlayerDirection.DotBetweenForwardDirectionAndRightVector() > 0)
                {
                        nextCity.SetActive(true);
                }
                else
                {
                        nextCity.SetActive(false);
                }
            };
            if (previousCity != null)
            {
                if (PlayerDirection.DotBetweenForwardDirectionAndRightVector() > 0)
                {
                    previousCity.SetActive(false);
                }
                else
                {
                    previousCity.SetActive(true);
                }
            }
        }
    }
}
