using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacController : MonoBehaviour
{
    public GameObject playerG=null;
    public bool rightattackSwicth;//弱攻撃の場合
    public bool heavyattackSwicth;//強攻撃の場合

    private CircleCollider2D CC2D;//自身のCircleCollider2Dを保存
    private Vector3 savePpos;
    private float speed;
    public  bool StartSwicth;
    // Start is called before the first frame update
    void Start()
    {
        CC2D = GetComponent<CircleCollider2D>();
        rightattackSwicth = false;
        heavyattackSwicth = false;
        StartSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightattackSwicth==true)//弱攻撃
        {
            if (StartSwicth == true)
            {
                CC2D.radius = 0.5f;
                heavyattackSwicth = false;
                savePpos.x = 0;
                speed = 5;
                Debug.Log("rightattackSwicth" + rightattackSwicth);
                transform.position = playerG.transform.position;
                StartSwicth = false;
            }

            if (savePpos.x >= 3.04f)
            {
                transform.position = playerG.transform.position;
                CC2D.radius = 0.2f;
                StartSwicth = true;
                rightattackSwicth = false;
            }
            else if (savePpos.x < 3.04f)
            {         
                savePpos.x += speed * Time.deltaTime;
                transform.localPosition = savePpos;
            }
        }
        else if (heavyattackSwicth==true)//強攻撃
        {
            if (StartSwicth == true)
            {
                gameObject.tag = "playerHeavyattack";//tagを変更
                CC2D.radius = 0.5f;
                rightattackSwicth = false;
                savePpos.x = 0;
                speed = 5;
                Debug.Log("heavyattackSwicth" + heavyattackSwicth); 
                transform.localPosition = playerG.transform.position;
                StartSwicth = false;
            }

            if (savePpos.x >= 3.84f)
            {
                gameObject.tag = "playerRightattack";//tagを元に戻す
                transform.position = playerG.transform.position;
                CC2D.radius = 0.2f;
                StartSwicth = true;
                heavyattackSwicth = false;
            }
            else if (savePpos.x < 3.84f)
            {
                savePpos.x += speed * Time.deltaTime;
                transform.localPosition = savePpos;
            }
        }
    }
}
