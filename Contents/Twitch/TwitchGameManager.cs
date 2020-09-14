using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TwitchGameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject UiPannel;
    public TwitchStream twitchStream;
    public static int Round = 10;
    public GameObject MiddleObj;
    public GameObject OObj;
    public GameObject XObj;
    public GameObject RemoveCollider;


    List<string> ListViewer = new List<string>();
    List<int> ListProblem = new List<int>();

    QuizModel quiz = new QuizModel();
    // Start is called before the first frame update
    void Start()
    {
        quiz.Setup("Quiz");
        twitchStream.PlayerJoinMsg += TwitchViewerGenerate;
        twitchStream.PlayerMoveMsg += TwitchViewerMove;
        RemoveCollider.GetComponent<TwitchRemoveCollider>().playerRemoveMsg += PlayerRemove;

        UiPannel.GetComponent<TwitchPannel>().SetInfo("참가 희망자는 !참가를 입력해주세요.");
        StartCoroutine(JoinCount(10));
    }

    void TwitchViewerGenerate(string viewers)
    {
        if (!ListViewer.Contains(viewers))
        {
            ListViewer.Add(viewers);
            float vecX = Random.Range(-7, 7);
            var obj = Instantiate(playerPrefab, new Vector2(vecX, 0), Quaternion.identity);
            obj.GetComponentInChildren<TextMesh>().text = viewers;
            obj.SetActive(true);
            obj.name = viewers;
            UiPannel.GetComponent<TwitchPannel>().SetSuvive(ListViewer.Count);
        }
    }

    private void TwitchViewerMove(string userName, string msg)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (userName == players[i].name)
            {
                players[i].GetComponent<TwitchPlayer>().ReceiveMessage(msg);
            }
        }
    }

    private void PlayerRemove(GameObject player, string name)
    {
        Destroy(player);
        ListViewer.Remove(name);
        UiPannel.GetComponent<TwitchPannel>().SetSuvive(ListViewer.Count);
    }

    IEnumerator JoinCount(float limitCount)
    {
        while (limitCount > 0)
        {
            limitCount -= Time.deltaTime;
            yield return null;
            UiPannel.GetComponent<TwitchPannel>().SetCount(Convert.ToInt32(limitCount));
        }

        twitchStream.isJoinPossible = false;

        StartCoroutine(ProblemStart(15));
    }

    IEnumerator ProblemStart(float limitCount)
    {
        if (ListProblem.Count == Round)// || ListViewer.Count ==1)
        {
            Debug.Log("Game Over");
            yield break;
        }

        //문제 찾기
        int rndQuiz = 0;
        while (ListProblem.Count < Round)
        {
            rndQuiz = Random.Range(0, quiz.GetProblemCount());
            if (!ListProblem.Contains(rndQuiz))
            {
                //제출 하지 문제 선정
                ListProblem.Add(rndQuiz);
                break;
            }
        }

        //문제 제출시 이동 입력 허용
        UiPannel.GetComponent<TwitchPannel>().SetInfo("문제 : " + quiz.GetProblem(rndQuiz));
        twitchStream.isInputPossible = true;

        UiPannel.GetComponent<TwitchPannel>().txtInputInfo.gameObject.SetActive(true);

        while (!UiPannel.GetComponent<TwitchPannel>().isProblemTimeComplete)
        {
            yield return null;
        }

        //카운트
        while (limitCount > 0)
        {
            limitCount -= Time.deltaTime;
            yield return null;
            UiPannel.GetComponent<TwitchPannel>().SetCount(Convert.ToInt32(limitCount));
        }

        UiPannel.GetComponent<TwitchPannel>().txtInputInfo.gameObject.SetActive(false);
        //정답 발표 이동 입력 방지
        twitchStream.isInputPossible = false;
        bool isTrue = quiz.GetAnswer(rndQuiz);
        string OX = isTrue ? "O" : "X";

        UiPannel.GetComponent<TwitchPannel>().SetInfo("정답은 : " + OX);

        yield return new WaitForSeconds(5.0f);
        //정답 발표 후 탈락저 처리


        MiddleObj.SetActive(false);
        if (isTrue)
            XObj.SetActive(false);
        else
            OObj.SetActive(false);


        yield return new WaitForSeconds(5.0f);

        MiddleObj.SetActive(true);
        XObj.SetActive(true);
        OObj.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        //코르틴 재호출
        StartCoroutine(ProblemStart(15));
    }
}
