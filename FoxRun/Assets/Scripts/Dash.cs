using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	[Header("ダッシュの効力と時間")]
	[SerializeField] private float m_dash_move_force = 1.0f;
	[SerializeField] private float m_duration = 1.0f;

	[Header("プレイヤーに付けるエフェクト")]
	[SerializeField] private GameObject m_DashEffect_obj;
	[SerializeField] private Vector3 m_offset_position;
	[SerializeField] private float m_add_duration = 0.5f;

	[SerializeField] private GameObject m_DrinkEffect_obj;

	private float m_move_force;
	private Player m_Player_cs;
	
	// Start is called before the first frame update
	void Start()
    {
		m_Player_cs = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			//効果音
			this.GetComponent<AudioSource>().Play();

			//プレイヤーを加速
			m_move_force = m_Player_cs.GetMoveForce();
			m_Player_cs.SetMoveForce(m_dash_move_force);

			//加速エフェクト生成
			Vector3 pos = collision.transform.position + m_offset_position;
			var dash_effect = Instantiate(m_DashEffect_obj, pos, Quaternion.identity, collision.transform);
			Destroy(dash_effect, m_duration + m_add_duration);

			//取得エフェクト生成
			var drink_effect = Instantiate(m_DrinkEffect_obj, this.transform.position, Quaternion.identity, collision.transform);
			drink_effect.transform.position += new Vector3(-1.0f, 0.5f, 0.0f);
			Destroy(drink_effect, 0.6f);

			//透明化
			Destroy(this.GetComponent<SpriteRenderer>());


			StartCoroutine(SpeedUp());
		}
	}

	private IEnumerator SpeedUp()
	{
		yield return new WaitForSeconds(m_duration);
		m_Player_cs.SetMoveForce(m_move_force);
	}

}
