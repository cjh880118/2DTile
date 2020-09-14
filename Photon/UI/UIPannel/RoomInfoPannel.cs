using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoPannel : MonoBehaviour
{
    public Text txtRoomName;
    public Text txtPlayerCount;
    public Text txtPossible;
    public Button btnJoinRoom;
    public delegate void EventClick(string roomName);
    public EventClick ClickRoom;

    private void Start()
    {
        btnJoinRoom.onClick.AddListener(() =>
            ClickRoom.Invoke(txtRoomName.text)
        );
    }

    public void InItRoomJoinButton(string roomName, string playerCount)
    {
        txtRoomName.text = roomName;
        txtPlayerCount.text = playerCount;
    }

    public void SetRoomJoinPossible(bool isPossible)
    {
        if (isPossible) {
            txtPossible.text = "ON";
            txtPossible.color = Color.blue;
        }
        else
        {
            txtPossible.text = "OFF";
            txtPossible.color = Color.red;

        }
    }
}
