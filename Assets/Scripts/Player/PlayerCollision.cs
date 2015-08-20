using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public int health = 100;
	public float timeBetweenDamage = 1f;
	public float restartDelay = 1f;
	public AudioClip audioclipDamage;
	public AudioClip audioclipDeath;

	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.2f);
	public Text healthText;

	private AudioSource audiosource;
	private float timer;
	private float restartTimer;
	private bool dead;
	private bool damaged;

	void Start()
	{
		dead = false;
		timer = 0;
		audiosource = GetComponent<AudioSource> ();
		UpdateDamageText ();
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (health <= 0) {
			GameOver();
		}
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy") && timer >= timeBetweenDamage)
		{
			Hit (10);			
		}
	}

	void Hit(int damage)
	{
		timer = 0;
		health -= damage;

		UpdateDamageText ();
		audiosource.clip = audioclipDamage;
		audiosource.Play ();
		damaged = true;
	}

	void GameOver()
	{
		if (!dead) {
			audiosource.clip = audioclipDeath;
			audiosource.Play ();
		}
		dead = true;
		restartTimer += Time.deltaTime;

		if (restartTimer >= restartDelay) {
			Application.LoadLevel ("GameOver");
		}
	}

	void UpdateDamageText()
	{
		if (health < 0) {
			health = 0;
		}
		healthText.text = "Health: " + health;
	}
}
