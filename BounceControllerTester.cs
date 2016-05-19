using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class BounceControllerTester : MonoBehaviour {
	private SapienTools.BounceController [] bouncers;
	// Use this for initialization
	void Start () {
		bouncers = GetComponents<SapienTools.BounceController>();
		foreach (BounceController bounce in bouncers) {
			if(bounce.name == "Your bounce name")bounce.setBounce(true);
		}
	}
	
	// Update is called once per frame
	void Update(){
		foreach (SapienTools.BounceController bounce in bouncers) {
			if(bounce.name == "Your bounce name"){
				float P_Scale = bounce.getBounce();
				transform.localScale = new Vector3(P_Scale, P_Scale, P_Scale);//old stuff for debugging
				//bounce.setBounce(false); whenever you want
			}
		}
	}
}