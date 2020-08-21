using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using JHchoi.UI.Event;

namespace JHchoi.Contents
{
    public abstract class UserInterface : MonoBehaviour
    {
        public delegate void ItemDestory(InventoryType type, GameObject obj);
        public event ItemDestory itemDestort;
        public InventoryObject inventory;
        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        // Start is called before the first frame update
        public void SetInventory()
        {
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                inventory.GetSlots[i].parent = this;
                inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
            }

            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        }

        private void OnSlotUpdate(InventorySlot _slot)
        {
            if (_slot.item.Id >= 0)
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            }
            else
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        public abstract void CreateSlots();


        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoveredOver = obj;
        }

        public void OnExit(GameObject obj)
        {
            MouseData.slotHoveredOver = null;
            Message.Send<UIItemInfoCloseMsg>(new UIItemInfoCloseMsg());
        }

        public void OnDragStart(GameObject obj)
        {
            MouseData.tempItemBegingDragged = CreateTempItem(obj);
        }

        public GameObject CreateTempItem(GameObject obj)
        {
            GameObject tempItem = null;
            if (slotsOnInterface[obj].item.Id >= 0)
            {
                tempItem = new GameObject();
                var rt = tempItem.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(50, 50);
                tempItem.transform.SetParent(transform.parent);
                var img = tempItem.AddComponent<Image>();
                img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
                img.raycastTarget = false;
            }

            return tempItem;
        }


        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.tempItemBegingDragged);
            
            if (MouseData.interfaceMouseIsOver == null || slotsOnInterface[obj].parent.inventory.type == InventoryType.Consume)
            {
                itemDestort(inventory.type, obj);
                return;
            }

            if (MouseData.slotHoveredOver)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            }
        }

        public void OnDrag(GameObject obj)
        {
            if (MouseData.tempItemBegingDragged != null)
            {
                MouseData.tempItemBegingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }

        public void OnClick(GameObject obj)
        {
            InventorySlot tempslot = slotsOnInterface[obj];

            string itemInfo = "";
            for (int i = 0; i < tempslot.item.buffs.Length; i++)
            {
                itemInfo += slotsOnInterface[obj].item.buffs[i].status.ToString() + " : " + slotsOnInterface[obj].item.buffs[i].value;
            }

            if (slotsOnInterface[obj].item.Id >= 0)
                Message.Send<UIItemInfoMsg>(new UIItemInfoMsg(tempslot.ItemObject.uiDisplay, tempslot.item.Name, itemInfo));
        }

        private void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }

        private void OnExitInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = null;
        }
    }
    public static class MouseData
    {
        public static UserInterface interfaceMouseIsOver;
        public static GameObject tempItemBegingDragged;
        public static GameObject slotHoveredOver;
    }

    public static class ExtensionMethods
    {
        public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
            {
                if (_slot.Value.item.Id >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}