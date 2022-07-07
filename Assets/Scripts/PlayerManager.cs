using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    // 属性
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    // 引用
    public GameObject bornEffect;
    public TMP_Text playerScoreText;
    public TMP_Text playerLifeText;
    public GameObject GameOverUI;

    //单例
    private static PlayerManager instance;
    public static PlayerManager Instance{
        get {
            return instance;
        }
        set{
            instance = value;
        }
    }

    private void Awake(){
        Instance = this;
    }

    private void Update(){
        if(isDead){
            Recover();
        }
        if(isDefeat){
            GameOverUI.SetActive(true);
            Invoke("ReturnToMenu",3);
            return;
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeText.text = lifeValue.ToString();
    }

    private void Recover(){
        if(lifeValue<=0){
            // 游戏失败
            isDefeat = true;
        }else{
            lifeValue--;
            GameObject go = Instantiate(bornEffect,new Vector3(-2,-8,0),Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

}
