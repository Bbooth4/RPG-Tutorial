using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController: MonoBehaviour {
  // Start is called before the first frame update
  public int maxHealth = 10;
  public float timeInvincible = 2.0f;
  public float speed = 3.0f;
  int currentHealth;
  bool isInvincible;
  float invincibleTimer;

  public int health { get { return currentHealth; } }
    
  Vector2 lookDirection = new Vector2(1,0);
  Rigidbody2D rigidbody2d;
  Animator animator;

  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    currentHealth = maxHealth;
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    Vector2 move = new Vector2(horizontal, vertical);

    if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
      lookDirection.Set(move.x, move.y);
      lookDirection.Normalize();
    }

    animator.SetFloat("Look X", lookDirection.x);
    animator.SetFloat("Look Y", lookDirection.y);
    animator.SetFloat("Speed", move.magnitude);

    Vector2 position = rigidbody2d.position;
    position = position + move * speed * Time.deltaTime;
    rigidbody2d.MovePosition(position);

    if (isInvincible) {
      Debug.Log(isInvincible);
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer < 0) isInvincible = false;
    }
  }

  public void ChangeHealth(int amount) {
    if (amount < 0) {
      if (isInvincible) return;
      animator.SetTrigger("Hit");
      isInvincible = true;
      invincibleTimer = timeInvincible;
    }
    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    Debug.Log(currentHealth + "/" + maxHealth);
  }
}
