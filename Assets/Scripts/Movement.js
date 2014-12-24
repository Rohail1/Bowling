#pragma strict

 private  var screenPoint:Vector3 ;
 private  var offset:Vector3;

function Start () {

}

function Update () {

}
function FixedUpdate()
{
//		if(Input.GetKeyDown(KeyCode.UpArrow))
//		{
//			this.gameObject.transform.position.z += 1;
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
 
 		
 
 
 }