using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchRange : MonoBehaviour
{
	private EnemyA m_EnemyA_cs;

    // Start is called before the first frame update
    void Start()
    {
		m_EnemyA_cs = this.transform.parent.GetComponent<EnemyA>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player") {
			m_EnemyA_cs.FoundPlayer();
		}
	}
}
