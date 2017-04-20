using UnityEngine;
using System.Collections;

public class MoveMgr : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDamping = 10f;

    /// <summary>
    /// 速度
    /// </summary>
    private Vector3 velocity;
    private Vector3 speedDir = Vector3.zero;

    private Charactor chara;

    void Awake()
    {
        chara = this.transform.GetComponent<Charactor>();
    }

    void Update()
    {
        speedDir = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            speedDir.x += 1;
            if (transform.localScale.x < 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            speedDir.x -= 1;
            if (transform.localScale.x > 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            speedDir.y += 1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            speedDir.y -= 1;
        }
        
        velocity = Vector3.Lerp(velocity, speedDir * moveSpeed, Time.deltaTime * moveDamping);
        chara.Move(velocity * Time.deltaTime);
        velocity = chara.velocity;
    }
}