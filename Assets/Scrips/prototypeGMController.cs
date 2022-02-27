using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ゲームマネージャー
public class prototypeGMController : MonoBehaviour
{
    private enum Stage {Stage1,Stage2,Stage3,Stage4}
    private Stage start=Stage.Stage1;//最初に始まるイーナム
    private Stage nextstart ;//次に始まるイーナムを入れる変数

    public GameObject Enemy, Enemy1, Enemy2;//三種の敵キャラを入れる変数
    public float MaxExplanationTime;//説明できる最大時間

    private GameObject SaveObject;
    private Text tutorialT;//チュートリアル用textを入れる変数
    private int gimmickNumber;//
    private float ExplanationTime;//
    private bool Stagestart;//ステージが始まったかどうか
    private bool changebool;//Enemy2のスクリプトにあるboolを変えるようにする変数
    private bool TimeSwicth;//説明時間が動いているかどうか

    guardController gC;
    // Start is called before the first frame update
    void Start()
    {
        gimmickNumber = 0;
        gC = GameObject.Find("prototypePlayer").GetComponent<guardController>();
        tutorialT = GameObject.Find("tutorialText").GetComponent<Text>();
        TimeSwicth = true;
        //Stagestart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gC.hp<=0)//死んだのでシーンをやり直す
        {
            Debug.Log("プレイヤー死亡");
            SceneManager.LoadScene("prototypeActionScene");
        }

        

        if (changebool==true)
        {
            if (start==Stage.Stage3)
            {
                var c = SaveObject.GetComponent<scarecrowEnemy3C>();
                c.rightattackSwicth = true;
            }
            else if (start == Stage.Stage4)
            {
                var c = SaveObject.GetComponent<scarecrowEnemy3C>();
                c.heavyattackSwicth = true;
            }
            changebool = false;
        }
        //イーナムを呼び出す
        switch (start) {
            case Stage.Stage1:
                STAGE1();
                break;
            case Stage.Stage2:
                STAGE2();
                break;
            case Stage.Stage3:
                STAGE3();
                break;
            case Stage.Stage4:
                STAGE4();
                break;
        }

        //イーナムを入れ替える
        if (start!=nextstart)
        {
            start = nextstart;
        }

        Explanationtime();
    }
    private void Explanationtime()
    {
        if (TimeSwicth == true)
        {
            if (MaxExplanationTime >ExplanationTime)
            {
                ExplanationTime += 1 * Time.deltaTime;
                Debug.Log(ExplanationTime);
            }else if (MaxExplanationTime <= ExplanationTime)
            {
                gimmickNumber++;
                ExplanationTime = 0;
            }
        }
    }
    private void changeStage(Stage _stage)//ステージを切り替えたいときに使用
    {
        TimeSwicth = true;
        gimmickNumber = 0;
        nextstart = _stage;
    }
    private void STAGE1()//ステージ1
    {
        if (gimmickNumber == 0)
        {
            tutorialT.text = "操作説明";
        } else if (gimmickNumber == 1)
        {
            tutorialT.text = "AキーとＤキーで左右移動";
        }
        else if (gimmickNumber == 2)
        {
            tutorialT.text = "spaceキーでジャンプ";
        }
        else if (gimmickNumber == 3)
        {
            tutorialT.text = "マウス左ボタンで弱攻撃";
        }else if (gimmickNumber == 4)
        {
            tutorialT.text = "マウス左ボタンを長押しすることでオレンジ色のゲージが溜まり";
            if (MaxExplanationTime <= ExplanationTime)
            {
                Stagestart = true;
            }
        } 
        else if (gimmickNumber == 5) {
            tutorialT.text = "最大の状態で左ボタンを離すと強攻撃が出せます";
            if (Stagestart == true)
            {
                TimeSwicth = false;
                SaveObject = Instantiate(Enemy);
                SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
                Stagestart = false;
            }
            else if (SaveObject == null && gimmickNumber == 5)
            {
                Debug.Log(SaveObject);
                Stagestart = true;
                changeStage(Stage.Stage2);
            }
        }

    }
    private void STAGE2()//ステージ2
    {
        if (gimmickNumber == 0)
        {
            tutorialT.text = "マウス右ボタンでカウンターが出せ";
        }
        else if (gimmickNumber == 1)
        {
            tutorialT.text = "マウス右ボタンを長押しすることでガードがだせ";
            if (MaxExplanationTime <= ExplanationTime)
            {
                Stagestart = true;
            }
        }
        else if (gimmickNumber == 2)
        {
            tutorialT.text = "このガードは敵の攻撃を防ぐことが出来る";
            if (Stagestart == true)
            {
                TimeSwicth = false;
                SaveObject = Instantiate(Enemy1);
                SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
                Stagestart = false;
            }

            if (SaveObject == null && gimmickNumber == 2)
            {
                Debug.Log(SaveObject);
                Stagestart = true;
                changeStage(Stage.Stage3);
            }
        }
    }
    private void STAGE3()//ステージ3
    {
        if (gimmickNumber == 0)
        {
            tutorialT.text = "敵の攻撃には2種類あり弱攻撃と強攻撃があります";
        }
        else if (gimmickNumber == 1)
        {
            tutorialT.text = "水色の攻撃が弱攻撃であり、くらうと2ダメージくらい";
            if (MaxExplanationTime <= ExplanationTime)
            {
                Stagestart = true;
            }
        }
        else if (gimmickNumber == 2)
        {
            tutorialT.text = "ガードすればダメージをくらいません";
            if (Stagestart == true)
            {
                TimeSwicth = false;
                SaveObject = Instantiate(Enemy2);
                SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
                changebool = true;
                Stagestart = false;
            }

            if (SaveObject == null&& gimmickNumber == 2)
            {
                Debug.Log(SaveObject);
                Stagestart = true;
                changeStage(Stage.Stage4);
            }
        }
    }
    private void STAGE4()//ステージ4
    {
        if (gimmickNumber == 0)
        {
            tutorialT.text = "オレンジ色の攻撃は強攻撃であり、3ダメージくらい";
        }
        else if (gimmickNumber == 1)
        {
            tutorialT.text = "ガードすると1ダメージくらい水色のゲージが減ってしまう";
        }
        else if (gimmickNumber == 2)
        {
            tutorialT.text = "この水色のゲージが0になるとガードブレイクされてしまい、一定時間動けなくなってしまう。";
        }
        else if (gimmickNumber == 3)
        {
            tutorialT.text = "だが、この攻撃をカウンターすることで相手をスキだらけにできます";
            if (MaxExplanationTime <= ExplanationTime)
            {
                Stagestart = true;
            }
        } else if (gimmickNumber==4) {
            tutorialT.text = "敵からオレンジ色が見えたら、カウンターできるのでやってみよう";
            if (Stagestart == true)
            {
                TimeSwicth = false;
                SaveObject = Instantiate(Enemy2);
                SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
                changebool = true;
                Stagestart = false;
            }

            if (SaveObject == null&& gimmickNumber == 4)
            {
                TimeSwicth = true;
                //Stagestart = true;
            }
        }else if (gimmickNumber == 5)
        {
            tutorialT.text = "チュートリアル終了";
        }
    }
}
