using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {

    public GameObject[] Group;//所有形状的数组

	void Start () {
        SpawnNext();  
	}
    public void SpawnNext() //随机创建出形状的方法
    {
        int i = Random.Range(0,Group .Length); //随机数
        GameObject ins= Instantiate(Group[i],transform.position ,Quaternion.identity)as GameObject;  //创建
    }
}
