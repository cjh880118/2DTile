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
        Dictionary<PlayerType, int> DicPlayerDefence = new Dictionary<PlayerType, int>();

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
                var defence = fileData.GetValue("Defence", index);

                DicPlayerName.Add((PlayerType)index, prefabName);
                DicPlayerHp.Add((PlayerType)index, int.Parse(hp));
                DicPlayerMoveSpeed.Add((PlayerType)index, float.Parse(moveSpeed));
                DicPlayerAttackDamage.Add((PlayerType)index, int.Parse(attackDamage));
                DicPlayerDefence.Add((PlayerType)index, int.Parse(defence));
            }
        }

        public string GetPlayerName(PlayerType _player)
        {
            return DicPlayerName[_player];
        }

        public int GetPlayerHp(PlayerType _player)
        {
            return DicPlayerHp[_player];
        }

        public float GetPlayerMoveSpeed(PlayerType _player)
        {
            return DicPlayerMoveSpeed[_player];
        }

        public int GetPlayerAttackDamage(PlayerType _player)
        {
            return DicPlayerAttackDamage[_player];
        }

        public int GetPlayerDefence(PlayerType _player)
        {
            return DicPlayerDefence[_player];
        }
    }
}