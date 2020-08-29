using JHchoi.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Constants;
using System;

namespace JHchoi.Managers
{
    public class PlayerManager : ManagerBase
    {
        PlayerModel playerModel = Model.First<PlayerModel>();
        GameObject playerObject;
        public PlayerBase player;
        public override IEnumerator Load_Resource()
        {
            string path = "Prefabs/Player/Witch";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                playerObject = Instantiate(o) as GameObject;
                playerObject.name = "Player";
                playerObject.transform.parent = transform;
                player = playerObject.GetComponent<PlayerBase>();
                player.InitIPlayer(
                    playerModel.GetPlayerName(PlayerType.Witch),
                    playerModel.GetPlayerHp(PlayerType.Witch),
                    playerModel.GetPlayerMoveSpeed(PlayerType.Witch),
                    playerModel.GetPlayerAttackDamage(PlayerType.Witch),
                    playerModel.GetPlayerDefence(PlayerType.Witch)
                    );

            }));
        }


        public void UpdateEquipment(Attribute[] attributes)
        {
            int tempAttack = 0;
            int tempDefence = 0;
            float tempMoveSpeed = 0;
            for (int i = 0; i < attributes.Length; i++)
            {
                Debug.Log(string.Concat("Item Type : ", attributes[i].type, " Item Value : ", attributes[i].value.ModifiedValue));
                switch (attributes[i].type)
                {
                    case Status.Attack:
                        tempAttack = attributes[i].value.ModifiedValue;
                        break;
                    case Status.Defence:
                        tempDefence = attributes[i].value.ModifiedValue;
                        break;
                    case Status.MoveSpeed:
                        tempMoveSpeed = attributes[i].value.ModifiedValue;
                        break;
                    default:
                        break;
                }
            }
            SetItemStatus(tempAttack, tempDefence, tempMoveSpeed);
        }

        public void SetItemStatus(int _itemAttack, int _ItemDefence, float _ItemMoveSpeed)
        {
            player.PlayerItem.ItemAttack = _itemAttack;
            player.PlayerItem.ItemDefence = _ItemDefence;
            player.PlayerItem.ItemMoveSpeed = _ItemMoveSpeed;
        }

        public void UsePotion(int _hp, int _mp)
        {
            player.Hp += _hp;
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

        public int GetPlayerAttack()
        {
            return player.TotalAttack;
        }

        public int GetPlayerDefence()
        {
            return player.Defecnce;
        }

        public float GetPlayerMoveSpeed()
        {
            return player.MoveSpeed;
        }

        public void SetInventory(bool isOpen)
        {
            player.IsInventoryOpen = isOpen;
        }


        public void SetPlayerInMap(bool _isBattleOn, Vector2 _pos)
        {
            player.IsBattleOn = _isBattleOn;
            playerObject.transform.localPosition = _pos;
        }

    }
}