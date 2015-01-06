using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	public Text displayRound;
	public Text displayScore;
	public Text displayFinalScore;
	private  Vector3 screenPoint ;
	private  Vector3 offset;
	public   Transform[] PinSet = new Transform[10];
	private Vector3[] PinPosition = new Vector3[10];
	private Quaternion[]  PinRotation = new Quaternion[10];
	private Vector3 defaultPosition;
	private Vector3 LastPinDefaultPosition;
	public Camera cam1,cam2;
	private int currentTurn = 0;
	int totalScore = 0;
	public static int FinalScore = 0;
	int NumberOfGames = 1;
	Vector3 defaultCameraPosition;
	// Use this for initialization
	void Start () {

		defaultCameraPosition = cam1.transform.position;
		LastPinDefaultPosition = PinSet[PinSet.Length - 1].position;
		defaultPosition  = new Vector3(0f,1f,0f);
		cam2.enabled = false;
		NumberOfGames = 1;
		totalScore = 0;
		FinalScore = 0;
		displayScore.text = displayFinalScore.text = "";
		displayRound.text = NumberOfGames+"";
		for (int i = 0; i < 10; i++) {
						PinPosition [i] = PinSet [i].position;
						PinRotation [i] = PinSet [i].rotation;

				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//Rigidbody rigidbody = GetComponent<Rigidbody> ();
		
		if (NumberOfGames > 5) {
			Application.LoadLevel("ScoreMenu");
		}
		cam2.enabled = Vector3.Distance(defaultPosition,transform.position) >  Vector3.Distance(defaultPosition,LastPinDefaultPosition * 0.8f);
		cam1.enabled = !cam2.enabled;

		if (transform.rigidbody.velocity.magnitude > 2 && cam1.enabled) {
			if(Vector3.Distance(cam1.transform.position ,transform.position) > 5){
				cam1.transform.position = new Vector3(cam1.transform.position.x,cam1.transform.position.y,transform.position.z - 5);
			}
				}

		if((Vector3.Distance(defaultPosition,transform.position) >  Vector3.Distance(defaultPosition,LastPinDefaultPosition * 0.5f)
		     &&
		     transform.rigidbody.velocity.magnitude < .25f) || 
		   (Vector3.Distance(defaultPosition,transform.position) >  Vector3.Distance(defaultPosition,LastPinDefaultPosition * 4f)))

		{
			reset();
		}


	}

	IEnumerator CallReset(){

		yield return new  WaitForSeconds (2);
		reset ();
		yield return null;
	}

	Vector2 velocity = Vector2.zero;
	Vector2 previousPosition;
	void reset()
	{
		transform.position = defaultPosition;
		cam1.transform.position = defaultCameraPosition;
		
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		int score = GetFallenPinsCount ();
		if (currentTurn == 0) {

						if (score == PinPosition.Length ) {
								score = 20;
								displayScore.text = score+"";
								FinalScore += score;	
								displayFinalScore.text = FinalScore+"";
								ResetAll ();

						} else {
								totalScore = score;
								FinalScore += totalScore;
								displayScore.text = score+"";
								displayFinalScore.text = FinalScore+"";
								currentTurn++;
						}
				} else if (currentTurn == 1) {
			if(score + totalScore == PinPosition.Length) {
				displayScore.text = (score+4)+"";
				FinalScore += (score+4);
				displayScore.text = FinalScore+"";

			}else {
				totalScore += score;
				displayScore.text = score+"";
				FinalScore += score;
				displayFinalScore.text = FinalScore+"";
			}
			ResetAll ();
				
			}
		}

	void ResetAll(){
			currentTurn = 0;
			displayScore.text = 0 + "";
			NumberOfGames++;
			displayRound.text = NumberOfGames+"";
		for (int i = 0; i < 10; i++) {
			PinSet[i].position = PinPosition[i];
			PinSet[i].rotation = PinRotation[i];

			PinSet[i].transform.renderer.enabled = true;
			PinSet[i].transform.collider.enabled = true;

			PinSet[i].rigidbody.velocity = Vector3.zero;
			PinSet[i].rigidbody.angularVelocity = Vector3.zero;
			
		}
	}

	int GetFallenPinsCount(){
		int score = 0;
		for (int i = 0; i < 10; i++) {
			Vector3 cuurentRotation = PinSet[i].rotation.eulerAngles;
			Vector3 defaultRotation = PinRotation[i].eulerAngles;

			if(!PinSet[i].transform.renderer.enabled)
				continue;

			if((Mathf.Abs(cuurentRotation.x - defaultRotation.x) > 30 && Mathf.Abs(cuurentRotation.x - defaultRotation.x) < 330  ) ||
			   (Mathf.Abs(cuurentRotation.y - defaultRotation.y) > 30 && Mathf.Abs(cuurentRotation.y - defaultRotation.y) < 330  ) ||
			   (Mathf.Abs(cuurentRotation.z - defaultRotation.z) > 30 && Mathf.Abs(cuurentRotation.z - defaultRotation.z) < 330  )
			   ){
				PinSet[i].transform.renderer.enabled = PinSet[i].transform.collider.enabled = false;
				score++;
			}
			
		}
		return score;
	}

	void OnMouseDown() { 
		
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		previousPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

	}




	void OnMouseDrag() 
	{  
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector2 newPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		velocity = (newPosition - previousPosition);
		previousPosition = newPosition;
		Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint)+ offset;
		transform.position = new Vector3(curPosition.x,defaultPosition.y, defaultPosition.z + curPosition.y);
		
	}
	
	void OnMouseUp()
	{
		if (Mathf.Abs (velocity.x) > 80) {
			velocity = new Vector2(Mathf.Sign(velocity.x) * 80,velocity.y);
				}
		if (Mathf.Abs (velocity.y) > 80) {
			velocity = new Vector2(velocity.x,Mathf.Sign(velocity.y) * 80);
		}

		if(velocity.y > 10)
		rigidbody.AddForce(new Vector3(velocity.x,0,velocity.y) * 40.0f);

		
		
	}
}
