using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Managers;
using System;

namespace JHchoi.Contents
{
    public class DungeonContent : IContent
    {
        List<GameObject> Managers = new List<GameObject>();
        Camera MainCamera;
        protected override void OnLoadStart()
        {
            StartCoroutine(Load_Manager());

        }

        IEnumerator Load_Manager()
        {
            string path = "Manager/MapManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _mapManger = Instantiate(o) as GameObject;
                _mapManger.transform.parent = transform;
                Managers.Add(_mapManger);
                StartCoroutine(_mapManger.GetComponent<MapManager>().Load_Resource());
            }));

            path = "Manager/PlayerManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _playerManager = Instantiate(o) as GameObject;
                _playerManager.transform.parent = transform;
                Managers.Add(_playerManager);
                StartCoroutine(_playerManager.GetComponent<PlayerManager>().Load_Resource());
            }));

            path = "Manager/MonsterManager";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _monsterManager = Instantiate(o) as GameObject;
                _monsterManager.transform.parent = transform;
                Managers.Add(_monsterManager);
                StartCoroutine(_monsterManager.GetComponent<MonsterManager>().Load_Resource());
            }));

            path = "Camera/Main Camera";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path, o =>
            {
                var _mainCamera = Instantiate(o) as GameObject;
                _mainCamera.transform.parent = transform;
                _mainCamera.GetComponent<CameraController>().InitCamera();
            }));

            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Managers[0].GetComponent<MapManager>().Load_Map(Constants.Map.World);
            AddMessage();
        }

        private void AddMessage()
        {
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