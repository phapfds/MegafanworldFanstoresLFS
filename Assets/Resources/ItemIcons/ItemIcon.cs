using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemIcon : MonoBehaviour
{
    [SerializeField] string nameItem;
    public GameObject gameObject_;

    public void Awake()
    {
        if (nameItem != null)
            gameObject_ = GameObject.Find(nameItem);
    }
    IEnumerator Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (gameObject_ != null)
                gameObject_.SetActive(!gameObject_.activeSelf);
            PlayerPrefs.SetString("ItemIcon" + gameObject_.name, gameObject_.activeSelf.ToString());
            Debug.Log(gameObject_.activeSelf.ToString());
            LightManage.ins.CheckLight();

            if (!gameObject_.activeSelf)
            {
                ColorBlock colorBlock = GetComponent<Button>().colors;
                colorBlock.normalColor = Color.grey;
                GetComponent<Button>().colors = colorBlock;
            }
            else
            {
                ColorBlock colorBlock = GetComponent<Button>().colors;
                colorBlock.normalColor = Color.white;
                GetComponent<Button>().colors = colorBlock;
            }
        });
        yield return new WaitForSeconds(0.01f);
        string status = PlayerPrefs.GetString("ItemIcon" + gameObject_.name);
        Debug.Log(status);
        if (status == "False")
        {
            gameObject_.SetActive(false);
        }
        LightManage.ins.CheckLight();

    }
}
