using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Constants
{
    public enum ManagerType
    {
        Map,
        Player,
        Monster,
        Inventory,
        Npc,
    }

    public enum MapType
    {
        Stage1_1,
        Stage1_2,
        Stage1_3,
    }

    public enum MapMovePointType
    {
        Start,
        End,
    }

    public enum MonsterType
    {
        Demon,
        Ogre,
        Zombie,
        Phoenix
    }

    public enum PlayerType
    {
        Witch,
        Warrior
    }


    public enum InventoryType
    {
        Inventory,
        Equipment,
        Consume,
    }

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
        End,
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

}