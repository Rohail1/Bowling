using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayObject {
	private List<ReplayObjectDO> replayList = new List<ReplayObjectDO>();
	private Transform currentObject;

	public ReplayObject(Transform CurrentObject){
		this.currentObject = CurrentObject;
	}


	public bool GotoFrame(int index){
		if(index > replayList.Count - 1)
			return false;


		currentObject.position = replayList [index].Position;
		currentObject.rotation = replayList [index].Rotation;

		return true;
	}

	public int TotalFrames(){
		return replayList.Count;
	}

	public void Record(){
		replayList.Add(new ReplayObjectDO(currentObject.position,currentObject.rotation));
	}


	public Transform CurrentObject{
		get{
			return currentObject;
				}
	}
}
