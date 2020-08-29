using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JHchoi.Constants;

[CreateAssetMenu(fileName = "New Map", menuName = "Map System/Map")]
public class MapObject : ScriptableObject
{
    public MapType mapType;
    public Vector2 startPos;
    public Vector2 endPos;
    public bool isBattleAble;
    public bool isBossMap;
    public MapCameraLimit CameraLimit = new MapCameraLimit();

}

[Serializable]
public class MapCameraLimit
{
    public float Camera_Min_X;
    public float Camera_Max_X;
    public float Camera_Min_Y;
    public float Camera_Max_Y;
}

