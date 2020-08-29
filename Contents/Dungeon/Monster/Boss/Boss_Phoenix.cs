﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace JHchoi.Contents
{
    public class Boss_Phoenix : BossMonsterInterface
    {
        delegate void MoveDelegate();
        MoveDelegate moveDelegate;
        public GameObject bullet1_Prefab;
        public GameObject bullet2_Prefab;
        public GameObject bullet3_Prefab;
        Coroutine MoveCor;
        Coroutine AttackCor;

        protected override void MoveStart()
        {
            Move_Pattern_1();
            StartCoroutine(CheckHp());
        }

        IEnumerator CheckHp()
        {
            AttackCor = StartCoroutine(ShotGun());

            while (hp > monsterObject.data.Hp / 2)
            {
                yield return null;
            }

            StopCoroutine(AttackCor);
            AttackCor = StartCoroutine(CircleShoot());

            while (hp > monsterObject.data.Hp / 3)
            {
                yield return null;
            }

            StopCoroutine(AttackCor);
            StartCoroutine(AroundShoot(60));
            StartCoroutine(AroundShoot(120));
            StartCoroutine(AroundShoot(180));
            StartCoroutine(AroundShoot(240));
            StartCoroutine(AroundShoot(300));
            StartCoroutine(AroundShoot(360));

        }

        //좌우 패트롤
        protected override void Move_Pattern_1()
        {
            int rndMoveType = Random.Range((int)Ease.Unset, (int)Ease.INTERNAL_Zero);
            int rndLoopCount = Random.Range(0, 4);
            moveDelegate = Move_Pattern_2;
            gameObject.transform.DOMove(new Vector2(0, 1), 5.0f).SetEase((Ease)rndMoveType).SetLoops(rndLoopCount, LoopType.Yoyo).OnComplete(() => { moveDelegate(); });
        }

        //중심 회전
        protected override void Move_Pattern_2()
        {
            StartCoroutine(MoveAround());
        }

        IEnumerator MoveAround()
        {
            float time = 0;
            while (time > 30)
            {
                time += Time.deltaTime;
                this.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.RotateAround(Vector3.zero, Vector3.forward, 20 * Time.deltaTime);
                yield return null;
            }

            Move_Pattern_1();

        }

        //대각 찌르기
        protected override void Move_Pattern_3()
        {
        }


        protected override void Move_Pattern_4()
        {
        }

        protected override void Move_Pattern_5()
        {
        }

        protected override void Move_Pattern_6()
        {
        }


        protected override void Attack_Pattern_1()
        {
            Move_Pattern_1();
        }

        protected override void Attack_Pattern_2()
        {

        }

        protected override void Attack_Pattern_3()
        {
        }

        protected override void Attack_Pattern_4()
        {
        }


        IEnumerator ShotGun()
        {
            int count = 5;
            float angle = 60;
            float step = angle / (count - 1);
            while (true)
            {
                Vector2 dirVec = target.transform.position - this.transform.position;
                float atan = Mathf.Atan2(dirVec.x, dirVec.y);
                float startAngle = -angle / 2 - (atan * Mathf.Rad2Deg - 90);

                for (int i = 0; i < count; i++)
                {
                    float theta = startAngle + step * (float)i;
                    var magic = Instantiate(bullet1_Prefab, transform.position, Quaternion.identity) as GameObject;
                    magic.GetComponent<MagicBase>().InitMagic(15, 5, 5);
                    theta *= Mathf.Deg2Rad;
                    magic.GetComponent<MagicBase>().Shoot(new Vector2(3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta)));
                }

                yield return new WaitForSeconds(0.5f);
            }
        }


        IEnumerator CircleShoot()
        {
            float angle = 360;
            int count = 30;
            float step = angle / (count - 1);
            float startAngle = 0;

            while (true)
            {

                for (int i = 0; i < count - 1; i++)
                {
                    float theta = startAngle + step * (float)i;
                    var magic = Instantiate(bullet2_Prefab, transform.position, Quaternion.identity) as GameObject;
                    magic.GetComponent<MagicBase>().InitMagic(15, 5, 5);
                    theta *= Mathf.Deg2Rad;
                    magic.GetComponent<MagicBase>().Shoot(new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)));
                }

                startAngle += 15;

                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerator AroundShoot(float theta)
        {
            int count = 30;

            while (true)
            {
                theta += 10;
                var magic = Instantiate(bullet3_Prefab, transform.position, Quaternion.identity) as GameObject;
                theta *= Mathf.Deg2Rad;
                magic.GetComponent<MagicBase>().InitMagic(15, 3, 5);
                magic.GetComponent<MagicBase>().Shoot(new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)));
                theta *= Mathf.Rad2Deg;
                yield return new WaitForSeconds(0.07f);
            }
        }
    }
}