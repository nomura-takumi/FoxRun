using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
	[Header("ボーナスによる加算スコア")]
	[SerializeField] private List<int> m_bonus_score_list = new List<int>();

	private List<string> m_rank_list = new List<string>();
	private Dictionary<string, int> m_rank_and_score_dic = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
		//ランク
		m_rank_list.Add("S");
		m_rank_list.Add("A");
		m_rank_list.Add("B");
		m_rank_list.Add("C");
    }

	/// <summary>
	/// ランクとボーナススコア決定
	/// </summary>
	public Dictionary<string, int> JudgeRank()
	{
		return m_rank_and_score_dic;
	}
}
