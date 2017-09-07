using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class TopAndBottomCardScript : MonoBehaviour {
    private int cardPoint;

    //=========================================
	public Image cardImg;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update () {
	
	}

    public void setPoint(int _cardPoint)
    {
    
        cardPoint = _cardPoint;//设置所有牌指针
        MyDebug.Log("put out cardPoint" + cardPoint + "----------------------4.2-111-------------");
		cardImg.sprite = Resources.Load("Cards/Small/s"+cardPoint,typeof(Sprite)) as Sprite;
        MyDebug.Log("put out cardPoint" + cardPoint + "----------------------4.2--------------");
    }

	public void setLefAndRightPoint(int _cardPoint){
		cardPoint = _cardPoint;//设置所有牌指针
        MyDebug.Log("put out cardPoint" + cardPoint + "----------------------4.3--------------");
		cardImg.sprite = Resources.Load("Cards/Left&Right/lr"+cardPoint,typeof(Sprite)) as Sprite;

	}

    public int getPoint()
    {
        return cardPoint;
    }
}
