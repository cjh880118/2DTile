using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Constants
{
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

    public enum Map
    {
        World,
        Dungeon
    }

    public enum Monster 
    { 
        Demon,
        Zombie,
        Ogre
    }

}