using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;   //移動スピード

    Vector3 SaveVec;   //移動などを保存する
    Rigidbody2D Rd2D;　//Rigidbody2Dを保存する
    private Animator anim = null;
    float InputVec;    //横移動時の向きの値を入れる
    


    //移動制限処理用変数
    private Vector2 playerPos;
    private readonly float playerPosXClamp = 15.0f;
    private readonly float playerPosYClamp = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if(InputVec>0)
        {
           anim.SetBool("run", true);
        }
        else if(InputVec<0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

       
        
        
    }
   

    void FixedUpdate()
    {
        SaveVec.x = MoveSpeed * InputVec * Time.deltaTime;
        transform.position += SaveVec;

        
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
    

}
