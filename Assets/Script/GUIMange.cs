using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMange : MonoBehaviour {

    float time, startTime;

    Text  timer;

    bool isEnd = false;

    GameObject gameOverUI;

	void Start () {

        gameOverUI = transform.Find("GameOver_bg").gameObject;

        timer = GameObject.Find("Timer").GetComponent<Text>();

        //得到游戏开始的时间(单位:秒)
        startTime = Time.time;

	}
	
	void Update () {

        if (isEnd) return;

        time = Time.time - startTime;

        int seconds = (int)(time % 60);
        int minutes = (int)(time/60);

        timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        
	}
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        isEnd = true;
    }
}
