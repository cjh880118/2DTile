using JHchoi.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class MapBase : MonoBehaviour
    {
        public MapObject mapObject;

        [SerializeField] private MapType MapType;
        [SerializeField] private GameObject monsters;
        [SerializeField] private GameObject npcs;
        [SerializeField] private GameObject mapMove;

        public bool IsBattleMap { get => mapObject.isBattleAble; }
        public Vector2 StartPos { get => mapObject.startPos; }
        public Vector2 EndPos { get => mapObject.endPos; }

        public bool IsBossMap { get => mapObject.isBossMap; }

        public GameObject Monsters { get => monsters; }
        public GameObject Npcs { get => npcs; }

        public MapCameraLimit GetCameraLimit()
        {
            return mapObject.CameraLimit;
        }

    }
}