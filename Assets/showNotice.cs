using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showNotice : MonoBehaviour {

   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
  public void   show()
    {
        //if (this.gameObject == null)
        this.gameObject.SetActive(true);
    }
    public void hide()
    {
        if (this.gameObject!= null)
            this.gameObject.SetActive(false);
    }
}
