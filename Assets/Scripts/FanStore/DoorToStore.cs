using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorToStore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag.Contains("Player"))
        //{
        //    InGameManager.Instance.IngameState = IngameState.FanStore;
        //    other.transform.position = new Vector3(this.gameObject.transform.position.x, other.transform.position.y, 160); ;
        //    SceneManager.LoadScene("FanStore", LoadSceneMode.Additive);
        //}
    }
}
