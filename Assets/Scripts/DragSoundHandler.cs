using UnityEngine;
using System.Collections;

public class DragSoundHandler : MonoBehaviour {

	void Start(){
		GetComponent<DragHandler>().StartDragging += PlaySound;
		GetComponent<DragHandler>().StopDragging += StopSound;
	}

	public void PlaySound(){
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}
	
	public void StopSound(){
		GetComponent<AudioSource>().Stop();
	}
}
