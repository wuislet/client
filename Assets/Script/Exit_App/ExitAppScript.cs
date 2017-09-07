using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ExitAppScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void exit(){
		CustomSocket.getInstance().sendMsg(new LoginRequest());

        #if UNITY_ANDROID
              Application.Quit();
        #elif UNITY_IPHONE
        	TipsManagerScript.getInstance ().setTips ("苹果手机请按Home键盘进行退出！");
        #endif

        //多态  调用退出登录接口

    }

	public void cancle(){
		ExitAppScript self = GetComponent<ExitAppScript> ();
		self = null;
		Destroy (self);
		Destroy (gameObject);
	}
}
