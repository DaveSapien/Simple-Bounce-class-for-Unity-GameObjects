using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(BounceController))]
public class BounceControllerEditor : Editor {
	private float timePreview = 2f;
	public AnimationCurve CurvePreview = new AnimationCurve();
	bool showcurve = true;
	private string curvePreviewStatus = "Show Curve";

	bool showQuickControlls = false;
	private string QuickControllsStatus = "Show QuickControlls for direct gameobject controll";
	public void OnEnable(){
		BounceController myTarget = (BounceController)target;
		refreshCurve(myTarget);
	}
	public override void OnInspectorGUI()
	{
		BounceController myTarget = (BounceController)target;
		myTarget.name = EditorGUILayout.TextField("Bounce Name", myTarget.name);
		bool max = myTarget.ShowMaxSize;
		bool min = myTarget.ShowMinSize;
		bool bncon = myTarget.BounceOn;
		//--
		EditorGUI.BeginChangeCheck();
		myTarget.BounceOn = EditorGUILayout.Toggle("Bounce Active", myTarget.BounceOn);
		GUILayout.BeginHorizontal();
		myTarget.ShowMaxSize = EditorGUILayout.Toggle("Show Max Size", myTarget.ShowMaxSize);
		myTarget.ShowMinSize = EditorGUILayout.Toggle("Show Min Size", myTarget.ShowMinSize);
		GUILayout.EndHorizontal();
		//--
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
		//--
		EditorGUI.BeginChangeCheck();
		myTarget.StartMagnitude = EditorGUILayout.FloatField("Start Magnitude",myTarget.StartMagnitude );
		myTarget.TargetMagnitude = EditorGUILayout.FloatField("Target Magnitude",myTarget.TargetMagnitude );
		myTarget.Deterioration = EditorGUILayout.FloatField("Deterioration ",myTarget.Deterioration );
		myTarget.Speed = EditorGUILayout.FloatField("Speed ",myTarget.Speed );
		if(EditorGUI.EndChangeCheck()){
			refreshCurve(myTarget);
		}
		//--
		if(myTarget.Deterioration < 0.0f)myTarget.Deterioration = 0.0f;
		if(myTarget.Deterioration > 1.0f)myTarget.Deterioration = 1.0f;
		if(myTarget.Speed < 0.0f)myTarget.Speed = 0.0f;
		if(myTarget.Speed > 1.0f)myTarget.Speed = 1.0f;
		//--

		//------------draw the curve preview-------------
		showcurve = EditorGUILayout.Foldout(showcurve, curvePreviewStatus);
		if(showcurve){
			myTarget.C_Energy = 0f;
			myTarget.C_Bounce = 0f;
			curvePreviewStatus = "Curve in "+timePreview+" seconds";
			EditorGUILayout.CurveField("", CurvePreview, GUILayout.Height(150));
			EditorGUILayout.LabelField("Edit visualization timeframe (in seconds)");
			EditorGUI.BeginChangeCheck();
			timePreview = EditorGUILayout.Slider(timePreview,0.5f, 10f);
			if(EditorGUI.EndChangeCheck()){
				refreshCurve(myTarget);
			}
		}else{
			curvePreviewStatus = "Show Curve";
		}
		EditorUtility.SetDirty(myTarget);
		//------------draw the curve preview-------------
		//------------draw the Quick Controlls-----------
		QuickControlls(myTarget);
		//------------draw the Quick Controlls-----------
	}

	/// <summary>
	/// Draws the Quick controll options.
	/// </summary>
	/// <param name="myTarget">My target.</param>
	private void QuickControlls(BounceController myTarget){
		showQuickControlls = EditorGUILayout.Foldout(showQuickControlls, QuickControllsStatus);
		if(showQuickControlls){
			EditorGUIUtility.LookLikeControls(50);
			EditorGUIUtility.labelWidth = 18f;
			EditorGUILayout.LabelField("Scale gameobject bools");
			GUILayout.BeginHorizontal();
			myTarget.Scale_X = EditorGUILayout.Toggle("X:", myTarget.Scale_X,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.Scale_Y = EditorGUILayout.Toggle("Y:", myTarget.Scale_Y,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.Scale_Z = EditorGUILayout.Toggle("Z:", myTarget.Scale_Z,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			EditorGUILayout.Space();
			if(GUILayout.Button("Reset Scale To Start Magnitude",GUILayout.MaxWidth(220))){
				myTarget.setScaleToStartMagnitude();
			}
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Move gameobject bools");
			GUILayout.BeginHorizontal();
			myTarget.LocalPosition_X = EditorGUILayout.Toggle("X:", myTarget.LocalPosition_X,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.LocalPosition_Y = EditorGUILayout.Toggle("Y:", myTarget.LocalPosition_Y,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.LocalPosition_Z = EditorGUILayout.Toggle("Z:", myTarget.LocalPosition_Z,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			EditorGUILayout.Space();
			if(GUILayout.Button("Reset Position To Start Magnitude",GUILayout.MaxWidth(220))){
				myTarget.setPositionToStartMagnitude();
			}
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Rotation gameobject bools");
			GUILayout.BeginHorizontal();
			myTarget.eulerAngles_X = EditorGUILayout.Toggle("X:", myTarget.eulerAngles_X,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.eulerAngles_Y = EditorGUILayout.Toggle("Y:", myTarget.eulerAngles_Y,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));
			myTarget.eulerAngles_Z = EditorGUILayout.Toggle("Z:", myTarget.eulerAngles_Z,GUILayout.MaxWidth(32), GUILayout.MaxHeight(32));

			EditorGUILayout.Space();
			if(GUILayout.Button("Reset Rotation To Start Magnitude",GUILayout.MaxWidth(220))){
				myTarget.setRotationToStartMagnitude();
			}
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			GUILayout.FlexibleSpace();
			if(GUILayout.Button("Reset GameObject transform values",GUILayout.MaxWidth(220))){
				myTarget.Scale_X = myTarget.Scale_Y =myTarget.Scale_Z = false;
				myTarget.LocalPosition_X = myTarget.LocalPosition_Y =myTarget.LocalPosition_Z = false;
				myTarget.eulerAngles_X = myTarget.eulerAngles_Y =myTarget.eulerAngles_Z  = false;
				myTarget.BounceOn = false;
				myTarget.ResetGameObjectTransform();
			}
			QuickControllsStatus = "Collapse";
		}else{
			QuickControllsStatus = "Show QuickControlls for direct gameobject controll";
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}

	/// <summary>
	/// Refreshes the curve preview of the bounce. (try to call this sparingly)
	/// </summary>
	/// <param name="myTarget">My target.</param>
	private void refreshCurve(BounceController myTarget){
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
	}

}