using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public class FireCircle : IMagic
    {
        public void CreateFireCircle(int _dmage, float _moveSpeed)
        {
            Damage = _dmage;
            MoveSpeed = _moveSpeed;
        }
    }
}