using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	public Text livesText;
	
	private void Update()
	{
		livesText.text = PlayerStats.lives + (PlayerStats.lives < 2 ? " LIFE" : " LIVES");
	}
}
