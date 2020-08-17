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
        Dictionary<ItemKind, int> DicItemAttack = new Dictionary<ItemKind, int>();
        Dictionary<ItemKind, int> DicItemDefence = new Dictionary<ItemKind, int>();
        Dictionary<ItemKind, float> DicItemMoveSpeed = new Dictionary<ItemKind, float>();
        Dictionary<ItemKind, int> DicItemHp = new Dictionary<ItemKind, int>();
        Dictionary<ItemKind, int> DicItemMaxHp = new Dictionary<ItemKind, int>();


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

                var attack = fileData.GetValue("Attack", index);
                var defence = fileData.GetValue("Defence", index);
                var moveSpeed = fileData.GetValue("MoveSpeed", index);
                var hp = fileData.GetValue("Hp", index);
                var maxHp = fileData.GetValue("MaxHp", index);



                DicItemName.Add((ItemKind)index, prefabName);
                DicItemType.Add((ItemKind)index, (ItemType)Enum.Parse(typeof(ItemType), itemType));
                DicItemComment.Add((ItemKind)index, comment);
                DicItemUseAble.Add((ItemKind)index, bool.Parse(isUseAble));
                DicItemStackAble.Add((ItemKind)index, bool.Parse(isStackAble));

                DicItemAttack.Add((ItemKind)index, int.Parse(attack));
                DicItemDefence.Add((ItemKind)index, int.Parse(defence));
                DicItemMoveSpeed.Add((ItemKind)index, float.Parse(moveSpeed));
                DicItemHp.Add((ItemKind)index, int.Parse(hp));
                DicItemMaxHp.Add((ItemKind)index, int.Parse(maxHp));
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

        public int GetItemAttack(ItemKind _itemKind)
        {
            return DicItemAttack[_itemKind];
        }
        public int GetItemDefence(ItemKind _itemKind)
        {
            return DicItemDefence[_itemKind];
        }
        public float GetItemMoveSpeed(ItemKind _itemKind)
        {
            return DicItemMoveSpeed[_itemKind];
        }
        public int GetItemHp(ItemKind _itemKind)
        {
            return DicItemHp[_itemKind];
        }
        public int GetItemMaxHp(ItemKind _itemKind)
        {
            return DicItemMaxHp[_itemKind];
        }

    }
}