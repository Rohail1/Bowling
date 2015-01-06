using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	
	public Text displayScore; 	

	// Use this for initialization

	void Start () {
		displayScore.text =   ""+Ball.FinalScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAgain()
	{
		Application.LoadLevel("Main");
		}
	public void MainMenu()
	{
		Application.LoadLevel ("MainMenu");
		}

}
