using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class IMap : MonoBehaviour
    {
        [SerializeField] protected MapType MapType;
        [SerializeField] private GameObject monsters;
        [SerializeField] private GameObject npcs;
        private bool isBattleMap;
        private Vector2 startPos;

        public bool IsBattleMap { get => isBattleMap; set => isBattleMap = value; }
        public Vector2 StartPos { get => startPos; set => startPos = value; }
        public GameObject Monsters { get => monsters; set => monsters = value; }
        public GameObject Npcs { get => npcs; set => npcs = value; }

        public virtual void InitMap( Vector2 _startPos, bool _isBattlePossible)
        {
            startPos = _startPos;
            isBattleMap = _isBattlePossible;
        }
    }
}