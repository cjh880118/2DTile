using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;


public class TwitchPannel : MonoBehaviour
{
    public Text txtInfo;
    public TextMeshProUGUI txtCount;
    public Text txtSuvive;
    public Text txtInputInfo;
    public bool isProblemTimeComplete;
    // Start is called before the first frame update
   
    public void SetInfo(string msg)
    {
        isProblemTimeComplete = false;
        txtInfo.text = "";
        txtInfo.DOText(msg, msg.Length * 0.2f).OnComplete(()=> { isProblemTimeComplete = true; });
    }

    public void SetCount(int count)
    {
        bool isActive = count > 0 ? true : false;
        txtCount.gameObject.SetActive(isActive);
        txtCount.text = count.ToString();
    }

    public void SetSuvive(int playerCount)
    {
        txtSuvive.text = string.Concat("생존자 : ", playerCount);
    }

}
