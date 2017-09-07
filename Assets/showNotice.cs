using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showNotice : MonoBehaviour {
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
