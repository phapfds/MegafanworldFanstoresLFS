using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingBeer : MonoBehaviour
{
    [SerializeField] GameObject beerGlass;
    [SerializeField] GameObject bembel;
    private void OnEnable()
    {
        bembel.SetActive(SelectCityView.city == City.Frankfurt);
        beerGlass.SetActive(SelectCityView.city != City.Frankfurt);
    }
}
