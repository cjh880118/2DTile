using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.Contents
{
    public abstract class IPlayer : MonoBehaviour
    {
        public GameObject[] Parts;
        int hp;
        int mp;
        public float moveSpeed;
        float attackSpeed;

        protected void InitIPlayer(int hp, int mp, float moveSpeed, float attackSpeed)
        {
            Debug.Log("케릭터 초기화");
            this.hp = hp;
            this.mp = mp;
            this.moveSpeed = moveSpeed;
            this.attackSpeed = attackSpeed;
        }


        public int Hp { get => hp; set => hp = value; }
        public int Mp { get => mp; set => mp = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }

        private void Update()
        {
            Move();
            Attack();
            Skill_1();
            Skill_2();
        }

        protected void Move()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
                SetDirection(Direction.UP);
                Debug.Log("위");
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("아래");
                this.gameObject.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
                SetDirection(Direction.DOWN);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                Debug.Log("왼쪽");
                SetDirection(Direction.LEFT);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                SetDirection(Direction.RIGHT);
                Debug.Log("오른쪽");
            }
        }

        void SetDirection(Direction dir)
        {
            for (int i = 0; i < Parts.Length; i++)
            {
                bool activeTrue = (int)dir == i ? true : false;
                Parts[i].SetActive(activeTrue);
            }
        }

        protected abstract void Attack();
        protected abstract void Skill_1();
        protected abstract void Skill_2();

        private void OnCollisionEnter(Collision collision)
        {
            //todo.. 탄막 히트시 hp체크
            Debug.Log("충돌");
        }
    }
}