using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Managers;
using System;
using JHchoi.Constants;
using JHchoi.UI.Event;

namespace JHchoi.Contents
{
    public class DungeonContent : IContent
    {
        private Dictionary<ManagerType, GameObject> Managers = new Dictionary<ManagerType, GameObject>();
        private GameObject MainCamera;

        private MapManager mapManager;
        private PlayerManager playerManager;
        private NpcManager npcManager;
        private MonsterManager monsterManager;
        private InventoryManager inventoryManager;
        private MapType NowMapType;

        private bool isInventoryOpen = false;

        protected override void OnLoadStart()
        {
            StartCoroutine(Load_Manager());
        }

        IEnumerator Load_Manager()
        {

            string path = "Camera/Main Camera";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                MainCamera = Instantiate(o) as GameObject;
                MainCamera.transform.parent = transform;
            }));


            path = "Manager/MapManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _mapManger = Instantiate(o) as GameObject;
                _mapManger.transform.parent = transform;
                mapManager = _mapManger.GetComponent<MapManager>();
                Managers.Add(ManagerType.Map, _mapManger);
                
            }));

            path = "Manager/PlayerManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _playerManager = Instantiate(o) as GameObject;
                _playerManager.transform.parent = transform;
                playerManager = _playerManager.GetComponent<PlayerManager>();
                Managers.Add(ManagerType.Player, _playerManager);
                StartCoroutine(playerManager.Load_Resource());
            }));

            path = "Manager/NpcManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _npcManager = Instantiate(o) as GameObject;
                _npcManager.transform.parent = transform;
                npcManager = _npcManager.GetComponent<NpcManager>();
                Managers.Add(ManagerType.Npc, _npcManager);
                StartCoroutine(npcManager.Load_Resource());
            }));

            path = "Manager/MonsterManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _monsterManager = Instantiate(o) as GameObject;
                _monsterManager.transform.parent = transform;
                monsterManager = _monsterManager.GetComponent<MonsterManager>();
                Managers.Add(ManagerType.Monster, _monsterManager);
                StartCoroutine(_monsterManager.GetComponent<MonsterManager>().Load_Resource());
                
            }));

            path = "manager/inventorymanager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _inventorymanager = Instantiate(o) as GameObject;
                _inventorymanager.transform.parent = transform;
                inventoryManager = _inventorymanager.GetComponent<InventoryManager>();
                Managers.Add(ManagerType.Inventory, _inventorymanager);
                StartCoroutine(_inventorymanager.GetComponent<InventoryManager>().Load_Resource());
                
            }));

            //Event
            mapManager.EvnetMapMove += MapMove;
            monsterManager.EventMonsterHit += MonsterHit;
            playerManager.player.EvnetPlayerDie += PlayerDie;
            inventoryManager.ItemUpdate += ItemUpdate;
            inventoryManager.EvnetUsePotion += UsePotion;

            SetLoadComplete();
        }

        private void MapMove(UnityEngine.Object sender, MapMovePointType type)
        {
            if (type == MapMovePointType.End)
                NowMapType++;
            else
                NowMapType--;

            StartCoroutine(LoadMap(NowMapType, type));
        }


        private void ItemUpdate(Managers.Attribute[] attributes)
        {
            playerManager.UpdateEquipment(attributes);

            Message.Send<UIInventoryStatusMsg>(new UIInventoryStatusMsg(playerManager.GetPlayerName(),
                   playerManager.GetPlayerMaxHp(),
                   playerManager.GetPlayerHp(),
                   playerManager.GetPlayerAttack(),
                   playerManager.GetPlayerDefence(),
                   playerManager.GetPlayerMoveSpeed()
                   ));
        }

        private void MonsterHit(object sender, bool _isAllDie)
        {
            MainCamera.GetComponent<CameraController>().CameraShake();

            if (_isAllDie && mapManager.GetIsBossMap())
            {
                UI.IDialog.RequestDialogEnter<UI.EndDialog>();
                Message.Send<UIEndGameMsg>(new UIEndGameMsg("Game Clear"));
            }
        }

        private void PlayerDie(object sender, EventArgs e)
        {
            UI.IDialog.RequestDialogEnter<UI.EndDialog>();
            Message.Send<UIEndGameMsg>(new UIEndGameMsg("Game Over"));
        }

        private void UsePotion(object sender, ConsumeItem _consumeItem)
        {
            playerManager.UsePotion(_consumeItem.Hp, _consumeItem.Mp);

            Message.Send<UIPlayerHpMsg>(new UIPlayerHpMsg(playerManager.GetPlayerName(), playerManager.GetPlayerMaxHp(), playerManager.GetPlayerHp()));

            Message.Send<UIInventoryStatusMsg>(new UIInventoryStatusMsg(playerManager.GetPlayerName(),
                    playerManager.GetPlayerMaxHp(),
                    playerManager.GetPlayerHp(),
                    playerManager.GetPlayerAttack(),
                    playerManager.GetPlayerDefence(),
                    playerManager.GetPlayerMoveSpeed()
                    ));
        }

        protected override void OnEnter()
        {
            NowMapType = MapType.Stage1_1;
            StartCoroutine(LoadMap(NowMapType, MapMovePointType.End));
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!isInventoryOpen)
                {
                    isInventoryOpen = true;
                    UI.IDialog.RequestDialogEnter<UI.InventoryDialog>();
                    Message.Send<UIInventoryStatusMsg>(new UIInventoryStatusMsg(playerManager.GetPlayerName(),
                        playerManager.GetPlayerMaxHp(),
                        playerManager.GetPlayerHp(),
                        playerManager.GetPlayerAttack(),
                        playerManager.GetPlayerDefence(),
                        playerManager.GetPlayerMoveSpeed()
                        ));
                }
                else
                {
                    isInventoryOpen = false;
                    UI.IDialog.RequestDialogExit<UI.InventoryDialog>();
                }

                playerManager.SetInventory(isInventoryOpen);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                inventoryManager.UseItem();
            }
        }

        IEnumerator LoadMap(MapType _mapType, MapMovePointType _pointType)
        {
            Message.Send<FadeInMsg>(new FadeInMsg());
            yield return StartCoroutine(mapManager.Load_Resource(_mapType.ToString()));

            while (!mapManager.IsLoadComplete)
                yield return null;

            if (mapManager.GetBattlePossible())
            {
                UI.IDialog.RequestDialogEnter<UI.CharacterDialog>();
                Message.Send<UIPlayerHpMsg>(new UIPlayerHpMsg(playerManager.GetPlayerName(),
                    playerManager.GetPlayerMaxHp(),
                    playerManager.GetPlayerHp()
                    ));
            }
            else
                UI.IDialog.RequestDialogExit<UI.CharacterDialog>();

            for (int i = 0; i < monsterManager.transform.childCount; i++)
                Destroy(monsterManager.transform.GetChild(i).gameObject);


            if (mapManager.GetMonsters() != null)
                mapManager.GetMonsters().transform.parent = monsterManager.gameObject.transform;

            monsterManager.Init_Monster(mapManager.GetMonsters());

            Vector2 startPos;
            if (_pointType == MapMovePointType.End)
                startPos = mapManager.GetStartPos();
            else
                startPos = mapManager.GetEndPos();

            playerManager.SetPlayerInMap(mapManager.GetBattlePossible(), startPos);
            Message.Send<FadeOutMsg>(new FadeOutMsg());
        }


        protected override void OnExit()
        {
        }

    }
}