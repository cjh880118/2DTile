using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Common;

namespace JHchoi.Contents
{
    public class Witch : PlayerBase
    {
        public GameObject FireCirclePrefab;
        public GameObject MagicBallPrefab;
        public GameObject BlueCirclePrefab;
        public GameObject Shield;
        private Camera mainCamera;
        private PlayerAnimation playerAnimation;

        private void Awake()
        {
            mainCamera = Camera.main;
            playerAnimation = transform.Find("Witch Sprite").GetComponent<PlayerAnimation>();
        }

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

            playerAnimation.SetDirection(Vector2.zero, _dir);

            GameObject fireCircle = Instantiate(FireCirclePrefab, transform.position, Quaternion.identity);
            MagicBase fireCircleBase = fireCircle.GetComponent<MagicBase>();
            fireCircleBase.InitMagic(10 + TotalAttack, 20, 0.1f);
            fireCircleBase.Shoot(dirVec);
            StartCoroutine(CoolTimeCheck(fireCircleBase.CoolTime, 0));
        }


        protected override void Skill_2(Vector2 _dir)
        {
            if (!isSkillPossbile[1])
                return;

            float cooltime = 0;
            int count = 5;
            float angle = 30;
            float step = angle / (count - 1);

            Vector2 dirVec = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.gameObject.transform.position;
            float atan = Mathf.Atan2(dirVec.x, dirVec.y);
            float startAngle = -angle / 2 - (atan * Mathf.Rad2Deg - 90);

            playerAnimation.SetDirection(Vector2.zero, _dir);

            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + step * (float)i;
                GameObject magicBall = Instantiate(MagicBallPrefab, transform.position, Quaternion.identity);
                MagicBase magicBallBase = magicBall.GetComponent<MagicBase>();
                theta *= Mathf.Deg2Rad;
                magicBallBase.InitMagic(5 + TotalAttack, 25, 0.3f);
                magicBallBase.Shoot(new Vector2(3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta)));
                cooltime = magicBallBase.CoolTime;
            }

            StartCoroutine(CoolTimeCheck(cooltime, 1));
        }

        protected override void Skill_3(Vector2 _dir)
        {
            if (!isSkillPossbile[2])
                return;

            Vector2 dirVec = _dir.normalized;

            playerAnimation.SetDirection(Vector2.zero, _dir);
            GameObject blueCircle = Instantiate(BlueCirclePrefab, transform.position, Quaternion.identity);
            MagicBase blueCircleBase = blueCircle.GetComponent<MagicBase>();
            blueCircleBase.InitMagic(30 + TotalAttack, 15, 0.5f);
            blueCircleBase.Shoot(dirVec);
            StartCoroutine(CoolTimeCheck(blueCircleBase.CoolTime, 2));
        }


        IEnumerator CoolTimeCheck(float _cooltime, int _skillIndex)
        {
            isSkillPossbile[_skillIndex] = false;
            yield return new WaitForSeconds(_cooltime);
            isSkillPossbile[_skillIndex] = true;
        }
    }
}