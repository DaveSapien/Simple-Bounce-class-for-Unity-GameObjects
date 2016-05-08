using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class BounceController : MonoBehaviour {
	public string name;
	public bool BounceOn = false;
	public bool ShowMaxSize = false;
	public bool ShowMinSize = false;
	public float StartMagnitude = 0.1f;
	public float TargetMagnitude = 1f;
	public float Deterioration = 0.8f;
	public float Speed = 0.6f;
	private float P_Bounce = 1f;
	private float P_Energy = 0.0f;

	// Use this for initialization
	void Start () {	
		if(Application.isPlaying) ShowMaxSize = false;
		P_Bounce = StartMagnitude;
	}

	// Update is called once per frame
	void Update () {
		if(BounceOn){
			Spring();
		}else{
			P_Energy = 0;
			P_Bounce = StartMagnitude;
		}
		if(ShowMaxSize){
			P_Energy = 0;
			P_Bounce = TargetMagnitude;
			BounceOn = false; 
		}
		if(ShowMinSize){
			P_Energy = 0;
			P_Bounce = StartMagnitude;
			BounceOn = false;
		}
	}
	/// <summary>
	/// Quick reset for the spring.
	/// </summary>
	public void quickReset(){
		P_Bounce = StartMagnitude;
	}
	/// <summary>
	/// runs the springy behavour
	/// </summary>
	private void Spring(){
		float curscale = P_Bounce;
		float x = - curscale + TargetMagnitude;
		P_Energy = P_Energy * Deterioration + x;
		P_Bounce += P_Energy*(Speed);
	}
	/// <summary>
	/// Gets the bounce value
	/// </summary>
	/// <returns>The bounce.</returns>
	/// <param name="percentage">Percentage.</param>
	public float getBounce(){
		return P_Bounce;
	}
	public void setBounce(bool b){
		BounceOn = b;
	}

	//--------curve preview------------------
	//--------curve preview------------------
	public float C_Energy = 0f;
	public float C_Bounce = 0f;
	/// <summary>
	/// Used to callculate the preview curve in the inspector.
	/// </summary>
	public void SpringCurve(){
		float curscale = C_Bounce;
		float x = - curscale + TargetMagnitude;
		C_Energy = C_Energy * Deterioration + x;
		C_Bounce += C_Energy*Speed;
	}
	//--------curve preview------------------
	//--------curve preview------------------

}


//-------usage from another script-----------
/*	
 [ExecuteInEditMode]
 		bounce = GetComponent<BounceController>();
		if(bounce.name == "an ID name"){
			bounce.setBounce(true);
			float P_Scale = bounce.getBounce();
			if(bounce.name == "an ID name")transform.localScale = new Vector3(P_Scale, P_Scale, P_Scale);//old stuff for debugging
		}
*/
//-------usage from another script-----------