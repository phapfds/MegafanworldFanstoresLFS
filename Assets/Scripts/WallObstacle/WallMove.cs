using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  enum WallDir
{
    LeftToRight,
    RightToLeft
}
public class WallMove : MonoBehaviour
{
    public Vector3 left;
    public Vector3 right;
    public WallDir currentDir;
    private void Start()
    {
        left.y = transform.position.y;
        left.z = transform.position.z;
        right.y = transform.position.y;
        right.z = transform.position.z;
        currentDir = (WallDir)Random.Range(0, 2);
    }
    private void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, left) == 0 || Vector3.Distance(transform.position, right) == 0)
        {
            currentDir = currentDir == WallDir.LeftToRight ? WallDir.RightToLeft : WallDir.LeftToRight;
        };
        if (currentDir == WallDir.LeftToRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, right, 0.03f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, left, 0.03f);

        }
    }

}
