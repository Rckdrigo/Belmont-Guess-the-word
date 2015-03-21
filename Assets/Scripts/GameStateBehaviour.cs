using UnityEngine;
using System.Collections;

public class GameStateBehaviour : Singleton<GameStateBehaviour> {

	[System.Serializable()]
	public struct PhrasePresets{
		public string phrase;
		public float time;
		public int columnNumber;
		public int hintLetters;
	}

	public delegate void GameEvents();
	public GameEvents GameOver;
	public GameEvents Reset;

	public PhrasePresets[] presets;
	public Animator anim;

	public UnityEngine.UI.Image message;
	
	public Sprite win;
	public AudioClip winClip;
	
	public Sprite lose;
	public AudioClip loseClip;
	
	[HideInInspector()]
	public int maxPoints;
	
	int actualPoints;
	int phraseIndex;

	void Start(){
		phraseIndex= 0;
#if UNITY_IPHONE
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
		Timer.Instance.TimeOver += Lose;
	}

	public void AddPoint(){
		actualPoints++;
//		print (actualPoints +"/"+ maxPoints);
		if(actualPoints == maxPoints){
			GameOver();
			message.sprite = win;
			GetComponent<AudioSource>().PlayOneShot(winClip);
			anim.SetTrigger("Results");
		}
	}

	public void Quit () {
		Application.Quit();
	}
	
	public void Clean(){
		GuessSlotsGenerator.Instance.CleanPhrase();
	
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Letter")){
			o.name = "Letter";
			ObjectPool.Instance.PoolGameObject(o);
		}
		
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Slot")){
			o.name = "Slot";
			ObjectPool.Instance.PoolGameObject(o);
		}
	}
		
	public void RestartGame(){
		Reset();
		StartGame ();
		actualPoints = 0;
	}
	
	public void  Return (){
		GameOver();
		Clean();
		anim.SetTrigger("Return");
	}
	
	void Lose(){
		GameOver();
		message.sprite = lose;
		GetComponent<AudioSource>().PlayOneShot(loseClip);
		anim.SetTrigger("Results");
	}
	
	public void StartGame () {
		anim.SetTrigger("StartGame");

		string actualPhrase = presets[phraseIndex].phrase;
		char[] hints = new char[presets[phraseIndex].hintLetters]; 
		
		for(int i = 0 ; i < hints.Length; i++){
			int random = Random.Range(0,presets[phraseIndex].phrase.Length);
			if(presets[phraseIndex].phrase[random] != ' ')
				hints[i] = presets[phraseIndex].phrase[random];
		}
		
		GuessSlotsGenerator.Instance.SetGuessSlots(actualPhrase, presets[phraseIndex].columnNumber);
		LetterPoolGenerator.Instance.SetLetterPoolSlots(actualPhrase,presets[phraseIndex].hintLetters);
		Timer.Instance.SetTimer(presets[phraseIndex].time);
		
		phraseIndex++;
		if(phraseIndex >= presets.Length)
			phraseIndex = 0;
		
	}

}
