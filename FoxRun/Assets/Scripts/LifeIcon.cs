using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
	[Header("ライフ表示するオブジェクト")]
	[SerializeField] private GameObject m_icon_obj;

	[Header("アイコンの間隔")]
	[SerializeField] private float m_icon_space = 25.0f;

	private PlayerLife m_PlayerLife_cs;

	List<GameObject> m_player_life_obj_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
		m_PlayerLife_cs = GameObject.FindWithTag("Player").GetComponent<PlayerLife>();

		//ライフ表示
		LifeIconOptimization();
    }

	/// <summary>
	/// ライフアイコンを正しく表示する
	/// example.) ライフが増えるとき、ライフが減るとき
	/// </summary>
	public void LifeIconOptimization()
	{
		//表示されているものを全て削除
		foreach(var element in m_player_life_obj_list) {
			Destroy(element);
		}
		m_player_life_obj_list.Clear();

		//現在のライフの状態を表示
		for(int i = 0; i < m_PlayerLife_cs.GetLife(); i++) {
			var icon = Instantiate(m_icon_obj, new Vector2(this.transform.position.x + (i * m_icon_space), this.transform.position.y), Quaternion.identity, this.transform);
			m_player_life_obj_list.Add(icon);
		}
	}
}
