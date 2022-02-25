using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guardController : MonoBehaviour
{
    [SerializeField] GameObject CounterObject;
    public GameObject hpui;//HPのUIを入れる変数
    public float counterTime;//カウンターできる時間
    public float damageTime;//無敵時間

    EcColliderController ECC;
    SpriteRenderer SR;//自分のSpriteRendererを入れる変数
    int hp,deletehp;//HPの変数

    private SpriteRenderer sR;//自分のSpriteRendererをいれる
    private Animator anim;
    private Slider slider;//ガードゲージのUI
    private float CGtime,cTime;//
    private float TransparentTime;//透明になれる時間
    private float sliderS;
    private bool MouseSwicth;//マウスが動かせるかどうか
    private bool gadeSwicth;//ガードが起動しているかどうか
    private bool counterSwicth;//
    private bool damageSwicth;//ダメージを食らっているかどうか
    private bool damageHetSwcith;//ダメージを食らったときにうごく
    void animationcancel()//アニメーションを途中で終わらせるのに使う
    {
        anim.SetBool("counterattack", false);
        anim.SetBool("grndbreak", false);
        gameObject.layer = LayerMask.NameToLayer("Default");
        MouseSwicth = true;
        Debug.Log("動いた");
    }
    void Damage()//ダメージ時のHPの減りや無敵時間などを設定
    {
        if (damageSwicth == true)
        {
            //HPを減らす
            if (damageHetSwcith==true)
            {
                for(int i=0;i< deletehp; i++)
                {
                    GameObject s=hpui.transform.GetChild(hp-1).gameObject;
                    s.SetActive(false);
                    hp--;
                    Debug.Log(hp);
                    if (hp<=0)
                    {
                        damageSwicth = false;
                        i = deletehp;
                    }
                }
                deletehp = 0;
                TransparentTime = 0;
                damageHetSwcith = false;
            }
            cTime += 1 * Time.deltaTime;
            TransparentTime+=100*Time.deltaTime;
            Debug.Log(TransparentTime);
            if (cTime<=0.01f) {
                gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
            }else if (cTime > 0.01f&&cTime<damageTime)
            {
                //if (TransparentTime % 2==0)
                //{
                //    sR.color = new Color(1,1,1,1);
                //}
                //else
                //{
                //    sR.color = new Color(1, 1, 1, 0);
                //}
            }
            else if (cTime>= damageTime)
            {
                sR.color = new Color(1, 1, 1, 1);
                gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                damageSwicth = false;
                TransparentTime = 0;
                cTime = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;//最大hpの設定
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        slider = GameObject.Find("guardgage").GetComponent<Slider>();
        MouseSwicth = true;
        sliderS = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //ガードブレイク
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

        //カウンターが成功した時
        if (counterSwicth==true)
        {
            if (ECC.counterHetSwicth == true)
            {
                Debug.Log("当たった");
                gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
                anim.SetBool("counterattack", true);
                anim.SetBool("counter", false);
                CounterObject.SetActive(false);
            }
            counterSwicth = false;
        }

        //カウンターとガードの操作
        if (MouseSwicth==true) {
            if (Input.GetMouseButton(1))//右クリック押しっぱなし
            {
                CGtime += 1 * Time.deltaTime;
                if (CGtime < counterTime) //カウンター確認
                {
                    //Debug.Log("カウンター中"+CGtime);
                    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");
                    anim.SetBool("counter", true);
                    CounterObject.SetActive(true);

                } else if (CGtime >= counterTime) //ガード中
                {
                    //Debug.Log("ガード中");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    anim.SetBool("guard", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                    gadeSwicth = true;
                }
            } else if (Input.GetMouseButtonUp(1))//右マウスボタンを上げた時
            {
                if (CGtime <= 0.05f)//すぐにできないよう猶予を作っている
                {
                    Debug.Log("カウンターキャンセル");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                }
                else if (CGtime > 0.05f && CGtime < counterTime)//カウンター
                {
                    Debug.Log("カウンター");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    //anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);

                }
                else if (CGtime >= counterTime)//ガードを終わらせる
                {
                    Debug.Log("ガード終了");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    anim.SetBool("guard", false);
                    gadeSwicth = false;
                }
                CGtime = 0;
            }
        }else
        {
            Debug.Log("ガード中止");
            gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
            CGtime = 0;
        }

        

        Damage();
        if (hp<=0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="counterHet")
        {
            //Debug.Log(collision.gameObject);
            ECC = collision.gameObject.GetComponent<EcColliderController>();
            counterSwicth = true;

        }

        if (collision.gameObject.tag=="enemyrightattack")
        {
            damageHetSwcith = true;
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
                deletehp = 2;
            }
        }
        else if (collision.gameObject.tag == "enemyheavyattack")
        {
            damageHetSwcith = true;
            damageSwicth = true;
            if (gadeSwicth == true)
            {
                Debug.Log("敵の強攻撃を軽減");
                //減るのはハート半分でガードゲージが減る
                deletehp = 1;
                slider.value -= 20;
                sliderS = slider.value;
                //Debug.Log(sliderS);
            }
            else
            {
                Debug.Log("敵の強攻撃がヒット");
                //減るのはハート1.5つ
                deletehp = 3;
            }
        }
    }
}
