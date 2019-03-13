using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketTest : MonoBehaviour {

    void Start()
    {
        //创建socket连接
        SocketHelper s = SocketHelper.GetInstance();
        //发送信息向服务器端
        s.SendMessage("123");
    }
}
