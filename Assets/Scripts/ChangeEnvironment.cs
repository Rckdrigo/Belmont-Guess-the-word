using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeEnvironment : MonoBehaviour {

	public Sprite[] sprites;
	
	public void ChangeWallpaper(){
		GetComponent<Image>().sprite = sprites[Random.Range(0,sprites.Length-1)];
	}

}
