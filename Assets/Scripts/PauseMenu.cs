using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	public string menuSceneName = "MainMenu";
	public SceneFader sceneFader;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		// open or close the menu
		ui.SetActive(!ui.activeSelf);
		// if the menu is open, pause the game by changing the time scale
		Time.timeScale = ui.activeSelf ? 0f : 1f;
	}

	public void Retry()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}
}
