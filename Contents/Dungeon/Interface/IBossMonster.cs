using JHchoi.Constants;
using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.UI.Event;

namespace JHchoi.Contents
{
    public abstract class IBossMonster : MonoBehaviour
    {
        private string name;
        private int maxHp;
        private int hp;
        private float moveSpeed;
        private int attackDamage;

        protected GameObject target;
        [SerializeField] private MonsterType monsterType;

        public int Hp { get => hp; set => hp = value; }
        public string Name { get => name; set => name = value; }
        public MonsterType MonsterType { get => monsterType; set => monsterType = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }

        public virtual void Init_Monster(string _name, int _hp, float _moveSpeed, int _attackDamage)
        {
            name = _name;
            maxHp = _hp;
            hp = _hp;
            moveSpeed = _moveSpeed;
            attackDamage = _attackDamage;
            target = GameObject.FindGameObjectWithTag("Player");

        }
        protected abstract void Move_Pattern_1();
        protected abstract void Move_Pattern_2();
        protected abstract void Move_Pattern_3();
        protected abstract void Move_Pattern_4();
        protected abstract void Move_Pattern_5();
        protected abstract void Move_Pattern_6();

        protected abstract void Attack_Pattern_1();
        protected abstract void Attack_Pattern_2();
        protected abstract void Attack_Pattern_3();
        protected abstract void Attack_Pattern_4();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.collider.tag);
            if (collision.collider.tag == "Bullet")
            {
                hp -= collision.collider.GetComponent<IMagic>().Damage;
                Message.Send<UIMonsterHpMsg>(new UIMonsterHpMsg(name, maxHp, hp));
                Destroy(collision.collider.gameObject);
            }
        }
    }
}
