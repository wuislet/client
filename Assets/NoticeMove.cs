using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>/// Notice 公告的自动移动/// </summary>

public class NoticeMove : MonoBehaviour {

   private  float moveSpeed = 50f;
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        Vector3 position = this.transform.position;
        if (position.x <= -200f)
        {
            this.transform.position = new Vector3(position.x + 500*2, position.y, position.z);
        }

	}
}
