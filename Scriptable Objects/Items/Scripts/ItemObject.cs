using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Itemtype
{
    Sword,
    Hammer,
    Axe,
    Dagger,
    Robe,
    LeatherArmor,
    SteelArmor,
    Cap,
    Necklace,
    Boots,
    HealPotion,
    Coin,
}

public enum Status
{
    Attack,
    Defence,
    MoveSpeed,
}

public enum EquipSlot
{
    Weapon,
    Armor,
    Accessory,
    Consume,
    ETC,
}



[CreateAssetMenu(fileName = "New Itme", menuName = "Inventory System/Item/Inven")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable;
    public Itemtype type;
    public EquipSlot equipSlot;
    [TextArea(15, 20)]
    public string description;
    public int recoverHp;
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;

    public Item()
    {
        Name = "";
        Id = -1;

    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {

            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                status = item.data.buffs[i].status
            };
        }
    }
}

[Serializable]
public class ItemBuff : IModifiers
{
    public Status status;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

}


