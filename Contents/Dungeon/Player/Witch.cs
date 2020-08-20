using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Common;

namespace JHchoi.Contents
{
    public class Witch : IPlayer
    {
        public GameObject FireCirclePrefab;
        public GameObject MagicBallPrefab;
        public GameObject BlueCirclePrefab;
        public GameObject Shield;

        protected override void OnShield()
        {
            if (!Shield.activeSelf)
            {
                Shield.SetActive(true);
                StartCoroutine(ShieldTimer());
            }
        }

        IEnumerator ShieldTimer()
        {
            yield return new WaitForSeconds(3.0f);
            Shield.SetActive(false);
        }

        //단발
        protected override void Skill_1(Vector2 _dir)
        {
            if (!isSkillPossbile[0])
                return;

            Vector2 dirVec = _dir.normalized;

            FindObjectOfType<PlayerAnimation>().SetDirection(Vector2.zero, _dir);

            var fireCircle = Instantiate(FireCirclePrefab) as GameObject;
            fireCircle.transform.position = transform.position;
            fireCircle.GetComponent<IMagic>().InitMagic(10 + TotalAttack, 20, 0.1f);
            fireCircle.GetComponent<IMagic>().Shoot(dirVec);
            StartCoroutine(CoolTimeCheck(fireCircle.GetComponent<IMagic>().CoolTime, 0));
        }


        protected override void Skill_2(Vector2 _dir)
        {
            if (!isSkillPossbile[1])
                return;

            float cooltime = 0;
            int count = 5;
            float angle = 30;
            float step = angle / (count - 1);

            Vector2 dirVec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.gameObject.transform.position;
            float atan = Mathf.Atan2(dirVec.x, dirVec.y);
            float startAngle = -angle / 2 - (atan * Mathf.Rad2Deg - 90);

            FindObjectOfType<PlayerAnimation>().SetDirection(Vector2.zero, _dir);

            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + step * (float)i;
                var magicBall = Instantiate(MagicBallPrefab) as GameObject;
                magicBall.transform.position = transform.position;
                theta *= Mathf.Deg2Rad;
                magicBall.GetComponent<IMagic>().InitMagic(5 + TotalAttack, 25, 0.3f);
                magicBall.GetComponent<IMagic>().Shoot(new Vector2(3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta)));
                cooltime = magicBall.GetComponent<IMagic>().CoolTime;
            }

            StartCoroutine(CoolTimeCheck(cooltime, 1));
        }

        protected override void Skill_3(Vector2 _dir)
        {
            if (!isSkillPossbile[2])
                return;

            Vector2 dirVec = _dir.normalized;

            FindObjectOfType<PlayerAnimation>().SetDirection(Vector2.zero, _dir);
            var blueCircle = Instantiate(BlueCirclePrefab) as GameObject;
            blueCircle.transform.position = this.gameObject.transform.position;
            blueCircle.GetComponent<IMagic>().InitMagic(30 + TotalAttack, 15, 0.5f);
            blueCircle.GetComponent<IMagic>().Shoot(dirVec);
            StartCoroutine(CoolTimeCheck(blueCircle.GetComponent<IMagic>().CoolTime, 2));
        }


        IEnumerator CoolTimeCheck(float _cooltime, int _skillIndex)
        {
            isSkillPossbile[_skillIndex] = false;
            yield return new WaitForSeconds(_cooltime);
            isSkillPossbile[_skillIndex] = true;
        }
    }
}