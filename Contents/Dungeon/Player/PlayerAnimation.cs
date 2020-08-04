using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject tempObj;
    private Animator anim;
    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    int lastDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 _dir)
    {
        string[] directionArray = null;

        if (_dir.magnitude < 0.01)
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(_dir);
        }

        anim.Play(directionArray[lastDirection]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack(_dir);
        }
    }

    private int DirectionToIndex(Vector2 _dir)
    {
        Vector2 norDir = _dir.normalized;
        float step = 360 / 8;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, norDir);
        angle += offset;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }

    private void Attack(Vector2 dir)
    {
        Vector2 norDir = dir.normalized;
        GameObject obj = Instantiate(tempObj);
        obj.transform.position = this.gameObject.transform.position;
        obj.GetComponent<Rigidbody2D>().AddForce(norDir * 100f);


        Debug.Log("공격");
    }
}
