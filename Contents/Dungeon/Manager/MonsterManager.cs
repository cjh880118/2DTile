using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Constants;
using JHchoi.Contents;

namespace JHchoi.Managers
{
    public class MonsterManager : IManager
    {
        MonsterModel monsterModel = Model.First<MonsterModel>();
        Dictionary<MonsterType, GameObject> DicMonsterObject = new Dictionary<MonsterType, GameObject>();


        public void Init_Monster(GameObject _monsters)
        {
            GameObject normalMonster = _monsters.transform.transform.GetChild(0).gameObject;
            GameObject bossMonster = _monsters.transform.transform.GetChild(1).gameObject;


            for (int i = 0; i < normalMonster.transform.childCount; i++)
            {
                MonsterType monsterType = normalMonster.transform.GetChild(i).GetComponent<IMonster>().MonsterType;
                normalMonster.transform.GetChild(i).GetComponent<IMonster>().Init_Monster(
                    monsterModel.GetMonsterName(monsterType),
                    monsterModel.GetMonsterHp(monsterType),
                    monsterModel.GetMonsterMoveSpeed(monsterType),
                    monsterModel.GetMonsterAttackDamage(monsterType)
                    );
            }

            for (int i = 0; i < bossMonster.transform.childCount; i++)
            {
                MonsterType monsterType = bossMonster.transform.GetChild(i).GetComponent<IBossMonster>().MonsterType;
                bossMonster.transform.GetChild(i).GetComponent<IBossMonster>().Init_Monster(monsterModel.GetMonsterName(monsterType),
                    monsterModel.GetMonsterHp(monsterType),
                    monsterModel.GetMonsterMoveSpeed(monsterType),
                    monsterModel.GetMonsterAttackDamage(monsterType)
                    );
            }
        }

        public void MonsterClear()
        {
            DicMonsterObject.Clear();
        }
    }
}