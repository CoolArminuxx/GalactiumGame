using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LivesSO : ScriptableObject
{
    [SerializeField] private int LivesAmount;

    private void OnEnable()
    {
        Value = 3;
    }
    public int Value
    {
        get { return LivesAmount; }
        set { LivesAmount = value; }
    }
}
