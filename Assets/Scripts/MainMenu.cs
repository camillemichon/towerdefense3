using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "MainLevel";
	public SceneFader sceneFader;

	public void Play()
	{
		sceneFader.FadeTo(levelToLoad);
	}

	public void Quit()
	{
		if (Application.isEditor)
			EditorApplication.ExitPlaymode();
		else
			Application.Quit();
	}
}
