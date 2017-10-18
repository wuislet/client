using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class bottomScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Rigidbody2D pai;
   
   // public GameObject Bigmajiang;
   // public GameObject Image;
    private float timer = 0;
    private int cardPoint;
    private Vector3 RawPosition;
    private Vector3 oldPosition;
    //==================================================
    public Image image;
    public GameObject Image_bg_color;
    public Image guiIcon;
    public Text showLabel;
    public float speed = 1.0f;
    public float ShowTime = 1.5f;
    //
    public delegate void EventHandler(GameObject obj);
    public event EventHandler onSendMessage;
	public event EventHandler reSetPoisiton;
	private bool selected = false;
    public bool isSelect
    {
        get { return selected; }
    }
    public bool cardenable = true;
    public bool isSpecialClick = false;
    public event EventHandler onSpecialClick;

    private void Start()
    {
        Image_bg_color.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        return;
        //删除拖牌到桌面打出的功能。
        if (!GlobalDataScript.isDrag || !cardenable)
        {
            return;
        }
        
        GetComponent<RectTransform>().pivot.Set(0, 0);
        transform.position = Input.mousePosition;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GlobalDataScript.isDrag || !cardenable)
        {
            return;
        }
        
        if(isSpecialClick)
        {
            if(onSpecialClick != null)
            {
                onSpecialClick(gameObject);
            }
            return;
        }

		if (selected == false) {
			selected = true;
			oldPosition = transform.localPosition;
		} else {
			sendObjectToCallBack ();
		}
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GlobalDataScript.isDrag || !cardenable)
        {
            return;
        }
        if (isSpecialClick)
        {
            return;
        }
        reSetPoisitonCallBack();
    }

	private void sendObjectToCallBack(){
        if (onSendMessage != null)     //发送消息
        {
            onSendMessage(gameObject);//发送当前游戏物体消息
        }
	}

	private void reSetPoisitonCallBack(){
		if (reSetPoisiton != null) {
			reSetPoisiton (gameObject);
		}
	}

    public void setPoint(int _cardPoint, int guiPai=-1)
    {
        cardPoint = _cardPoint;//设置所有牌指针
		image.sprite = Resources.Load("Cards/Big/b"+cardPoint,typeof(Sprite)) as Sprite;

        if (guiPai != -1)
        { //显示鬼牌icon
            if (_cardPoint == guiPai)
                guiIcon.gameObject.SetActive(true);
        }

    }

    public void setGuiPoint(int _cardPoint)
    {
        cardPoint = _cardPoint;//设置所有牌指针
        image.sprite = Resources.Load("Cards/Small/s" + cardPoint, typeof(Sprite)) as Sprite;
        guiIcon.gameObject.SetActive(true);
    }

    public void setGuiShow() {
        guiIcon.gameObject.SetActive(true);
    }

    public int getPoint()
    {
        return cardPoint;
    }

    private void destroy()
    {
       // Destroy(this.gameObject);
    }

    public void SelectCard(bool select)
    {
        selected = select;
        if (select)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -272f);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -292f);
        }
    }

    public void EnableCard(bool isEnable)
    {
        cardenable = isEnable;
        if (isEnable)
        {
            Image_bg_color.SetActive(false);
        }
        else
        {
            Image_bg_color.SetActive(true);
        }
    }
}
