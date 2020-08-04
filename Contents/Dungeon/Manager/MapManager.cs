using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.Managers
{
    public class MapManager : IManager
    {
        public override IEnumerator Load_Resource()
        {
            string path = "Map/Desert/Desert_Map";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var WorldMap = Instantiate(o) as GameObject;
                WorldMap.transform.parent = transform;
                WorldMap.name = "World_Map";
                gameObjects.Add(WorldMap);
                WorldMap.SetActive(false);
            }));


            path = "Map/Dungeon/Dungeon_Map";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var DungeonMap = Instantiate(o) as GameObject;
                DungeonMap.transform.parent = transform;
                DungeonMap.name = "Dungeon_Map";
                gameObjects.Add(DungeonMap);
                DungeonMap.SetActive(false);
            }));

        }

        public void Load_Map(Map _map)
        {
            for(int i = 0; i < gameObjects.Count; i++)
            {
                bool isActive = (i == (int)_map) ? true : false;
                gameObjects[i].SetActive(isActive);
            }
        }
    }
}
