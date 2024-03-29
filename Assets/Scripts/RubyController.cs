﻿using System.Collections;
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

  public GameObject projectilePrefab;
  public int health { get { return currentHealth; } }
    
  Vector2 lookDirection = new Vector2(1,0);
  Rigidbody2D rigidbody2D;
  Animator animator;

  void Start() {
    rigidbody2D = GetComponent<Rigidbody2D>();
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

    Vector2 position = rigidbody2D.position;
    position = position + move * speed * Time.deltaTime;
    rigidbody2D.MovePosition(position);

    if (isInvincible) {
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer < 0) isInvincible = false;
    }

    if (Input.GetKeyDown(KeyCode.C)) {
      Launch();
    }

    if (Input.GetKeyDown(KeyCode.X)) {
      RaycastHit2D hit = Physics2D.Raycast(
        rigidbody2D.position + Vector2.up * 0.2f,
        lookDirection,
        1.5f,
        LayerMask.GetMask("NPC")
      );
      if (hit.collider != null) {
        NPC character = hit.collider.GetComponent<NPC>();
        Debug.Log("Hit");
        if (character != null) {
          Debug.Log("Character");
          character.DisplayDialog();
        }
      }
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
    UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
  }

  void Launch() {
    GameObject projectileObject = Instantiate(
      projectilePrefab,
      rigidbody2D.position + Vector2.up * 0.5f,
      Quaternion.identity
    );

    Projectile projectile = projectileObject.GetComponent<Projectile>();
    projectile.Launch(lookDirection, 300f);

    animator.SetTrigger("Launch");
  }
}
