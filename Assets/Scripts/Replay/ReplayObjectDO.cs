using UnityEngine;
using System.Collections;

public class ReplayObjectDO {
	public Vector3 Position;
	public Quaternion Rotation;

	public ReplayObjectDO(Vector3 Position, Quaternion Rotation){
		this.Position = Position;
		this.Rotation = Rotation;
		}
}
