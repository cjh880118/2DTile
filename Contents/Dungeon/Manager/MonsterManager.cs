using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Constants;
using JHchoi.Contents;
using System;

namespace JHchoi.Managers
{
    public class MonsterManager : IManager
    {
        Dictionary<MonsterType, GameObject> DicMonsterObject = new Dictionary<MonsterType, GameObject>();
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler HitMonster;

        public void Init_Monster(GameObject _monsters)
        {

            for (int i = 0; i < _monsters.transform.childCount; i++)
            {
                _monsters.transform.GetChild(i).GetComponent<IMonster>().EventHitMonster += HitMonsters;
            }
        }

        private void HitMonsters(object sender, EventArgs e)
        {
            Debug.Log(string.Concat(sender.ToString(), "         :   ", e.ToString()));
            HitMonster?.Invoke(this, e);
        }


        public void MonsterClear()
        {
            DicMonsterObject.Clear();
        }
    }
}