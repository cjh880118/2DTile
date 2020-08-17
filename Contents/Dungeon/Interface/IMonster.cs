using JHchoi.Constants;
using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class IMonster : MonoBehaviour
    {
        private string name;
        private int maxHp;
        private int hp;
        private int attack;
        private float moveSpeed;
        private Vector3 startPos;
        [SerializeField] private MonsterType monsterType;

        protected GameObject target;
        bool isTargetOn;
        bool isdestory;
        bool isCollisionable = true;

        public int Hp { get => hp; set => hp = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public Vector3 StartPos { get => startPos; set => startPos = value; }
        public MonsterType MonsterType { get => monsterType; set => monsterType = value; }
        public int Attack { get => attack; set => attack = value; }
        public string Name { get => name; set => name = value; }

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        public void Init_Monster(string _name, int _hp, float _moveSpeed, int _attack)
        {
            name = _name;
            maxHp = _hp;
            hp = _hp;
            moveSpeed = _moveSpeed;
            attack = _attack;
            StartPos = transform.position;
        }

        private void FixedUpdate()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack_Pattern1();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack_Pattern2();
            }
        }

        protected virtual void Move()
        {
            if (isTargetOn)
            {
                gameObject.transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
                bool isFlip = target.transform.position.x < transform.position.x ? true : false;
                GetComponent<SpriteRenderer>().flipX = isFlip;
            }
        }

        protected abstract void Attack_Pattern1();
        protected abstract void Attack_Pattern2();
        protected abstract void Attack_Pattern3();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isTargetOn = true;
            }
        }

        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Bullet")
            {

                if (!isCollisionable)
                {
                    Destroy(collision.collider.gameObject);
                    return;
                }
                else
                {
                    hp -= collision.collider.GetComponent<IMagic>().Damage;
                    Destroy(collision.collider.gameObject);
                    StartCoroutine(HItDelay());
                    Message.Send<UIMonsterHpMsg>(new UIMonsterHpMsg(name, maxHp, hp));
                    Message.Send<CameraShakeMsg>(new CameraShakeMsg());

                    if (hp <= 0)
                    {
                        Message.Send<DropItemMsg>(new DropItemMsg(transform.position));
                        Destroy(gameObject);
                    }
                }
            }
        }

        IEnumerator HItDelay()
        {
            isCollisionable = false;
            yield return null;
            isCollisionable = true;
        }


        protected virtual void DropItem()
        {

        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isTargetOn = false;
            }
        }
    }
}