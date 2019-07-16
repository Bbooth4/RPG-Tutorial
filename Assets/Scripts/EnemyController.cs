using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour {
  public int maxHealth = 10;
  public float speed = 2.0f;
  public float changeTime = 1.0f;
  public bool vertical;
  float timer;
  int direction = 1;
  int currentHealth;

  public int enemyHealth { get { return currentHealth; } }

  Rigidbody2D rigidbody2D;
  Animator animator;
  
  void Start() {
    rigidbody2D = GetComponent<Rigidbody2D>();
    currentHealth = maxHealth;
    timer = changeTime;
    animator = GetComponent<Animator>();
  }


  void Update() {
    timer -= Time.deltaTime;

    if (timer < 0) {
      direction = -direction;
      timer = changeTime;
    }

    Vector2 position = rigidbody2D.position;

    if (vertical) {
      animator.SetFloat("Move X", 0);
      animator.SetFloat("Move Y", direction);
      position.y = position.y + Time.deltaTime * speed * direction;
    } else {
      animator.SetFloat("Move X", direction);
      animator.SetFloat("Move Y", 0);
      position.x = position.x + Time.deltaTime * speed * direction;
    }
    
    rigidbody2D.MovePosition(position);
  }

  void OnCollisionEnter2D(Collision2D other) {
    RubyController player = other.gameObject.GetComponent<RubyController>();

    if (player != null) {
      player.ChangeHealth(-1);
    }
  }
}
