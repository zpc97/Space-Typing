using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageList : MonoBehaviour {
    private List<string> messages;
    private List<string> IDS;
	// Use this for initialization
	void Start () {
        messages = new List<string>();
        IDS = new List<string>();
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl"); messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
        messages.Add("111111111111");
        messages.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        IDS.Add("zpc");
        IDS.Add("sl");
    }
	
	// Update is called once per frame
	void Update () {
        if (SocketHelper.GetInstance().getHasSetMessage())
        {
            IDS.Add(SocketHelper.GetInstance().getID());
            messages.Add(SocketHelper.GetInstance().getMessage());
            SocketHelper.GetInstance().initHasSetMessage();
        }
	}

    public void addMessage(string message,string ID)
    {
        messages.Add(message);
        IDS.Add(ID);
    }

    public List<string> getMessages()
    {
        return messages;
    }

    public List<string> getIDS()
    {
        return IDS;
    }


}
