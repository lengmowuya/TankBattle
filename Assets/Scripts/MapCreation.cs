using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour{
    // 老家 墙 障碍 出生效果 河流 草
    public GameObject[] item;
    
    void Awake(){
        // 生成老家
        CreateItem(item[0],new Vector3(0,-8,0),Quaternion.identity);
        // 生成老家围墙
        CreateItem(item[1],new Vector3(-1,-8,0),Quaternion.identity);
        CreateItem(item[1],new Vector3(1,-8,0),Quaternion.identity);
        for(int i = -1; i<2;i++){
            CreateItem(item[1],new Vector3(i,-7,0),Quaternion.identity);
        }
    }
    private void CreateItem(GameObject createItem,Vector3 createPosition,Quaternion createQuaternion){
        GameObject item =  Instantiate(createItem,createPosition,createQuaternion);
        item.transform.SetParent(gameObject.transform);
    }
}