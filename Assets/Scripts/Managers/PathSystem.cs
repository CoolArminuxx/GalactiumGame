using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class PathSystem : MonoBehaviour
{
    [SerializeField] private Transform Track;
    private PathType pathType = PathType.CatmullRom;
    public int PathNum;
    public int Speed;
    public bool isBoss;

    public void StartPath()
    {
        if (isBoss == true)
        {
            Track = GameObject.FindGameObjectWithTag("Track2").transform;
        }
        else
        {
            Track = GameObject.FindGameObjectWithTag("Track").transform;
        }
        Vector3[] pathArray = new Vector3[Track.childCount];
        for (int i = PathNum; i < pathArray.Length; i++)
        {
            pathArray[i] = Track.GetChild(i).position;
        }
        pathArray = pathArray.Skip(PathNum).ToArray();
        transform.DOPath(pathArray, Speed, pathType).SetEase(Ease.Linear).OnComplete(() =>
        {
            MainPath();
        });
    }

    private void MainPath()
    {
        Vector3[] pathArray = new Vector3[Track.childCount];
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = Track.GetChild(i).position;
        }
        pathArray.Reverse();
        transform.DOPath(pathArray, Speed, pathType).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        Debug.Log("MADE IT!!");
    }

    public void EndPathFollow()
    {
        DOTween.Kill(transform);
    }
}
