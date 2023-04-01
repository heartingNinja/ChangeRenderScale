# Change Render Scale Auto
Changes the render scale for Unity for a targeted FPS. Can also set the target FPS and Quality Level.  
To really test you will need to add some visual. I made empty of visual for smaller download.
Made for URP.

There are 3 scripts:

1)ChooseFPS:
Here you set can set the PlayerPrefs for the target FPS. RIght now set up for 30 or 60 as it is for mobile. Higher numbers could be good for VR. Also a void to reset all the PlayerPrefs.

2)FPS:
This script finds the current FPS. The int frameRate is public as it is used with the next script.

3)Set Quality:
This is used to Set the QualitySettings or the renderScale. 
This sets a PlayerPref for QualitySettings. 
Where the SetGraphicsRenderByFPS() void is and where to edit the auto graphics change.

 
