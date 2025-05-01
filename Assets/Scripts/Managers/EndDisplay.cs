using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class EndDisplay : MonoBehaviour
{
    [SerializeField] private Transform[] endPositionTransform;
    [SerializeField] private RectTransform titleTransform;
    [SerializeField] private RectTransform textTransform;
    [SerializeField] private GameObject Button;
    public Vector3 punchStrength;
    public float moveDuration;

    private void Start()
    {
        StartCoroutine(WaveAnimation());
    }

    private IEnumerator WaveAnimation()
    {
        yield return new WaitForSeconds(1);
        textTransform.DOMove(endPositionTransform[0].position, moveDuration);
        titleTransform.DOMove(endPositionTransform[1].position, moveDuration);
        yield return new WaitForSeconds(moveDuration - 0.1f);
        textTransform.DOPunchScale(punchStrength, 2f);
        titleTransform.DOPunchScale(punchStrength, 2f);
        yield return new WaitForSeconds(2f);
        Button.SetActive(true);
        yield return new WaitForSeconds(1f);
        Button.GetComponent<Button>().Select();
        yield break;
    }
}
