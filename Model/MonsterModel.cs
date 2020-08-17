using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.Models
{
    public class MonsterModel : Model
    {
        GameModel owner;
        StreamingCSVLoader fileData = new StreamingCSVLoader();

        Dictionary<MonsterType, string> DicMonsterName = new Dictionary<MonsterType, string>();
        Dictionary<MonsterType, int> DicMonsterHp = new Dictionary<MonsterType, int>();
        Dictionary<MonsterType, float> DicMonsterSpeed = new Dictionary<MonsterType, float>();
        Dictionary<MonsterType, int> DicAttackMonsterDamage = new Dictionary<MonsterType, int>();
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
                var hp = fileData.GetValue("Hp", index);
                var moveSpeed = fileData.GetValue("MoveSpeed", index);
                var attackDamage = fileData.GetValue("AttackDamage", index);

                DicMonsterName.Add((MonsterType)index, prefabName);
                DicMonsterHp.Add((MonsterType)index, int.Parse(hp));
                DicMonsterSpeed.Add((MonsterType)index, float.Parse(moveSpeed));
                DicAttackMonsterDamage.Add((MonsterType)index, int.Parse(attackDamage));
            }
        }

        public string GetMonsterName(MonsterType _monster)
        {
            return DicMonsterName[_monster];
        }

        public int GetMonsterHp(MonsterType _monster)
        {
            return DicMonsterHp[_monster];
        }

        public float GetMonsterMoveSpeed(MonsterType _monster)
        {
            return DicMonsterSpeed[_monster];
        }

        public int GetMonsterAttackDamage(MonsterType _monster)
        {
            return DicAttackMonsterDamage[_monster];
        }

    }
}
