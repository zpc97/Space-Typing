using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DataControl : MonoBehaviour {
    public List<GameObject> EnemyList;
    public List<GameObject> BulletList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void addEnemy(GameObject enemy)
    {
        EnemyList.Add(enemy);
    }
    public void removeEnemy(GameObject enemy)
    {
        EnemyList.Remove(enemy);
    }
    public void addBullet(GameObject bullet)
    {
        BulletList.Add(bullet);
    }
    public int GetEnemyListLength()
    {
        return EnemyList.Count;
    }
    public int GetBulletListLengh()
    {
        return BulletList.Count;
    }
}
