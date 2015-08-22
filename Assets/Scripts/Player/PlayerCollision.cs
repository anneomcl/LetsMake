using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public int health = 100;
	public float timeBetweenDamage = 1f;
	public float restartDelay = 1f;

	public AudioClip audioclipDamageFemale;
	public AudioClip audioclipDeathFemale;
	public AudioClip audioclipDamageMale;
	public AudioClip audioclipDeathMale;
	
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.2f);
	public Text healthText;

	private AudioSource audiosource;
	private float timer;
	private float restartTimer;
	private bool dead;
	private bool damaged;

	private string gender;

	void Start()
	{
		dead = false;
		timer = 0;
		audiosource = GetComponent<AudioSource> ();
		UpdateDamageText ();
		gender = PlayerPrefs.GetString("gender", "girl");
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
		if (gender == "girl") {
			audiosource.clip = audioclipDamageFemale;
		} else {
			audiosource.clip = audioclipDamageMale;
		}

		audiosource.Play ();
		damaged = true;
	}

	void GameOver()
	{
		if (!dead) {
			if (gender == "girl") {
				audiosource.clip = audioclipDeathFemale;
			} else {
				audiosource.clip = audioclipDeathMale;
			}
			audiosource.Play ();
		}
		dead = true;
		restartTimer += Time.deltaTime;

		if (restartTimer >= restartDelay) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("GameOver");
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
