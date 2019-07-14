using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour {
  public int maxHealth = 10;
  public float speed = 2.0f;
  public bool vertical;
  public float changeTime = 2.0f;
  float timer;
  int direction = 1;
  int currentHealth;

  public int enemyHealth { get { return currentHealth; } }

  Rigidbody2D rigidbody2D;
  
  void Start() {
    rigidbody2D = GetComponent<Rigidbody2D>();
    currentHealth = maxHealth;
    timer = changeTime;
  }


  void Update() {
    Debug.Log(timer);
    if (timer < 0) {
      direction = -direction;
      timer = changeTime;
    }

    Vector2 position = rigidbody2D.position;

    if (vertical) {
      position.y = position.y + Time.deltaTime * speed;
    } else {
      position.x = position.x + Time.deltaTime * speed;
    }
    
    rigidbody2D.MovePosition(position);
  }
}
