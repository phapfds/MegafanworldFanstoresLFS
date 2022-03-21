using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disnable : MonoBehaviour
{
    [SerializeField] float seconds;
    private IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(DisableAfterSeconds());
    }
}
