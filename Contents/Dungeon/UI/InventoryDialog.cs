using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Contents;
using UnityEngine.UI;
using TMPro;

namespace JHchoi.UI
{
    public class InventoryDialog : IDialog
    {
        [SerializeField] private InventoryBase equipmentInventory;
        [SerializeField] private InventoryBase begInvenotry;
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtHp;
        [SerializeField] private TextMeshProUGUI txtAttack;
        [SerializeField] private TextMeshProUGUI txtDefence;
        [SerializeField] private TextMeshProUGUI txtMoveSpeed;
        [SerializeField] private GameObject objItemInfo;


        protected override void OnLoad()
        {
            equipmentInventory.SetInventory();
            begInvenotry.SetInventory();
        }

        protected override void OnEnter()
        {
            AddMessage();
            objItemInfo.SetActive(false);
        }

        private void AddMessage()
        {
            Message.AddListener<UIInventoryStatusMsg>(UIInventoryStatus);
            Message.AddListener<UIItemInfoMsg>(UIItemInfo);
            Message.AddListener<UIItemInfoCloseMsg>(UIItemInfoClose);
        }



        private void UIInventoryStatus(UIInventoryStatusMsg msg)
        {
            txtName.SetText(msg.name);
            txtHp.SetText("Hp : " + msg.hp + " / " + msg.maxHp);
            txtAttack.SetText("Attack : " + msg.attack.ToString());
            txtDefence.SetText("Defence : " + msg.defence.ToString());
            txtMoveSpeed.SetText("MoveSpeed : " + msg.moveSpeed.ToString());
        }
        private void UIItemInfo(UIItemInfoMsg msg)
        {
            objItemInfo.SetActive(true);
            objItemInfo.GetComponentInChildren<Image>().sprite = msg.itemSprite;
            objItemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = msg.itemName;
            objItemInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = msg.itemInfo;
        }

        private void UIItemInfoClose(UIItemInfoCloseMsg msg)
        {
            objItemInfo.SetActive(false);
        }


        protected override void OnExit()
        {
            Message.RemoveListener<UIInventoryStatusMsg>(UIInventoryStatus);
            Message.RemoveListener<UIItemInfoMsg>(UIItemInfo);
            Message.RemoveListener<UIItemInfoCloseMsg>(UIItemInfoClose);
        }
    }
}