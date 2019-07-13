using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard: MonoBehaviour {
  void OnTriggerStay2D(Collider2D other) {
    RubyController ruby = other.GetComponent<RubyController>();
    if (ruby != null && ruby.health > 0) {
      ruby.ChangeHealth(-1);
    }
  }
}
