using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotator : MonoBehaviour
{
    public GameObject Target;
    private int rotateNum;
    private int spriteNum;
    public Sprite[] ShipSprites;
    public SpriteRenderer EnemySR;

    public void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        EnemyRotation();

        EnemySR.sprite = ShipSprites[spriteNum];
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void EnemyRotation()
    {
        Vector3 diff = (Target.transform.position - transform.position);
        float angle = Mathf.Atan2(diff.y, diff.x);
        int rawNum = (int)(angle * Mathf.Rad2Deg);
        rotateNum = 10 * ((rawNum + 9) / 10) - 90;
        spriteNum = rotateNum / 10;
        if (spriteNum < 0)
        {
            spriteNum += 36;
        }
        transform.transform.eulerAngles = new Vector3(0f, 0f, rotateNum);
    }
}
