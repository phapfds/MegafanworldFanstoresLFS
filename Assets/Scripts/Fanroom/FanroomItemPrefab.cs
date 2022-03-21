using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanroomItemPrefab : MonoBehaviour
{
    public ItemType item;
    public bool needToSave;
    private void OnEnable()
    {
        if (ViewsManager.Instance.currentView.viewType == ViewType.FanroomView)
        {
            needToSave = true;
            if (PlayerPrefs.GetString(((ItemType)item).ToString()) != null)
                gameObject.transform.localPosition = StringToVector3(PlayerPrefs.GetString(((ItemType)item).ToString()));
        }


    }
    private void OnDestroy()
    {
        if (needToSave)
            PlayerPrefs.SetString(((ItemType)item).ToString(), gameObject.transform.localPosition.ToString());
    }

    private void OnDisable()
    {
        if (needToSave)
            PlayerPrefs.SetString(((ItemType)item).ToString(), gameObject.transform.localPosition.ToString());
    }

    public Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
