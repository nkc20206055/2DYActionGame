using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゲームマネージャー
public class prototypeGMController : MonoBehaviour
{
    private enum Stage {Stage1,Stage2,Stage3,Stage4}
    private Stage start=Stage.Stage1;//最初に始まるイーナム
    private Stage nextstart = Stage.Stage2;//次に始まるイーナムを入れる変数

    public GameObject Enemy, Enemy1, Enemy2;//三種の敵キャラを入れる変数

    guardController gC;
    // Start is called before the first frame update
    void Start()
    {
        gC = GameObject.Find("prototypePlayer").GetComponent<guardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gC.hp<=0)//死んだのでシーンをやり直す
        {
            Debug.Log("プレイヤー死亡");
            SceneManager.LoadScene("prototypeActionScene");
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

    }
    private void STAGE2()//ステージ2
    {

    }
    private void STAGE3()//ステージ3
    {

    }
    private void STAGE4()//ステージ4
    {

    }
}
