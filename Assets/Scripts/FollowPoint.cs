using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    Vector2 followPoint;
    Vector2 mousePos;
    Transform pos;
    Player player;
    [SerializeField] Camera cam;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        pos = GetComponent<Transform>();
        if(cam == null)cam = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            followPoint = (mousePos + (Vector2)player.transform.position) / 2; //midpoint
            followPoint = (followPoint + (Vector2)player.transform.position) / 2; //quarter point
            pos.position = new Vector3(followPoint.x, followPoint.y, 0);
            Debug.Log(pos.position);
        }
    }
}
