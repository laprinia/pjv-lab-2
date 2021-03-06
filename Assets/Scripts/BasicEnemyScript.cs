﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemyScript : MonoBehaviour
{
    private bool allowFire = true;
    private bool isFacingRight = true;
    public Animator Animator;
    public Transform Player;
    public float MoveSpeed = 0.4f;
    public int maxDistanceToShoot = 5;
    public int minDistanceToFollow = 2;
    private float horizontalMove = 0f;
    public bool hasProjectiles = false;
    public GameObject projectile;
    public Transform shootPoint;

   

    void Update()
    {
        if (Player.transform.position.x > transform.position.x && !isFacingRight)
            Flip();
        if (Player.transform.position.x < transform.position.x && isFacingRight)
            Flip();

        horizontalMove = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        Animator.SetFloat("Speed", Math.Abs(horizontalMove));
        Vector2 playerXAxis;
        playerXAxis = new Vector2(Player.position.x, Player.position.y);

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer >= minDistanceToFollow && distanceToPlayer <= 10)    
        {
            transform.position = Vector2.MoveTowards(transform.position, playerXAxis, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Player.position) <= maxDistanceToShoot)
            {
                Animator.SetInteger("State", 1);
                if (hasProjectiles && allowFire)
                {
                    StartCoroutine(Shoot());
                }
            }
            else Animator.SetInteger("State", 0);
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }

    IEnumerator Shoot()
    {
        allowFire = false;
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(1f);
        allowFire = true;
    }
}