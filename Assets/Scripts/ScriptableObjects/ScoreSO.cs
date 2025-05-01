using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSO : ScriptableObject
{
    [SerializeField] private int ScoreAmount;
    [SerializeField] private int KillAmount;
    [SerializeField] private int WaveAmount;

    private void OnEnable()
    {
        Score = 0;
        Kills = 0;
        Waves = 0;
    }
    public int Score
    {
        get { return ScoreAmount; }
        set { ScoreAmount = value; }
    }
    public int Kills
    {
        get { return KillAmount; }
        set { KillAmount = value; }
    }
    public int Waves
    {
        get { return WaveAmount; }
        set { WaveAmount = value; }
    }
}
