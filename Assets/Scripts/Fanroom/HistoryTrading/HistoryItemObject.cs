using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HistoryItemObject : MonoBehaviour
{
    public Text itemName;
    public Text buytime;
    public Text cost;

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
