using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomDestory : MonoBehaviour {
    private float lifetime;
	// Use this for initialization
	void Start () {
        lifetime = 2f;
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
