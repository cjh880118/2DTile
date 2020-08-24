using JHchoi.Constants;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Object = System.Object;

namespace JHchoi.Contents
{
    public abstract class IMonster : MonoBehaviour
    {
        public delegate void EventHandler(Object sender, EventArgs e);
        public event EventHandler EventHitMonster;
        public MonsterObject monsterObject;
        protected Animator anim;
        protected GameObject target;
        protected bool isTargetOn;
        private bool isCollisionable = true;
        private int hp;
        public int Attack { get => monsterObject.data.Attack; }

        private void Start()
        {
            hp = monsterObject.data.Hp;
            target = GameObject.FindGameObjectWithTag("Player");
            anim = GetComponent<Animator>();
            anim.runtimeAnimatorController = monsterObject.aniController;
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
                    Message.Send<UIMonsterHpMsg>(new UIMonsterHpMsg(monsterObject.data.Name, monsterObject.data.Hp, hp));
                    EventHitMonster?.Invoke(this, new EventArgs());

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

    }
}