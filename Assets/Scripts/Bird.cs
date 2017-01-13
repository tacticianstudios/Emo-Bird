using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public float upForce = 200f;
    private bool isDead = false;
    private bool hasStarted = false;
    private Rigidbody2D rb2d;
    private Animator anim;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.Sleep();
	}
	
	void Update () {
		if(!isDead )
        {
            if (hasStarted)
            {
                if ((Input.GetMouseButtonDown(0)
                    || Input.GetKeyDown(KeyCode.Space)
                    || Input.GetKeyDown(KeyCode.Joystick1Button0)
                    ))
                {

                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, upForce));
                    anim.SetTrigger("Flap");
                }
            }
            else
            {
                if ((Input.GetMouseButtonDown(0)
                     || Input.GetKeyDown(KeyCode.Space)
                     || Input.GetKeyDown(KeyCode.Joystick1Button0)
                     ))
                {
                    
                    GameController.instance.BirdTapped();
                    hasStarted = true;
                    rb2d.WakeUp();
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, upForce));
                    anim.SetTrigger("Flap");
                }
            }
        }
	}

    void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.BirdDie();
    }
}
