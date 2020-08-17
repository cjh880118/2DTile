using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Models
{
    public class PlayerModel : Model
    {
        GameModel owner;
        StreamingCSVLoader fileData = new StreamingCSVLoader();

        Dictionary<PlayerType, string> DicPlayerName = new Dictionary<PlayerType, string>();
        Dictionary<PlayerType, int> DicPlayerHp = new Dictionary<PlayerType, int>();
        Dictionary<PlayerType, float> DicPlayerMoveSpeed = new Dictionary<PlayerType, float>();
        Dictionary<PlayerType, int> DicPlayerAttackDamage = new Dictionary<PlayerType, int>();

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

                DicPlayerName.Add((PlayerType)index, prefabName);
                DicPlayerHp.Add((PlayerType)index, int.Parse(hp));
                DicPlayerMoveSpeed.Add((PlayerType)index, float.Parse(moveSpeed));
                DicPlayerAttackDamage.Add((PlayerType)index, int.Parse(attackDamage));
            }
        }

        public string GetPlayerName(PlayerType _monster)
        {
            return DicPlayerName[_monster];
        }

        public int GetPlayerHp(PlayerType _monster)
        {
            return DicPlayerHp[_monster];
        }

        public float GetPlayerMoveSpeed(PlayerType _monster)
        {
            return DicPlayerMoveSpeed[_monster];
        }

        public int GetPlayerAttackDamage(PlayerType _monster)
        {
            return DicPlayerAttackDamage[_monster];
        }
    }
}