using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public Attribute[] attributes;

    private void Start()
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
                print(string.Concat("Remove ", _slot.ItemObject," on", _slot.parent.inventory.type,", Allowed Items : ", string.Join(", ", _slot.AllowedItems)));
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

        Debug.Log(attributes[0].value.BaseValue);
        Debug.Log(attributes[0].value.ModifiedValue);
        print("OnAfterSlotUpdate");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    var item = other.GetComponent<GroundItem>();
    //    if (item)
    //    {
    //        Item _item = new Item(item.item);
    //        if (inventory.AddItem(_item, 1))
    //        {
    //            Destroy(other.gameObject);
    //        }
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            inventory.Load();
            equipment.Load();
        }

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
}

[Serializable]
public class Attribute
{
    [NonSerialized]
    public TestPlayer parent;
    public Status type;
    public ModifiableInt value;
    public void SetParent(TestPlayer _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
