using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JHchoi.Contents
{
    public class IItem : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private bool isUsePossible;
        private string itemName;
        private string itemComment;
        private ItemKind itemKind;
        private ItemType itemType;
        private int count;
        private bool isStackable;
        private bool isItemInfoOpen = false;
        [SerializeField] private GameObject ItemInfo;

        public bool IsUsePossible { get => isUsePossible; set => isUsePossible = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemComment { get => itemComment; set => itemComment = value; }
        public ItemKind ItemKind { get => itemKind; set => itemKind = value; }
        public int Count { get => count; set => count = value; }
        public bool IsStackable { get => isStackable; set => isStackable = value; }
        public ItemType ItemType { get => itemType; set => itemType = value; }


        public void Init_Item(ItemKind _ItemKind, ItemType _itemType, string _name, string _itemComment, int _count, bool _isUsePossible, bool _isStackable)
        {
            itemKind = _ItemKind;
            itemType = _itemType;
            itemName = _name;
            itemComment = _itemComment;
            count = _count;
            isUsePossible = _isUsePossible;
            isStackable = _isStackable;
            ItemInfo = transform.GetChild(0).gameObject;
        }

        public virtual void UseItem() { }

        public virtual void ItemEffect() { }

        private void OnMouseOver()
        {
            if (!isItemInfoOpen)
            {
                isItemInfoOpen = true;
                ItemInfo.SetActive(isItemInfoOpen);
                ItemInfo.GetComponent<TextMeshPro>().SetText(itemName + "\n  " + itemComment);
                Debug.Log(itemComment);
            }
        }

        private void OnMouseExit()
        {
            isItemInfoOpen = false;
            ItemInfo.SetActive(isItemInfoOpen);
        }

        //public void OnBeginDrag(PointerEventData eventData)
        //{
        //    Debug.Log("OnBeginDrag");
        //    canvasGroup.alpha = 0.6f;
        //    canvasGroup.blocksRaycasts = false;
        //}

        //public void OnDrag(PointerEventData eventData)
        //{
        //    Debug.Log("OnDrag");
        //    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //}

        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    Debug.Log("OnEndDrag");
        //    canvasGroup.alpha = 1f;
        //    canvasGroup.blocksRaycasts = true;
        //}

        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    Debug.Log("OnPointerDown");
        //}
    }
}