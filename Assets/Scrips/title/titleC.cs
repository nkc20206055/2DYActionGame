using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleC : MonoBehaviour
{
    public void GameStart()//ゲームを始める（ボタンで使う）
    {
        Debug.Log("まだ作成中");
    }
    public void TutorialStart()//チュートリアルを始める（ボタンで使う）
    {
        Debug.Log("チュートリアルスタート");
        SceneManager.LoadScene("prototypeActionScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
