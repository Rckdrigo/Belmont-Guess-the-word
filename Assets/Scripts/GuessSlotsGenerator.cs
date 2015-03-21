using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GuessSlotsGenerator : Singleton<GuessSlotsGenerator> {
	
	string phrase = "";
	
	void Start(){
		GameStateBehaviour.Instance.Reset += Reset;
	}
	
	void Reset(){
		CleanPhrase();
	}
	
	public void CleanPhrase(){
		phrase = ""; 
	}
	
	public void SetGuessSlots(string guessPhrase, int columnCount){
		if(phrase == ""){
			GetComponent<GridLayoutGroup>().constraintCount = columnCount;
			phrase = guessPhrase.ToUpper();
			for(int i = 0; i < phrase.Length; i++){
				GameObject obj = ObjectPool.Instance.GetGameObjectOfType("Slot",true);
				obj.transform.SetParent(transform);
				
				obj.GetComponent<Image>().color = new Color(1,1,1,1);
				if(phrase[i].Equals(' '))
					obj.GetComponent<Image>().color = new Color(0,0,0,0);
				
				obj.GetComponent<Slot>().enabled = true;
				obj.name = ""+phrase[i];
			}
		}
	}
	
	public void SetHints(List<char> hints){		
		for(int i = 0; i < hints.Count; i++){
			GameObject letter = ObjectPool.Instance.GetGameObjectOfType("Letter",true);
			letter.name = ""+hints[i];
			letter.GetComponent<SpriteSelector>().SetSprite();
			Transform slot = transform.FindChild(""+hints[i]);
			letter.transform.SetParent(transform.FindChild(""+hints[i]));
		}
	}
}
