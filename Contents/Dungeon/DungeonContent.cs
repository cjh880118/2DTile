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
        //private Inventory inventory;

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

            //inventoryManager = new InventoryManager();

            path = "manager/inventorymanager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _inventorymanager = Instantiate(o) as GameObject;
                _inventorymanager.transform.parent = transform;
                inventoryManager = _inventorymanager.GetComponent<InventoryManager>();
                Managers.Add(ManagerType.Inventory, _inventorymanager);
                StartCoroutine(_inventorymanager.GetComponent<InventoryManager>().Load_Resource());
            }));


            SetLoadComplete();
        }


        protected override void OnEnter()
        {
            AddMessage();
            LoadMap(new LoadMapMsg(MapType.Stage1_1));
            //inventoryManager.OnItemListChange += ItemListChange;
        }

        private void ItemListChange(object sender, EventArgs e)
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                LoadMap(new LoadMapMsg(MapType.Stage1_1));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                LoadMap(new LoadMapMsg(MapType.Stage1_2));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                LoadMap(new LoadMapMsg(MapType.Stage1_3));
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                if (!isInventoryOpen)
                {
                    isInventoryOpen = true;
                    UI.IDialog.RequestDialogEnter<UI.InventoryDialog>();
                    Message.Send<UIInventoryMsg>(new UIInventoryMsg(inventoryManager.GetItemList()));
                }
                else
                {
                    isInventoryOpen = false;
                    UI.IDialog.RequestDialogExit<UI.InventoryDialog>();
                }

                playerManager.SetInventory(isInventoryOpen);
            }
        }

        private void AddMessage()
        {
            Message.AddListener<LoadMapMsg>(LoadMap);
        }

        private void LoadMap(LoadMapMsg msg)
        {
            StartCoroutine(LoadMap(msg.map));
        }

        IEnumerator LoadMap(MapType _mapType)
        {
            yield return StartCoroutine(mapManager.Load_Resource(_mapType.ToString()));

            while (!mapManager.IsLoadComplete)
                yield return null;


            if (mapManager.GetBattlePossible())
            {
                UI.IDialog.RequestDialogEnter<UI.CharacterDialog>();
                Message.Send<UIPlayerMsg>(new UIPlayerMsg(playerManager.GetPlayerName(),
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

            playerManager.SetPlayerInMap(mapManager.GetBattlePossible(), mapManager.GetStartPos());
        }


        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {

        }
    }
}