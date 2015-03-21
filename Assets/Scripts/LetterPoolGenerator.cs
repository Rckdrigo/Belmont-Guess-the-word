using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LetterPoolGenerator : Singleton<LetterPoolGenerator> {
	
	string[] letterDictionary = {"A","B","C","D","E","F","G","H","I","J",
	"K","L","M","N","Ñ","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
	
	List<string> letters;
	
	public void SetLetterPoolSlots(string phrase, int hintNumber){
		
		letters = new List<string>();
		phrase = phrase.ToUpper().Replace(" ","");
		
		GameStateBehaviour.Instance.maxPoints = phrase.Length;
		
		int letterPoolSize = (phrase.Length % 2 == 0) ? phrase.Length + 4 : phrase.Length + 3;
	
		List<char> hints = new List<char>();
		int indexHint = 0;
		
		for(int i = 0; i < letterPoolSize; i++){
			if(i < phrase.Length){
				if(Random.value < 0.30 && indexHint < hintNumber && !hints.Contains(phrase[i])){
					hints.Add(phrase[i]);
					letters.Add(letterDictionary[Random.Range(0,letterDictionary.Length-1)]);
					GameStateBehaviour.Instance.maxPoints--;
					indexHint++;
				}
				else
					letters.Add(""+phrase[i]);		
			}
			else
				letters.Add(letterDictionary[Random.Range(0,letterDictionary.Length-1)]);
		}
	
		GuessSlotsGenerator.Instance.SetHints(hints);
	
		for(int i = 0; i < letterPoolSize; i++){
			GameObject slot = ObjectPool.Instance.GetGameObjectOfType("Slot",true);
			slot.transform.SetParent(transform);
			slot.GetComponent<Slot>().enabled = false;
			slot.GetComponent<Image>().color = new Color(0,0,0,0);
			
			GameObject letter = ObjectPool.Instance.GetGameObjectOfType("Letter",true);
			letter.transform.parent = slot.transform;
			
			int randomIndex = Random.Range(0,letters.Count-1);
			letter.name = letters[randomIndex];
			letters.RemoveAt(randomIndex);
			
			letter.GetComponent<SpriteSelector>().SetSprite();
		}
		
		
		
	}
}
