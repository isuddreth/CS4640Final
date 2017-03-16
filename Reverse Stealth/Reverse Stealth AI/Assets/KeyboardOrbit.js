var target : Transform;
var distanceMin = 10.0;
var distanceMax = 15.0;
var distanceInitial = 12.5;
var scrollSpeed = 1.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

var cameraHeight = 0.0f;

private var x = 0.0;
private var y = 0.0;
private var distanceCurrent = 0.0;

@script AddComponentMenu ("Camera-Control/Key Mouse Orbit")


function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

	distanceCurrent = distanceInitial;

	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}

function LateUpdate () {
    if (target){ 
        var axisInputValue : Vector2;
        axisInputValue = new Vector2(0, 0);

        if (GameObject.FindWithTag("Player").GetComponent("TPCharController").isControllable)
        {
            axisInputValue.x = Input.GetAxis("Mouse X");
            axisInputValue.y = Input.GetAxis("Mouse Y");








        }

        x += axisInputValue.x * xSpeed * 0.02;
        y -= axisInputValue.y * ySpeed * 0.02;
 		distanceCurrent -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
		
		distanceCurrent = Mathf.Clamp(distanceCurrent, distanceMin, distanceMax);
		y = ClampAngle(y, yMinLimit, yMaxLimit);
        
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * Vector3(0.0, 0.0, -distanceCurrent) + target.position + new Vector3(0, cameraHeight, 0);
        
        transform.rotation = rotation;
        transform.position = position;
        
        CheckCameraCollision();
        SetPlayerAlpha();
    }
}

static function ClampAngle (angle : float, min : float, max : float)
{
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}

 

function CheckCameraCollision()
{
    var offset = 1.0;
    var newPos = target.position + new Vector3(0, cameraHeight, 0);

	var ray = new Ray(newPos, transform.position - newPos);

    var hit = new RaycastHit();

    if (Physics.Raycast(ray, hit, (transform.position - newPos).magnitude))
    {
        if(hit.distance > offset)
            transform.position = ray.GetPoint(hit.distance - offset);
        else
            transform.position = newPos;
    }
}


function SetPlayerAlpha()
{
	var player = GameObject.FindWithTag("Player");
	//if (distanceCurrent < 40)
		//player.renderer.material.color.a = distanceCurrent / 40;
}