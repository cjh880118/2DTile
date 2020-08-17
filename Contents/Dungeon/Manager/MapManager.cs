using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using JHchoi.Contents;
using System;
using JHchoi.Models;
using JHchoi.UI.Event;

namespace JHchoi.Managers
{
    public class MapManager : IManager
    {
        MapModel mapModel = Model.First<MapModel>();
        private GameObject mapObject;
        private MapType nowMap;
        private bool isLoadComplete;
        public MapType NowMap { get => nowMap; set => nowMap = value; }
        public bool IsLoadComplete { get => isLoadComplete; set => isLoadComplete = value; }

        public override IEnumerator Load_Resource(string name)
        {
            isLoadComplete = false;
            ClearMap();

            string path = "Prefabs/Map/" + name;
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                nowMap = (MapType)Enum.Parse(typeof(MapType), name);
                mapObject = Instantiate(o) as GameObject;
                mapObject.transform.parent = transform;
                mapObject.name = name;
                mapObject.GetComponent<IMap>().InitMap(mapModel.GetStartPos(nowMap), mapModel.GetIsBattleOn(nowMap));
                Message.Send<CameraLimitMsg>(new CameraLimitMsg(
                    mapModel.GetCameraMinX(nowMap),
                    mapModel.GetCameraMaxX(nowMap),
                    mapModel.GetCameraMinY(nowMap),
                    mapModel.GetCameraMaxY(nowMap)
                    )); ;

            }));

            isLoadComplete = true;
        }

        public void ClearMap()
        {
            Destroy(mapObject);
            mapObject = null;
        }

        public bool GetBattlePossible()
        {
            return mapObject.GetComponent<IMap>().IsBattleMap;
        }

        public Vector2 GetStartPos()
        {
            return mapObject.GetComponent<IMap>().StartPos;
        }

        public GameObject GetMonsters()
        {
            return mapObject.GetComponent<IMap>().Monsters;
        }

        public GameObject GetNpcs()
        {
            return mapObject.GetComponent<IMap>().Npcs;
        }

    }
}
