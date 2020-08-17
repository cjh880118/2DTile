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
        [SerializeField] private Transform itemSlotContainer;
        [SerializeField] private Transform itemSlotTemplate;
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtHp;
        [SerializeField] private TextMeshProUGUI txtAttack;
        [SerializeField] private TextMeshProUGUI txtDefence;
        [SerializeField] private TextMeshProUGUI txtMoveSpeed;

        protected override void OnEnter()
        {
            AddMessage();

        }

        private void AddMessage()
        {
            Message.AddListener<UIInventoryMsg>(UIInventory);
            Message.AddListener<UIInventoryStatusMsg>(UIInventoryStatus);
        }

        private void UIInventory(UIInventoryMsg msg)
        {
            RefreshInventoryItems(msg.listItem);
        }

        private void RefreshInventoryItems(LinkedList<IItem> items)
        {
            int x = 0;
            int y = 0;
            float itemSlotCellSize = 120f;

            for (int i = 0; i < itemSlotContainer.gameObject.transform.childCount - 1; i++)
                Destroy(itemSlotContainer.transform.GetChild(i + 1).gameObject);

            foreach (var o in items)
            {
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.gameObject.transform.parent = itemSlotContainer;
                itemSlotRectTransform.gameObject.transform.GetChild(1).GetComponent<UIItem>().Init_UIItem(o, o.ItemName, o.ItemComment, o.ItemKind, o.ItemType);
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
                TextMeshProUGUI uiText = itemSlotRectTransform.Find("txtCount").GetComponent<TextMeshProUGUI>();

                if (o.Count > 1)
                    uiText.SetText(o.Count.ToString());
                else
                    uiText.SetText("");

                x++;
                if (x > 3)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void UIInventoryStatus(UIInventoryStatusMsg msg)
        {
            txtName.SetText(msg.name);
            txtHp.SetText("Hp : " + msg.hp + " / " + msg.maxHp);
            txtAttack.SetText("Attack : " + msg.attack.ToString());
            txtDefence.SetText("Defence : " + msg.defence.ToString());
            txtMoveSpeed.SetText("MoveSpeed : " + msg.moveSpeed.ToString());
        }


        protected override void OnExit()
        {
            Message.RemoveListener<UIInventoryMsg>(UIInventory);
            Message.RemoveListener<UIInventoryStatusMsg>(UIInventoryStatus);
        }
    }
}