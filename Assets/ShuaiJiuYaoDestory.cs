using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuaiJiuYaoDestory : MonoBehaviour {


	void Start () {
 
    }
	
	void Update () {
        Invoke("Destroy", 5f);
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}
