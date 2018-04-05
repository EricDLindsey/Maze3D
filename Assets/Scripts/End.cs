using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
	private void Start()
	{
		transform.position = new Vector3((GameData.MazeX - 1) * 2, 0, (GameData.MazeY - 1) * 2);
	}

	public PlayerMove player;
	public GameObject panel;

	public void GoMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponentInParent<Rigidbody>().tag == "Player")
		{
			player.CanMove = false;
			panel.SetActive(true);
		}
	}
}
