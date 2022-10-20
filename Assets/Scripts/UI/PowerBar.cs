using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{

    [SerializeField] Bow bow;
    [SerializeField] float barMaxSize;
    Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.localScale = new Vector3((bow.drawPower / bow.maxDrawPower) * barMaxSize, m_transform.localScale.y, m_transform.localScale.z);
    }
}
