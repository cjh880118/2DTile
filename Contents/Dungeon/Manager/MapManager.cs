﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using JHchoi.Contents;
using System;
using JHchoi.Models;
using JHchoi.UI.Event;

namespace JHchoi.Managers
{
    public class EventMapMove : EventArgs
    {
        public object sender;
        public MapMovePointType type;
        public EventMapMove(object sender, MapMovePointType type)
        {
            this.sender = sender;
            this.type = type;
        }
    }


    public class MapManager : ManagerBase
    {
        private GameObject mapObject;
        private bool isLoadComplete;
        public bool IsLoadComplete { get => isLoadComplete; }
        //public delegate void EventHandler(UnityEngine.Object sender, MapMovePointType type);
        public event EventHandler<EventMapMove> EvnetMapMove;

        public override IEnumerator Load_Resource(string name)
        {
            isLoadComplete = false;
            ClearMap();

            string path = "Prefabs/Map/" + name;
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                mapObject = Instantiate(o) as GameObject;
                mapObject.transform.parent = transform;
                mapObject.name = name;
                Message.Send<CameraLimitMsg>(new CameraLimitMsg(mapObject.GetComponent<MapBase>().GetCameraLimit()));

                var obj = mapObject.transform.Find("MapMovePoint");

                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    if (obj.transform.GetChild(i).gameObject.activeSelf)
                        obj.transform.GetChild(i).gameObject.GetComponentInChildren<MapMovePoint>().MoveMap += MoveMapEvent;
                }

            }));

            isLoadComplete = true;
        }

        public void MapClear()
        {
            Debug.Log("MapClear");
        }

        private void MoveMapEvent(object sender, MapMovePointType type)
        {
            EvnetMapMove?.Invoke(this, new EventMapMove(this, type));
        }

        public void ClearMap()
        {
            Destroy(mapObject);
            mapObject = null;
        }

        public bool GetBattlePossible()
        {
            return mapObject.GetComponent<MapBase>().IsBattleMap;
        }

        public Vector2 GetStartPos()
        {
            return mapObject.GetComponent<MapBase>().StartPos;
        }

        public Vector2 GetEndPos()
        {
            return mapObject.GetComponent<MapBase>().EndPos;
        }

        public GameObject GetMonsters()
        {
            return mapObject.GetComponent<MapBase>().Monsters;
        }

        public GameObject GetNpcs()
        {
            return mapObject.GetComponent<MapBase>().Npcs;
        }

        public bool GetIsBossMap()
        {
            return mapObject.GetComponent<MapBase>().IsBossMap;
        }

        public Grid GetGrid()
        {
            return mapObject.GetComponent<MapBase>().Grid;
        }

    }
}
