using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowMessage : MonoBehaviour {
    public Text MessagePlayfabs;
    public RectTransform content;
    public GameObject messageGameobject;
    public GameObject MessageListGameobject;
    private MessageList messagelistscript;
    private bool isShow;
    private int n;
    public Text InputText;
    public GameObject sendButton;
    public GameObject InputArea;
	// Use this for initialization
	void Start () {
        messagelistscript = MessageListGameobject.GetComponent<MessageList>();
        isShow = false;
        n = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShow&&SocketHelper.GetInstance().getHasMessage())
        {
            showMessage();
            SocketHelper.GetInstance().initHasMessage();
        }
	}

    public void showMessage()
    {
        for(int j = 0; j < content.GetChildCount(); j++)
        {
            Destroy(content.GetChild(j).gameObject);
        }
        if (n % 2 == 1)
        {
            Time.timeScale = 1;
            sendButton.SetActive(false);
            InputArea.SetActive(false);
            messageGameobject.SetActive(false);
            isShow = false;
        }
        else
        {
            Time.timeScale = 0;
            isShow = true;
            messageGameobject.SetActive(true);
            sendButton.SetActive(true);
            InputArea.SetActive(true);
            float height = 0;
            List<string> IDs = messagelistscript.getIDS();
            List<string> Messages = messagelistscript.getMessages();
            for (int i = 0; i < IDs.Count; i++)
            {
                Text mes = Instantiate(MessagePlayfabs, content);
                mes.text = IDs[i]+ ":"+"\n"  +Messages[i];
                height += mes.preferredHeight;
                content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }
        n++;
    }

    public bool getIsShow()
    {
        return isShow;
    }


    public void addMessageList()
    {
        Debug.Log("hhhh");
        if (!InputText.text.Equals(""))
        {
            messagelistscript.addMessage(InputText.text,"me");
            showMessage();
            InputArea.GetComponent<InputField>().text = "";
            //SocketHelper.GetInstance().SendMessage("abcd:" + InputText.text);
        }
    }
}
