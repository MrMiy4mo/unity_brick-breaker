using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public Ball ball { get; private set; }
	public Paddle paddle{ get; private set; }
	public Brick[] bricks { get; private set; }
	
	public int level = 1;
	public int score = 0;
	public int lives = 3;
	
	private Text score_Text;
	private Text lives_Text;
	private Text level_Text;
	
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		
		SceneManager.sceneLoaded += OnLevelLoaded;
	}
	
	private void Start()
	{
		NewGame();
		score_Text = GameObject.Find("Score").GetComponent<Text>();	
		lives_Text = GameObject.Find("Lives").GetComponent<Text>();	
		level_Text = GameObject.Find("Level").GetComponent<Text>();	
	}
	
	private void NewGame()
	{
		this.score = 0;
		this.lives = 3;
		
		LoadLevel(1);
	}
	
	private void LoadLevel(int level)
	{
		//Seviye tamamlandığında bir sonraki seviyeyi görüntüleyen metot
		//Seviye 5 tamamlandığında oyunun bittiğini belirten sahneyi görüntüle
		this.level = level;
		if(level > 5) {
			SceneManager.LoadScene("LevelsCompleted");
		} else {
			SceneManager.LoadScene("Level" + level);
		}
	}
	
	private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		this.ball = FindObjectOfType<Ball>();
		this.paddle = FindObjectOfType<Paddle>();
		this.bricks = FindObjectsOfType<Brick>();
	}
	
	// Can tükendiğinde top ve pedal ın konumunu sıfırlayan metot
	public void ResetLevel() 
	{
		this.ball.ResetBall();
		this.paddle.ResetPaddle();		
	}
	
	//Tüm canlar tükendiğinde oyunun bittiğinin belirten sahneyi görüntüleyen metot
	public void GameOver()
	{
		SceneManager.LoadScene("GameOver");
		
	}
	
	//top alt kenara çarptığında can eksilten metot
	public void DropLive()
	{
		this.lives--;
		lives_Text.text = "Can: " + this.lives;
		
		if (this.lives > 0) 
		{
			ResetLevel();
		} else 
		{
			GameOver();
		}
	}
	
	//top tuğlaya çarptığında puan arttıran metot
	//tüm tuğlalar bittiğinde bir sonraki seviyeyi görüntüle
	public void Hit(Brick brick)
	{
		this.score += brick.points;
		
		if (Cleared()) {
			this.lives++;
			lives_Text.text = "Can: " + this.lives;
			LoadLevel(this.level + 1);
			level_Text.text = "Seviye: " + this.level;
		}
		
		score_Text.text = "Skor: " + this.score;
	}
	
	//sahnede tuğla kalma durumunu bildiren metot
	private bool Cleared()
	{
		for (int i = 0; i < this.bricks.Length; i++)
		{
			if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable){
				return false;
			}
		}
		return true;
	}

}
