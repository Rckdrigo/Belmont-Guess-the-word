using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public delegate void DragEvents();
	public DragEvents StartDragging;
	public DragEvents StopDragging;

	public static GameObject itemDragged;
	Vector3 initialPos;
	Transform startParent;
	
	bool draggable;
	
	void OnEnable(){
		GameStateBehaviour.Instance.GameOver += GameOver;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		draggable = true;
	}
	
	void GameOver(){
		draggable = false;
	}
	
	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		if(draggable){
			itemDragged = gameObject;
			
			initialPos = transform.position;
			startParent = transform.parent;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
			StartDragging();
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if(draggable){
			transform.position = Input.mousePosition;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(draggable){
			StopDragging();
			GetComponent<CanvasGroup>().blocksRaycasts = true;
			itemDragged = null;
			if(transform.parent == startParent)
				transform.position = initialPos;
		}
	}

	#endregion
}
