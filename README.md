# Simple-Bounce-class-for-Unity-GameObjects
A simple bouncing variable class with its own inspector window.
The purpose for this is to add an easy to use 'bounce' controller to any game object in Unity. This is frame based, not time based. Only useful for visual effects.

Setup: 
Place the scripts into you project, make sure BounceControllerEditor.cs is in your "Editor" folder(or just use the one here).

To use:
You will need two scripts to use this, the BounceController.cs script and your own script that calls BounceController. An example script (BounceControllerTester.cs) is provided.

1). Drag BounceController.cs into your object and name it in the "Bounce Name" field. You will need this later in your own script.

2). Drag your script into the game object (or BounceControllerTester.cs).

3). Basically done.


BounceController fields:

Bounce Name, (variable name in the script is "name").
This is a script identifier to allow multiple BounceControllers on the one game object.

Bounce Active, turns on the bounce animation in both the inspector and in game.

Show Max Size, shows the maximum size and turns off the bounce animation. (editor only)

Show Min Size, shows the minimum size and turns off the bounce animation. (editor only)

Start Magnitude, (variable name in the script is "StartMagnitude").
Sets the starting magnitude.

Target Magnitude, (variable name in the script is "Target Magnitude").
Sets the Target magnitude.

Deterioration, (variable name in the script is "Deterioration").
Sets the Deterioration of the bounce.

Speed, (variable name in the script is "Speed").
Sets the Speed of the bounce.

Show curve, (only used to visualize the curve of the bounce).
Shows the bounce curve in seconds. 
Number of seconds in preview controlled by the "Edit visualization timeframe (in seconds)" Slider.



To call the bounce script in your own script follow the example in BounceControllerTester.cs.
For example, to link to your game object scale you would do something like this:
BounceController [] bouncers;
        foreach (BounceController bounce in bouncers) {
            if(bounce.name == "Youre bounce name"){
                float P_Scale = bounce.getBounce();
                transform.localScale = new Vector3(P_Scale, P_Scale, P_Scale);
 //bounce.setBounce(false); whenever you want to stop the bounce
            }
        }  


-- 
