using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArtManager : MonoBehaviour
{
    public List<GameObject> swapIcons;
    public GameObject currentArtToChange;
    public static MovingArtManager inst;
    public bool canSwap;
    public List<MeshRenderer> frames;
    private void Awake()
    {
        inst = this;
        DisableSwapIcons();
        canSwap = true;
    }
    public void DisableSwapIcons()
    {
        foreach (GameObject swi in swapIcons)
        {
            if (swi != null)
                swi.SetActive(false);

        }
        foreach (MeshRenderer meshRenderer in frames)
        {
            if (meshRenderer != null)
                meshRenderer.enabled = false;
        }
    }

    public void EnableSwapIcons()
    {
        foreach (GameObject swi in swapIcons)
        {
            if(swi!=null)
                swi.SetActive(true);
        }
        foreach (MeshRenderer meshRenderer in frames)
        {
            if (meshRenderer != null)
                meshRenderer.enabled = true;
        }
    }

}
