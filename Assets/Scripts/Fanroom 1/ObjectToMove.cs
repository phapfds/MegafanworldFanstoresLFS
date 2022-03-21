using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToMove : MonoBehaviour
{
    public bool canMove;
    private void Awake()
    {
        canMove = false;
        if (GetComponent<DragObject>() == null)
        {
            string vector3_ = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name);
            if (vector3_ != null && vector3_ != "")
                transform.localPosition = StringToVector3(vector3_);

            string rotation_ = PlayerPrefs.GetString(UserInfoManager.Instance.userInfo.userID + gameObject.name + "rotation");
            //Debug.Log(UserInfoManager.Instance.userInfo.userID + gameObject.name + "rotation");
            if (rotation_ != null && rotation_ != "")
                transform.eulerAngles = StringToVector3(rotation_);
        }
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

    private void Update()
    {
        if (transform.position.y < -5)
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
    }
}
