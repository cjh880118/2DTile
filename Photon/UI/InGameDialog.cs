using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InGameDialog : MonoBehaviour
{
    public GameObject problemPannel;
    public GameObject countPannel;
    public GameObject suvivePannel;
    public GameObject GameOverPannel;
    public Button btnGotoLobby;
    bool isProblemComplete;

    private void Start()
    {
        GameOverPannel.SetActive(false);
        btnGotoLobby.onClick.AddListener(() => { NetworkManager.Instance.LeaveRoom(); });
    }

    public bool IsProblemComplete { get => isProblemComplete; }

    public void SetProblem(string msg)
    {
        isProblemComplete = false;
        problemPannel.SetActive(true);
        problemPannel.GetComponentInChildren<Text>().text = "";
        float time = msg.Length * 0.2f;
        problemPannel.GetComponentInChildren<Text>().DOText(msg, time).OnComplete(() => isProblemComplete = true);
    }

    public void SetCount(float timer)
    {
        if (timer < 0)
            countPannel.SetActive(false);
        else
        {
            countPannel.SetActive(true);
            countPannel.GetComponentInChildren<Text>().text = timer.ToString();
        }
    }

    public void SetSuvive(int playerCount)
    {
        suvivePannel.GetComponentInChildren<Text>().text = "생존자 : " + playerCount;
    }

    public void SetGameOverPannel(bool isTrue)
    {
        GameOverPannel.SetActive(isTrue);
    }
}
