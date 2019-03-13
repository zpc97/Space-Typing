using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBulltByTime : MonoBehaviour {

    public float lifetime;
    //数据控制组件
    private int number;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
