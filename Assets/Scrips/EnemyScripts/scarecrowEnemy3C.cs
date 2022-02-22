using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrowEnemy3C : MonoBehaviour
{
    public int MaxHP;//最大HP
    public float MRaxattacktime, MHaxattacktime;//
    public bool rightattackSwicth;//Inspector側からONにするすると弱攻撃しかしなくなる
    public bool heavyattackSwicth;//Inspector側からONにするすると強攻撃しかしなくなる

    GameObject AttackCorider;

    private Animator anim;//Animatorを保存
    private int HP;//このエネミーのHP
    private float attacktime;//攻撃するまでを計る
    private bool AttackSwicth;//攻撃中かどうか
    public  bool counterSwicth;//カウンターできるかどうか

    void normalanimation()//アニメーションを元に戻す場合
    {
        AttackCorider.tag = "enemyrightattack";//指定したGameObjectのtagを変更
        AttackSwicth = false;
        attacktime = 0;
        anim.Play("normal");
    }
    void CounterSwicthONOFF()//アニメーション中でcounterSwicthを起動できるようにする
    {
        if (counterSwicth == false)
        {
            //Debug.Log("呼び出された1");
            counterSwicth = true;
        } else if (counterSwicth == true)
        {
            //Debug.Log("呼び出された2");
            counterSwicth = false;
        }
    }
    void Damage()//攻撃をくらったときかカウンターされたとき
    {
        if (counterSwicth == true)
        {
            anim.Play("counter");
            counterSwicth = false;
        } else if (counterSwicth == false) {
            anim.Play("damage");
            HP--;
        }
    }
    void rightattack()//弱攻撃
    {
        attacktime += 1 * Time.deltaTime;
        //Debug.Log(attacktime);
        if (AttackSwicth == false&&attacktime>= MRaxattacktime) {

            anim.Play("rightattack");
            AttackSwicth = true;
        }
    }
    void heavyattack()//強攻撃
    {
        attacktime += 1 * Time.deltaTime;
        //Debug.Log(attacktime);
        if (AttackSwicth == false && attacktime >= MRaxattacktime)
        {
            AttackCorider.tag = "enemyheavyattack";//指定したGameObjectのtagを変更
            anim.Play("heavyattack");
            AttackSwicth = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        AttackCorider = transform.GetChild(0).gameObject;//子オブジェクトを取得
        //Debug.Log(AttackCorider);
        anim = GetComponent<Animator>();
        AttackSwicth = false;
        counterSwicth = false;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (state) {
        //    case STATE:
        //        break;
        //}

        if (rightattackSwicth==true&& heavyattackSwicth == false)//弱攻撃のみ
        {
            rightattack();
        }
        else if (rightattackSwicth == false && heavyattackSwicth == true)//強攻撃のみ
        {
            heavyattack();
        }

        if (Input.GetKeyDown(KeyCode.V))//ダメージのアニメーションが動くかどうかの確認
        {
            Damage();
        }

        if (HP<=0)//死亡
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counterSwicth==true) {
            if (collision.gameObject.tag == "playerCounterattack")
            {
                Damage();
            }
        }
    }
}
