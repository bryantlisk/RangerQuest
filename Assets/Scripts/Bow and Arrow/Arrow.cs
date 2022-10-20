using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Bow bow;
    [SerializeField] float maxDamage = 50f;
    [SerializeField] Transform launchForcePoint;
    [SerializeField] Transform centerOfMass;
    GameObject buffer;
    Vector2 positionRelativeToPlayer;
    Vector2 mousePos;
    private Vector2 velocity;
    Rigidbody2D m_RigidBody2D;
    Transform m_Transform;
    SpriteRenderer m_SpriteRenderer;
    Transform spawnPoint;
    bool fired = false;
    bool drawing = false;
    bool flying = false;
    bool landed = false;
    float bowPower;
    Vector2 lastVelocity;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        m_Transform = GetComponent<Transform>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.enabled = true;
        velocity = new Vector2(0, 0);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), bow.player.GetComponent<PolygonCollider2D>());
        
        
    }

    public void Fire()
    {
        bowPower = bow.drawPower;
        fired = true;
        flying = true;
        m_RigidBody2D.simulated = true;
        m_Transform.parent = null;
        m_RigidBody2D.centerOfMass = centerOfMass.localPosition;
        
        m_RigidBody2D.AddForceAtPosition(bow.positionRelativeToPlayer * bowPower, launchForcePoint.position, ForceMode2D.Impulse);
        Debug.Log(positionRelativeToPlayer * bowPower);
    }



    void FixedUpdate()
    {
        if (fired && flying)
        {
            lastVelocity = velocity;
            float angle;
            if (Mathf.Abs(m_RigidBody2D.velocity.x) > 0)
                angle = Mathf.Atan(m_RigidBody2D.velocity.y / m_RigidBody2D.velocity.x) * Mathf.Rad2Deg;
            else
                angle = 90f;
            m_Transform.rotation = Quaternion.Euler(m_Transform.eulerAngles.x, m_Transform.eulerAngles.y, angle);
            velocity = m_RigidBody2D.velocity;
           

        }
    }

    void DamageTarget(Collision2D collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        float damage = Mathf.Lerp(0, maxDamage, (bowPower / bow.maxDrawPower));
        enemy.TakeDamage(damage);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        float angle;
        if (Mathf.Abs(lastVelocity.x) > 0)
        {
            angle = Mathf.Atan(lastVelocity.y / lastVelocity.x) * Mathf.Rad2Deg;
        }
        else if (lastVelocity.x == 0 && lastVelocity.y == 0)
        {
            angle = m_Transform.eulerAngles.z;
        }
        else
        {
            angle = -90f;
        }

        m_Transform.rotation = Quaternion.Euler(m_Transform.eulerAngles.x, m_Transform.eulerAngles.y, angle);
        m_RigidBody2D.simulated = false;
       
        if(collision.gameObject.tag == "Enemy")
        {
            DamageTarget(collision);
        }
        
        flying = false;
        buffer = new GameObject();
        buffer.transform.parent = collision.transform.Find("buffer");
        if (buffer.transform.parent == null)
        {
            buffer = new GameObject("buffer");
            buffer.transform.parent = collision.transform;
        }
        m_Transform.parent = buffer.transform;
    }
}
