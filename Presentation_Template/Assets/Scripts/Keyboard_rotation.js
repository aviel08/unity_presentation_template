    #pragma strict
     
    var rotAmount = 90.0;
    var destEuler = Vector3.zero;
    var speed = 5.0;
    private var currEuler;
     
    function Start() {
    currEuler = destEuler;
    transform.eulerAngles = destEuler;
    }
     
    function Update () {
    if (Input.GetKeyDown(KeyCode.RightArrow) ) {
    destEuler.y += -rotAmount;
    }
    
    if (Input.GetKeyDown(KeyCode.LeftArrow) ) {
    destEuler.y += rotAmount;
    }
     
     
    currEuler = Vector3.Lerp(currEuler, destEuler, Time.deltaTime * speed);
    transform.eulerAngles = currEuler;
    }