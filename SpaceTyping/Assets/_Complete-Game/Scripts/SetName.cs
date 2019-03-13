using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetName : MonoBehaviour {
    public Text nameText;
    SocketHelper sh = SocketHelper.GetInstance();
    public void getName()
    {
        
        sh.SendMessage("Reg:"+nameText.text);
        StartCoroutine(waitForJudgeName());
        
    }

    IEnumerator waitForJudgeName()
    {
        yield return new WaitForSeconds(4f);
        if (!sh.JudgeName())
        {
            Application.LoadLevel("Done_Main");
        }
        else
        {
            Debug.Log("用户名存在");
        }
        sh.initNameJudge();
    }

}
