using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletData : MonoBehaviour {
    int enemyNumber;
    //数据控制组件
    private GameObject dataGameobject;
    //数据控制文本
    private DataControl datacontrol;
    //敌机物体
    private GameObject enemy;

    private Quaternion bulletRotation;
    // Use this for initialization
    void Start () {
        //获取数据控制组件
        dataGameobject = GameObject.FindGameObjectWithTag("dataControl");
        //获取数据控制文本组件
        if (dataGameobject != null)
        {
            datacontrol = dataGameobject.GetComponent<DataControl>();
        }
        bulletRotation = Quaternion.Euler(0, 0, 0);
        enemy = datacontrol.EnemyList[enemyNumber];
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.DOMove(datacontrol.EnemyList[enemyNumber].transform.position, 0.4f);
        float angle= Mathf.Atan2(datacontrol.EnemyList[enemyNumber].transform.position.x-gameObject.transform.position.x, datacontrol.EnemyList[enemyNumber].transform.position.z -gameObject.transform.position.z) * 180 / Mathf.PI;
        bulletRotation = Quaternion.Euler(0, angle, 0);
        gameObject.transform.rotation = bulletRotation;
    }
    public void setEnemy(int enemyTemp)
    {
        enemyNumber = enemyTemp;
    }
    public string getEnemyName()
    {
        return enemy.name;
    }
}
