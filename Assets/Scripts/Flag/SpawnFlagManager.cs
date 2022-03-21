
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlagManager : MonoBehaviour
{
    [SerializeField] List<Transform> posSpawnFlags;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            List<int> positionsSelcted = new List<int>();
            for (var i = 0; i <= 3; i++)
            {
                var x = Random.Range(0, posSpawnFlags.Count);
                while (positionsSelcted.Contains(x) == true)
                {
                    x = Random.Range(0, posSpawnFlags.Count);
                }
                positionsSelcted.Add(x);
            }
            foreach (int i in positionsSelcted)
            {
                FlagManager.Instance.SpawnWindowFlag(posSpawnFlags[i].position);
            }
            Destroy(this);
        }
    }
}
