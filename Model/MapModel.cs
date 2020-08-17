using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Models
{
    public class MapModel : Model
    {
        GameModel owner;
        StreamingCSVLoader fileData = new StreamingCSVLoader();
        Dictionary<MapType, string> DicMapName = new Dictionary<MapType, string>();
        Dictionary<MapType, float> DicStartVecX = new Dictionary<MapType, float>();
        Dictionary<MapType, float> DicStartVecY = new Dictionary<MapType, float>();
        Dictionary<MapType, bool> DicIsBattleOn = new Dictionary<MapType, bool>();
        Dictionary<MapType, bool> DicIsBossMap = new Dictionary<MapType, bool>();

        Dictionary<MapType, float> DicCameraMinX = new Dictionary<MapType, float>();
        Dictionary<MapType, float> DicCameraMaxX = new Dictionary<MapType, float>();
        Dictionary<MapType, float> DicCameraMinY = new Dictionary<MapType, float>();
        Dictionary<MapType, float> DicCameraMaxY = new Dictionary<MapType, float>();

     
        public void Setup(GameModel _owner, string _fileName)
        {
            owner = _owner;
            fileData.Load(_fileName + ".CSV", CsvLoaded);
        }

        void CsvLoaded()
        {
            var datas = fileData.GetValue("Index");

            foreach (var o in datas)
            {
                var index = fileData.GetEqualsIndex("Index", o);
                var prefabName = fileData.GetValue("PrefabName", index);
                var startX = fileData.GetValue("StartX", index);
                var startY = fileData.GetValue("StartY", index);
                var isBattleMap = fileData.GetValue("IsBattleOn", index);
                var isBossMap = fileData.GetValue("IsBossMap", index);

                var cameraMinX = fileData.GetValue("CameraMinX", index);
                var cameraMaxX = fileData.GetValue("CameraMaxX", index);
                var cameraMinY = fileData.GetValue("CameraMinY", index);
                var cameraMaxY = fileData.GetValue("CameraMaxY", index);

                DicMapName.Add((MapType)index, prefabName);
                DicStartVecX.Add((MapType)index, float.Parse(startX));
                DicStartVecY.Add((MapType)index, float.Parse(startY));
                DicIsBattleOn.Add((MapType)index, bool.Parse(isBattleMap));
                DicIsBossMap.Add((MapType)index, bool.Parse(isBossMap));

                DicCameraMinX.Add((MapType)index, float.Parse(cameraMinX));
                DicCameraMaxX.Add((MapType)index, float.Parse(cameraMaxX));
                DicCameraMinY.Add((MapType)index, float.Parse(cameraMinY));
                DicCameraMaxY.Add((MapType)index, float.Parse(cameraMaxY));
            }
        }

        public string GetMapName (MapType _mapType)
        {
            return DicMapName[_mapType];
        }

        public Vector2 GetStartPos(MapType _mapType)
        {
            return new Vector2(DicStartVecX[_mapType], DicStartVecY[_mapType]);
        }

        public bool GetIsBattleOn(MapType _mapType)
        {
            return DicIsBattleOn[_mapType];
        }

        public bool GetIsBossMap(MapType _mapType)
        {
            return DicIsBossMap[_mapType];
        }
    

        public float GetCameraMinX(MapType _mapType) { return DicCameraMinX[_mapType]; }
        public float GetCameraMaxX(MapType _mapType) { return DicCameraMaxX[_mapType]; }
        public float GetCameraMinY(MapType _mapType) { return DicCameraMinY[_mapType]; }
        public float GetCameraMaxY(MapType _mapType) { return DicCameraMaxY[_mapType]; }
    }
}