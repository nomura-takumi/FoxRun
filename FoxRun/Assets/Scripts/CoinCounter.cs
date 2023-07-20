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
	/// �����̃R�C���������擾
	/// </summary>
	public int GetTotalCoin()
	{
		return m_total_coin;
	}

	/// <summary>
	/// ���ݑ��݂���R�C���̌v��
	/// </summary>
	public int CalculateCoints()
	{
		return this.transform.childCount;
	}
}
