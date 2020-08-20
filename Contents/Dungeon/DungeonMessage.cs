using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Contents;

namespace JHchoi.UI.Event
{
    public class UIPlayerHpMsg : Message
    {
        public string name;
        public int maxHp;
        public int hp;

        public UIPlayerHpMsg(string _name, int _maxHp, int _hp)
        {
            name = _name;
            maxHp = _maxHp;
            hp = _hp;
        }
    }

    public class UIMonsterHpMsg : Message
    {
        public string name;
        public int maxHp;
        public int hp;

        public UIMonsterHpMsg(string _name, int _maxHp, int _hp)
        {
            name = _name;
            maxHp = _maxHp;
            hp = _hp;
        }
    }

    public class UISkillMsg : Message
    {
        public int skillNum;
        public UISkillMsg(int _skillNum)
        {
            skillNum = _skillNum;
        }
    }

    public class UIInventoryStatusMsg : Message
    {
        public string name;
        public int maxHp;
        public int hp;
        public int attack;
        public int defence;
        public float moveSpeed;
        public UIInventoryStatusMsg(string _name, int _maxHp, int _hp, int _attack, int _defence, float _moveSpeed)
        {
            name = _name;
            maxHp = _maxHp;
            hp = _hp;
            attack = _attack;
            defence = _defence;
            moveSpeed = _moveSpeed;
        }
    }

    public class UIItemInfoMsg : Message
    {
        public Sprite itemSprite;
        public string itemName;
        public string itemInfo;
        public UIItemInfoMsg(Sprite _sprite, string _name, string _info)
        {
            itemSprite = _sprite;
            itemName = _name;
            itemInfo = _info;
        }
    }

    public class UIItemInfoCloseMsg : Message { }


    public class LoadMapMsg : Message
    {
        public MapType map;
        public LoadMapMsg(MapType _map)
        {
            map = _map;
        }
    }

    public class CameraShakeMsg : Message { }

    public class BloodEffectMsg : Message { }

    public class CameraLimitMsg : Message
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public CameraLimitMsg(float _minX, float _maxX, float _minY, float _maxY)
        {
            minX = _minX;
            maxX = _maxX;
            minY = _minY;
            maxY = _maxY;
        }
    }





    /// <summary>
    /// Item Message
    /// </summary>
    public class DropItemMsg : Message
    {
        public Vector2 dropPos;
        public DropItemMsg(Vector2 _dropPos)
        {
            dropPos = _dropPos;
        }
    }

    public class AddItemMsg : Message
    {
        public GameObject item;
        public AddItemMsg(GameObject _item)
        {
            item = _item;
        }
    }
}