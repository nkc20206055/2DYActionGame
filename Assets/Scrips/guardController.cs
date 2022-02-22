using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guardController : MonoBehaviour
{
    public float counterTime;//カウンターできる時間

    private Animator anim;
    private Slider slider;//ガードゲージのUI
    private float CGtime;//
    void animationcancel()
    {
        anim.SetBool("counterattack", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        slider = GameObject.Find("guardgage").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{

        //}
        //else
        if (Input.GetMouseButton(1))//右クリックで起動
        {
            CGtime += 1 * Time.deltaTime;
            if (CGtime< counterTime) //カウンター確認
            {
                //Debug.Log("カウンター中"+CGtime);
                anim.SetBool("counter",true);
                
            } else if (CGtime >= counterTime) //ガード確認
            {
                //Debug.Log("ガード中");
                anim.SetBool("guard", true);
                anim.SetBool("counter", false);
            }
        }else if (Input.GetMouseButtonUp(1))//右マウスボタンを上げた時
        {
            if (CGtime <= 0.1f)
            {
                Debug.Log("カウンターキャンセル");
                anim.SetBool("counter", false);
            }
            else if (CGtime > 0.1f&&CGtime < counterTime)
            {
                Debug.Log("カウンター");
                anim.SetBool("counterattack", true);
                anim.SetBool("counter", false);

            }
            else if (CGtime >= counterTime)
            {
                Debug.Log("ガード終了");
                anim.SetBool("guard", false);
            }
            CGtime = 0;
        }
    }
}
