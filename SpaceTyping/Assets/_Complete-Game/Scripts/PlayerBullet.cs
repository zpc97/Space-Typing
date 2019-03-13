using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerBullet : MonoBehaviour {
    private char[] c = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public GameObject shot;
    public Transform shotSpawn;
    public Vector3 position;
    //GameController物体
    private GameObject gameControllerGameObject;
    //Done_GameController脚本
    private Done_GameController gameControllerScript; 
    private bool istrue;
    //数据控制组件
    private GameObject dataGameobject;
    //数据控制文本
    private DataControl datacontrol;
    //子弹数据文本
    private BulletData bulletdata;
    private ShowMessage showmessage;
    public GameObject showMessageGameObject;

    // Use this for initialization
    void Start () {
        showmessage = showMessageGameObject.GetComponent<ShowMessage>();
        position = new Vector3(0, 0, 0);
        //获取数据控制组件
        dataGameobject = GameObject.FindGameObjectWithTag("dataControl");
        //获取数据控制文本组件
        if (dataGameobject != null)
        {
            datacontrol = dataGameobject.GetComponent<DataControl>();
        }
        istrue = false;
        gameControllerGameObject = GameObject.FindGameObjectWithTag("GameController");
        gameControllerScript = gameControllerGameObject.GetComponent<Done_GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            string inputMessage = Input.inputString;
            if(Input.inputString!=""&&!showmessage.getIsShow())
            {
                //Debug.Log(Input.inputString);
                //Debug.Log(Input.inputString.Substring(0,1));
                for (int i = 0; i < datacontrol.GetEnemyListLength(); i++)
                {
                    if (datacontrol.EnemyList[i] != null&&datacontrol.EnemyList[i].transform.position.z>=-4.8f)
                    {

                        Data data = datacontrol.EnemyList[i].GetComponent<Data>();
                        if (Input.inputString.Substring(0, 1).ToUpper().Equals(data.CharName.ToString())&&!data.getChanged())
                        {                       
                            GameObject bullet= Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                            bulletdata = bullet.GetComponent<BulletData>();
                            bulletdata.setEnemy(i);
                            GetComponent<AudioSource>().Play();
                            data.setChanged();
                            istrue = true;
                            return;
                        }
                    }
                }   
            }
            if (istrue == false&&!inputMessage.Equals("")&&!showmessage.getIsShow())
            {
                gameControllerScript.Reduce(10);
                gameControllerScript.UpdateScore();
            }
            istrue = false;
        }
        
    }

    public bool InputIsTrue(string temp)
    {
        for(int i = 0; i < c.Length; i++)
        {
            if (temp.Trim().Equals(c[i]))
            {
                return true;
            }
        }
        return false;
    }
}
