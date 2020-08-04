using UnityEngine;
using System.Collections;
using JHchoi.Constants;
using JHchoi.Models;
using JHchoi.UI.Event;


namespace JHchoi.Contents
{
	public class TitleContent : IContent
	{
        protected override void OnEnter()
		{
            Log.Instance.log("TitleContent Enter");
            Message.Send<FadeOutMsg>(new FadeOutMsg());
            AddMessage();
            UI.IDialog.RequestDialogEnter<UI.ScreenTitleDialog>();
            UI.IDialog.RequestDialogEnter<UI.KioskTitleDialog>();
        }

		protected override void OnExit()
		{
			RemoveMessage();
            UI.IDialog.RequestDialogExit<UI.ScreenTitleDialog>();
            UI.IDialog.RequestDialogExit<UI.KioskTitleDialog>();
        }

		void AddMessage()
		{
            Message.AddListener<UI.Event.StartClickMsg>(OnStartClick);
		}

		void RemoveMessage()
		{
            Message.RemoveListener<UI.Event.StartClickMsg>(OnStartClick);
        }

        void OnStartClick(UI.Event.StartClickMsg msg)
        {
            StartCoroutine(ChangeScene());
        }

        IEnumerator ChangeScene()
        {
            Message.Send<FadeInMsg>(new FadeInMsg());
            yield return new WaitForSeconds(0.5f);
            Scene.SceneManager.Instance.Load(SceneName.Lobby);
        }
    }
}
