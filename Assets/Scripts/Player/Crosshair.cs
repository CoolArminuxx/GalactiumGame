using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject CrosshairIcon;
    private void FixedUpdate()
    {
        RayDetection();
    }

    private void RayDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        Debug.DrawRay(transform.position, transform.up * 10);

        if (hit.collider != null)
        {
            CrosshairIcon.transform.position = hit.point;
        }
    }
}
