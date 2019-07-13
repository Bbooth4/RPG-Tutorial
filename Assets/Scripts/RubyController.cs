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
    
  Rigidbody2D rigidbody2d;

  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    currentHealth = maxHealth;
  }

  // Update is called once per frame
  void Update() {
    Vector2 position = rigidbody2d.position;
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    position.x = position.x + speed * horizontal * Time.deltaTime;
    position.y = position.y + speed * vertical * Time.deltaTime;
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
      isInvincible = true;
      invincibleTimer = timeInvincible;
    }
    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    Debug.Log(currentHealth + "/" + maxHealth);
  }
}
