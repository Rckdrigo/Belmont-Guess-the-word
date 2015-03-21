using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour , IDropHandler{

	public AudioClip correct, incorrect;
	
	bool droppable;

	public GameObject item{
		get{
			if(transform.childCount > 0)
				return transform.GetChild(0).gameObject;
			return null;
		}
	}
	
	void OnEnable(){
		GameStateBehaviour.Instance.GameOver += GameOver;
		droppable = true;
	}
	
	void GameOver(){
		droppable = false;
	}
	
	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if(droppable){
			if(!item){
				if(DragHandler.itemDragged.name == name){
					DragHandler.itemDragged.transform.parent = transform;
					GetComponent<AudioSource>().PlayOneShot(correct);
					GameStateBehaviour.Instance.AddPoint();
				}
				else		
					GetComponent<AudioSource>().PlayOneShot(incorrect);
			}
		}
	}

	#endregion



}
