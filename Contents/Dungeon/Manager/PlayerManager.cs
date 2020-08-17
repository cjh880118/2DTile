using JHchoi.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Constants;

namespace JHchoi.Managers
{
    public class PlayerManager : IManager
    {
        PlayerModel playerModel = Model.First<PlayerModel>();
        GameObject playerObject;
        IPlayer player;
        public override IEnumerator Load_Resource()
        {
            string path = "Prefabs/Player/Witch";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                playerObject = Instantiate(o) as GameObject;
                playerObject.name = "Player";
                playerObject.transform.parent = transform;
                player = playerObject.GetComponent<IPlayer>();
                player.InitIPlayer(
                    playerModel.GetPlayerName(PlayerType.Witch),
                    playerModel.GetPlayerHp(PlayerType.Witch),
                    playerModel.GetPlayerMoveSpeed(PlayerType.Witch),
                    playerModel.GetPlayerAttackDamage(PlayerType.Witch)
                    );
            }));
        }

        public string GetPlayerName()
        {
            return player.Name;
        }

        public int GetPlayerMaxHp()
        {
            return player.MaxHp;
        }

        public int GetPlayerHp()
        {
            return player.Hp;
        }

        public void SetInventory(bool isOpen)
        {
            player.IsInventoryOpen = isOpen;
        }


        public void SetPlayerInMap(bool _isBattleOn, Vector2 _pos)
        {
            player.IsBattleOn = _isBattleOn;
            playerObject.transform.position = _pos;
        }

        public void SetPlayerBattle(bool _isBattleOn)
        {
            player.IsBattleOn = _isBattleOn;
        }


        public void SetPlayerStartPosition(Vector2 _pos)
        {
            playerObject.transform.position = _pos;
        }

        public void UpgradeHp(int hp)
        {
            player.Hp += hp;
        }

        public void UpgradeMoveSpeed(int moveSpeed)
        {
            player.MoveSpeed += moveSpeed;
        }

        public void UpgradeAttackDamage(int attackDamage)
        {
            player.AttackDamage += attackDamage;
        }
    }
}