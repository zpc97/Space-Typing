using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


public class SocketHelper : MonoBehaviour {

    private static SocketHelper socketHelper = new SocketHelper();

    private Socket socket;
    public string[] scoreranking;
    public string score="";
    public bool IPisexist =false;
    public bool NameExist=false;
    private bool HasMessage = false;
    private bool hasSetMessage = false;
    private string ID="";
    private string Message="";
    //单例模式
    public static SocketHelper GetInstance()
    {
        return socketHelper;
    }

    private SocketHelper()
    {

        //采用TCP方式连接
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //服务器IP地址
        IPAddress address = IPAddress.Parse("127.0.0.1");

        //服务器端口
        IPEndPoint endpoint = new IPEndPoint(address, 8016);

        //异步连接,连接成功调用connectCallback方法
        IAsyncResult result = socket.BeginConnect(endpoint, new AsyncCallback(ConnectCallback), socket);

        //这里做一个超时的监测，当连接超过5秒还没成功表示超时
        bool success = result.AsyncWaitHandle.WaitOne(5000, true);
        if (!success)
        {
            //超时
            Closed();
            Debug.Log("connect Time Out");
        }
        else
        {
            //与socket建立连接成功，开启线程接受服务端数据。
            Thread thread = new Thread(new ThreadStart(ReceiveSorket));
            thread.IsBackground = true;
            thread.Start();
        }

    }

    private void ConnectCallback(IAsyncResult asyncConnect)
    {
        Debug.Log("connect success");
    }

    private void ReceiveSorket()
    {
        //在这个线程中接受服务器返回的数据
        while (true)
        {

            if (!socket.Connected)
            {
                //与服务器断开连接跳出循环
                Debug.Log("Failed to clientSocket server.");
                socket.Close();
                break;
            }
            try
            {
                //接受数据保存至bytes当中
                byte[] bytes = new byte[4096];
                //Receive方法中会一直等待服务端回发消息
                //如果没有回发会一直在这里等着。
                int i = socket.Receive(bytes);
                if (i <= 0)
                {
                    socket.Close();
                    break;
                }
                string data = System.Text.Encoding.Default.GetString(bytes);
                //Debug.Log(scoreList);
                if (!data.Trim().Equals(""))
                {
                    Debug.Log("is not empty");
                    Debug.Log(data);
                    if(data.Substring(0, 9).Equals("score is "))
                    {
                        Debug.Log("reciver");
                        score = data.Substring(9);

                    }
                    if(data.Substring(0,5).Equals("exist"))
                    {
                        Debug.Log("IP is exist");
                        IPisexist = true;
                    }
                    if (data.Substring(0,8).Equals("notexist"))
                    {
                        Debug.Log("IP is not exist");
                        IPisexist = false;
                    }
                    if(data.Substring(0, 10).Equals("NameRepeat"))
                    {
                        NameExist = true;
                    }

                    if(data.Substring(0, 4).Equals("abcd"))
                    {
                        string[] messagesTemp = data.Substring(5).Split(';');
                        string name = messagesTemp[0];
                        string message = data.Substring(6 + name.Length);
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.Log("Failed to clientSocket error." + e);
                socket.Close();
                break;
            }
        }
    }

    //关闭Socket
    public void Closed()
    {
        if (socket != null && socket.Connected)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        socket = null;
    }
    //向服务端发送一条字符串
    //一般不会发送字符串 应该是发送数据包
    public void SendMessage(string str)
    {
        byte[] msg = Encoding.UTF8.GetBytes(str);

        if (!socket.Connected)
        {
            socket.Close();
            return;
        }
        try
        {
            IAsyncResult asyncSend = socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            bool success = asyncSend.AsyncWaitHandle.WaitOne(5000, true);
            if (!success)
            {
                socket.Close();
                Debug.Log("Failed to SendMessage server.");
            }
        }
        catch
        {
            Debug.Log("send message error");
        }
    }



    private void SendCallback(IAsyncResult asyncConnect)
    {
        Debug.Log("send success");
    }
    public void getScoreList()
    {
        PlayerPrefs.SetString("score",score);
        Debug.Log(score);
        //return scoreranking;
    }
    public bool judgeIP()
    {
        Debug.Log("exist?"+IPisexist);
        return IPisexist;
    }
    public bool JudgeName()
    {
        Debug.Log(NameExist);
        return NameExist;
    }

    public void initNameJudge()
    {
        NameExist = false;
    }

    public  void setMessageAndID(string id,string me)
    {
        hasSetMessage = true;
        HasMessage = true;
        Message = me;
        ID = id;
    }
    public bool getHasSetMessage()
    {
        return hasSetMessage;
    }
    public bool getHasMessage()
    {
        return HasMessage;
    }
    public string getMessage()
    {
        return Message;
    }

    public string getID()
    {
        return ID;
    }
    public void initHasMessage()
    {
        HasMessage = false;
    }

    public void initHasSetMessage()
    {
        hasSetMessage = false;
    }
}
