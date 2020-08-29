using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JHchoi.Contents
{
    public abstract class NormalMonsterInterface : MonsterBase
    {
        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            if (isTargetOn)
            {
                gameObject.transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * monsterObject.data.MoveSpeed);
                bool isFlip = target.transform.position.x < transform.position.x ? true : false;
                GetComponent<SpriteRenderer>().flipX = isFlip;
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isTargetOn = true;
                anim.SetBool("isTargetOn", isTargetOn);
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isTargetOn = false;
                anim.SetBool("isTargetOn", isTargetOn);
            }
        }
    }
}