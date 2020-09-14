using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class RoomDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txtRoomName;
    public Text txtPlayer;
    public GameObject readyPannelPrefab;
    public Button btnLeave;

    public delegate void RoomButtonHandler(string nickName, bool isReady, bool IsStartBtn);
    public RoomButtonHandler EventButton;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    private void Start()
    {
        btnLeave.onClick.AddListener(() => NetworkManager.Instance.LeaveRoom());
    }

    public void InitDialog(Room room)
    {
        txtRoomName.text = room.Name;
        txtPlayer.text = string.Concat(room.PlayerCount, " / ", room.MaxPlayers);
    }

    public void PlayerListRefresh(Photon.Realtime.Player[] playerList, Dictionary<string, bool> playerReady)
    {
        for (int i = 0; i < readyPannelPrefab.transform.parent.childCount; i++)
        {
            if (readyPannelPrefab.transform.parent.GetChild(i).gameObject.activeSelf)
            {
                Destroy(readyPannelPrefab.transform.parent.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < playerList.Length; i++)
        {
            var pannel = Instantiate(readyPannelPrefab, readyPannelPrefab.transform.parent);
            pannel.SetActive(true);
            pannel.name = playerList[i].NickName;
            pannel.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
            Color color = PhotonNetwork.NickName == playerList[i].NickName ? Color.blue : Color.black;
            pannel.GetComponentInChildren<Button>().enabled = false;
            pannel.GetComponent<ReadyPannel>().InitPannel(playerList[i].NickName, color, playerReady[playerList[i].NickName]);
            pannel.GetComponent<ReadyPannel>().EventButton += EventPlayerReady;
        }
    }

    private void EventPlayerReady(string nickName, bool isReady, bool isStartBtn)
    {
        EventButton?.Invoke(nickName, isReady, isStartBtn);
        //Debug.Log(nickName + "은 " + isReady);
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0);
    }
}
