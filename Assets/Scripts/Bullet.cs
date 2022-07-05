using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    private bool isPlayBullet = true;
    void Start()
    {
    } 

    void Update()
    {
        transform.Translate(transform.up*moveSpeed*Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        switch(collision.tag){
            case "Tank":
                // 只有敌人的子弹能打死玩家
                if(!isPlayBullet){
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Home":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
