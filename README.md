<h1>Simple-Bounce-class-for-Unity-GameObjects</h1>

<p>A simple bouncing variable class with its own inspector window.
The purpose for this is to add an easy to use 'bounce' controller to any game object in Unity. This is frame based, not time based. Only useful for visual effects.</p>
![screenshot](https://raw.githubusercontent.com/DaveSapien/Simple-Bounce-class-for-Unity-GameObjects/master/ScreenShot1.jpg)
<p>
Setup: 
Place the scripts into you project, make sure BounceControllerEditor.cs is in your "Editor" folder(or just use the one here).<p>
//---------------------------------------------------------------------------------------------<p>
//---------------------------------------------------------------------------------------------<p>
To use:
You will need two scripts to use this, the BounceController.cs script and your own script that calls BounceController. An example script (BounceControllerTester.cs) is provided.</p>

<p>1). Drag BounceController.cs into your object and name it in the "Bounce Name" field. You will need this later in your own script.</p>

<p>2). Drag your script into the game object (or BounceControllerTester.cs).</p>

<p>3). Basically done.<p>
//---------------------------------------------------------------------------------------------<br>
//---------------------------------------------------------------------------------------------<p>
BounceController fields:</p>

<p>Bounce Name, (variable name in the script is "name").
This is a script identifier to allow multiple BounceControllers on the one game object.</p>

<p>Bounce Active, turns on the bounce animation in both the inspector and in game.</p>

<p>Show Max Size, shows the maximum size and turns off the bounce animation. (editor only)</p>

<p>Show Min Size, shows the minimum size and turns off the bounce animation. (editor only)</p>

<p>Start Magnitude, (variable name in the script is "StartMagnitude").
Sets the starting magnitude.</p>

<p>Target Magnitude, (variable name in the script is "Target Magnitude").
Sets the Target magnitude.</p>

<p>Deterioration, (variable name in the script is "Deterioration").
Sets the Deterioration of the bounce.</p>

<p>Speed, (variable name in the script is "Speed").
Sets the Speed of the bounce.
//----
Show curve, (only used to visualize the curve of the bounce).
Shows the bounce curve in seconds. 
Number of seconds in preview controlled by the "Edit visualization timeframe (in seconds)" Slider.
//----
"Show QuickControlls for direct gameobject controll" (for quck and dirty usage)
A drop down to toggle direct control of the host game object without the need for another script.
Toggles to link Scale, Movement, and Rotation to the bounce Magnitude. (all of these are local controlls)<p>

//---------------------------------------------------------------------------------------------br>
//---------------------------------------------------------------------------------------------<p>
To call the bounce script in your own script follow the example in BounceControllerTester.cs.
//---------------------------------------------------------------------------------------------</p>

<p>-- </p>

