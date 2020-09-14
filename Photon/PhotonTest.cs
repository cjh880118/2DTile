using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;

        PhotonNetwork.JoinOrCreateRoom("abc", roomOptions, null);
    }
    static int count = 9;
    List<int> ListCards = new List<int>();
    public void Shuffle()
    {
        while (ListCards.Count < count)
        {
            int rndNum = Random.Range(0, 10);
            if (ListCards.Count == 0 || !ListCards.Contains(rndNum))
            {
                ListCards.Add(rndNum);
            }
        }
    }

   
    private void Update()
    {

    }




}
