using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovie : MonoBehaviour
{
    [SerializeField] Vector3 point_1;
    [SerializeField] Vector3 point_2;
    private Vector3 des;
    public int speed;
    private void Awake()
    {
        des = point_1;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, des, Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Space))
            des = des == point_1 ? point_2 : point_1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
    }

}
