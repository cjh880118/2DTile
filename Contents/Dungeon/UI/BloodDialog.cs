using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class BloodDialog : IDialog
    {
        [SerializeField] private Image imgBloodEffect;
        protected override void OnEnter()
        {
            imgBloodEffect.color = new Color(1, 1, 1, 0);
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<BloodEffectMsg>(BloodEffect);
        }

        private void BloodEffect(BloodEffectMsg msg)
        {
            StartCoroutine(BloodEffect());
        }

        IEnumerator BloodEffect()
        {
            imgBloodEffect.color = new Color(1, 1, 1, UnityEngine.Random.RandomRange(0.2f, 0.3f));
            yield return new WaitForSeconds(0.1f);
            imgBloodEffect.color = new Color(1, 1, 1, 0);
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<BloodEffectMsg>(BloodEffect);
        }
    }
}