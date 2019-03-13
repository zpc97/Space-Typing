using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    private char[] c = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N','O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    public char CharName;
    public Transform enemyText;
    public TextMesh WordText;
    public bool isChanged;
    // Use this for initialization
    void Start () {
        setName();
        enemyText = gameObject.transform.GetChild(3);
        WordText= enemyText.GetComponent<TextMesh>();
        WordText.text = CharName.ToString();
        isChanged = false;
        //Debug.Log("123"+WordText.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setName()
    {
        CharName = c[Random.Range(0, c.Length)];
    }
    public char getName()
    {
        return CharName;
    }
    public void setChanged()
    {
        isChanged = true;
    }
    public bool getChanged()
    {
        return isChanged;
    }
}
