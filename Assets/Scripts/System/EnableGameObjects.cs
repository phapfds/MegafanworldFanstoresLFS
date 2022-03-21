using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjects : MonoBehaviour
{
    public List<GameObject> gameObjects;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(20);
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
}
