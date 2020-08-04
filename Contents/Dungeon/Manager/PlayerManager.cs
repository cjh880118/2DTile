using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Managers
{
    public class PlayerManager : IManager
    {
        public override IEnumerator Load_Resource()
        {
            string path = "Player/Witch";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var Player = Instantiate(o) as GameObject;
                Player.name = "Player";
                Player.transform.parent = transform;
                gameObjects.Add(Player);
            }));
        }
    }
}