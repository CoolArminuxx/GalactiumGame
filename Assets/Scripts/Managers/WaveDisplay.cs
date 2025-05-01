using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private Transform OriginalPosition;
    [SerializeField] private Transform endPositionTransform1;
    [SerializeField] private Transform endPositionTransform2;
    [SerializeField] private TMP_Text waveTXT;
    [SerializeField] private RectTransform waveTransform;
    [SerializeField] private Enemy_SpawnSystem spawnRef;
    [SerializeField] private ScoreSO scoreRef;
    public int waveNum;
    public Vector3 punchStrength;
    public float moveDuration;

    private void Start()
    {
        scoreRef.Waves = 1;
        waveNum = scoreRef.Waves;
    }

    public void StartWaveMove()
    {
        StartCoroutine(WaveAnimation());
    }

    private IEnumerator WaveAnimation()
    {
        waveTXT.text = "Wave " + waveNum + " Completed";
        yield return new WaitForSeconds(2);
        waveTransform.DOMove(endPositionTransform1.position, moveDuration);
        yield return new WaitForSeconds(moveDuration - 0.1f);
        waveTransform.DOPunchScale(punchStrength, 2f);
        yield return new WaitForSeconds(moveDuration);
        waveTransform.DOMove(endPositionTransform2.position, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        waveTransform.position = OriginalPosition.position;
        scoreRef.Waves++;
        waveNum = scoreRef.Waves;
        yield break;
    }
}
