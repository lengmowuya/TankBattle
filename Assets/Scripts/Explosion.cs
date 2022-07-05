using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float animTimeVal = 0.16f;


    void Update()
    {
        if(animTimeVal<=0){
            Destroy(gameObject);
        }else{
            animTimeVal -= Time.deltaTime;
        }
    }
}
