using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
	private void Start()
	{
		if(GameData.MazeX > 0)
			widthInput.text = GameData.MazeX.ToString();
		else
			widthInput.text = "20";

		if(GameData.MazeY > 0)
			heightInput.text = GameData.MazeY.ToString();
		else
			heightInput.text = "20";
	}

	public InputField widthInput;
	public InputField heightInput;

	public void StartButton()
	{
		int width = Int32.Parse(widthInput.text);
		int height = Int32.Parse(heightInput.text);

		GameData.MazeX = width;
		GameData.MazeY = height;

		SceneManager.LoadScene("Maze");
	}
}
