using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomPannel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txtRoomName;
    public Text txtNickName;
    public Button btnJoin;
    string roomName;
    void Start()
    {
        btnJoin.onClick.AddListener(JoinRoom);
    }

    public void InItPannel(string _roomName)
    {
        roomName = _roomName;
        txtRoomName.text = string.Concat("방이름 : ", roomName);
    }

    private void JoinRoom()
    {
        if (txtNickName.text != "")
        {
            NetworkManager.Instance.JoinRoom(roomName, txtNickName.text);
            this.gameObject.SetActive(false);
        }
    }
}
