using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrophyType
{
    Ball,
    Shoes,
    Cup
}
public class TrophysManager : MonoBehaviour
{
    [SerializeField] List<Trophy> trophys;
    [SerializeField] GameObject player;
    IEnumerator Start()
    {
        foreach (Trophy trophy in trophys)
        {
            yield return new WaitForSeconds(Random.Range(20, 35));
            Trophy trop = Instantiate(trophy, player.transform.position + Vector3.right * 7, Quaternion.identity);
            trop.transform.position = new Vector3(trop.transform.position.x, trop.transform.position.y, 170);
            if (trophy.trophyType == TrophyType.Ball)
                trop.transform.position += Vector3.up;

        }
    }
}
