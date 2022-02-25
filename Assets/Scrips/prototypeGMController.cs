using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゲームマネージャー
public class prototypeGMController : MonoBehaviour
{
    guardController gC;
    // Start is called before the first frame update
    void Start()
    {
        gC = GameObject.Find("prototypePlayer").GetComponent<guardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gC.hp<=0)//死んだのでシーンをやり直す
        {

        }
    }
}
