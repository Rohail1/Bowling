using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayObject {
	private List<ReplayObjectDO> replayList = new List<ReplayObjectDO>();
	private Transform currentObject;

	public ReplayObject(Transform CurrentObject){
		this.currentObject = CurrentObject;
	}


	public void GotoFrame(int index){
		if(index > replayList.Count - 1)
			return;


		currentObject.position = replayList [index].Position;
		currentObject.rotation = replayList [index].Rotation;
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
