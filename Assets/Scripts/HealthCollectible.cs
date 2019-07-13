using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible: MonoBehaviour {
  void OnTriggerEnter2D(Collider2D other) {
    RubyController ruby = other.GetComponent<RubyController>();

    if (ruby != null && ruby.health < ruby.maxHealth) {
      ruby.ChangeHealth(2);
      // gameObject is always whatever object this method is attached to
		  Destroy(gameObject);
    }
  }
}
