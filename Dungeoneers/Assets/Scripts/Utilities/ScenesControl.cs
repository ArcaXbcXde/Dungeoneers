using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenesControl : MonoBehaviour {
	
	public static ScenesControl Instance {

		get;
		private set;
	}
	
	[HideInInspector]
	public Text textInfo;
	
    private string nextScreen;

	private void Awake () {

		Instance = this;
		nextScreen = "Stage0";
	}

	#region Go to specifics scenes

	public void Menu() {

        nextScreen = "StartMenu";
    }

    public void Map() {

        nextScreen = "LevelSelection";
    }
    
    public void Enciclopedia() {

        nextScreen = "Enciclopedia";
    }

    public void Stage() {

        nextScreen = "Stage";
    }

    public void SampleScene() {

        nextScreen = "SampleScene";
    }

    public void Quit() {

        nextScreen = null;
    }

	#endregion

	#region Scene selection

	public void RandomizeSceneNumber(int min, int max) {

		SelectSceneID (Random.Range(min, max));
	}

	public void RandomizeSceneNumber (int max) {

		SelectSceneID(Random.Range(4, max));
	}

	public void SelectSceneID (int sceneNumber) {

		string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneNumber);

		nextScreen = scenePath.Substring(scenePath.LastIndexOf('/') + 1, scenePath.Substring(scenePath.LastIndexOf('/') + 1).Length - 6);

		if (textInfo != null) {

			textInfo.text = nextScreen;
		}
	}

	public void SelectSceneName (string sceneName) {
		
		nextScreen = sceneName;
		
		if (textInfo != null) {

			textInfo.text = nextScreen;
		}
	}

	#endregion

	/// <summary>
	/// Go to the scene by the name previously given, if no name is given then it quits the application.
	/// If the default scene is still selected then changes the text to ask to select a scene
	/// </summary>
	public void GoToScreen() {
		
        if (nextScreen == null) {

            Application.Quit();
			Debug.Log ("Quit by going to a null scene");
        } else if (nextScreen == "Stage0") {

			textInfo.text = "Select a level before starting";
        } else {

            SceneManager.LoadScene(nextScreen, LoadSceneMode.Single);
		}
    }

	/// <summary>
	/// Just go to the scene by the name given, if no name is given then it quits the application.
	/// </summary>
	/// <param name="screenName">The name of the scene to go, keep it empty to quit</param>
	public void GoToScreen(string screenName) {

		if (nextScreen != null) {

			SceneManager.LoadScene(screenName);
		} else {

			Quit();
		}
	}
	
}
