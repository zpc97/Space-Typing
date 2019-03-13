using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonSetting : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cover;
    public Sprite sprite1;
    public Sprite sprite2;

    SocketHelper sh = SocketHelper.GetInstance();

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = sprite2;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = sprite1;
    }

    public void start()
    {
        sh.SendMessage("Login");

        StartCoroutine(WaitForStart());
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void ScoreScene()
    {
        
        SocketHelper sh = SocketHelper.GetInstance();
        sh.SendMessage("LoadScore");
        StartCoroutine(WaitLoadScore());
    }
    IEnumerator WaitLoadScore()
    {
        yield return new WaitForSeconds(4f);
        Application.LoadLevel("ScoreRanking");
    }

    IEnumerator WaitForStart()
    {
        
        
        yield return new WaitForSeconds(4f);
        if (!sh.judgeIP())
        {
            Debug.Log("IPNotExist");
            cover.SetActive(true);
        }
        else
        {
            Debug.Log("IPExist");
            Application.LoadLevel("Done_Main");
        }
       
    }

}
