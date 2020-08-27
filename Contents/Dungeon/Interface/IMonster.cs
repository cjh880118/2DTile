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
        public delegate void EventHandler(Object sender, MonsterInfo monsterInfo);
        public event EventHandler EventHitMonster;
        public MonsterObject monsterObject;

        //public delegate void EventHandlerMonsterDie(object sender, GameObject obj);
        //public event EventHandlerMonsterDie EventMonsterDie;

        protected Animator anim;
        protected GameObject target;
        protected bool isTargetOn;
        private bool isCollisionable = true;
        private int hp;
        public int Attack { get => monsterObject.data.Attack; }

        private  void Start()
        {
            hp = monsterObject.data.Hp;
            target = GameObject.FindGameObjectWithTag("Player");
            anim = GetComponent<Animator>();
            anim.runtimeAnimatorController = monsterObject.aniController;

            MoveStart();
        }

        protected virtual void MoveStart() { }


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
                    EventHitMonster?.Invoke(this, new MonsterInfo(this.gameObject, hp));

                    if (hp <= 0)
                    {
                        Message.Send<DropItemMsg>(new DropItemMsg(transform.position));
                        EventHitMonster?.Invoke(this, new MonsterInfo(this.gameObject, hp));
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

    public class MonsterInfo : EventArgs
    {
        public GameObject objMonster;
        public int hp;

        public MonsterInfo(GameObject _obj,int _hp)
        {
            objMonster = _obj;
            hp = _hp;
        }
    }
}