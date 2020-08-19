using JHchoi.Constants;
using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JHchoi.Contents
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] ItemType itemType;
        
        GameObject SlotItem;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                if (eventData.pointerDrag.gameObject.GetComponent<UIItem>().ItemType == itemType)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                        Message.Send<SlotItemMsg>(new SlotItemMsg(false, transform.GetChild(i).gameObject.GetComponent<UIItem>().ItemKind));
                    }


                    UIItem item = eventData.pointerDrag.gameObject.GetComponent<UIItem>();
                    SlotItem = Instantiate(eventData.pointerDrag.gameObject);
                    SlotItem.GetComponent<UIItem>().Init_UIItem(item.Item, item.ItemName, item.ItemComment, item.ItemKind, item.ItemType, item.Sprite);
                    SlotItem.transform.parent = transform;
                    SlotItem.transform.localScale = Vector3.one;
                    SlotItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    SlotItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    SlotItem.GetComponent<CanvasGroup>().alpha = 1;
                    Message.Send<SlotItemMsg>(new SlotItemMsg(true, SlotItem.GetComponent<UIItem>().ItemKind));
                }
            }
        }
    }
}