using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;  // 攻击时间间隔
    private float defendTimeVal = 5;
    private bool isDefend = true;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // 上 右 下 左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }
    void Start(){
    }

    void Update(){
        if(timeVal>=0.4f){
            if(PlayerManager.Instance.isDefeat) return;
            Attack();
        }else{
            timeVal += Time.deltaTime;
        }

        if(isDefend){
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal <= 0){
                defendEffectPrefab.SetActive(false);
                isDefend = false;
            }
        }
    }
    void FixedUpdate(){
        // 游戏失败后不能操作
        if(PlayerManager.Instance.isDefeat) return;

        Move();
    }
    // 坦克移动和转向
    private void Move(){
        // 控制玩家移动
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 播放行进或待机音效
        if((Mathf.Abs(v)+Mathf.Abs(h))>0.05f){
            moveAudio.clip = tankAudio[1];
            if(!moveAudio.isPlaying) moveAudio.Play();
        }else{
            moveAudio.clip = tankAudio[0];
            if(!moveAudio.isPlaying) moveAudio.Play();
        }

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
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(bulletPrefab,transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
    }

    private void Die(){
        // 无敌
        if(isDefend) return;

        // 产生爆炸特效
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        // 死亡
        Destroy(gameObject);

        PlayerManager.Instance.isDead = true;
    }


}
