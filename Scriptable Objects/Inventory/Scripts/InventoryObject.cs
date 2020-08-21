using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

namespace JHchoi.Contents
{
    public enum InventoryType
    {
        Inventory,
        Equipment,
        Consume,
    }

    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDatabaseObject database;
        public InventoryType type;
        public Inventory Container;
        public InventorySlot[] GetSlots { get { return Container.Slots; } }

        public bool AddItem(Item _item, int _amount)
        {
            if (EmtpySlotCount <= 0 && !database.ItemObjects[_item.Id].stackable)
                return false;

            InventorySlot slot = FindItemOnInventory(_item);
            if (!database.ItemObjects[_item.Id].stackable || slot == null)
            {
                SetEmptySlot(_item, _amount);
                return true;
            }

            slot.AddAmount(_amount);
            return true;
        }

        public int EmtpySlotCount
        {
            get
            {
                int counter = 0;
                for (int i = 0; i < GetSlots.Length; i++)
                {
                    if (GetSlots[i].item.Id <= -1)
                        counter++;
                }
                return counter;
            }
        }

        public InventorySlot FindItemOnInventory(Item _item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id == _item.Id)
                    return GetSlots[i];
            }

            return null;
        }


        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id <= -1)
                {
                    GetSlots[i].UpdateSlot(_item, _amount);
                    return GetSlots[i];
                }
            }

            //set up functionality for full inventory
            return null;
        }

        //item1 기존 item2 새로운
        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            {
                Debug.Log("item 1 : " + item1.parent.inventory.type);
                Debug.Log("item 2 : " + item2.parent.inventory.type);

                if (item2.parent.inventory.type == InventoryType.Consume)
                    item2.UpdateSlot(item1.item, item1.amount);

                else
                {
                    InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                    item2.UpdateSlot(item1.item, item1.amount);
                    item1.UpdateSlot(temp.item, temp.amount);
                }
            }

        }

        public void RemoveItem(Item _item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item == _item)
                {
                    GetSlots[i].UpdateSlot(null, 0);
                }
            }
        }

        [Serializable]
        public class Inventory
        {
            public InventorySlot[] Slots = new InventorySlot[25];
            public void Clear()
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].RemoveItem();
                }
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();

        }

        [ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("Load");
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                Inventory newContainer = (Inventory)formatter.Deserialize(stream);
                for (int i = 0; i < GetSlots.Length; i++)
                {
                    GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
                }
                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
        }
    }

    public delegate void SlotUpdated(InventorySlot _slot);

    [Serializable]
    public class InventorySlot
    {
        public EquipSlot[] AllowedItems = new EquipSlot[0];
        [System.NonSerialized]
        public UserInterface parent;
        [System.NonSerialized]
        public GameObject slotDisplay;
        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;
        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;

        public Item item;
        public int amount;

        public ItemObject ItemObject
        {
            get
            {
                if (item.Id >= 0)
                {
                    return parent.inventory.database.ItemObjects[item.Id];
                }
                return null;
            }
        }

        public InventorySlot()
        {
            UpdateSlot(new Item(), 0);
        }

        public InventorySlot(Item _item, int _amount)
        {
            UpdateSlot(_item, _amount);
        }

        public void UpdateSlot(Item _item, int _amount)
        {
            if (OnBeforeUpdate != null)
                OnBeforeUpdate.Invoke(this);

            item = _item;
            amount = _amount;

            if (OnAfterUpdate != null)
                OnAfterUpdate.Invoke(this);
        }

        public void RemoveItem()
        {
            UpdateSlot(new Item(), 0);
        }

        public void AddAmount(int value)
        {
            UpdateSlot(item, amount += value);
        }

        public bool CanPlaceInSlot(ItemObject _itemObject)
        {
            if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
                return true;

            for (int i = 0; i < AllowedItems.Length; i++)
            {
                if (_itemObject.equipSlot == AllowedItems[i])
                    return true;
            }

            return false;
        }
    }
}