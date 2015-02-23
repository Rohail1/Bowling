using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FbHolder : MonoBehaviour {

	public GameObject UIFbIsLoggedIn;
	public GameObject UIFbIsNotLoggedIn;
	public GameObject UIFbAvatar;
	public GameObject UIFbName;
	private Dictionary<string,string> profile = null;

	void Awake()
	{
		UIFbIsLoggedIn.SetActive (false);
		FB.Init (SetInit, onHideUnity);

	}
	void SetInit()
	{
		Debug.Log("Hi Hello");

		if (FB.IsLoggedIn) {
			Debug.Log("Yo its logged In");
				}
		else {


				}

	}
	private void onHideUnity(bool isGameShown)
	{

		if (!isGameShown) {
						Time.timeScale = 0;
				} else {
			Time.timeScale = 1;			
		}

	}

	public void FBLogin()
	{
		FB.Login ("user_about_me, user_birthday", AuthCallBack);

	}
	void AuthCallBack(FBResult result)
	{
		if (FB.IsLoggedIn) {
						Debug.Log ("It works");
			DealsWithLogin(true);
						
				} else {
			Debug.Log("No it doesnt");
			DealsWithLogin(false);
				}
	}

	void DealsWithLogin(bool isLoggedIn)
	{
			if (isLoggedIn) {
						UIFbIsLoggedIn.SetActive (true);
						UIFbIsNotLoggedIn.SetActive (false);

					// Get Profile Picture
			FB.API(Util.GetPictureURL("me",128,128),Facebook.HttpMethod.GET,dealWithProfilePicture);
			FB.API("/me?fields=id,first_name",Facebook.HttpMethod.GET,namegetting);
					// Get User Name


				} else {
			UIFbIsLoggedIn.SetActive(false);
			UIFbIsNotLoggedIn.SetActive(true);

				}


		}

	void dealWithProfilePicture(FBResult result)
	{

		if (result.Error !=  null) 
		{
			Debug.Log("Error getting the picture");
				
		}
		else 
		{
			Image userImage = UIFbAvatar.GetComponent<Image>();
			userImage.sprite = Sprite.Create(result.Texture,new Rect(0,0,128,128),Vector2.zero);

		}


	}
	void namegetting(FBResult result)
	{
		if (result.Error != null) {
						Debug.Log ("Error getting the picture");
			
				} else {
				
			profile = Util.DeserializeJSONProfile(result.Text);
			UIFbName.GetComponent<Text>().text = profile["first_name"];

				}

	}


}
