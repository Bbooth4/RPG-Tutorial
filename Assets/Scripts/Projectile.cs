using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour {
  Rigidbody2D rigidbody2D;

  void Awake() {
    rigidbody2D = GetComponent<Rigidbody2D>();
  }

  void Update() {
    
  }

  public void Launch(Vector2 direction, float force) {
    rigidbody2D.AddForce(direction * force);
  }

  void OnCollisionEnter2D(Collision2D other) {
    // we also add a debug log to know what the projectile touch
    Debug.Log("Projectile Collision with " + other.gameObject);
    Destroy(gameObject);
  }
}
