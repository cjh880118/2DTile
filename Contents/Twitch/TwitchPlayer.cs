using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwitchPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    bool isMove = false;
    Vector2 targetPos;
  
    private void Update()
    {
        if (isMove)
        {
            this.transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 0.8f);
            if (Vector2.Distance(transform.position, targetPos) < 0.05f)
            {
                GetComponent<Animator>().Play("Idle");
                isMove = false;
            }
        }
    }

    public void ReceiveMessage(string msg)
    {
        if (msg == "!오")
        {
            isMove = true;
            float vecX = Random.Range(-3, -7);

            targetPos = new Vector2(vecX, transform.position.y);
            Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
            Debug.Log("방향 :" + dir.normalized);
            if(dir.normalized.x > 0)
                GetComponent<Animator>().Play("RightRun");
            else
                GetComponent<Animator>().Play("LeftRun");

        }
        else if (msg == "!엑스")
        {
            isMove = true;
            float vecX = Random.Range(3, 7);
            targetPos = new Vector2(vecX, transform.position.y);
            Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
            Debug.Log("방향 :" + dir.normalized);
            if (dir.normalized.x > 0)
                GetComponent<Animator>().Play("RightRun");
            else
                GetComponent<Animator>().Play("LeftRun");
        }
    }
}

