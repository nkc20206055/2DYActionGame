using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrowEnemy2C : MonoBehaviour
{
    public GameObject bullet;//発射する弾を入れる
    public int MaxHP;//最大HP
    public float Maxattacktime;//
    Vector3 bulletPos;
    private SpriteRenderer sr;//自身のspriteを保存
    private Animator anim = null;
    private int HP;//この敵のHP
    private float time;//溜まったら動きための数値
    void Damage()
    {
        anim.Play("damage");
        HP--;
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.isVisible)//sr(SpriteRenderer)が画面に入っているとき動く
        {
            time += 1 * Time.deltaTime;
            Debug.Log(time);
            if (time >= Maxattacktime)
            {
                Debug.Log("射撃");
                var t=Instantiate(bullet);
                Vector3 ss = transform.position;
                bulletPos = new Vector3((float)(ss.x+ -0.79),(float)(ss.y+0.64),ss.z);
                t.transform.position = bulletPos;
                time = 0;
            }
        }
    }
}
