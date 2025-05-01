using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFollow : MonoBehaviour
{
    public GameObject FollowObject;
    void FixedUpdate()
    {
        transform.position = FollowObject.transform.position;
    }
}
