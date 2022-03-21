using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPort : MonoBehaviour
{
    private bool playerNotYetEnter = true;
    [SerializeField] ParticleSystem effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            if (playerNotYetEnter)
            {
                playerNotYetEnter = false;
                effect.gameObject.SetActive(true);
                effect.Play();
                InGameManager.Instance.IngameState = IngameState.EnterKeyport;
                StartCoroutine(Destroy());
            }
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
    //private IEnumerator Start()
    //{
    //    //yield return new WaitForSecondsRealtime(30);
    //    //Destroy(gameObject);
    //}
    private IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(1f);
        Waypoint.keyport = null;
        Destroy(gameObject);
    }

}
