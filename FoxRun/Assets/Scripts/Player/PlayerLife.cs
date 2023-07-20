using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
	[Header("プレイヤーの初期ライフ")]
	[SerializeField] private int m_life_max = 3;
	private int m_life = 3;

	private LifeIcon m_LifeIcon_cs;
	private bool m_alive = true;

	// Start is called before the first frame update
	void Start()
    {
		m_life = m_life_max;

		m_LifeIcon_cs = GameObject.Find("Life").GetComponent<LifeIcon>();
	}

    // Update is called once per frame
    void Update()
    {
		if (!m_alive) {
			return;
		}

		if (m_life == 0) {
			//プレイヤー制御
			GameObject.FindWithTag("Player").GetComponent<Player>().FinishGame();
			
			//ゲーム終了処理
			GameObject.Find("SYSTEM").GetComponent<FinishGame>().Finish(FinishGame.FinishState.GAME_OVER);

			m_alive = false;

			return;
		}
	}


	public void SetLife(int life)
	{
		m_life = life;

		m_LifeIcon_cs.LifeIconOptimization();
	}

	public int GetLife()
	{
		return m_life;
	}
}
