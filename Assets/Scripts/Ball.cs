using UnityEngine;

public class Ball : MonoBehaviour 
{
	public new Rigidbody2D rigidbody { get; private set; }
	public float speed = 15f;
	
	private void Awake()
	{
		this.rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void Start()
	{
		ResetBall();
	}
	
	//can tükendiğinde topun konumunu sıfırlar
	public void ResetBall()
	{
		this.transform.position = Vector2.zero;
		this.rigidbody.velocity = Vector2.zero;
		Invoke(nameof(SetRandomTrajectory), 1f);
	}
	
	//oyun başladığında topun ilk olarak hangi yöne hareket edeceğini belirler
	private void SetRandomTrajectory()
	{
		Vector2 force = Vector2.zero;
		force.x = Random.Range(-1f,1f);
		force.y = -1f;
		
		this.rigidbody.AddForce(force.normalized * this.speed);
	}
	
	private void FixedUpdate()
{
    rigidbody.velocity = rigidbody.velocity.normalized * speed;
}
	
}
