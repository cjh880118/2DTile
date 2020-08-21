using JHchoi.Contents;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class CharacterDialog : IDialog
    {
        Coroutine corShowMonsterHp;

        [SerializeField] private UserInterface consumeInventory;
        [SerializeField] private Text txtPlayerName;
        [SerializeField] private Image imgPlayerHp;

        [SerializeField] private Text txtMonsterName;
        [SerializeField] private Image imgMonsterHp;

        [SerializeField] private Image[] imgSkill;

        [SerializeField] Animator anim;

        protected override void OnEnter()
        {
            consumeInventory.SetInventory();
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<UIPlayerHpMsg>(UIPlayerHp);
            Message.AddListener<UIMonsterHpMsg>(UIMonsterHp);
            Message.AddListener<UISkillMsg>(UISkill);
        }


        private void UIPlayerHp(UIPlayerHpMsg msg)
        {
            txtPlayerName.text = msg.name;
            imgPlayerHp.fillAmount = (msg.hp * 100 / msg.maxHp) * 0.01f;
        }

        private void UIMonsterHp(UIMonsterHpMsg msg)
        {
            txtMonsterName.text = msg.name;

            imgMonsterHp.fillAmount = (msg.hp * 100 / msg.maxHp) * 0.01f;

            if (corShowMonsterHp != null)
                StopCoroutine(corShowMonsterHp);

            if(msg.hp <= 0)
            {
                anim.SetBool("isShow", false);
                return;
            }

            corShowMonsterHp = StartCoroutine(ShowMonsterHp());
        }

        private void UISkill(UISkillMsg msg)
        {
            for(int i = 0; i < imgSkill.Length; i++)
            {
                if (msg.skillNum == i)
                    imgSkill[i].color = Color.yellow;
                else
                    imgSkill[i].color = Color.white;
            }
        }



        IEnumerator ShowMonsterHp()
        {
            anim.SetBool("isShow", true);
            yield return new WaitForSeconds(3.0f);
            anim.SetBool("isShow", false);
        }


        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<UIPlayerHpMsg>(UIPlayerHp);
            Message.RemoveListener<UIMonsterHpMsg>(UIMonsterHp);
            Message.RemoveListener<UISkillMsg>(UISkill);
        }
    }
}