#pragma strict

private var theTime = Date();

function Start () {

}

function Update () {


    var days = theTime.Now.DayOfWeek;
    var day = theTime.Now.Day;
    var month = theTime.Now.Month;
    var year = theTime.Now.Year;
    var hours = theTime.Now.Hour;
    var minutes = theTime.Now.Minute;
    var seconds = theTime.Now.Second;
    
    guiText.text = days + " " + day + " / " + month + " / " + year + " // " + hours + ":" + minutes + ":" + seconds;

}