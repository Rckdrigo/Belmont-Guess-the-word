using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour {

	public void ResultsShown(){
		GameStateBehaviour.Instance.Clean();
	}
}
