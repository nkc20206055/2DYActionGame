using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;   //移動スピード

    Vector3 SaveVec;   //移動などを保存する
    Rigidbody2D Rd2D;　//Rigidbody2Dを保存する
    float InputVec;    //横移動時の向きの値を入れる
    // Start is called before the first frame update
    void Start()
    {
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        InputVec = Input.GetAxisRaw("Horizontal");
        if (InputVec!=0)//プレイヤーの向き
        {
            Vector3 SavelocalScale = transform.localScale;//現在の向きを保存
            transform.localScale = new Vector3(/*SavelocalScale.x **/ InputVec, SavelocalScale.y, SavelocalScale.z);
        }
    }
    void FixedUpdate()
    {
        SaveVec.x = MoveSpeed * InputVec*Time.deltaTime;
        transform.position += SaveVec;
    }
}
