using UnityEngine;
using System.Collections;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;
    //数据控制组件
    private GameObject dataGameobject;
    //数据控制文本
    private DataControl datacontrol;
    //飞机的编号
   
    void Start ()
	{
        //获取数据控制组件
        dataGameobject = GameObject.FindGameObjectWithTag("dataControl");
        //获取数据控制文本组件
        if (dataGameobject != null)
        {
            datacontrol = dataGameobject.GetComponent<DataControl>();
        }

	}
    private void Update()
    {
        if (gameObject.transform.position.z < -10)
        {
            Destroy(gameObject);
            datacontrol.removeEnemy(gameObject);
        }
    }
}
