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

        [SerializeField] private Text txtPlayerName;
        [SerializeField] private Image imgPlayerHp;

        [SerializeField] private Text txtMonsterName;
        [SerializeField] private Image imgMonsterHp;

        [SerializeField] Animator anim;

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<UIPlayerMsg>(UIPlayer);
            Message.AddListener<UIMonsterMsg>(UIMonster);
        }

        private void UIPlayer(UIPlayerMsg msg)
        {
            txtPlayerName.text = msg.name;

            imgPlayerHp.fillAmount = (msg.hp * 100 / msg.maxHp) * 0.01f;
        }

        private void UIMonster(UIMonsterMsg msg)
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
            Message.RemoveListener<UIPlayerMsg>(UIPlayer);
            Message.RemoveListener<UIMonsterMsg>(UIMonster);
        }
    }
}