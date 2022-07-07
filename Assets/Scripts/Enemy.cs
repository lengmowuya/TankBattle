using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float v;
    private float h;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // 上 右 下 左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // 计时器
    private float timeVal;  // 攻击时间间隔
    private float changeDirTimeVal;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }
    void Start(){
    }

    void Update(){
        if(timeVal>=3f){
            timeVal = 0;
            Attack();
        }else{
            timeVal += Time.deltaTime;
        }
    }
    void FixedUpdate(){
        Move();
    }
    // 坦克移动和转向
    private void Move(){
        if(changeDirTimeVal >= 2){
            int num = Random.Range(0,8);
            if(num>5){
                // 朝下
                v = -1;
                h = 0;
            }else if(num == 0){
                // 朝上
                v = 1;
                h = 0;
            }else if(num>0&&num<=2){
                // 朝左
                h = -1;
                v = 0;
            }else if(num>2&&num<=4){
                // 朝右
                h = 1;
                v = 0;
            }
            changeDirTimeVal = 0;
        }else{
            changeDirTimeVal += Time.deltaTime;
        }

        // 控制玩家移动
        // h = Input.GetAxisRaw("Horizontal");
        // 方向 * 正负 * 速度 * 每帧
        transform.Translate(Vector3.right*h*moveSpeed*Time.fixedDeltaTime,Space.World);
        if(h<0){
            //朝左
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0,0,90);
        }else if(h>0){
            //朝右
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0,0,-90);
        }

        // 在左右移动时不能同时上下移动
        if(h!=0) return;

        // v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime,Space.World);
        if(v>0){
            //朝上
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0,0,0);
        }else if(v<0){
            //朝下
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0,0,180);
        }
    }
    // 坦克攻击
    private void Attack(){
        Instantiate(bulletPrefab,transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
    }
    private void Die(){
        // 产生爆炸特效
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        // 死亡
        Destroy(gameObject);
        PlayerManager.Instance.playerScore ++;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            changeDirTimeVal = 2;
        }
    }

}
