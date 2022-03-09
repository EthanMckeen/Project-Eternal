using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
	
	public int curHealth = 0;
    public int maxHealth = 100;
	
	
	public HealthBar healthBar;
	
	
	public GameObject gameOverUI;
	bool gameOver;

	

    // Start is called before the first frame update
    void Awake()
    {
		gameOver = false;
    }
	
	void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
		if( Input.GetKeyDown( KeyCode.Space ) )		//Tests to see if the player takes damage
		{
			DamagePlayer(7);
		}
    }


	void GameOver()									//Switches to the the Game Over state.
	{
		gameOver = true;
		gameOverUI.SetActive(true);
		Time.timeScale = 0f;	
	}
	
	
	
	public void DamagePlayer( int damage )			//This will apply damage to the player's health.
	{
		curHealth -= damage;
		
		healthBar.SetHealth( curHealth );
		
		if (curHealth <= 0)							//This will destroy the player when their health reaches 0
		{
			GameOver();
		}
	}
	
		
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("OnTriggerEnter2D " + collision.gameObject.name + " " + this.name);
		
		if (collision.gameObject.CompareTag("PowerupHealth") && (curHealth < 100))		//Triggers if the player collides with object with tag "PowerupHealth", they regain health.
		{
			Destroy(collision.gameObject);	
			DamagePlayer(-15);			
		}
	}
}
