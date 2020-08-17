using JHchoi.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Models
{
    public class ItemModel : Model
    {
        GameModel owner;
        StreamingCSVLoader fileData = new StreamingCSVLoader();

        Dictionary<ItemKind, string> DicItemName = new Dictionary<ItemKind, string>();
        Dictionary<ItemKind, ItemType> DicItemType = new Dictionary<ItemKind, ItemType>();
        Dictionary<ItemKind, string> DicItemComment = new Dictionary<ItemKind, string>();
        Dictionary<ItemKind, bool> DicItemUseAble = new Dictionary<ItemKind, bool>();
        Dictionary<ItemKind, bool> DicItemStackAble = new Dictionary<ItemKind, bool>();

        public void Setup(GameModel _owner, string _fileName)
        {
            owner = _owner;
            fileData.Load(_fileName + ".CSV", CsvLoaded);
        }

        void CsvLoaded()
        {
            var datas = fileData.GetValue("Index");

            foreach (var o in datas)
            {
                var index = fileData.GetEqualsIndex("Index", o);
                var prefabName = fileData.GetValue("PrefabName", index);
                var itemKind = fileData.GetValue("ItemKind", index);
                var itemType = fileData.GetValue("ItemType", index);
                var comment = fileData.GetValue("Comment", index);
                var isUseAble = fileData.GetValue("IsUseAble", index);
                var isStackAble = fileData.GetValue("isStackAble", index);

                DicItemName.Add((ItemKind)index, prefabName);
                DicItemType.Add((ItemKind)index, (ItemType)Enum.Parse(typeof(ItemType), itemType));
                DicItemComment.Add((ItemKind)index, comment);
                DicItemUseAble.Add((ItemKind)index, bool.Parse(isUseAble));
                DicItemStackAble.Add((ItemKind)index, bool.Parse(isStackAble));
            }
        }

        public string GetItemName(ItemKind _itemKind)
        {
            return DicItemName[_itemKind];
        }

        public ItemType GetItemType(ItemKind _itemKind)
        {
            return DicItemType[_itemKind];
        }

        public string GetItemComment(ItemKind _itemKind)
        {
            return DicItemComment[_itemKind];
        }

        public bool GetItemUseAble(ItemKind _itemKind)
        {
            return DicItemUseAble[_itemKind];
        }

        public bool GetItemStackAble(ItemKind _itemKind)
        {
            return DicItemStackAble[_itemKind];
        }


    }
}