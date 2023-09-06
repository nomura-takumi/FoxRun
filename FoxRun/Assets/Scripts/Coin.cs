using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using NCMB;

public class Coin : MonoBehaviour
{
	int m_coin;
	Text m_text;
	private int m_temp_coin;

	[Header("スコアアップ、エフェクト")]
	[SerializeField] private GameObject m_coin_effect_point_obj = null;
	[SerializeField] private GameObject m_coin_up_effect_obj = null;
	[SerializeField] private float m_coin_up_effect_obj_delete_time = 1.0f;
	

	// Start is called before the first frame update
	void Start()
    {
		m_text = GetComponent<Text>();
		m_coin = 0;
    }

	public void AddCoin(int value)
	{
		m_temp_coin = m_coin + value;
		GameObject effect = Instantiate(m_coin_up_effect_obj, m_coin_effect_point_obj.transform.position, Quaternion.identity, this.transform);
		Destroy(effect, m_coin_up_effect_obj_delete_time);

		StartCoroutine(AddCoinAnimation());
		m_coin = m_temp_coin;
	}

	private IEnumerator AddCoinAnimation()
	{
		while (true) {
			m_coin += 1;			
			if (m_temp_coin <= m_coin) {
				m_coin = m_temp_coin;
				m_text.text = string.Format("{0:D2}", m_coin);
				yield break;
			}
			m_text.text = string.Format("{0:D2}", m_coin);

			yield return null;
		}
	}

	public void Store()
	{
		NCMBObject obj = new("HighScore");
		obj["Score"] = m_coin;
		obj.SaveAsync();
	}
}
