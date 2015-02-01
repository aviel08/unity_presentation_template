#pragma strict

function Start () {

}

function Update () {
		// Slowly rotate the object around its X axis at 1 degree/second.
//transform.Rotate(Vector3.left * Time.deltaTime);
transform.Rotate(Vector3.up * Time.deltaTime*6, Space.World); 

// ... at the same time as spinning relative to the global // Y axis at the same speed. transform.Rotate(Vector3.up * Time.deltaTime, Space.World); } 
}