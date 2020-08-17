using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using JHchoi.UI.Event;

namespace JHchoi.Contents
{

    public abstract class IPlayer : MonoBehaviour
    {
        private float moveH, moveV;
        private string name;
        private int hp;
        private int maxHp;
        private float moveSpeed;
        private float hitDelay = 1.5f;
        private bool isHitAble = true;
        private int attackDamage;
        private bool isBattleOn = false;
        private bool isInventoryOpen = false;

        int skillNum = 1;
        protected bool[] isSkillPossbile = { true, true, true, true };
        delegate void Use_Skill(Vector2 dir);
        Use_Skill use_Skill;
        Vector2 Skilldir;

        public void InitIPlayer(string _name, int _hp, float _moveSpeed, int _attackDamage)
        {
            Debug.Log("케릭터 초기화");
            UI.IDialog.RequestDialogEnter<UI.BloodDialog>();
            use_Skill = Skill_1;
            name = _name;
            maxHp = _hp;
            hp = _hp;
            moveSpeed = _moveSpeed;
            attackDamage = _attackDamage;
        }

        public int Hp { get => hp; set => hp = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public bool IsBattleOn { get => isBattleOn; set => isBattleOn = value; }
        public string Name { get => name; set => name = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public bool IsInventoryOpen { get => isInventoryOpen; set => isInventoryOpen = value; }
        public float HitDelay { get => hitDelay; set => hitDelay = value; }

        private void FixedUpdate()
        {
            Move();
            InputSkill();
        }

        void InputSkill()
        {
            if (!isBattleOn || isInventoryOpen)
                return;

            SetSkill();

            if (Input.GetKey(KeyCode.Mouse0))
                use_Skill(Skilldir);
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                OnShield();
        }

        void SetSkill()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillNum = 1;
                use_Skill = Skill_1;
                Debug.Log("1번 스킬");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                skillNum = 2;
                use_Skill = Skill_2;
                Debug.Log("2번 스킬");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                skillNum = 3;
                use_Skill = Skill_3;
                Debug.Log("3번 스킬");
            }
        }

        protected void Move()
        {
            moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            moveV = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            this.gameObject.transform.Translate(new Vector3(moveH, moveV));

            Vector2 intputDir = new Vector2(moveH, moveV);
            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.gameObject.transform.position;
            Skilldir = direction;
            FindObjectOfType<PlayerAnimation>().SetDirection(intputDir);//, direction);
        }
        protected abstract void Skill_1(Vector2 _dir);
        protected abstract void Skill_2(Vector2 _dir);
        protected abstract void Skill_3(Vector2 _dir);
        protected abstract void OnShield();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Monster" && isHitAble)
            {
                hp -= collision.collider.GetComponent<IMonster>().AttackDamage;
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
            }

            if (collision.collider.tag == "BossMonster" && isHitAble)
            {
                hp -= collision.collider.GetComponent<IBossMonster>().AttackDamage;
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
            }

            if (collision.collider.tag == "MonsterBullet" && isHitAble)
            {
                hp -= collision.collider.GetComponent<IMagic>().Damage;
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
                Destroy(collision.collider.gameObject);
            }

            if (collision.collider.tag == "Item")
            {
                Message.Send<GainItemMsg>(new GainItemMsg(collision.collider.GetComponent<IItem>()));
                Destroy(collision.collider.gameObject);
            }

            if (collision.collider.tag == "Trap" && isHitAble)
            {
                hp -= collision.collider.GetComponent<Spike_Trap>().Damage;
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
            }

            Message.Send<UIPlayerMsg>(new UIPlayerMsg(name, maxHp, hp));
        }


        IEnumerator RecoveryTime()
        {
            isHitAble = false;
            yield return new WaitForSeconds(hitDelay);
            isHitAble = true;
        }
    }
}