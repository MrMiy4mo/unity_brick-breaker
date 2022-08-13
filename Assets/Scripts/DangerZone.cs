using UnityEngine;

public class DangerZone : MonoBehaviour
{
	//top alt kenara çarptığında canı siler
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			FindObjectOfType<GameManager>().DropLive();
		}
	}
}
