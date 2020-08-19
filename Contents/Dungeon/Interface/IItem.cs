using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JHchoi.Contents
{
    public class IItem : MonoBehaviour
    {
        private bool isUsePossible;
        private string itemName;
        private string itemComment;
        private ItemKind itemKind;
        private ItemType itemType;
        private int count;
        private bool isStackable;
        private bool isItemInfoOpen = false;
        private int attack;
        private int defence;
        private float moveSpeed;
        private int hp;
        private int maxHp;
        private Sprite sprite;
        [SerializeField] private GameObject ItemInfo;

        public bool IsUsePossible { get => isUsePossible; set => isUsePossible = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemComment { get => itemComment; set => itemComment = value; }
        public ItemKind ItemKind { get => itemKind; set => itemKind = value; }
        public int Count { get => count; set => count = value; }
        public bool IsStackable { get => isStackable; set => isStackable = value; }
        public ItemType ItemType { get => itemType; set => itemType = value; }
        public int Attack { get => attack; set => attack = value; }
        public int Defence { get => defence; set => defence = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public int Hp { get => hp; set => hp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }

        public void Init_Item(ItemKind _ItemKind, ItemType _itemType, string _name, string _itemComment, int _count, 
            bool _isUsePossible, bool _isStackable, int _attack, int _defence, float _moveSpeed, int _hp, int _maxHp)
        {
            itemKind = _ItemKind;
            itemType = _itemType;
            itemName = _name;
            itemComment = _itemComment;
            count = _count;
            isUsePossible = _isUsePossible;
            isStackable = _isStackable;
            attack = _attack;
            defence = _defence;
            moveSpeed = _moveSpeed;
            hp = _hp;
            maxHp = _maxHp;
            sprite = GetComponent<SpriteRenderer>().sprite;
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

       
    }
}