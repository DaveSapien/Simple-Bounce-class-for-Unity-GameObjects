using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SapienTools{
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
		//------quick effects menu-------------
		private Vector3 startLocalPos;
		public bool Scale_X = false;
		public bool Scale_Y = false;
		public bool Scale_Z = false;
		//--
		public bool LocalPosition_X = false;
		public bool LocalPosition_Y = false;
		public bool LocalPosition_Z = false;
		//--
		public bool eulerAngles_X = false;
		public bool eulerAngles_Y = false;
		public bool eulerAngles_Z = false;
		//------quick effects menu-------------
		// Use this for initialization
		void Start () {	
			if(Application.isPlaying) ShowMaxSize = false;
			P_Bounce = StartMagnitude;
			startLocalPos = gameObject.transform.localPosition;
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
			RunQuickEffects();
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

		private void RunQuickEffects(){
			//----scale
			bool shouldScale = false;
			Vector3 scl = Vector3.zero;
			if(Scale_X){
				shouldScale = true;
				scl.x = P_Bounce;
			}
			if(Scale_Y){
				shouldScale = true;
				scl.y = P_Bounce;
			}
			if(Scale_Z){
				shouldScale = true;
				scl.z = P_Bounce;
			}
			if(shouldScale)transform.localScale = scl;
			//----scale
			//----position
			bool shouldMove = false;
			Vector3 pos = startLocalPos;
			if(LocalPosition_X){
				shouldMove = true;
				pos.x += P_Bounce;
			}
			if(LocalPosition_Y){
				shouldMove = true;
				pos.y += P_Bounce;
			}
			if(LocalPosition_Z){
				shouldMove = true;
				pos.z += P_Bounce;
			}
			if(shouldMove)transform.localPosition = pos;
			//----position
			//----rotation
			bool shouldRot = false;
			Vector3 rot = Vector3.zero;
			if(eulerAngles_X){
				shouldRot = true;
				rot.x = P_Bounce;
			}
			if(eulerAngles_Y){
				shouldRot = true;
				rot.y = P_Bounce;
			}
			if(eulerAngles_Z){
				shouldRot = true;
				rot.z = P_Bounce;
			}
			if(shouldRot)transform.eulerAngles = rot;
			//----rotation
		}
		//---
		//reset buttons----
		/// <summary>
		/// Sets the scale to StartMagnitude.
		/// </summary>
		public void setScaleToStartMagnitude(){
			transform.localScale = new Vector3(StartMagnitude,StartMagnitude,StartMagnitude);
		}
		/// <summary>
		/// Sets the position to start magnitude.
		/// </summary>
		public void setPositionToStartMagnitude(){
			transform.localPosition = new Vector3(StartMagnitude,StartMagnitude,StartMagnitude);
		}
		/// <summary>
		/// Sets the rotation to start magnitude.
		/// </summary>
		public void setRotationToStartMagnitude(){
			transform.eulerAngles = new Vector3(StartMagnitude,StartMagnitude,StartMagnitude);
		}
		/// <summary>
		/// Resets the game object transform.
		/// </summary>
		public void ResetGameObjectTransform(){
			transform.localScale = Vector3.one;
			transform.localPosition = Vector3.zero;
			transform.eulerAngles = Vector3.zero;
		}
		//reset buttons----
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
}

//-------usage from another script-----------
/*	
 [ExecuteInEditMode]
 		bounce = GetComponent<SapienTools.BounceController>();
		if(bounce.name == "an ID name"){
			bounce.setBounce(true);
			float P_Scale = bounce.getBounce();
			if(bounce.name == "an ID name")transform.localScale = new Vector3(P_Scale, P_Scale, P_Scale);//old stuff for debugging
		}
*/
//-------usage from another script-----------