using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using JHchoi.UI.Event;

namespace JHchoi.Contents
{
    public class PlayerStatus
    {
        public string name;
        public int hp;
        public int maxHp;
        public int attack;
        public float moveSpeed;
        public int defecnce;
    }

    public class ItemStatus
    {
        public int ItemAttack;
        public int ItemDefence;
        public float ItemMoveSpeed;
    }


    public abstract class IPlayer : MonoBehaviour
    {
        delegate void Use_Skill(Vector2 dir);

        private PlayerStatus player = new PlayerStatus();
        private ItemStatus playerItem = new ItemStatus();

        private float moveH, moveV;
        private float hitDelay = 1.5f;
        private bool isHitAble = true;
        private bool isBattleOn = false;
        private bool isInventoryOpen = false;
        int skillNum = 0;
        protected bool[] isSkillPossbile = { true, true, true, true };
        Use_Skill use_Skill;
        Vector2 Skilldir;

        public void InitIPlayer(string _name, int _hp, float _moveSpeed, int _attack, int _defence)
        {
            Debug.Log("케릭터 초기화");
            UI.IDialog.RequestDialogEnter<UI.BloodDialog>();
            Message.Send<UISkillMsg>(new UISkillMsg(skillNum));
            use_Skill = Skill_1;
            player.name = _name;
            player.maxHp = _hp;
            player.hp = _hp;
            player.moveSpeed = _moveSpeed;
            player.attack = _attack;
            player.defecnce = _defence;
        }

        public int Hp { get => player.hp; }// set => player.hp = value; }
        public float MoveSpeed { get => player.moveSpeed + playerItem.ItemMoveSpeed; }
        public bool IsBattleOn { get => isBattleOn;  set => isBattleOn = value; }
        public string Name { get => name; }
        //set => name = value; }
        public int TotalAttack { get => player.attack + playerItem.ItemAttack; }
        public int MaxHp { get => player.maxHp; }
        public bool IsInventoryOpen { get => isInventoryOpen; set => isInventoryOpen = value; }
        public int Defecnce { get => player.defecnce + playerItem.ItemDefence; }
        public ItemStatus PlayerItem { get => playerItem; }

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
                skillNum = 0;
                use_Skill = Skill_1;
                Message.Send<UISkillMsg>(new UISkillMsg(skillNum));
                Debug.Log("1번 스킬");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                skillNum = 1;
                use_Skill = Skill_2;
                Message.Send<UISkillMsg>(new UISkillMsg(skillNum));
                Debug.Log("2번 스킬");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                skillNum = 2;
                use_Skill = Skill_3;
                Message.Send<UISkillMsg>(new UISkillMsg(skillNum));
                Debug.Log("3번 스킬");
            }
        }

        protected void Move()
        {
            moveH = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            moveV = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

            this.gameObject.transform.Translate(new Vector3(moveH, moveV));

            Vector2 intputDir = new Vector2(moveH, moveV);
            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.gameObject.transform.position;
            Skilldir = direction;

            FindObjectOfType<PlayerAnimation>().SetDirection(intputDir, direction);
        }
        protected abstract void Skill_1(Vector2 _dir);
        protected abstract void Skill_2(Vector2 _dir);
        protected abstract void Skill_3(Vector2 _dir);
        protected abstract void OnShield();

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.collider.tag == "Monster" && isHitAble)
            {
                player.hp -= (collision.collider.GetComponent<IMonster>().Attack - Defecnce);
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
            }
       
            if (collision.collider.tag == "MonsterBullet" && isHitAble)
            {
                player.hp -= (collision.collider.GetComponent<IMagic>().Damage - Defecnce);
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
                Destroy(collision.collider.gameObject);
            }

            if (collision.collider.tag == "Item")
            {
                Message.Send<AddItemMsg>(new AddItemMsg(collision.gameObject));
            }

            if (collision.collider.tag == "Trap" && isHitAble)
            {
                player.hp -= (collision.collider.GetComponent<Spike_Trap>().Damage - Defecnce);
                Message.Send<BloodEffectMsg>(new BloodEffectMsg());
                StartCoroutine(RecoveryTime());
            }

            Message.Send<UIPlayerHpMsg>(new UIPlayerHpMsg(name, player.maxHp, player.hp));
        }

        IEnumerator RecoveryTime()
        {
            isHitAble = false;
            yield return new WaitForSeconds(hitDelay);
            isHitAble = true;
        }
    }
}