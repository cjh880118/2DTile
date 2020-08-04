using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class IMonster : MonoBehaviour
    {
        protected int hp;
        protected float moveSpeed;
        protected Vector3 startPos;
        protected GameObject target;
        bool isTargetOn;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        protected void Init_Monster(int _hp, float _moveSpeed)
        {
            hp = _hp;
            moveSpeed = _moveSpeed;
            startPos = transform.position;
        }
        private void FixedUpdate()
        {
            Move();
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

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                isTargetOn = false;
            }
        }
    }
}