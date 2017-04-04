#pragma strict


var zDistance = 10.0;


function Start () {
	transform.localEulerAngles = new Vector3(180,263.7,90);
}

function Update () {
	    var mousePos = Input.mousePosition;
    transform.LookAt(Camera.main.ScreenToWorldPoint(Vector3(mousePos.x, mousePos.y, zDistance)));
}
