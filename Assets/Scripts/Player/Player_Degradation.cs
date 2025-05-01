using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Player_Degradation : MonoBehaviour
{
    [SerializeField] private Image MainBody;
    [SerializeField] private Image LeftWing;
    [SerializeField] private Image RightWing;
    [SerializeField] private Player_Movement pm;
    [SerializeField] private Player_Rotate pr;
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private GameObject[] Trails;

    public float bVal = 0;
    public float lVal = 0;
    public float rVal = 0;

    private float swayValue = 0;
    private int RandomNumber;

    public void Start()
    {
        StartCoroutine(RandomChoose());
    }

    private void Update()
    {
        ShipSway();
    }

    public void StandardShipValues()
    {
        bVal = 0;
        rVal = 0;
        lVal = 0;
        pm.SetSpeed();
        pr.LeftSpeed = 1;
        pr.RightSpeed = 1;
        MainBody.fillAmount = 1;
        RightWing.fillAmount = 1;
        LeftWing.fillAmount = 1;
        for (int i = 0; i < Trails.Length; i++)
        {
            Trails[i].SetActive(false);
        }
    }
    public void ShipSway()
    {
        if (rVal > 0 && RandomNumber == 1)
        {
            driftRight();
        }
        if (lVal > 0 && RandomNumber == 2)
        {
            driftLeft();
        }
    }
    public void DamageBody()
    {
        bVal += 0.125f;
        MainBody.fillAmount = 1 - bVal;
        pm.speed -= 0.5f;
        if (bVal == 0.5f)
        {
            Trails[0].SetActive(true);
        }
        if (bVal >= 1)
        {
            Trails[1].SetActive(true);
        }
    }
    public void DamageLeftWing()
    {
        lVal += 0.17f;
        LeftWing.fillAmount = 1 - lVal;
        pr.LeftSpeed -= 0.1f;
        if (lVal == 0.68f)
        {
            Trails[2].SetActive(true);
        }
        if (lVal >= 1)
        {
            Trails[3].SetActive(true);
        }
    }
    public void DamageRightWing()
    {
        rVal += 0.17f;
        RightWing.fillAmount = 1 - rVal;
        pr.RightSpeed -= 0.1f;
        if (rVal == 0.68f)
        {
            Trails[4].SetActive(true);
        }
        if (rVal >= 1)
        {
            Trails[5].SetActive(true);
        }
    }

    public void driftRight()
    {
        Vector3 movement = new Vector3(swayValue, 0);
        transform.Translate(movement * 1 * Time.deltaTime);
        swayValue = Mathf.PingPong(Time.time * 1, (rVal * 2));
    }
    public void driftLeft()
    {
        Vector3 movement = new Vector3(swayValue, 0);
        transform.Translate(-movement * 1 * Time.deltaTime);
        swayValue = Mathf.PingPong(Time.time * 1, (lVal * 2));
    }

    public IEnumerator RandomChoose()
    {
        RandomNumber = Random.Range(1, 3);
        yield return new WaitForSeconds(2);
        StartCoroutine(RandomChoose());
    }
}
