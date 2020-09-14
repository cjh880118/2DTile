using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ReadyPannel : MonoBehaviour
{
    public Text txtPlayerName;
    public Text txtRead;
    public Button btnReady;
    public bool isReady;
    public delegate void RoomButtonHandler(string nickName, bool isReady, bool IsStartBtn);
    public RoomButtonHandler EventButton;
    public void InitPannel(string name, Color color, bool _isReady)
    {
        txtPlayerName.text = name;
        txtPlayerName.color = color;

        if (color == Color.blue)
            btnReady.enabled = true;

        isReady = _isReady;

        if (PhotonNetwork.MasterClient.NickName == name)
        {
            isReady = true;
            txtRead.text = "Start";
            btnReady.gameObject.GetComponent<Image>().color = Color.red;
            return;
        }

        if (isReady)
        {
            txtRead.text = "Complete";
            btnReady.gameObject.GetComponent<Image>().color = Color.red;
                
        }
        else
        {
            btnReady.gameObject.GetComponent<Image>().color = Color.white;
            txtRead.text = "Ready";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btnReady.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        if (PhotonNetwork.MasterClient.NickName == txtPlayerName.text)
        {
            //방장은 게임 시작
            EventButton.Invoke(txtPlayerName.text, isReady, true);
            return;
        }

        isReady = !isReady;
        EventButton.Invoke(txtPlayerName.text, isReady, false);
    }
}
