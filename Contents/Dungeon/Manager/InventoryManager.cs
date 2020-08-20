using JHchoi.Constants;
using JHchoi.Contents;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attribute = JHchoi.Contents.Attribute;

namespace JHchoi.Managers
{
    public class InventoryManager : IManager
    {
        public delegate void SlotUpdated(Attribute[] attributes);
        public InventoryObject inventory;
        public InventoryObject equipment;
        public Attribute[] attributes;
        public SlotUpdated ItemUpdate;

        public override IEnumerator Load_Resource()
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }

            for (int i = 0; i < equipment.GetSlots.Length; i++)
            {
                equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            }


            AddMessage();
            yield return null;
        }

        public void OnBeforeSlotUpdate(InventorySlot _slot)
        {
            if (_slot.ItemObject == null)
                return;

            switch (_slot.parent.inventory.type)
            {
                case InventoryType.Inventory:
                    break;
                case InventoryType.Equipment:
                    print(string.Concat("Remove ", _slot.ItemObject, " on", _slot.parent.inventory.type, ", Allowed Items : ", string.Join(", ", _slot.AllowedItems)));
                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].status)
                                attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                    break;

                default:
                    break;
            }
            ItemUpdate.Invoke(attributes);

            Debug.Log(attributes[0].value.BaseValue);
            Debug.Log(attributes[0].value.ModifiedValue);
            print("OnBeforeSlotUpdate");
        }
        public void OnAfterSlotUpdate(InventorySlot _slot)
        {
            if (_slot.ItemObject == null)
                return;


            switch (_slot.parent.inventory.type)
            {
                case InventoryType.Inventory:
                    break;
                case InventoryType.Equipment:
                    print(string.Concat("Placed ", _slot.ItemObject, " on", _slot.parent.inventory.type, ", Allowed Items : ", string.Join(", ", _slot.AllowedItems)));
                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].status)
                                attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }

                    break;

                default:
                    break;
            }

            ItemUpdate.Invoke(attributes);
            Debug.Log(attributes[0].value.BaseValue);
            Debug.Log(attributes[0].value.ModifiedValue);
            print("OnAfterSlotUpdate");
        }

        private void AddMessage()
        {
            Message.AddListener<DropItemMsg>(DropItem);
            Message.AddListener<AddItemMsg>(AddItem);

        }

        private void AddItem(AddItemMsg msg)
        {
             var item = msg.item.GetComponent<GroundItem>();
            if (item)
            {
                Item _item = new Item(item.item);
                if (inventory.AddItem(_item, 1))
                {
                    Destroy(msg.item);
                }
            }
        }

        private void DropItem(DropItemMsg msg)
        {
            int itemKind = UnityEngine.Random.Range((int)ItemKind.Sword, (int)ItemKind.End);
            Drop((ItemKind)itemKind, msg.dropPos);
        }

        private void Drop(ItemKind _itmeKind, Vector2 _dropPos)
        {
            //테스트
            _itmeKind = ItemKind.Axe;

            //int dropCount;// = 1;
            //if (_itmeKind == ItemKind.Coin)
            //    dropCount = UnityEngine.Random.Range(1, 5);
            //else if (_itmeKind == ItemKind.HealPotion)
            //    dropCount = UnityEngine.Random.Range(1, 3);


            string path = "Prefabs/Item/" + _itmeKind.ToString();
            var obj = Instantiate(Resources.Load(path) as GameObject);
            obj.transform.position = _dropPos;
            obj.transform.parent = transform;

        }
        public void AttributeModified(Attribute attribute)
        {
            Debug.Log(string.Concat(attribute.type, " was update! value is now ", attribute.value.ModifiedValue));
        }

        private void OnApplicationQuit()
        {
            inventory.Clear();
            equipment.Clear();
        }

        public Attribute[] GetAttribute()
        {
            return attributes;
        }

    }

    [Serializable]
    public class Attribute
    {
        [NonSerialized]
        public InventoryManager parent;
        public Status type;
        public ModifiableInt value;
        public void SetParent(InventoryManager _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }
}