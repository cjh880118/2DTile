using UnityEngine;
using System.Collections;
using JHchoi.Models;
using JHchoi;


namespace JHchoi.Contents
{
	public class GlobalContent : IContent
	{
        protected override void OnEnter()
		{
            AddMessage();
            UI.IDialog.RequestDialogEnter<UI.GlobalDialog>();
            UI.IDialog.RequestDialogEnter<UI.ScreenGlobalDialog>();
        }

		protected override void OnExit()
		{
			RemoveMessage();
            UI.IDialog.RequestDialogExit<UI.GlobalDialog>();
            UI.IDialog.RequestDialogExit<UI.ScreenGlobalDialog>();
        }

		void AddMessage()
		{
        }

		void RemoveMessage()
		{
        }
    }
}
