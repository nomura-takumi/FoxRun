using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
	private int m_total_coin = 0;

    // Start is called before the first frame update
    void Start()
    {
		m_total_coin = CalculateCoints();
    }

	/// <summary>
	/// 初期のコイン枚数を取得
	/// </summary>
	public int GetTotalCoin()
	{
		return m_total_coin;
	}

	/// <summary>
	/// 現在存在するコインの計上
	/// </summary>
	public int CalculateCoints()
	{
		return this.transform.childCount;
	}
}
