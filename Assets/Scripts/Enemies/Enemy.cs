using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] public float maxHealth = 100;
    public float health;
    [Range(0,1)][SerializeField] public float resistance = 0f; //percent damage reduction between 1 and 100
    CharacterController2D m_CharacterController2D;
    Rigidbody2D m_Rigidbody2D;
    [SerializeField] public float damage;
    [SerializeField] float speed = 40f;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange = 10f;
    [SerializeField] float closestFollowDistanceToPlayer = 2f; //make sure this is less than the attack range
    [SerializeField] float targetRange;
    [SerializeField] float attackCooldown;
    [SerializeField] Color windUpColor;
    [SerializeField] Color attackingColor;
    [SerializeField] HealthBar healthBar;
    [SerializeField] int scoreValue;
    [SerializeField] GameObject magic;
    [SerializeField] float projectileRange;
    [SerializeField] float projectileCooldownMin;
    [SerializeField] float projectileCooldownMax;
    Canvas canvas;
    float canvasScale;
    Color originalColor;
    SpriteRenderer m_SpriteRenderer;
    Transform m_Transform;
    Transform target;
    Player player;
    LevelController levelController;
    Score score;
    float distanceToPlayer;
    Vector2 targetDirection;
    float absoluteMovementDirectionX;
    bool onProjectileCooldown = true;
    float baseScale;
    
    
    bool alive = true;
    bool onCooldown = false;
    bool jump = false;
    bool movingRight;



    void OnEnable()
    {
        player = FindObjectOfType<Player>();
        levelController = FindObjectOfType<LevelController>();
        score = FindObjectOfType<Score>();
        target = player.GetComponent<Transform>();
        health = maxHealth;
        healthBar.SetMaxHealth(health);
        m_CharacterController2D = GetComponent<CharacterController2D>();
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale.x;
        originalColor = m_SpriteRenderer.color;
        canvas = GetComponentInChildren<Canvas>();
        canvasScale = canvas.transform.localScale.x;
        StartCoroutine("ProjectileCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (target.position - m_Transform.position).normalized;
        absoluteMovementDirectionX = targetDirection.x < 0 ? -1 : 1;
        distanceToPlayer = (target.position - m_Transform.position).magnitude;
        if (!onCooldown && distanceToPlayer < attackRange)
        {
            Attack();
        }
        if(!onProjectileCooldown && distanceToPlayer < projectileRange)
        {
            FireMagic();
            StartCoroutine("ProjectileCooldown");
        }
        if(health <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs((target.position.x - m_Transform.position.x)) > closestFollowDistanceToPlayer &&
            distanceToPlayer < targetRange)
        { 
           this.Move(absoluteMovementDirectionX * speed * Time.fixedDeltaTime, jump); 
        }
    }

    void Die()
    {
        levelController.IncreaseScore(scoreValue);
        this.gameObject.SetActive(false);
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount*(1-resistance);
        healthBar.SetHealth(health);
    }

    void Attack()
    {

        //Animation.play();
        StartCoroutine("StartAttack");
    }

    void FireMagic()
    {
        GameObject newProjectile = Instantiate(magic, transform.position, Quaternion.identity);
        MagicProjectile magicProjectile = newProjectile.GetComponent<MagicProjectile>();
        magicProjectile.direction = targetDirection;
        magicProjectile.Launch();

    }


    void Move(float movement, bool jump)
    {
        m_Rigidbody2D.AddForce(new Vector2(movement, 0));
        if(movement < 0)
        {
            transform.localScale = new Vector3(-baseScale, transform.localScale.y, transform.localScale.z);
            canvas.transform.localScale = new Vector3(-canvasScale, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(baseScale, transform.localScale.y, transform.localScale.z);
            canvas.transform.localScale = new Vector3(canvasScale, canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
        Debug.Log("Movement value:" + movement);
    }

    //Placeholder until actual animations are working
    IEnumerator StartAttack()
    {
        onCooldown = true;
        m_SpriteRenderer.color = windUpColor;
        yield return new WaitForSeconds(1/attackSpeed);
        m_SpriteRenderer.color = attackingColor;
        yield return new WaitForSeconds(.1f);
        if(distanceToPlayer < attackRange)
            player.TakeDamage(damage);
        m_SpriteRenderer.color = originalColor;
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }

    IEnumerator ProjectileCooldown()
    {
        onProjectileCooldown = true;
        float timeToNextAttack = Random.Range(projectileCooldownMin, projectileCooldownMax);
        yield return new WaitForSeconds(timeToNextAttack);
        onProjectileCooldown = false;
    }

    IEnumerator DecideAttack()
    {
        float timeToNextAttack = Random.Range(0f, 2f);
        yield return new WaitForSeconds(timeToNextAttack);
    }


   
}
