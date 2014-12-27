#pragma strict

 private  var screenPoint:Vector3 ;
 private  var offset:Vector3;
 var PinSet : Transform[] = new Transform[9];
 private var flag;
 private var turnChecker : int;

function Start () {
	flag = false;
	turnChecker = 0;

}

function Update () {

}
function FixedUpdate()
{
		if(flag && rigidbody.velocity.z < 1)
		{
			
				transform.position = new Vector3(0,1,-9.25);
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
				flag = false;
		}
//		if(turnChecker == 1)
//		{
//		
//			for(var i = 0; i<10;i++)
//			{
//				if(PinSet[i].transform.Rotate)
//			
//			}
//		
//		
//		}
}
//
//function OnMouseDrag () {
//		rigidbody.AddForce(Vector3.forward*12);
//		transform.position = Input.mousePosition;
//	}
	
function  OnMouseDown() { 

	screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//	Debug.Log(screenPoint);
 	offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
// 	Debug.Log(offset);
 }
 
 function OnMouseDrag() 
 {  
 
	 var curScreenPoint:Vector3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//	 Debug.Log(curScreenPoint);
	 var curPosition:Vector3   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
//     Debug.Log(curPosition);
     transform.position = curPosition;
     rigidbody.AddForce(Vector3.forward * 12);
     
 }
 
 function OnMouseUp()
 {
 
 		flag = true;
 		
 		turnChecker++;
 
 
 }