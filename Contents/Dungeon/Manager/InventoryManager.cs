using JHchoi.Constants;
using JHchoi.Contents;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;


namespace JHchoi.Managers
{
    public class InventoryManager : IManager
    {
        private LinkedList<IItem> ListItem = new LinkedList<IItem>();

        public override IEnumerator Load_Resource()
        {
            AddMessage();
            yield return null;
        }

        private void AddMessage()
        {
            Message.AddListener<DropItemMsg>(DropItem);
            Message.AddListener<GainItemMsg>(GainItem);
            Message.AddListener<RemoveItemMsg>(RemoveItem);
        }

        private void DropItem(DropItemMsg msg)
        {
            int itemKind = UnityEngine.Random.Range((int)ItemKind.Sword, (int)ItemKind.End);
            //Drop((ItemKind)itemKind, msg.dropPos);
        }

        private void Drop(ItemKind _itmeKind, Vector2 _dropPos)
        {
            //테스트
            _itmeKind = ItemKind.HealPotion;

            int dropCount = 1;
            if (_itmeKind == ItemKind.Coin)
                dropCount = UnityEngine.Random.Range(1, 5);
            else if(_itmeKind == ItemKind.HealPotion)
                dropCount = UnityEngine.Random.Range(1, 3);


            string path = "Prefabs/Item/" + _itmeKind.ToString();
            var obj = Instantiate(Resources.Load(path) as GameObject);
            obj.transform.position = _dropPos;
            obj.transform.parent = transform;
            //obj.GetComponent<IItem>().Init_Item(_itmeKind,
            //    itemModel.GetItemType(_itmeKind),
            //    itemModel.GetItemName(_itmeKind),
            //    itemModel.GetItemComment(_itmeKind),
            //    dropCount,
            //    itemModel.GetItemUseAble(_itmeKind),
            //    itemModel.GetItemStackAble(_itmeKind),
            //    itemModel.GetItemAttack(_itmeKind),
            //    itemModel.GetItemDefence(_itmeKind),
            //    itemModel.GetItemMoveSpeed(_itmeKind),
            //    itemModel.GetItemHp(_itmeKind),
            //    itemModel.GetItemMaxHp(_itmeKind));
        }

        private void GainItem(GainItemMsg msg)
        {
            if (msg.item.IsStackable)
            {
                bool isItemAlreadyInInventory = false;
                foreach (var o in ListItem)
                {
                    if (o.ItemKind == msg.item.ItemKind)
                    {
                        o.Count += msg.item.Count;
                        isItemAlreadyInInventory = true;
                    }
                }

                if (!isItemAlreadyInInventory)
                    ListItem.AddLast(msg.item);
            }
            else
            {
                ListItem.AddLast(msg.item);
            }

            Message.Send<UIInventoryMsg>(new UIInventoryMsg(ListItem));
            //OnItemListChange?.Invoke(this, EventArgs.Empty);
        }

        private void RemoveItem(RemoveItemMsg msg)
        {
            ListItem.Remove(msg.item);
            Message.Send<UIInventoryMsg>(new UIInventoryMsg(ListItem));
        }

        //public int GetItemAttack(ItemKind _itemKind)
        //{
        //    return itemModel.GetItemAttack(_itemKind);
        //}

        //public int GetItemDefence(ItemKind _itemKind)
        //{
        //    return itemModel.GetItemDefence(_itemKind);
        //}

        //public float GetItemMoveSpeed(ItemKind _itemKind)
        //{
        //    return itemModel.GetItemMoveSpeed(_itemKind);
        //}


        public void GainItem(IItem _item)
        {
            ListItem.AddLast(_item);
        }

        public void RemoveItme(IItem item)
        {
            ListItem.Remove(item);
        }

        public LinkedList<IItem> GetItemList()
        {
            return ListItem;
        }

    }
}