using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public LobbyDialog lobbyDialog;
    public RoomDialog roomDialog;
    public InGameDialog inGameDialog;
    public GameObjectController objController;
    Dictionary<string, bool> PlayerReady = new Dictionary<string, bool>();
    PhotonView pv;

    QuizModel quiz = new QuizModel();
    List<int> ListProblem = new List<int>();

    public static int Round = 10;
    float timer = 0;
    int quizNum = 0;

    void Start()
    {
        //Screen.SetResolution(1280, 800, false);
        pv = GetComponent<PhotonView>();
        quiz.Setup("Quiz");
        lobbyDialog.gameObject.SetActive(true);
        roomDialog.gameObject.SetActive(false);
        inGameDialog.gameObject.SetActive(false);
        NetworkManager.Instance.EventConnect += NetJoinStatus;
        NetworkManager.Instance.EventPlayerChange += PlayerChange;
        NetworkManager.Instance.EventRoomList += RoomList;
        NetworkManager.Instance.EventMasterChange += MasterChange;
        roomDialog.EventButton += EventPlayerReady;
        objController.CharacterDie += PlayerDie;
        NetworkManager.Instance.Connect();
    }


    void ProblemSelect()
    {
        int suvivePlayerCount = objController.GetSuvivePlayerCount();
        pv.RPC("RPC_SuvivePannel", RpcTarget.All, suvivePlayerCount);
        if (suvivePlayerCount < 2)
        {
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.CurrentRoom.IsOpen = true;
            GameOver();
            return;
        }

        timer = 30f;
        while (ListProblem.Count < Round)
        {
            quizNum = UnityEngine.Random.Range(0, quiz.GetProblemCount());
            if (!ListProblem.Contains(quizNum))
            {
                //제출 하지 문제 선정
                ListProblem.Add(quizNum);
                break;
            }
        }

        string problem = "문제 : " + quiz.GetProblem(quizNum);
        pv.RPC("RPC_ProblemPannel", RpcTarget.All, problem);

        StartCoroutine(CountStart(quizNum));

    }

    IEnumerator CountStart(int problemNum)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!inGameDialog.IsProblemComplete)
                timer = 30;

            while (!inGameDialog.IsProblemComplete)
            {
                yield return null;
            }

            while (timer > 0)
            {
                yield return null;
                timer -= Time.deltaTime;
                pv.RPC("RPC_Timer", RpcTarget.All, timer);
            }

            //결과 발표
            string result = quiz.GetAnswer(problemNum) ? "정답은 : O" : "정답은 : X";
            pv.RPC("RPC_ProblemPannel", RpcTarget.All, result);

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(objController.MoveMace(quiz.GetAnswer(problemNum)));


            yield return new WaitForSeconds(5.0f);


            ProblemSelect();
        }
    }

    public void GameStart()
    {

    }

    void GameOver()
    {
        pv.RPC("RPC_GameOver", RpcTarget.All);
    }

    #region RoomJoinCallback
    private void NetJoinStatus(NetworkManager.NetStatusEventMsg msg)
    {
        switch (msg.netStatus)
        {
            case NetStatus.JoinServer:
                if (msg.isConnectSuccess)
                {
                    Debug.Log("서버 접속 성공");
                }
                else
                {
                    Debug.Log("서버 접속 실패");
                }
                break;
            case NetStatus.JoinLobby:
                if (msg.isConnectSuccess)
                {
                    Debug.Log("로비 접속 성공");
                    lobbyDialog.LoadingPannel.SetActive(false);
                    inGameDialog.SetGameOverPannel(false);
                    inGameDialog.gameObject.SetActive(false);
                    roomDialog.gameObject.SetActive(false);
                    lobbyDialog.gameObject.SetActive(true);
                }
                else
                    Debug.Log("로비 접속 실패");
                break;
            case NetStatus.JoinRoom:
                if (msg.isConnectSuccess)
                {
                    lobbyDialog.gameObject.SetActive(false);
                    objController.gameObject.SetActive(true);
                    roomDialog.gameObject.SetActive(true);

                    objController.GeneratePlayer();
                    objController.SetPlayerNameColor();

                    if (PhotonNetwork.IsMasterClient)
                    {
                        PlayerReady.Clear();
                        PlayerReady.Add(PhotonNetwork.NickName, true);
                        pv.RPC("RPC_PlayerCountUpdate", RpcTarget.All, PlayerReady);
                    }

                    Debug.Log("방 접속 성공");
                }
                else
                    Debug.Log("방 접속 실패");
                break;
            case NetStatus.CreateRoom:
                if (msg.isConnectSuccess)
                {
                    Debug.Log("방 생성 성공");
                    lobbyDialog.roomMakePannel.gameObject.SetActive(false);
                    //로비 이동
                }
                else
                    Debug.Log("방 생성 실패");
                break;
            default:
                break;
        }
    }
    #endregion

    private void PlayerChange(Photon.Realtime.Player player, bool isJoin)
    {
        Debug.Log("PlayerChange");
        if (PhotonNetwork.IsMasterClient)
        {
            if (isJoin)
                PlayerReady.Add(player.NickName, false);
            else
                PlayerReady.Remove(player.NickName);

            pv.RPC("RPC_PlayerCountUpdate", RpcTarget.All, PlayerReady);
        }
    }

    private void RoomList(List<RoomInfo> roomList)
    {
        lobbyDialog.NetRoomInfo(roomList);
    }

    private void MasterChange()
    {
        if (PhotonNetwork.IsMasterClient && !PhotonNetwork.CurrentRoom.IsOpen)
        {
            StartCoroutine(CountStart(quizNum));
        }
    }

    private void EventPlayerReady(string nickName, bool isReady, bool isStartBtn)
    {
        if (isStartBtn)
        {
            int readyCount = 0;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PlayerReady[PhotonNetwork.PlayerList[i].NickName])
                {
                    readyCount++;
                }
            }

            if (readyCount == PhotonNetwork.PlayerList.Length)
            {
                Debug.Log("게임시작");
                pv.RPC("RPC_GameStart", RpcTarget.All);
            }

            return;
        }

        PlayerReady[nickName] = isReady;
        pv.RPC("RPC_PlayerCountUpdate", RpcTarget.All, PlayerReady);
    }


 
    private void PlayerDie()
    {
        inGameDialog.SetGameOverPannel(true);
        Debug.Log("나 죽음");
    }


    #region PunRPC
    [PunRPC]
    void RPC_PlayerCountUpdate(Dictionary<string, bool> _PlayerReady)
    {
        PlayerReady = _PlayerReady;
        roomDialog.InitDialog(PhotonNetwork.CurrentRoom);
        roomDialog.PlayerListRefresh(PhotonNetwork.PlayerList, _PlayerReady);
    }

    [PunRPC]
    void RPC_GameStart()
    {
        roomDialog.gameObject.SetActive(false);
        inGameDialog.gameObject.SetActive(true);
        objController.GameStart();

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            ProblemSelect();
        }
    }

    [PunRPC]
    void RPC_GameOver()
    {
        objController.SetPlayerClear();
        inGameDialog.SetGameOverPannel(false);
        inGameDialog.gameObject.SetActive(false);
        roomDialog.gameObject.SetActive(true);
        objController.GeneratePlayer();

        for (int i = 0; i < PlayerReady.Count; i++)
        {
            for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++)
            {
                if(PhotonNetwork.PlayerList[j].NickName != PhotonNetwork.MasterClient.NickName)
                {
                    PlayerReady[PhotonNetwork.PlayerList[j].NickName] = false;
                }
            }
        }

        pv.RPC("RPC_PlayerCountUpdate", RpcTarget.All, PlayerReady);
        Debug.Log("게임 끝");
    }

    [PunRPC]
    void RPC_Timer(float time)
    {
        timer = time;
        inGameDialog.SetCount(Convert.ToInt32(time));
    }

    [PunRPC]
    void RPC_ProblemPannel(string msg)
    {
        inGameDialog.SetProblem(msg);
    }

    [PunRPC]
    void RPC_SuvivePannel(int playerCount)
    {
        inGameDialog.SetSuvive(playerCount);
    }

    #endregion
}
