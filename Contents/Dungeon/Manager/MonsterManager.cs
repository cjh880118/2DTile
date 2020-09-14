using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Constants;
using JHchoi.Contents;
using System;

namespace JHchoi.Managers
{
    public class MonsterManager : ManagerBase
    {
        Astar2D astar2D;
        Dictionary<MonsterType, GameObject> DicMonsterObject = new Dictionary<MonsterType, GameObject>();
        public delegate void EventHandler(object sender, bool isAllDie);
        public event EventHandler EventMonsterHit;

        GameObject objMonsters;
        int monsterCount;

        Grid grid;

        public void Init_Monster(GameObject _monsters, Grid _grid )
        {
            astar2D = new Astar2D(new Vector2Int(-18, -16), new Vector2Int(33, 11));

            objMonsters = _monsters;
            for (int i = 0; i < objMonsters.transform.childCount; i++)
            {
                objMonsters.transform.GetChild(i).GetComponent<MonsterBase>().InitMonster(astar2D, _grid);
                objMonsters.transform.GetChild(i).GetComponent<MonsterBase>().EventHitMonster += HitMonsters;
                
                //astar2D.PathFinding(tempVec, new Vector2Int(-16, -5));
            }
        }

        private void HitMonsters(object sender, EventMonsterHit e)
        {
            if (e.hp > 0)
                EventMonsterHit?.Invoke(this, false);
            else
                StartCoroutine(MonsterCountCheck(e.monster));
        }

        IEnumerator MonsterCountCheck(GameObject obj)
        {
            Destroy(obj);
            yield return new WaitForSeconds(3.0f);
            monsterCount = objMonsters.transform.childCount;
            Debug.Log("Monster count : " + monsterCount);

            if (monsterCount <= 0)
                EventMonsterHit?.Invoke(this, true);
        }


        public void MonsterClear()
        {
            DicMonsterObject.Clear();
        }
    }
}