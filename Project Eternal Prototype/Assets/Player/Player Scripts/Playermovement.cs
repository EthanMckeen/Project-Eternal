using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Camera cam;
	
	public GameObject speedTimerUI;

    Vector2 movement;

    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - -5f;

        rb.rotation = angle;
    }
		private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("OnTriggerEnter2D " + collision.gameObject.name + " " + this.name);
		
		if (collision.gameObject.CompareTag("PowerupSpeed"))		//Triggers if the player collides with object with tag "PowerupHealth", they regain health.
		{
			Destroy(collision.gameObject);	
			moveSpeed = moveSpeed * 1.5f;
			
		}
	}
	
}
