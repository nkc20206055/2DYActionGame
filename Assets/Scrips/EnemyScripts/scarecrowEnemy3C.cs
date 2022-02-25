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
    EcColliderController ECC;

    private Animator anim;//Animatorを保存
    private int HP;//このエネミーのHP
    private float attacktime;//攻撃するまでを計る
    private bool AttackSwicth;//攻撃中かどうか
    public  bool counterSwicth;//カウンターできるかどうか

    void normalanimation()//アニメーションを元に戻す場合
    {
        ECC.counterHetSwicth = false;
        counterSwicth = false;
        AttackCorider.tag = "enemyrightattack";//指定したGameObjectのtagを変更
        AttackSwicth = false;
        attacktime = 0;
        anim.SetBool("rightattack", false);
        anim.SetBool("heavyattack", false);
        anim.SetBool("counterhet", false);
        anim.SetBool("damage", false);
        //anim.Play("normal");
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
    void Damage(int HetHp)//攻撃をくらったときかカウンターされたとき
    {
        //if (counterSwicth == false) {
        //    anim.SetBool("rightattack", false);
        //    anim.SetBool("heavyattack", false);
        //    anim.SetBool("damage",true);
        //    HP--;
        //}
        anim.SetBool("counterhet", false);
        anim.SetBool("rightattack", false);
        anim.SetBool("heavyattack", false);
        anim.SetBool("damage", true);
        HP=HP-HetHp;
    }
    void CounterHet()
    {
        if (counterSwicth == true)
        {
            anim.SetBool("heavyattack", false);
            anim.SetBool("counterhet", true);
            counterSwicth = false;
        }
    }
    void rightattack()//弱攻撃
    {
        attacktime += 1 * Time.deltaTime;
        //Debug.Log(attacktime);
        if (AttackSwicth == false&&attacktime>= MRaxattacktime) {

            anim.SetBool("rightattack", true);
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
            anim.SetBool("heavyattack", true);
            //CounterHet();
            AttackSwicth = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        AttackCorider = transform.GetChild(0).gameObject;//子オブジェクトを取得
        ECC = transform.GetChild(1).gameObject.GetComponent<EcColliderController>();
        //Debug.Log(ECC);
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


        if (HP<=0)//死亡
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerCounterattack")
        {
            //Debug.Log("ヒット");
            CounterHet();
        }
        if (collision.gameObject.tag =="playerRightattack")//プレイヤーの弱攻撃
        {
            Damage(1);
        }
        
        if (collision.gameObject.tag == "playerHeavyattack")//プレイヤーの強攻撃
        {
            Damage(2);
        }
    }
}
