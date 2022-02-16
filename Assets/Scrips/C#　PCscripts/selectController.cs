using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectController : MonoBehaviour
{
    GameObject[] selectI;            //選択する項目(Button)を保存する配列
    public GameObject sentakuI;      //選ぶ囲いのimageを入れるところ
    public  int Selectmode,MaxCountS;//選択するときの状態の切り替え,配列などの最大値を保存
    public float plusPosx;           //横などに選択矢印を配置する場合の代入する数値
    public bool ButtonSwicth;        //ボタンを押せるかどうか
    Button[] selectBs;               //子オブジェクトのButtonを保存する配列
    Vector2[] selectIPos;            //子オブジェクトButtonの位置を保存する配列
    Vector2 SaveUIPos;
    int CountS;
    float MoveUD;
    float KeyDownTime;//Keyを押している時間をはかる
    private bool StartS=true;

    //スタート時の処理
    void SelectMode1()
    {
        if (StartS == true)
        {
            SaveUIPos = new Vector2(0, 0);
            int Scount = transform.childCount;//自身の子オブジェクトの数を代入
            MaxCountS = Scount;
            Debug.Log(MaxCountS);
            selectI = new GameObject[MaxCountS];//MaxCountS分、配列を増やす
            selectIPos = new Vector2[MaxCountS];//MaxCountS分、配列を増やす
            selectBs = new Button[MaxCountS];//MaxCountS分、配列を増やす
            for (int i = 0; i < MaxCountS; i++)
            {
                selectI[i] = transform.GetChild(i).gameObject;
                selectIPos[i] = selectI[i].GetComponent<RectTransform>().anchoredPosition;
                SaveUIPos = new Vector2(selectIPos[i].x + plusPosx, selectIPos[i].y);
                selectIPos[i] = SaveUIPos;
                selectBs[i] = selectI[i].GetComponent<Button>();
            }
            sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[CountS]/*.anchoredPosition*/;
            ButtonSwicth = true;
            StartS = false;
        }
        else
        {

        }
    }

    //縦、横に選択できる
    void SelectMode2()
    {
        //MoveUD = Input.GetAxisRaw("Vertical");
        //MoveUD = MoveUD * 0.01f;
        //選択カーソルのsentakuIを動かす(一回押し)
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)/*MoveUD <0*/)
        {
            ButtonSwicth = false;
            CountS++;
            if (CountS >= MaxCountS)//一番下から行こうとしたとき
            {
                //一番上に選択imageをおく
                sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[0];
                CountS = 0;
            }
            else
            {
                sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[CountS];
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)/*MoveUD >0*/)
        {
            ButtonSwicth = false;
            CountS--;
            if (CountS <= -1) //一番上からいこうとした場合
            {
                //一番下に選択imageをおく
                sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[MaxCountS - 1];
                CountS = MaxCountS - 1;
            }
            else
            {
                sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[CountS];
            }
        }

        //選択カーソルのsentakuIを動かす(長押し)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)/*MoveUD <0*/)
        {
            KeyDownTime += Time.deltaTime;
            if (KeyDownTime >= 0.2f)
            {
                CountS++;
                if (CountS >= MaxCountS)//一番下から行こうとしたとき
                {
                    //一番上に選択imageをおく
                    sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[0];
                    CountS = 0;
                }
                else
                {
                    sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[CountS];
                }
                KeyDownTime = 0;
            }
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)/*MoveUD >0*/)
        {
            KeyDownTime += Time.deltaTime;
            if (KeyDownTime >= 0.2f)
            {
                CountS--;
                if (CountS <= -1) //一番上からいこうとした場合
                {
                    //一番下に選択imageをおく
                    sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[MaxCountS - 1];
                    CountS = MaxCountS - 1;
                }
                else
                {
                    sentakuI.GetComponent<RectTransform>().anchoredPosition = selectIPos[CountS];
                }
                KeyDownTime = 0;
            }
        }

        //W,S,↑,↓キーを上げた時
        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.S) || 
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            KeyDownTime = 0;
            ButtonSwicth = true;
        }

        if (ButtonSwicth == true && Input.GetKeyDown(KeyCode.Return))//エンターを押した場合
        {
            selectBs[CountS].onClick.Invoke();//ボタンのonClickを起動する
                                              //Debug.Log(selectI[CountS]+"を押した");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        SelectMode1();
    }

    // Update is called once per frame
    void Update()
    {
        SelectMode2();
    }
}
