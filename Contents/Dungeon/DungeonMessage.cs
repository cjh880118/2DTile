using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Contents;

namespace JHchoi.UI.Event
{
    public class UIPlayerMsg : Message
    {
        public string name;
        public int maxHp;
        public int hp;

        public UIPlayerMsg(string _name, int _maxHp, int _hp)
        {
            name = _name;
            maxHp = _maxHp;
            hp = _hp;
        }
    }

    public class UIMonsterMsg : Message
    {
        public string name;
        public int maxHp;
        public int hp;

        public UIMonsterMsg(string _name, int _maxHp, int _hp)
        {
            name = _name;
            maxHp = _maxHp;
            hp = _hp;
        }
    }

    public class UIInventoryMsg : Message
    {
        public LinkedList<IItem> listItem;
        public Inventory inventory;
        public UIInventoryMsg(LinkedList<IItem> _listItem)
        {
            listItem = _listItem;
            //inventory = _inventroy;
        }
    }

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

    public class DropItemMsg : Message
    {
        public Vector2 dropPos;
        public DropItemMsg(Vector2 _dropPos)
        {
            dropPos = _dropPos;
        }
    }

    public class GainItemMsg : Message
    {
        public IItem item;
        public GainItemMsg(IItem _item)
        {
            item = _item;
        }
    }

    public class RemoveItemMsg : Message
    {
        public IItem item;
        public RemoveItemMsg(IItem _item)
        {
            item = _item;
        }
    }
}