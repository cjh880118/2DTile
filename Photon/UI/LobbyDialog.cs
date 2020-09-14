using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;

public class LobbyDialog : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject LoadingPannel;
    public GameObject roomPannelPrefabs;
    public Button btnOpenMakeRoomPannel;
    public RoomMakePannel roomMakePannel;
    public JoinRoomPannel JoinRoomPannel;


    List<RoomInfo> roomInfos = new List<RoomInfo>();

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    void Start()
    {
        roomMakePannel.EvnetMakeRoom += RoomMake;
        btnOpenMakeRoomPannel.onClick.AddListener(() => roomMakePannel.gameObject.SetActive(true));
        roomMakePannel.gameObject.SetActive(false);
        JoinRoomPannel.gameObject.SetActive(false);
    }

    public void NetRoomInfo(List<RoomInfo> _roomInfos)
    {
        Debug.Log(roomInfos.Count);

        for (int i = 0; i < _roomInfos.Count; i++)
        {
            if (!_roomInfos[i].RemovedFromList)
            {
                bool isChange = false;
                for (int j = 0; j < roomInfos.Count; j++)
                {
                    if (_roomInfos[i].Name == roomInfos[j].Name)
                    {
                        isChange = true;
                        roomInfos[j] = _roomInfos[i];
                        break;
                    }
                }

                if (isChange) continue;

                roomInfos.Add(_roomInfos[i]);
            }
            else
                roomInfos.Remove(_roomInfos[i]);
        }

        for (int i = 0; i < roomPannelPrefabs.transform.parent.childCount; i++)
        {
            if (roomPannelPrefabs.transform.parent.GetChild(i).gameObject.activeSelf)
            {
                Destroy(roomPannelPrefabs.transform.parent.GetChild(i).gameObject);
            }
        }

        Debug.Log("2번째" + roomInfos.Count);
        for (int i = 0; i < roomInfos.Count; i++)
        {
            var pannel = Instantiate(roomPannelPrefabs, roomPannelPrefabs.transform.parent);
            pannel.SetActive(true);
            pannel.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
            string playerCount = string.Concat(roomInfos[i].PlayerCount, " / ", roomInfos[i].MaxPlayers);
            pannel.GetComponent<RoomInfoPannel>().InItRoomJoinButton(roomInfos[i].Name, playerCount);
            pannel.GetComponent<RoomInfoPannel>().ClickRoom += JoinRoom;

            bool tempJoinRoomPossible = true;
            if (!roomInfos[i].IsOpen || roomInfos[i].PlayerCount >= roomInfos[i].MaxPlayers)
                tempJoinRoomPossible = false;
        
            pannel.GetComponent<RoomInfoPannel>().SetRoomJoinPossible(tempJoinRoomPossible);
        }
    }

    private void JoinRoom(string roomName)
    {
        JoinRoomPannel.gameObject.SetActive(true);
        JoinRoomPannel.InItPannel(roomName);
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0);
    }


    private void RoomMake(string roomName, int playerCount)
    {
        roomMakePannel.gameObject.SetActive(false);
    }
}
