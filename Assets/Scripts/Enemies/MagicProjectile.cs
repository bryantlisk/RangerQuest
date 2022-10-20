using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour

    
{

    [SerializeField] float stayingSeconds; //How many seconds the projectile stays in the air for
    [SerializeField] float damage; //How much damage the projectile does to the player
    [SerializeField] float speed; //How fast the projectile travels
    Transform m_Transform;
    Rigidbody2D m_Rigidbody2D;
    CircleCollider2D m_Collider2D;
    Vector2 velocity;
    public Vector2 direction;
    Player player;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Collider2D = GetComponent<CircleCollider2D>();
        
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = velocity*Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided with something");
        player = other.gameObject.GetComponent<Player>();
        if(player != null && !player.invincible)
        {
            Debug.Log("working");
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void Launch()
    {
        velocity = speed * direction;
        Debug.Log(velocity);
        StartCoroutine("Fly");
    }

    IEnumerator Fly()
    {
        
        yield return new WaitForSeconds(stayingSeconds);
        Destroy(gameObject);
    }

 
}
