using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Replay {
	public bool isRecordingStarted = false;
	bool isPlay = false;
	List<ReplayObject> ObjectsForReplay = new List<ReplayObject>();
	float waitCount = 0;
	float currentFrame = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		if (isRecordingStarted) {
						RecordData ();
				} else  if(isPlay){
			PlayRecording();
				}
	}


	public void PlayRecording(){
		if (ShouldWait())
			return;

		currentFrame += Time.deltaTime * 500;

		for(int i = 0; i < ObjectsForReplay.Count; i++){
			ObjectsForReplay[i].GotoFrame((int)(currentFrame));
			}

		}

	public void RecordData(){
		if (ShouldWait())
						return;
		
		for(int i = 0; i < ObjectsForReplay.Count; i++){
			ObjectsForReplay[i].Record();
		}
	}


	public void AddObjectForReplay(Transform t){
		ObjectsForReplay.Add (new ReplayObject(t));
	}



	bool ShouldWait(){
		if(waitCount < 1){
			waitCount += Time.deltaTime * 500;
			return true;
		}
		
		waitCount = 0;
		return false;
	}

	public bool Play{
		set{

			if(!isPlay && value){
				currentFrame = 0;
			}

			isPlay = value;

			for(int i = 0; i < ObjectsForReplay.Count; i++){
				ObjectsForReplay[i].CurrentObject.GetComponent<Rigidbody>().isKinematic = true;
			}



				}
		get{
			return  isPlay;
				}

	}
}
