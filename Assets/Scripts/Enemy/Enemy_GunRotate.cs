using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GunRotate : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
