using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCut : MonoBehaviour
{
    public Terrain t;
    [SerializeField]
    protected int[,] backupMap;

    private void Awake()
    {
        CreateBackup(t);
    }
    // Set all pixels in a detail map below a certain threshold to zero.

    private void Start()
    {
        StartCoroutine( DetailMapCutoff(t,0,0.1f));
    }
    IEnumerator DetailMapCutoff(Terrain t, int on, float time)
    {
        Debug.Log("Width :" + t.terrainData.detailWidth);
        Debug.Log("Width :" + t.terrainData.detailHeight);

        // Get all of layer zero.
        var map = t.terrainData.GetDetailLayer(0, 0, t.terrainData.detailWidth, t.terrainData.detailHeight, 0);
        // For each pixel in the detail map...
        for (int x = 1022 / 2; x > 0; x--)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Cut");
            for (int y = 512 - 15; y < 512 + 15; y++)
            {

                map[x, y] = on;

            }
            t.terrainData.SetDetailLayer(0, 0, 0, map);

        }
        t.terrainData.SetDetailLayer(0, 0, 0, map);


    }
    void CreateBackup(Terrain t)
    {
        backupMap = t.terrainData.GetDetailLayer(0, 0, t.terrainData.detailWidth, t.terrainData.detailHeight, 0);
    }

    void OnDestroy()
    {
        t.terrainData.SetDetailLayer(0, 0, 0, backupMap);
    }

    private void OnDisable()
    {
        t.terrainData.SetDetailLayer(0, 0, 0, backupMap);

    }
}
