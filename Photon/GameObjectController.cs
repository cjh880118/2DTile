using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;
using System;

public class GameObjectController : MonoBehaviour
{
    public GameObject Mace;
    Vector2 vec2MacePos;
    public GameObject[] players;
    public event Action CharacterDie;
    private void Start()
    {
        vec2MacePos = Mace.transform.position;
    }

    public void GeneratePlayer()
    {
        float posX = UnityEngine.Random.Range(-6, 6);
        var player = PhotonNetwork.Instantiate("Prefabs/Photon/PhotonPlayer", new Vector2(posX,-1), Quaternion.identity);
        player.transform.parent = transform;
    }

    public void GameStart()
    {
        foreach (var players in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.GetComponent<PhotonPlayer>().EventDie += PlayerDie;
        }
    }
    private void PlayerDie()
    {
        CharacterDie?.Invoke();
        //inGameDialog.SetGameOverPannel(true);
        Debug.Log("나 죽음");
    }

    public int GetSuvivePlayerCount()
    {
        int playerCount = 0;
        foreach (var o in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (o.activeSelf)
                playerCount++;
        }
        return playerCount;
    }

    public void SetPlayerClear()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Player"))
        {
            o.GetComponent<PhotonPlayer>().GameObjectDestory();
        }
    }


    public void SetPlayerNameColor()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == PhotonNetwork.NickName)
            {
                players[i].GetComponentInChildren<TextMesh>().color = Color.blue;
            }
        }
    }


    public IEnumerator MoveMace(bool isTrue)
    {
        Mace.GetComponent<Rigidbody2D>().gravityScale = 10f;

        yield return new WaitForSeconds(1.0f);
        if (isTrue)
            Mace.transform.DOMove(new Vector2(6, transform.position.y), 1.5f).SetLoops(2, LoopType.Yoyo).OnComplete(MaceResetPos);
        else
            Mace.transform.DOMove(new Vector2(-6, transform.position.y), 1.5f).SetLoops(2, LoopType.Yoyo).OnComplete(MaceResetPos);
    }


    void MaceResetPos()
    {
        Mace.GetComponent<Rigidbody2D>().gravityScale = 0;
        Mace.transform.DOMove(vec2MacePos, 1.5f);
    }

}
