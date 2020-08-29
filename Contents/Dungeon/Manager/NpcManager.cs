using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Managers
{
    public class NpcManager : ManagerBase
    {
        public override IEnumerator Load_Resource()
        {
            yield return null;
            //string path = "Prefabs/Npc/Dungeon_Npc";
            //yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            //{
            //    var dungeonNpc = Instantiate(o) as GameObject;
            //    dungeonNpc.name = "DungeonNpc";
            //    dungeonNpc.transform.parent = transform;
            //}));
        }
    }
}