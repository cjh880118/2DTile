using JHchoi.Constants;
using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JHchoi.Contents
{
    public class UIItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private string itemName;
        private string itemComment;
        private ItemKind itemKind;
        private ItemType itemType;
        private IItem item;
        private int count;

        [SerializeField] Canvas canvas;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;


        public ItemType ItemType { get => itemType; set => itemType = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemComment { get => itemComment; set => itemComment = value; }
        public ItemKind ItemKind { get => itemKind; set => itemKind = value; }

        private void Awake()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Init_UIItem(IItem _item, string _name, string _comment, ItemKind _itemKind, ItemType _itemType)
        {
            item = _item;
            itemName = _name;
            itemComment = _comment;
            ItemKind = _itemKind;
            ItemType = _itemType;

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            rectTransform.anchoredPosition = Vector2.zero;
            //Message.Send<RemoveItemMsg>(new RemoveItemMsg(item));
            Destroy(this.gameObject);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            GameObject obj = Instantiate(this.gameObject);
            obj.transform.position = this.transform.position;
            obj.transform.parent = this.gameObject.transform.parent;
            obj.transform.localScale = new Vector3(gameObject.transform.localScale.x,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z);
            obj.GetComponent<UIItem>().Init_UIItem(item, itemName, itemComment, itemKind, itemType);
        }


    }
}