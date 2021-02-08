using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform follow;
    public bool followX;
    public bool followY;
    public bool followZ;
    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        float x = followX ? follow.position.x : startPos.x;
        float y = followY ? follow.position.y : startPos.y;
        float z = followZ ? follow.position.z : startPos.z;
        transform.position = new Vector3(x,y,z);
    }
}
