using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(BounceController))]
public class BounceControllerEditor : Editor {
	private float timePreview = 2f;
	public AnimationCurve CurvePreview = new AnimationCurve();
	bool showcurve = true;
	private string curvePreviewStatus = "Show Curve";
	public override void OnInspectorGUI()
	{
		BounceController myTarget = (BounceController)target;
		myTarget.name = EditorGUILayout.TextField("Bounce Name", myTarget.name);
		bool max = myTarget.ShowMaxSize;
		bool min = myTarget.ShowMinSize;
		bool bncon = myTarget.BounceOn;
		EditorGUI.BeginChangeCheck();
		myTarget.BounceOn = EditorGUILayout.Toggle("Bounce Active", myTarget.BounceOn);
		GUILayout.BeginHorizontal();
		myTarget.ShowMaxSize = EditorGUILayout.Toggle("Show Max Size", myTarget.ShowMaxSize);
		myTarget.ShowMinSize = EditorGUILayout.Toggle("Show Min Size", myTarget.ShowMinSize);
		GUILayout.EndHorizontal();
		if(EditorGUI.EndChangeCheck()){
			bool maxch = myTarget.ShowMaxSize == max;
			bool minch = myTarget.ShowMinSize == min;
			if(!maxch && minch && myTarget.ShowMaxSize)myTarget.ShowMinSize = false;
			if(maxch && !minch && myTarget.ShowMinSize)myTarget.ShowMaxSize = false;
			if(bncon != myTarget.BounceOn  &&  myTarget.BounceOn){
				myTarget.ShowMinSize = false;
				myTarget.ShowMaxSize = false;
				myTarget.quickReset();
			}
		}
		myTarget.StartMagnitude = EditorGUILayout.FloatField("Start Magnitude",myTarget.StartMagnitude );
		myTarget.TargetMagnitude = EditorGUILayout.FloatField("Target Magnitude",myTarget.TargetMagnitude );
		myTarget.Deterioration = EditorGUILayout.FloatField("Deterioration ",myTarget.Deterioration );
		myTarget.Speed = EditorGUILayout.FloatField("Speed ",myTarget.Speed );

		if(myTarget.Deterioration < 0.0f)myTarget.Deterioration = 0.0f;
		if(myTarget.Deterioration > 1.0f)myTarget.Deterioration = 1.0f;
		if(myTarget.Speed < 0.0f)myTarget.Speed = 0.0f;
		if(myTarget.Speed > 1.0f)myTarget.Speed = 1.0f;


		//------------draw the curve preview-------------
		showcurve = EditorGUILayout.Foldout(showcurve, curvePreviewStatus);
		if(showcurve){
			myTarget.C_Energy = 0f;
			myTarget.C_Bounce = 0f;
			int numberOfFrames = (int)(timePreview * 60f);
			Keyframe[] ks;
			ks = new Keyframe[numberOfFrames];//draw Curve for two seconds
			int i = 0;
			while (i < ks.Length) {
				ks[i] = new Keyframe(i, myTarget.C_Bounce);
				myTarget.SpringCurve();
				i++;
			}
			CurvePreview = new AnimationCurve(ks);
				curvePreviewStatus = "Curve in "+timePreview+" seconds";
			EditorGUILayout.CurveField("", CurvePreview, GUILayout.Height(150));
			EditorGUILayout.LabelField("Edit visualization timeframe (in seconds)");
			timePreview = EditorGUILayout.Slider(timePreview,0.5f, 10f);
		}else{
			curvePreviewStatus = "Show Curve";
		}
		EditorUtility.SetDirty(myTarget);
		//------------draw the curve preview-------------
	}


}