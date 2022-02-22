using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guardController : MonoBehaviour
{
    public float counterTime;//カウンターできる時間
    public float damageTime;//無敵時間

    private Animator anim;
    private Slider slider;//ガードゲージのUI
    private float CGtime,cTime;//
    private float sliderS;
    private bool MouseSwicth;//マウスが動かせるかどうか
    private bool gadeSwicth;//ガードが起動しているかどうか
    private bool counterSwicth;//
    private bool damageSwicth;//
    void animationcancel()//アニメーションを途中で終わらせるのに使う
    {
        anim.SetBool("counterattack", false);
        anim.SetBool("grndbreak", false);
        MouseSwicth = true;
        Debug.Log("動いた");
    }
    void Damage()//ダメージ時のHPの減りや無敵時間などを設定
    {
        if (damageSwicth == true)
        {
            cTime += 1 * Time.deltaTime;
            if (cTime<=0.01f) {
                gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
            }



            if (cTime>= damageTime)
            {
                gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                damageSwicth = false;
                cTime = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        slider = GameObject.Find("guardgage").GetComponent<Slider>();
        MouseSwicth = true;
        sliderS = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderS <= 0)
        {
            MouseSwicth = false;
            gadeSwicth = false;
            anim.SetBool("grndbreak", true);
            anim.SetBool("guard", false);
            damageSwicth = false;
            sliderS = slider.maxValue;
            slider.value = slider.maxValue;
        }

        if (MouseSwicth==true) {
            if (Input.GetMouseButton(1))//右クリックで起動
            {
                CGtime += 1 * Time.deltaTime;
                if (CGtime < counterTime) //カウンター確認
                {
                    //Debug.Log("カウンター中"+CGtime);
                    anim.SetBool("counter", true);

                } else if (CGtime >= counterTime) //ガード中
                {
                    //Debug.Log("ガード中");
                    anim.SetBool("guard", true);
                    anim.SetBool("counter", false);
                    gadeSwicth = true;
                }
            } else if (Input.GetMouseButtonUp(1))//右マウスボタンを上げた時
            {
                if (CGtime <= 0.1f)//すぐにできないよう猶予を作っている
                {
                    Debug.Log("カウンターキャンセル");
                    anim.SetBool("counter", false);
                }
                else if (CGtime > 0.1f && CGtime < counterTime)//カウンター
                {
                    Debug.Log("カウンター");
                    anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);

                }
                else if (CGtime >= counterTime)//ガードを終わらせる
                {
                    Debug.Log("ガード終了");
                    anim.SetBool("guard", false);
                    gadeSwicth = false;
                }
                CGtime = 0;
            }
        }else
        {
            Debug.Log("ガード中止");
            CGtime = 0;
        }

        

        Damage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="enemyrightattack")
        {
            damageSwicth = true;
            if (gadeSwicth==true)
            {
                Debug.Log("敵の弱攻撃を軽減");
                //ノーダメージ

            }
            else
            {
                Debug.Log("敵の弱攻撃がヒット");
                //減るのはハート1つ

            }
        }
        else if (collision.gameObject.tag == "enemyheavyattack")
        {
            damageSwicth = true;
            if (gadeSwicth == true)
            {
                Debug.Log("敵の強攻撃を軽減");
                //減るのはハート半分でガードゲージが減る
                slider.value -= 20;
                sliderS = slider.value;
                Debug.Log(sliderS);
            }
            else
            {
                Debug.Log("敵の強攻撃がヒット");
                //減るのはハート2つ

            }
        }
    }
}
