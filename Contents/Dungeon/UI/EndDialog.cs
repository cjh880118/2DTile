using JHchoi.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JHchoi.UI.Event;

namespace JHchoi.UI
{
    public class EndDialog : IDialog
    {
        public Button button;
        public TextMeshProUGUI text;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                Scene.SceneManager.Instance.Load(SceneName.Title);
            });
        }

        IEnumerator NewGame()
        {
            yield return new WaitForSeconds(3.0f);
            Scene.SceneManager.Instance.Load(SceneName.Title);
        }


        protected override void OnEnter()
        {
            AddMessage();
            StartCoroutine(NewGame());
        }

        private void AddMessage()
        {
            Message.AddListener<UIEndGameMsg>(UIEndGame);
        }

        private void UIEndGame(UIEndGameMsg msg)
        {
            text.text = msg.message;
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<UIEndGameMsg>(UIEndGame);
        }
    }
}