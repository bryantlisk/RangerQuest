using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Vector2 mousePos;
    [HideInInspector] public Vector2 positionRelativeToPlayer;
    Transform m_Transform;
    [SerializeField] public Transform player;
    [SerializeField] public Camera cam;
    [SerializeField] float moveRadius = 1f;
    [SerializeField] public float maxDrawPower;
    [SerializeField] public float drawSpeed;
    [SerializeField] public Transform arrowPoint;
    [SerializeField] Arrow arrow;
    [SerializeField] float knockbackFactor = 1f;
    [HideInInspector] public float drawPower = 0;
    GameManager gameManager;
    bool drawing = false;
    bool shooting = false;
    bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManager.isPaused)
        {

            moveAroundCircle();
            if (Input.GetMouseButtonDown(0))
            {
                drawing = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                drawing = false;
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        if (drawing)
            DrawBow();

    }

    void Shoot()
    {
        Debug.Log("Power: " + drawPower);
        shooting = true;
        Arrow newArrow = Object.Instantiate(arrow, arrowPoint);
        newArrow.Fire();
        player.GetComponent<Rigidbody2D>().AddForce(positionRelativeToPlayer * -1 * drawPower * knockbackFactor, ForceMode2D.Impulse); //knockback
        drawPower = 0;
    }
    void DrawBow()
    {
        if(drawPower < maxDrawPower)
            drawPower += drawSpeed*Time.fixedDeltaTime;
        if (drawPower > maxDrawPower)
            drawPower = maxDrawPower;
    }

    void moveAroundCircle()
    {
        mousePos = Input.mousePosition;
        positionRelativeToPlayer = mousePos - (Vector2)cam.WorldToScreenPoint(player.position);
        positionRelativeToPlayer.Normalize();
        m_Transform.rotation = Quaternion.Euler(m_Transform.eulerAngles.x,
            m_Transform.eulerAngles.y,
            Mathf.Atan(positionRelativeToPlayer.y / positionRelativeToPlayer.x) * Mathf.Rad2Deg);
        if (positionRelativeToPlayer.x < 0 && !facingRight)
            Flip();
        else if (positionRelativeToPlayer.x > 0 && facingRight)
            Flip();
        Vector2 center = player.position;
        m_Transform.position = center +  positionRelativeToPlayer * moveRadius;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = m_Transform.localScale;
        theScale.x *= -1;
        m_Transform.localScale = theScale;
    }
}
