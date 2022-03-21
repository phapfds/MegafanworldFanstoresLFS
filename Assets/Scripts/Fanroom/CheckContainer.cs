using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckContainer : MonoBehaviour
{

    RaycastHit raycastHit;
    public void Start()
    {
        Check();
    }
    public void Check()
    {
        Physics.Raycast(transform.position, Vector3.down, out raycastHit);
        if (raycastHit.collider != null)
            if (raycastHit.collider.gameObject.tag.Contains("Shelf"))
                transform.SetParent(raycastHit.collider.transform);
    }

    private void Update()
    {
        if (transform.parent == null)
        {
            Debug.LogError("NO PARENT");
            Check();
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down);
    }
    public void SavePosition()
    {
        PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + gameObject.name, gameObject.transform.position.ToString());
        PlayerPrefs.SetString(UserInfoManager.Instance.userInfo.userID + gameObject.name + "rotation", gameObject.transform.eulerAngles.ToString());
    }
}
