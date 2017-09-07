using UnityEngine;
using System.Collections;
using DG.Tweening;
using AssemblyCSharp;

public class MessageBoxScript : MonoBehaviour {
	MyMahjongScript myMaj;
	// Use this for initialization
	void Start () {
		SocketEventHandle.getInstance ().messageBoxNotice += messageBoxNotice;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void btnClick(int index){
         


        SoundCtrl.getInstance ().playMessageBoxSound(index, GlobalDataScript.loginResponseData.account.sex);
		CustomSocket.getInstance ().sendMsg (new MessageBoxRequest(GlobalDataScript.loginResponseData.account.sex == 1 ? index + 1000 : index + 2000,GlobalDataScript.loginResponseData.account.uuid));
		if (myMaj == null) {
			myMaj = GameObject.Find ("Panel_GamePlay(Clone)").GetComponent<MyMahjongScript>();
		}
		if (myMaj != null) {
			myMaj.playerItems [0].showChatMessage (index);
		}
		hidePanel ();
	}

	public void showPanel(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		gameObject.transform.DOLocalMove (new Vector3(472,113), 0.4f);
	}

	public void hidePanel(){
		 
		gameObject.transform.DOLocalMove (new Vector3(472,697), 0.4f);
	}

    public void messageBoxNotice(ClientResponse response) {
        string[] arr = response.message.Split(new char[1] { '|' });
        int code = int.Parse(arr[0]);
        //传输性别  大于3000为女
        if (code > 3000)
        {

            code = code - 2000;
            SoundCtrl.getInstance().playMessageBoxSound(code, 0);
        }
        else {

            code = code - 1000;
            SoundCtrl.getInstance().playMessageBoxSound(code, 1);
        }

     
       


       
	}

	public void Destroy(){
		SocketEventHandle.getInstance ().messageBoxNotice -= messageBoxNotice;
	}
}
