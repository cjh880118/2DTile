using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;

public enum NetStatus
{
    JoinServer,
    JoinLobby,
    JoinRoom,
    OtherJoin,
    OtherLeave,
    CreateRoom,
}


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public delegate void NetHandler(NetStatusEventMsg msg);
    public event NetHandler EventConnect;
    public delegate void RoomListHandler(List<RoomInfo> roomList);
    public event RoomListHandler EventRoomList;
    public delegate void PlayerListHandler(Photon.Realtime.Player player, bool isJoin);
    public PlayerListHandler EventPlayerChange;
    private NetStatus joinStatus;
    public delegate void MasterChangeHandler();
    public MasterChangeHandler EventMasterChange;


    private static NetworkManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static NetworkManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(NetworkManager)) as NetworkManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }


    /*******************************
     * 서버 접속 시도
     *******************************/
    public void Connect()
    {
        joinStatus = NetStatus.JoinServer;
        if (PhotonNetwork.ConnectUsingSettings())
        {
            EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, true));
        }
        else
        {
            EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, false));
        }
    }

    /************************************
     * CallBack
     * 서버 접속 성공 
     ************************************/
    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    /*****************************************
     * 로비 접속 시도
     *****************************************/
    public void JoinLobby()
    {
        joinStatus = NetStatus.JoinLobby;
        if (PhotonNetwork.JoinLobby())
        {
            EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, true));
        }
        else
        {
            EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, false));
        }
    }


    /************************************
     * 방 접속 시도
     * parm (roomName : 방이름, nickName : 닉네임)
     ***********************************/
    public void JoinRoom(string roomName, string nickName)
    {
        joinStatus = NetStatus.JoinRoom;
        if (PhotonNetwork.JoinRoom(roomName))
        {
            PhotonNetwork.NickName = nickName;
        }
        else
        {
            Debug.Log("방접속 실패");
        }
    }

    /********************************
    * 방접속 성공 CallBack
    **********************************/
    public override void OnJoinedRoom()
    {
        joinStatus = NetStatus.JoinRoom;
        EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, true));

        if (!isAbleNickName(PhotonNetwork.NickName))      //닉 중복방 접속 불가
        {
            //todo.. 방에 중복닉 알림
            Debug.Log("중복 닉 있네");
            LeaveRoom();
        }
    }

    /* *****************************
     * 방에 닉네임 중복 확인
     * retrun 타입 중복 있는지 없는지
     ***************************/
    bool isAbleNickName(string nickName)
    {
        int nickOverlapCount = 0;
        for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
        {
            if (PhotonNetwork.PlayerListOthers[i].NickName == nickName)
            {
                nickOverlapCount++;

                if (nickOverlapCount >= 2)
                    return false;
            }
        }

        return true;
    }


    /**************************************
     * CallBack 방접속 실패시
     * parm (retrunCode : ? ,  message : ?)
     *****************************************/
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //방참가 실패
        joinStatus = NetStatus.JoinRoom;
        EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, false));
        Debug.Log(message);
    }


    /************************************
     * CallBack 방 목록 갱신시 콜백
     * parm (roomList : 방목록 변경된)
     *************************************/
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Debug.Log("roomName: "+ roomList[i].Name);
            Debug.Log("Delete : " + roomList[i].RemovedFromList);
        }


        Debug.Log("room count : " + roomList.Count);
        EventRoomList?.Invoke(roomList);
    }


    /************************************************
     * 방생성 
     * parm (roomName : 방이름, playerCount : 방 인원수, nickName : 닉네임)
     *************************************************/
    public void CreateRoom(string roomName, int playerCount, string nickName)
    {

        if (PhotonNetwork.CreateRoom(roomName, new RoomOptions{ MaxPlayers = Convert.ToByte(playerCount) }))
        {
            PhotonNetwork.NickName = nickName;
        }
        else
        {
            Debug.Log("방생성 실패");
        }

    }

    /********************************
     * Callback
     * 방 생성 성공시 호출
     ************************************/
    public override void OnCreatedRoom()
    {
        joinStatus = NetStatus.CreateRoom;
        EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, true));
    }

    /**************************************
    * CallBack 방생성 실패시
    * parm (retrunCode : ? ,  message : ?)
    *****************************************/
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        joinStatus = NetStatus.CreateRoom;
        EventConnect?.Invoke(new NetStatusEventMsg(joinStatus, false));
        Debug.Log(message);
    }


    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("연결끊김");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    /********************************
     * Callback
     * 방에서 나감
     * parm(otherPlayer : 나간 플레이어)
     ************************************/
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        EventPlayerChange?.Invoke(otherPlayer, false);
    }

    /********************************
   * Callback
   * 방에서 나감
   * parm(otherPlayer : 들어온 플레이어)
   ************************************/
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        EventPlayerChange?.Invoke(newPlayer, true);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        EventMasterChange?.Invoke();
    }

    //넷 상태 이벤트 콜백 메세지
    public class NetStatusEventMsg : EventArgs
    {
        public NetStatus netStatus;
        public bool isConnectSuccess;

        public NetStatusEventMsg(NetStatus status, bool _isConnect)
        {
            netStatus = status;
            isConnectSuccess = _isConnect;
        }
    }

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
    }
}