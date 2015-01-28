using UnityEngine;
using System.Collections;

public class ChangingBallScript : MonoBehaviour {


	public static int colorIndex;
	private int selectedIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Wood()
	{
		selectedIndex = 1;
		colorIndex = selectedIndex;
	}
	
	public void Red()
	{
		selectedIndex = 0;
		colorIndex = selectedIndex;
	}
	
	public void Gum()
	{
		selectedIndex = 2;
		colorIndex = selectedIndex;
	}
}
