using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour{
    // 老家 墙 障碍 出生效果 河流 草
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();
    // 敌人位置
    Vector3[] EnemyPos = {new Vector3(-10,8,0),new Vector3(0,8,0),new Vector3(10,8,0)};
    void Awake(){
        InitMap();
        InitUnit();
        InvokeRepeating("CreateEnemy",12,6);
    }

    private void CreateItem(GameObject createItem,Vector3 createPosition,Quaternion createQuaternion){
        GameObject item =  Instantiate(createItem,createPosition,createQuaternion);
        item.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    // 产生随机位置的方法
    private Vector3 CreateRandomPosition(){
        // 不生成x=-10,10的两列，y=-8,8两行的位置
        int i = 0;
        while(true){
            Vector3 createPosition = new Vector3(Random.Range(-9,10),Random.Range(-7,8),0);
            i++;
            if(!HasThePosition(createPosition) || i >= 1000){
                return createPosition;
            }
        }
    }

    // 判断位置是否为空
    private bool HasThePosition(Vector3 createPos){
        for(int i = 0; i < itemPositionList.Count;i++){
            if(createPos == itemPositionList[i]){
                return true;
            }
        }
        return true;
    }
    private void CreateEnemy(){
        int num = Random.Range(0,3);
        CreateItem(item[3], EnemyPos[num],Quaternion.identity);
    }

    private void InitMap(){
        // 生成老家
        CreateItem(item[0],new Vector3(0,-8,0),Quaternion.identity);
        // 生成老家围墙
        CreateItem(item[1],new Vector3(-1,-8,0),Quaternion.identity);
        CreateItem(item[1],new Vector3(1,-8,0),Quaternion.identity);
        for(int i = -1; i<2;i++){
            CreateItem(item[1],new Vector3(i,-7,0),Quaternion.identity);
        }
        // 实例化外围墙
        for(int i = -11;i < 12;i++){
            CreateItem(item[2],new Vector3(i,9,0),Quaternion.identity);
        }
        for(int i = -11;i < 12;i++){
            CreateItem(item[2],new Vector3(i,-9,0),Quaternion.identity);
        }
        for(int i = -8;i < 9;i++){
            CreateItem(item[2],new Vector3(-11,i,0),Quaternion.identity);
        }
        for(int i = -8;i < 9;i++){
            CreateItem(item[2],new Vector3(11,i,0),Quaternion.identity);
        }

        // 实例化地图
        for(int i = 0;i<80;i++){
            CreateItem(item[1],CreateRandomPosition(),Quaternion.identity);
        }
        for(int i = 0;i<20;i++){
            CreateItem(item[2],CreateRandomPosition(),Quaternion.identity);
        }
        for(int i = 0;i<20;i++){
            CreateItem(item[4],CreateRandomPosition(),Quaternion.identity);
        }
        for(int i = 0;i<20;i++){
            CreateItem(item[5],CreateRandomPosition(),Quaternion.identity);
        }
    }
    private void InitUnit(){
        //  初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2,-8,0),Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //  初始化敌人
        CreateItem(item[3], EnemyPos[0],Quaternion.identity);
        CreateItem(item[3], EnemyPos[1],Quaternion.identity);
        CreateItem(item[3], EnemyPos[2],Quaternion.identity);
    }
}
