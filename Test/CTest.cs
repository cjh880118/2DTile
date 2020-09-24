using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void EventHandler(int a);
public class CTest : MonoBehaviour
{
    TstClass tstClass;

    EventHandler a;
    AsyncCallback callBack = new AsyncCallback(ProcessDnsInformation);

    private static void ProcessDnsInformation(IAsyncResult ar)
    {
        Debug.Log("TT");
    }

    // Start is called before the first frame update
    void Start()
    {
        tstClass = new TstClass();
        //tstClass.happen += Evnt;
        //a += ee;
    }

    private void ee(int a)
    {
        Debug.Log("TT");
    }

    private void Evnt(int a)
    {
        Debug.Log(a);
    }

    void Test(object sender, EventArgs e)
    {
        Debug.Log(sender.ToString());
    }

    private void avc(object sender, EventArgs e)
    {
        Debug.Log("TTT");
    }

    private void aAA(object sender, EventArgs e)
    {
        Debug.Log(e.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tstClass.EvnetTest();
            //a.BeginInvoke(1, callBack, this);
        }
    }
}

public class TstClass
{
    public event EventHandler happen;
    public void EvnetTest()
    {
        //happen(1);
    }
}
