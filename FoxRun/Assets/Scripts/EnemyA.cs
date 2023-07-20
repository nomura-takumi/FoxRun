using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
	[SerializeField] private float m_move_speed = 1.0f;

	private bool m_is_found_player = false;
	private Rigidbody2D m_rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
		m_rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//プレイヤーが索敵範囲内に居る
		if (m_is_found_player) {
			m_rigidbody2D.velocity = new Vector2(-m_move_speed, 0.0f);
		}
    }

	/// <summary>
	/// プレイヤーと衝突
	/// </summary>
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			m_rigidbody2D.gravityScale = 0;
			Destroy(this.GetComponent<BoxCollider2D>());
		}
	}

	/// <summary>
	/// プレイヤーを発見した
	/// </summary>
	public void FoundPlayer()
	{
		m_is_found_player = true;
		Destroy(this.gameObject, 3.0f);
	}
}
