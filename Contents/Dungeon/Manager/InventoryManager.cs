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
    public class InventoryManager : ManagerBase
    {
        public delegate void SlotUpdated(Attribute[] attributes);
        public event SlotUpdated ItemUpdate;

        public delegate void EventHandler(object sender, ConsumeItem e);
        public event EventHandler EvnetUsePotion;

        public InventoryObject inventory;
        public InventoryObject equipment;
        public InventoryObject consume;

        public InventoryBase inventoryInterface;
        public InventoryBase inventoryEquipment;
        public InventoryBase inventoryConsume;

        public Attribute[] attributes;

        GameObject manager;

        public override IEnumerator Load_Resource()
        {
            UI.IDialog.RequestDialogEnter<UI.InventoryDialog>();
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }

            for (int i = 0; i < equipment.GetSlots.Length; i++)
            {
                equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            }

            inventoryConsume = GameObject.Find("imgPotion").GetComponent<InventoryBase>();
            inventoryInterface = GameObject.Find("ImgBagInventory").GetComponent<InventoryBase>();
            inventoryEquipment = GameObject.Find("ImgCharacterInventory").GetComponent<InventoryBase>();

            inventoryConsume.itemDestort += ItemDestory;
            inventoryInterface.itemDestort += ItemDestory;
            inventoryEquipment.itemDestort += ItemDestory;


            UI.IDialog.RequestDialogExit<UI.InventoryDialog>();

            manager = this.gameObject;
            inventory.Clear();
            equipment.Clear();
            consume.Clear();
            AddMessage();
            yield return null;
        }

        private void ItemDestory(InventoryType type, GameObject obj)
        {
            InventoryBase tempInterface = null;

            switch (type)
            {
                case InventoryType.Inventory:
                    tempInterface = inventoryInterface;
                    break;
                case InventoryType.Equipment:
                    tempInterface = inventoryEquipment;
                    break;
                case InventoryType.Consume:
                    tempInterface = inventoryConsume;
                    break;
                default:
                    break;
            }

            if (type == InventoryType.Inventory)
            {
                for (int i = 0; i < consume.GetSlots.Length; i++)
                {
                    if (consume.GetSlots[i].item == inventoryInterface.slotsOnInterface[obj].item)
                        return;
                }
            }
            else if (type == InventoryType.Equipment)
                return;

            tempInterface.slotsOnInterface[obj].RemoveItem();
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

            ItemUpdate(attributes);
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

            ItemUpdate(attributes);
        }

        private void AddMessage()
        {
            Message.AddListener<DropItemMsg>(DropItem);
            Message.AddListener<AddItemMsg>(AddItem);

        }

        private void AddItem(AddItemMsg msg)
        {
            var item = msg.item.GetComponent<ItemBase>();

            if (item)
            {
                Item _item = new Item(item.item);

                if (inventory.AddItem(_item, 1))
                {
                    if (item.item.type == Itemtype.HealPotion)
                    {
                        for (int i = 0; i < consume.GetSlots.Length; i++)
                        {
                            if (consume.GetSlots[i].item.Id == _item.Id)
                            {
                                consume.AddItem(_item, 1);
                                break;
                            }
                        }
                    }
                    Destroy(msg.item);
                }
            }
        }

        private void DropItem(DropItemMsg msg)
        {
            int itemKind = UnityEngine.Random.Range((int)Itemtype.Sword, (int)Itemtype.End);
            Drop((Itemtype)itemKind, msg.dropPos);
        }

        private void Drop(Itemtype _itmeKind, Vector2 _dropPos)
        {
            //테스트
            string path = "Prefabs/Item/" + _itmeKind.ToString();
            var obj = Instantiate(Resources.Load(path) as GameObject);
            obj.transform.position = _dropPos;
            obj.transform.SetParent(manager.transform);

        }
        public void AttributeModified(Attribute attribute)
        {
            Debug.Log(string.Concat(attribute.type, " was update! value is now ", attribute.value.ModifiedValue));
        }

        public void UseItem()
        {
            for (int i = 0; i < consume.GetSlots.Length; i++)
            {
                if (consume.GetSlots[i].ItemObject == null)
                    return;

                if (consume.GetSlots[i].ItemObject.equipSlot == EquipSlot.Consume)
                {
                    if (consume.GetSlots[i].amount > 0)
                    {
                        EvnetUsePotion?.Invoke(this, new ConsumeItem(consume.GetSlots[i].ItemObject.recoverHp, 0));

                        if (consume.GetSlots[i].amount == 1)
                        {
                            for (int j = 0; j < inventory.GetSlots.Length; j++)
                            {
                                if (inventory.GetSlots[j].item == consume.GetSlots[i].item)
                                {
                                    inventory.GetSlots[j].RemoveItem();
                                    break;
                                }
                            }

                            consume.GetSlots[i].RemoveItem();
                        }
                        else
                        {
                            consume.AddItem(consume.GetSlots[i].item, -1);
                            inventory.AddItem(consume.GetSlots[i].item, -1);
                        }

                        
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            inventory.Clear();
            equipment.Clear();
            consume.Clear();
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

    public class ConsumeItem : EventArgs
    {
        public int Hp;
        public int Mp;

        public ConsumeItem(int _hp, int _mp)
        {
            Hp = _hp;
            Mp = _mp;
        }
    }
}