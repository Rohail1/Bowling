using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public GameObject Traget1;
	public GameObject Traget2;
	public GameObject Traget3;
	public GameObject Traget4;
	public GameObject TragetMiddle;
	// Use this for initialization
	void Start () {
	
		this.transform.LookAt (TragetMiddle.transform);
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.LookAt (TragetMiddle.transform);

	
	}
}
