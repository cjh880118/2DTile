using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class Zombie_Monster : NormalMonsterInterface
    {
        //public GameObject bullet;
        ////5발 타겟 샷건
        //IEnumerator TestAttack()
        //{

        //    int count = 5;
        //    float angle = 60;
        //    float step = angle / (count - 1);


        //    while (true)
        //    {
        //        Vector2 dirVec = target.transform.position - this.transform.position;
        //        float atan = Mathf.Atan2(dirVec.x, dirVec.y);
        //        float startAngle = -angle / 2 - (atan * Mathf.Rad2Deg - 90);

        //        for (int i = 0; i < count; i++)
        //        {
        //            float theta = startAngle + step * (float)i;
        //            bullets[i] = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        //            theta *= Mathf.Deg2Rad;
        //            bullets[i].GetComponent<FireCircle>().MoveSpeed = 5;
        //            bullets[i].GetComponent<FireCircle>().Shoot(new Vector2(3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta)));
        //        }

        //        yield return new WaitForSeconds(0.5f);
        //    }
        //}

        ////원형 
        //IEnumerator Test2Attack()
        //{
        //    float angle = 360;
        //    int count = 12;
        //    float step = angle / (count - 1);
        //    float startAngle = 0;

        //    while (true)
        //    {

        //        for (int i = 0; i < count; i++)
        //        {
        //            float theta = startAngle + step * (float)i;
        //            bullets[i] = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        //            theta *= Mathf.Deg2Rad;
        //            bullets[i].GetComponent<FireCircle>().Shoot(new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)));
        //        }

        //        startAngle += 90;
        //        yield return new WaitForSeconds(0.3f);
        //    }
        //}

        ////단발 타겟
        //IEnumerator Test3Attack()
        //{
        //    while (true)
        //    {
        //        Vector2 dirVec = target.transform.position - this.transform.position;
        //        dirVec = dirVec.normalized;
        //        bullets[0] = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        //        bullets[0].GetComponent<FireCircle>().Shoot(new Vector2(dirVec.x, dirVec.y));
        //        yield return new WaitForSeconds(0.5f);
        //    }
        //}

        ////회전 쏘기
        //IEnumerator Test4Attack()
        //{
        //    int count = 30;
        //    float theta = 0;

        //    while (true)
        //    {
        //        theta += 10;
        //        Debug.Log(theta);
        //        bullets[0] = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        //        theta *= Mathf.Deg2Rad;
        //        bullets[0].GetComponent<FireCircle>().Shoot(new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)));
        //        theta *= Mathf.Rad2Deg;
        //        yield return new WaitForSeconds(0.01f);
        //    }
        //}


        //{
        //    bullets[0] = Instantiate(bullet) as GameObject;
        //    bullets[0].transform.position = transform.position;
        //    bullets[0].AddComponent<Rigidbody2D>();
        //    Vector2 dir = target.transform.position - transform.position;
        //dir = dir.normalized;
        //    bullets[0].GetComponent<Rigidbody2D>().gravityScale = 0;
        //    bullets[0].GetComponent<Rigidbody2D>().AddForce(dir.normalized* 5, ForceMode2D.Impulse);

        //}

        //{
        //for (int i = 0; i < 5; i++)
        //{
        //    bullets[i] = Instantiate(bullet) as GameObject;
        //    bullets[i].transform.position = transform.position;
        //    bullets[i].AddComponent<Rigidbody2D>();
        //    bullets[i].GetComponent<Rigidbody2D>().gravityScale = 0;
        //    Vector2 dir = target.transform.position - transform.position;
        //    dir = dir.normalized;
        //    Vector2 rndVec = new Vector2(i, i);
        //    dir += rndVec;
        //    bullets[i].GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        //}
        //}

        //{
    }


}
