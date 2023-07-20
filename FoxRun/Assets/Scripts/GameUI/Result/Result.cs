using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
	[Header("�{�[�i�X�ɂ����Z�X�R�A")]
	[SerializeField] private List<int> m_bonus_score_list = new List<int>();

	private List<string> m_rank_list = new List<string>();
	private Dictionary<string, int> m_rank_and_score_dic = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
		//�����N
		m_rank_list.Add("S");
		m_rank_list.Add("A");
		m_rank_list.Add("B");
		m_rank_list.Add("C");
    }

	/// <summary>
	/// �����N�ƃ{�[�i�X�X�R�A����
	/// </summary>
	public Dictionary<string, int> JudgeRank()
	{
		return m_rank_and_score_dic;
	}
}
