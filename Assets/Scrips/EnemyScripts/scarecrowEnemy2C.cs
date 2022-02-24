using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrowEnemy2C : MonoBehaviour
{
    public GameObject bullet;//発射する弾を入れる
    public int MaxHP;//最大HP
    public float Maxattacktime;//
    public bool GanEnemyOn;//射撃を行うEnemyかどうかをきめる
    Vector3 bulletPos;
    private SpriteRenderer sr;//自身のspriteを保存
    private Animator anim = null;
    private int HP;//この敵のHP
    private float time;//溜まったら動きための数値
    private bool attackSwicth;
    void NormalAnimaton()
    {
        attackSwicth = true;
        time = 0;
        anim.SetBool("damage",false);
    }
    void Damage()
    {
        anim.SetBool("damage",true);
        HP--;
        Debug.Log(HP);
    }
    void Destroy()
    {
        if (HP<=0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        attackSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.isVisible)//sr(SpriteRenderer)が画面に入っているとき動く
        {
            if (attackSwicth == true && GanEnemyOn == true) {
                time += 1 * Time.deltaTime;
                //Debug.Log(time);
                if (time >= Maxattacktime)//射撃
                {
                    //Debug.Log("射撃");
                    var t = Instantiate(bullet);
                    Vector3 ss = transform.position;
                    bulletPos = new Vector3((float)(ss.x + -0.79), (float)(ss.y + 0.64), ss.z);
                    t.transform.position = bulletPos;
                    time = 0;
                }
            }
            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    Damage();
            //}
            Destroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "playerAttackCollider")
        {
            attackSwicth = false;
            Damage();
        }
    }
}
