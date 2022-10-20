using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    public float health;
    [SerializeField] HealthBar healthBar;
    [SerializeField] float invincibilityTime;
    public bool invincible;
    GameManager gameManager;
    Bow bow;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = FindObjectOfType<GameManager>();
        bow = FindObjectOfType<Bow>();
    }

    public void TakeDamage(float amount)
    {
        if (!invincible)
        {
            health -= amount;
            healthBar.SetHealth(health);
            StartCoroutine("InvincibilityTime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameManager.GameOver();
        bow.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator InvincibilityTime()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
