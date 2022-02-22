using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float Speed;//弾のスピード
    public float objectPeel;
    Vector3 nn;
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SR.isVisible) {
            nn.x = objectPeel * Speed * Time.deltaTime;
            transform.position += nn;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
