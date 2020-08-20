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

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public enum PlayerStatus
    {
        Idle,
        Move,
        Atk,
        Hit,
        Die
    }

    public enum MapType
    {
        Stage1_1,
        Stage1_2,
        Stage1_3,
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

    public enum ItemKind
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
        boots,
        HealPotion,
        Coin,
        End
    }

    public enum ItemType
    {
        Gold,
        Weapon,
        Armor,
        Accessory,
        Consume,
    }

}