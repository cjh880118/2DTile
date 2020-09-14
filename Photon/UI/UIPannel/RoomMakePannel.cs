using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMakePannel : MonoBehaviour
{
    public static int MaxPlayer = 8; 
    public Text txtRoomName;
    public Text txtNickName;
    public Text txtPlayerCount;
    public Button btnPlayerPlus;
    public Button btnPlayerMinus;
    public Button btnMakeRoom;
    public Button btnClose;
    public delegate void MakeRoomEvent(string roomName, int playerCount);
    public event MakeRoomEvent EvnetMakeRoom;


    // Start is called before the first frame update
    void Start()
    {
        btnPlayerMinus.onClick.AddListener(() => CountChange(false));
        btnPlayerPlus.onClick.AddListener(() => CountChange(true));
        btnClose.onClick.AddListener(() => this.gameObject.SetActive(false));
        btnMakeRoom.onClick.AddListener(MakeRoom);
    }

    private void MakeRoom()
    {
        if (txtRoomName.text == "")
        {
            //todo..방제를 입력해주세요.
            return;
        }

        NetworkManager.Instance.CreateRoom(txtRoomName.text, Convert.ToInt32(txtPlayerCount.text), txtNickName.text);
    }

    private void CountChange(bool isPlus)
    {
        int tempCount;
        if (isPlus)
        {
            tempCount = (Convert.ToInt32(txtPlayerCount.text) + 1) > MaxPlayer ? MaxPlayer : (Convert.ToInt32(txtPlayerCount.text) + 1);
        }
        else
            tempCount = (Convert.ToInt32(txtPlayerCount.text) - 1) < 2 ? 2 : (Convert.ToInt32(txtPlayerCount.text) - 1);


        txtPlayerCount.text = tempCount.ToString();

    }



    // Update is called once per frame
    void Update()
    {

    }
}
