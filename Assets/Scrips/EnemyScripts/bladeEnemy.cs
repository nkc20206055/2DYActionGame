using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeEnemy : MonoBehaviour
{
    //ステートマシンでのAIを作成
    private enum STATE {normal,move,attack,counterMe, damage }
    private STATE state = STATE.normal;//enumのnormalを入れる
    private STATE saveState=STATE.move;//enumを変えるとき変化するほうを保存する変数
    //private STATE _state;
    //private STATE _saveState;

    public int Maxhp;//最大体力
    public float Speet;//移動スピード
    public float NPos;//敵とプレイヤーの距離の差を入れる変数
    GameObject playerO;//プレイヤーのオブジェクトを保存
    Vector3 savePos,savePlayerPos;
    private Animator anim;
    private int hp;//現在の体力を保存
    private bool attackswicth;//攻撃をしているかどうか
    private bool counterswicth;//カウンターできるかどうか
    private bool destroyswicth;//死亡しているかどうか


    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp;
        attackswicth = false;
        counterswicth = false;
        destroyswicth = false;
        playerO = GameObject.Find("prototypePlayer");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyswicth == false) {
            //現在のステートを呼び出す
            switch (state)
            {
                case STATE.move://歩く
                    Move();
                    break;
                case STATE.attack://攻撃する
                    Attack();
                    break;
                case STATE.counterMe://カウンターを食らったとき
                    counterHet();
                    break;
                case STATE.damage://ダメージ
                    Damege();
                    break;
            }

            //ステートが変わったとき
            if (state != saveState)
            {
                //次のステートに切り替わる
                state = saveState;
                //switch (state)
                //{
                //    case STATE.move://歩く
                //        break;
                //    case STATE.attack://攻撃する
                //        break;
                //}

            }

            //if (Input.GetKeyDown(KeyCode.V))//HPが減るかどうかの確認
            //{
            //    //counterMeandDamege();
            //    changeState(STATE.damage);
            //}
            
        }
        if (hp <= 0)//死亡
        {
            destroy();
        }
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        saveState = _state;
    }
    void counterSwicth()
    {
        if (counterswicth == false)
        {
            counterswicth = true;
        }
        else if(counterswicth==true)
        {
            counterswicth = false;
        }
    }
    private void Normal()//通常状態
    {
        //anim.Play("normal");
        anim.SetBool("run", false);
        anim.SetBool("attack", false);
        anim.SetBool("counterhet", false);
        anim.SetBool("damage", false);
        attackswicth = false;
        Debug.Log("戻る");
        changeState(STATE.move);
    }
    private void Move()//歩き（※ステートで使用）
    {
        //anim.Play("Move");
        anim.SetBool("run", true);
        playerO = GameObject.Find("prototypePlayer");
        savePlayerPos = playerO.transform.position;
        savePlayerPos =  transform.position- savePlayerPos;
        //Debug.Log(savePlayerPos.x);
        if (savePlayerPos.x<= NPos&& savePlayerPos.x >= -NPos)
        {
            attackswicth = true;
            anim.SetBool("run", false);
            changeState(STATE.attack);
        }
        else
        {
            //attackswicth = true;
            //anim.SetBool("run", false);
            //changeState(STATE.attack);
        }
        float s = transform.position.x- playerO.transform.position.x;
        //Debug.Log(s);
        float g=0;
        if (s>=0)//向きを右に変える
        {
            g = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(g*-1, r.y, r.z);
        }else if (s<0)//向きを左に変える
        {
            g = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(g*-1, r.y, r.z);
        }
        savePos.x = g*Speet * Time.deltaTime;
        transform.position += savePos;
    }
    private void Attack()//攻撃（※ステートで使用）
    {
        //attackswicth = true;
        anim.SetBool("attack", true);
        if (attackswicth==false)
        {
            anim.SetBool("attack", false);
            changeState(STATE.move);
        }
    }
    private void counterHet()//カウンターを当られたら
    {
        anim.SetBool("run", false);
        anim.SetBool("attack", false);
        anim.SetBool("counterhet", true);
        counterswicth = false;
    }
    private void Damege()//ダメージ
    {
        //if (counterswicth==true) {
        //    anim.Play("counter");
        //    counterswicth = false;
        //}
        //else if (counterswicth==false)
        //{
        //    anim.Play("damage");
        //    hp--;
        //}
        anim.SetBool("run", false); 
        anim.SetBool("attack", false); 
        anim.SetBool("counterhet", false);
        anim.SetBool("damage", true);
        hp--;
        Debug.Log(hp);
        //changeState(STATE.normal);
    }
    private void destroy()//死亡時
    {
        destroyswicth = true;
        anim.Play("death");
    }
    private void gameODestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counterswicth==true) {
            if (collision.gameObject.tag == "playerCounterattack")
            {
                //Debug.Log("ヒット");
                changeState(STATE.counterMe);
            }
        }
        if (collision.gameObject.name == "playerAttackCollider")//お試し　プレイヤーの攻撃がヒットしたら
        {
            changeState(STATE.damage);
        }
    }
}
