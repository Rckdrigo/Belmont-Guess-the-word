using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Timer : Singleton<Timer> {

	public delegate void TimeEvents();
	public TimeEvents TimeOver;

	bool timerActive;
	float timeLimit;
	float actualTime;
	float initialTime;
	float time;
	
	
	void Start(){
		GameStateBehaviour.Instance.Reset += Reset;
		GameStateBehaviour.Instance.GameOver += GameOver;
	}
	
	void Reset(){
		timerActive = true;
	}
	
	void GameOver(){
		timerActive = false;
	}
	
	public void SetTimer(float maxTime){
		initialTime = Time.time;
		time = maxTime;
		
		timeLimit = time + initialTime;
		actualTime = 0;
		GetComponent<Text>().color = Color.yellow;
		timerActive = true;
	}
	
	void Update () {
		if(timerActive){
			if(actualTime <= time){
				actualTime = Time.time - initialTime;
				//print ("A: "+actualTime + " T: " + time);
			}
			else{
				actualTime = time;
				timerActive = false;
				TimeOver();
			}
			if(actualTime > time - 10){
				GetComponent<Text>().color = Color.red;
				if(!GetComponent<AudioSource>().isPlaying)
					GetComponent<AudioSource>().Play();
			}
				
			TimeSpan t = TimeSpan.FromSeconds(time - actualTime);
			GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
		}
	}
}
