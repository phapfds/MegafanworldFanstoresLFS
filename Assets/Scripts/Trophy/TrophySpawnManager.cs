using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrophySpawnManager : MonoBehaviour
{
    public static TrophySpawnManager Ins { get; private set; }
    [SerializeField] List<Transform> pointSpawns;
    [SerializeField] List<Trophy> trophies;
    public List<Trophy> currenItems = new List<Trophy>();
    public void Awake()
    {
        Ins = this;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 5; i++)
        {
            int point = Random.Range(0, pointSpawns.Count);
            int trophy = Random.Range(0, trophies.Count);
            Trophy item = Instantiate(trophies[trophy], pointSpawns[point].transform);
            currenItems.Add(item);
            pointSpawns.RemoveAt(point);
            trophies.RemoveAt(trophy);
        }
    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(1);
        if (InGameManager.Instance.IngameType == IngameType.OutsideStadium && currenItems.Count == 2)
        {
            InGameManager.Instance.Endgame?.Invoke();
        }
    }
}
