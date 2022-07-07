using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOption : MonoBehaviour
{
    private int choice = 1;
    public Transform  posOne;
    public Transform  posTwo;

    void Update(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            choice = 1;
            transform.position = posOne.position;
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            choice = 2;
            transform.position = posTwo.position;
        }
        if(choice ==1&&Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(1);
        }
    }
}
