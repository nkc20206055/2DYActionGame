using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;   //移動スピード

    Vector3 SaveVec;   //移動などを保存する
    Rigidbody2D Rd2D;　//Rigidbody2Dを保存する
    float InputVec;    //横移動時の向きの値を入れる
    //ジャンプ変数1
    enum Status
    {
        GROUND = 1,
        UP = 2,
        DOWN = 3
    }

    Status playerStatus = Status.GROUND;   //プレイヤーの状態
    float firstSpeed = 16f;   //初速
    const float gravity = 120.0f;    //重力
    const float jumpLowerLimit = 0.03f;

    float timer = 0f;  //経過時間
    bool jumpKey = false;    //ジャンプキー
    bool keyLook = false;


    //移動制限処理用変数
    private Vector2 playerPos;
    private readonly float playerPosXClamp = 15.0f;
    private readonly float playerPosYClamp = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動制限
        this.MovingRestrictions();


        //移動
        InputVec = Input.GetAxisRaw("Horizontal");
        if (InputVec != 0)//プレイヤーの向き
        {
            Vector3 SavelocalScale = transform.localScale;//現在の向きを保存
            transform.localScale = new Vector3(/*SavelocalScale.x **/ InputVec, SavelocalScale.y, SavelocalScale.z);
        }
        //ジャンプ
        if(Input.GetKey(KeyCode.Space))
        {
            if(!keyLook)
            {
                jumpKey = true;
            }
            else
            {
                jumpKey = false;
            }
        }
        else
        {
            jumpKey = false;
            keyLook = false;
        }
        

        
    }
    void FixedUpdate()
    {
        SaveVec.x = MoveSpeed * InputVec * Time.deltaTime;
        transform.position += SaveVec;

        Vector2 newvec=Vector2.zero;
        switch (playerStatus)
        {
            // 接地時
            case Status.GROUND:
                if (jumpKey)
                {
                    playerStatus = Status.UP;
                }
                break;

            // 上昇時
            case Status.UP:
                timer += Time.deltaTime;

                if (jumpKey || jumpLowerLimit>timer)
                {
                    newvec.y = firstSpeed;
                    newvec.y -= (gravity * Mathf.Pow(timer,2));
                }
                else
                {
                    timer += Time.deltaTime; // 落下を早める
                    newvec.y = firstSpeed;
                    newvec.y -= (gravity * Mathf.Pow(timer, 2));
                }
                if (0f > newvec.y)
                {
                    playerStatus = Status.DOWN;
                    newvec.y = 0f;
                    timer = 0.1f;
                }
                break;

            // 落下時
            case Status.DOWN:
                timer += Time.deltaTime;

                newvec.y = 0f;
                newvec.y = -(gravity * Mathf.Pow(timer, 2));
                break;

            default:
                break;
        }

        Rd2D.velocity = newvec;
    }
    //カメラ位置制限
    private void MovingRestrictions()
    {
        //変数に自分の今の位置を入れる
        this.playerPos = transform.position;

        //playerPos変数のxとyに制限した値を入れる
        //playerPos.xという値を-playerPosXClamp～playerPosXClampの間に収める
        this.playerPos.x = Mathf.Clamp(this.playerPos.x, -this.playerPosXClamp, this.playerPosXClamp);
        this.playerPos.y = Mathf.Clamp(this.playerPos.y, -this.playerPosYClamp, this.playerPosYClamp);

        transform.position = new Vector2(this.playerPos.x, this.playerPos.y);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (playerStatus == Status.DOWN &&
            collision.gameObject.name.Contains("Ground"))
        {
            playerStatus = Status.GROUND;
            timer = 0f;
            keyLook = true; // キー操作をロックする
        }
    }
    //攻撃判定確かめ…後日変更
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit");
    }
}
