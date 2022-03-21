using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public List<GameObject> gameObjects;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ob in gameObjects)
        {
            if (InGameManager.Instance.IngameState == IngameState.DrinkBeer)
            {
                if (ob.activeSelf) ob.SetActive(false);
            }
            else
            {
                if (!ob.activeSelf) ob.SetActive(true);
            };
        }
    }
}
