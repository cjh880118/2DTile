using JHchoi.Constants;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;

//using UnityEditor.Animations;
//#endif
using UnityEngine;
using Object = System.Object;

namespace JHchoi.Contents
{
    #region EventArgs
    public class EventMonsterHit : EventArgs
    {
        public GameObject monster;
        public int hp;

        public EventMonsterHit(GameObject _monster, int _hp)
        {
            monster = _monster;
            hp = _hp;
        }
    }
    #endregion

    public abstract class MonsterBase : MonoBehaviour
    {
        public event EventHandler<EventMonsterHit> EventHitMonster;
        public MonsterObject monsterObject;
        public GameObject bloodPrefab;
        protected Animator anim;
        protected GameObject target;
        protected bool isTargetOn;
        private bool isCollisionable = true;
        protected int hp;
        protected Astar2D astar2D;
        protected Grid grid;
        public int Attack { get => monsterObject.data.Attack; }

        public void InitMonster(Astar2D astar, Grid _grid)
        {
            //astar2D = new Astar2D();
            grid = _grid;
            hp = monsterObject.data.Hp;
            target = GameObject.FindGameObjectWithTag("Player");
            anim = GetComponent<Animator>();
#if UNITY_EDITOR
            anim.runtimeAnimatorController = monsterObject.aniController;
#endif
            astar2D = astar;
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
                    hp -= collision.collider.GetComponent<MagicBase>().Damage;
                    Destroy(collision.collider.gameObject);
                    StartCoroutine(HItDelay());
                    Message.Send<UIMonsterHpMsg>(new UIMonsterHpMsg(monsterObject.data.Name, monsterObject.data.Hp, hp));
                    EventHitMonster?.Invoke(this, new EventMonsterHit(this.gameObject, hp));

                    if (hp <= 0)
                    {
                        var o = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
                        o.transform.SetParent(this.transform.parent);
                        Message.Send<DropItemMsg>(new DropItemMsg(transform.position));
                        EventHitMonster?.Invoke(this, new EventMonsterHit(this.gameObject, hp));
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