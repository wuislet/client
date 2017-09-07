using UnityEngine;
using System.Collections;
/**
 * sound control class
 * 
 * author :kevin
 * 
 * */
public class SoundCtrl : MonoBehaviour
{

    private Hashtable soudHash = new Hashtable();

    private static SoundCtrl _instance;

    private static AudioSource audioS;
    private static AudioSource cardSounPlay;
    private static AudioSource ActionSounPlay;
    private static AudioSource MessageSounPlay;
    public static SoundCtrl getInstance()
    {
        if (_instance == null)
        {
            _instance = new SoundCtrl();
            audioS = GameObject.Find("MyAudio").GetComponent<AudioSource>();
            cardSounPlay = GameObject.Find("cardSoundPlay").GetComponent<AudioSource>();
            ActionSounPlay = GameObject.Find("ActionSound").GetComponent<AudioSource>();
            MessageSounPlay = GameObject.Find("MessageSound").GetComponent<AudioSource>();
		

        }

        return _instance;
    }

    public void playSound(int cardPoint, int sex)
    {
        Debug.Log("----------------------------------------------------playSound");

        if (GlobalDataScript.soundToggle)
        {



            string path = "Sounds/";
            if (sex == 1)
            {
                path += "boy/" + (cardPoint + 1);
            }
            else {
                path += "girl/" + (cardPoint + 1);
            }
            AudioClip temp = (AudioClip)soudHash[path];
            if (temp == null)
            {
                temp = GameObject.Instantiate(Resources.Load(path)) as AudioClip;
                soudHash.Add(path, temp);
            }
            cardSounPlay.volume = GlobalDataScript.yinxiaoVolume;
            cardSounPlay.clip = temp;
            cardSounPlay.loop = false;
            cardSounPlay.Play();

            //if (audioS != null)
            //    audioS.volume = 1;


        }


    }

    public void playMessageBoxSound(int codeIndex, int sex)
    {
        Debug.Log("----------------------------------------------------playMessageBoxSound");
        if (GlobalDataScript.soundToggle)
        {
            string path;
            if (sex == 1)
                path = "Sounds/other/boy/" + codeIndex;
            else
                path = "Sounds/other/women/" + codeIndex;
            AudioClip temp = (AudioClip)soudHash[path];
            if (temp == null)
            {
                temp = GameObject.Instantiate(Resources.Load(path)) as AudioClip;
                soudHash.Add(path, temp);
            }
            MessageSounPlay.volume = GlobalDataScript.yinxiaoVolume;
            MessageSounPlay.clip = temp;
            MessageSounPlay.Play();
            //if (audioS != null)
            //    audioS.volume = 1;


        }
    }

    public void playBGM(int type)
    {
        string path = "";
        switch (type)
        {
            case 0:
                audioS.loop = false;
                audioS.Stop();
                return;
            case 1:
                path = "Sounds/BackAudio1";
                break;
            case 2:
                path = "Sounds/mjBGM";
                break;
        }
        AudioClip temp = (AudioClip)soudHash[path];
        if (temp == null)
        {
            temp = GameObject.Instantiate(Resources.Load(path)) as AudioClip;
            soudHash.Add(path, temp);
        }
        audioS.volume = GlobalDataScript.yinyueVolume;
        audioS.clip = temp;
        audioS.loop = true;
        audioS.Play();
        if (GlobalDataScript.soundToggle)
        {
            audioS.mute = false;
        }
        else {
            audioS.mute = true;
        }
    }



    public void playSoundByAction(string str, int sex)
    {
        Debug.Log("----------------------------------------------------playSoundByAction");

        string path = "Sounds/";
        if (sex == 1)
        {
            path += "boy/" + str;
        }
        else {
            path += "girl/" + str;
        }
        AudioClip temp = (AudioClip)soudHash[path];
        if (temp == null)
        {
            temp = GameObject.Instantiate(Resources.Load(path)) as AudioClip;
            soudHash.Add(path, temp);
        }

        ActionSounPlay.volume = GlobalDataScript.yinxiaoVolume;
        ActionSounPlay.clip = temp;
        ActionSounPlay.Play();
        //if (audioS != null)
        //    audioS.volume = 1;
    }





	public void playSoundByActionButton(int type)
	{
		Debug.Log("----------------------------------------------------playSoundByAction");

		string path = "Sounds/other/";
		//按钮
		if (type == 1)
		{
			path += "clickbutton";
			//发牌
		}else if (type == 2)
		{
			path += "dice";
			//准备
		}else if (type == 3)
		{
			path += "ready";
			//打牌
		}else if (type == 4)
		{
			//path += "tileout";
			path += "out";
			//摸牌
		} else if (type == 5)
		{
			path += "select";
		} else if (type == 6)
		{
			//path += "tileout";
			path += "tileout";
			//摸牌
		}
        else if (type == 7)
        {
            //path += "tileout";
            path += "touzi";
            //骰子
        }
        AudioClip temp = (AudioClip)soudHash[path];
		if (temp == null)
		{
			temp = GameObject.Instantiate(Resources.Load(path)) as AudioClip;
			soudHash.Add(path, temp);
		}

		ActionSounPlay.volume = GlobalDataScript.yinxiaoVolume;
		ActionSounPlay.clip = temp;
		ActionSounPlay.Play();
		//if (audioS != null)
		//	audioS.volume = 1;
	}



}
