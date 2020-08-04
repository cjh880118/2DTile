using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JHchoi.Constants;
using JHchoi.Models;


namespace JHchoi.Contents
{
	public class LobbyContent : IContent
	{
        protected override void OnEnter()
        {
            Message.Send<UI.Event.FadeOutMsg>(new UI.Event.FadeOutMsg());
            AddMessage();

            UI.IDialog.RequestDialogEnter<UI.ScreenLobbyDialog>();
            UI.IDialog.RequestDialogEnter<UI.KioskLobbyDialog>();
        }

        protected override void OnExit()
        {
            RemoveMessage();

            UI.IDialog.RequestDialogExit<UI.ScreenLobbyDialog>();
            UI.IDialog.RequestDialogExit<UI.KioskLobbyDialog>();
        }

        void AddMessage()
        {
            Message.AddListener<UI.Event.GameModeClickMsg>(OnGameModeClick);
        }

		void RemoveMessage()
		{
            Message.RemoveListener<UI.Event.GameModeClickMsg>(OnGameModeClick);
        }

        void OnGameModeClick(UI.Event.GameModeClickMsg msg)
        {
            StartCoroutine(ChangeScene());
        }

        IEnumerator ChangeScene()
        {
            Message.Send<UI.Event.FadeInMsg>(new UI.Event.FadeInMsg());
            yield return new WaitForSeconds(0.5f);
            Scene.SceneManager.Instance.Load(SceneName.InGame);
        }
    }
}
