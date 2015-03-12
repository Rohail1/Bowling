using UnityEngine;
using System.Collections;

public class BallMaterialScript : MonoBehaviour {

	public Material[] myMaterialArray;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		GetComponent<Renderer>().material = myMaterialArray [ChangingBallScript.colorIndex];

	}
}
