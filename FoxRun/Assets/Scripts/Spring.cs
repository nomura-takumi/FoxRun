using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
	[SerializeField] private float m_spring_jump_force = 1.0f;
	[SerializeField] private float m_spring_move_force = 1.0f;
	[SerializeField] private float m_duration = 1.0f;
	[SerializeField] private Animator m_animator;

	private float m_move_force;
	private Player m_player;

    // Start is called before the first frame update
    void Start()
    {
		m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			m_move_force = m_player.GetMoveForce();
			m_player.SetMoveForce(m_spring_move_force);
			m_player.SetStateJump(m_spring_jump_force, true);

			m_animator.SetBool("Down", true);

			StartCoroutine(SpringJumping());
		}
	}

	private IEnumerator SpringJumping()
	{
		yield return new WaitForSecondsRealtime(m_duration);

		m_player.SetMoveForce(m_move_force);
	}
}
