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

    private GameObject SaveObject;
    private Text tutorialT;//チュートリアル用textを入れる変数
    private int gimmickNumber;//
    private bool Stagestart;//ステージが始まったかどうか
    private bool changebool;//Enemy2のスクリプトにあるboolを変えるようにする変数

    guardController gC;
    // Start is called before the first frame update
    void Start()
    {
        gimmickNumber = 0;
        gC = GameObject.Find("prototypePlayer").GetComponent<guardController>();
        tutorialT = GameObject.Find("tutorialText").GetComponent<Text>();
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

    }
    private void changeStage(Stage _stage)//ステージを切り替えたいときに使用
    {
        nextstart = _stage;
    }
    private void STAGE1()//ステージ1
    {
        if (gimmickNumber==0)
        {
            tutorialT.text = "操作説明";
        }
        if (Stagestart==true)
        {
            SaveObject = Instantiate(Enemy);
            SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
            Stagestart = false;
        }

        //Debug.Log("動いている");
        if (SaveObject==null&&gimmickNumber==10)
        {
            Debug.Log(SaveObject);
            Stagestart = true;
            changeStage(Stage.Stage2);
        }
    }
    private void STAGE2()//ステージ2
    {
        if (Stagestart == true)
        {
            SaveObject = Instantiate(Enemy1);
            SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
            Stagestart = false;
        }

        if (SaveObject == null)
        {
            Debug.Log(SaveObject);
            Stagestart = true;
            changeStage(Stage.Stage3);
        }
    }
    private void STAGE3()//ステージ3
    {
        if (Stagestart == true)
        {
            SaveObject = Instantiate(Enemy2);
            SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
            changebool = true;
            Stagestart = false;
        }

        if (SaveObject == null)
        {
            Debug.Log(SaveObject);
            Stagestart = true;
            changeStage(Stage.Stage4);
        }
    }
    private void STAGE4()//ステージ4
    {
        if (Stagestart == true)
        {
            SaveObject = Instantiate(Enemy2);
            SaveObject.transform.position = new Vector3(7.366421f, 19.35053f, 0);
            changebool = true;
            Stagestart = false;
        }

        if (SaveObject == null)
        {
            Debug.Log(SaveObject);
            Debug.Log("チュートリアル終了");
        }
    }
}
