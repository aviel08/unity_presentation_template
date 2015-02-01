#pragma strict

var imageArray : Texture[];
var currentImage : int;
var imageRect : Rect;
var buttonRect : Rect;
 
function Start()
{
    currentImage = 0;
    imageRect = Rect(0, 0, Screen.width, Screen.height / 1 );
    buttonRect = Rect(Screen.width - Screen.height / 4, 600 - Screen.height / 10, Screen.width / 20, Screen.height / 20);
}
 
function OnGUI()
{
    GUI.Label(imageRect, imageArray[currentImage]);
    if(GUI.Button(buttonRect, "Next"))
        currentImage++;
    
      if(currentImage > 2)
    currentImage = 0;

}
 


