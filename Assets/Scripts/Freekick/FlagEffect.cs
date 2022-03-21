using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEffect : MonoBehaviour
{

    float defaultPosY;
    float toPos;
    [SerializeField] float offset;
    public bool play;
    private void Awake()
    {
        defaultPosY = transform.position.y;
        toPos = defaultPosY + offset;
    }
    private void Update()
    {
        if (transform.position.y >= defaultPosY + offset)
            toPos = defaultPosY - offset;
        if (transform.position.y <= defaultPosY - offset)
            toPos = defaultPosY + offset;
        if (play)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, toPos, transform.position.z), 0.05f);
    }
}
