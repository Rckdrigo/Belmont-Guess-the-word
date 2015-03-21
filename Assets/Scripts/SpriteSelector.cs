using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSelector : MonoBehaviour {

	[System.Serializable()]
	public struct letterSpriteReference{
		public string letter;
		public Sprite sprite;
	}
	
	public letterSpriteReference[] reference;
	
	public void SetSprite(){
		for(int i = 0; i < reference.Length; i++){
			if(reference[i].letter.Equals(name))
				GetComponent<Image>().sprite = reference[i].sprite;
		}
	}

}
