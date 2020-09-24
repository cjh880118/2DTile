using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using DG.Tweening;

public class TwitchStream : MonoBehaviour
{
    WebClient webTwitchClient;
    TwitchIRC twitchIRC;
    public int maxMessage = 100;
    public bool isJoinPossible = true;
    public bool isInputPossible = false;
    public delegate void PlayerJoinHandler(string name);
    public PlayerJoinHandler PlayerJoinMsg;
    public delegate void PlayerMoveHandler(string name, string msg);
    public PlayerMoveHandler PlayerMoveMsg;


    private LinkedList<GameObject> message = new LinkedList<GameObject>();

    private void Awake()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
           delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
           System.Security.Cryptography.X509Certificates.X509Chain chain,
           System.Net.Security.SslPolicyErrors sslPolicyErrors)
           { return true; };

        
    }
    // Start is called before the first frame update
    void Start()
    {
        webTwitchClient = new WebClient();
        webTwitchClient.Encoding = System.Text.Encoding.UTF8;
        webTwitchClient.Headers.Set("Content-Type", "json");

        twitchIRC = GetComponent<TwitchIRC>();
        twitchIRC.messageRecievedEvent.AddListener(TwitchMessageReceive);

        
        //StartCoroutine(TwitchConnect());
    }

    IEnumerator TwitchConnect()
    {
        while (true)
        {
            string url = "http://tmi.twitch.tv/group/user/yanggang0118/chatters";
            //string url = "http://tmi.twitch.tv/group/user/rudbeckia7/chatters";
            string jsonData = webTwitchClient.DownloadString(url);


            TwitchJson twitchJson = JsonUtility.FromJson<TwitchJson>(jsonData);
            Debug.Log("Viewr Count : " + twitchJson.chatter_count);

            Debug.Log("global_mods");
            for (int i = 0; i < twitchJson.chatters.global_mods.Length; i++)
            {
                Debug.Log(twitchJson.chatters.global_mods);
            }

            Debug.Log("Viewer");
            for (int i = 0; i < twitchJson.chatters.viewers.Length; i++)
            {
                Debug.Log(twitchJson.chatters.viewers[i]);
            }

            yield return new WaitForSeconds(3.0f);

            TwitchViewerGenerate(twitchJson.chatters.viewers);
        }
    }

    
    //temp
    List<string> ListViewer = new List<string>();
    public GameObject playerPrefab;


    void TwitchViewerGenerate(string[] viewers)
    {
        for (int i = 0; i < viewers.Length; i++)
        {
            if (ListViewer.Count == 0 || !ListViewer.Contains(viewers[i]))
            {
                ListViewer.Add(viewers[i]);
                var obj = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
                obj.GetComponentInChildren<TextMesh>().text = viewers[i];
                obj.SetActive(true);
                obj.name = viewers[i];
            }
        }
    }

    private void TwitchMessageReceive(string msg)
    {
        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + twitchIRC.channelName.Length + 11);
        string userName = msg.Substring(1, msg.IndexOf('!') - 1);

        //remove old messages for performance reasons.
        if (message.Count > maxMessage)
        {
            Destroy(message.First.Value);
            message.RemoveFirst();
        }

        Debug.Log(string.Concat(userName, " : ", msgString));


        if (msgString[0] != '!' || !isInputPossible)
        {
            return;
        }

        if (msgString == "!참가" && isJoinPossible)
        {
            PlayerJoinMsg?.Invoke(userName);
        }

        if (isInputPossible)
            PlayerMoveMsg?.Invoke(userName, msgString);
      
    }
}
