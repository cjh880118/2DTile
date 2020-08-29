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
        Dictionary<MonsterType, GameObject> DicMonsterObject = new Dictionary<MonsterType, GameObject>();
        public delegate void EventHandler(object sender, bool isAllDie);
        public event EventHandler EventMonsterHit;
    
        GameObject objMonsters;
        int monsterCount;

        public void Init_Monster(GameObject _monsters)
        {
            objMonsters = _monsters;
            for (int i = 0; i < objMonsters.transform.childCount; i++)
            {
                objMonsters.transform.GetChild(i).GetComponent<MonsterBase>().EventHitMonster += HitMonsters;
            }
        }


        private void HitMonsters(object sender, MonsterInfo _info)
        {
            if (_info.hp > 0)
                EventMonsterHit?.Invoke(this, false);
            else
                StartCoroutine(MonsterCountCheck(_info.objMonster));
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