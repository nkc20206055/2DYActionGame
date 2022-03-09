using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Maxtime;
    GameObject Player;//プレイヤーを取得
    Camera MiCamera;//自身のCameraを取得
    Vector3 SaveMiPos,SavePos;
    float Sizutime=0;
    bool SizuSwicth;
    void CameraSizu()//カメラをプレイヤーに近づける
    {
        if (Maxtime>Sizutime)
        {
            Sizutime += 1 * Time.deltaTime;
            MiCamera.orthographicSize = 5;
        }
        else if (Maxtime <= Sizutime)
        {
            gameObject.transform.position = SaveMiPos;
            MiCamera.orthographicSize = 10.96355f;
            SizuSwicth = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveMiPos = gameObject.transform.position;
        Player = GameObject.Find("prototypePlayer");
        MiCamera = GetComponent<Camera>();
        SizuSwicth = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//お試し
        {
            Sizutime = 0;
            SavePos = Player.transform.position;
            gameObject.transform.position = new Vector3(SavePos.x,SavePos.y,-10);
            SizuSwicth = true;
        }

        if (SizuSwicth==true)
        {
            CameraSizu();
        }
    }
}
