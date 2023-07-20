using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
	[Header("���C�t�\������I�u�W�F�N�g")]
	[SerializeField] private GameObject m_icon_obj;

	[Header("�A�C�R���̊Ԋu")]
	[SerializeField] private float m_icon_space = 25.0f;

	private PlayerLife m_PlayerLife_cs;

	List<GameObject> m_player_life_obj_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
		m_PlayerLife_cs = GameObject.FindWithTag("Player").GetComponent<PlayerLife>();

		//���C�t�\��
		LifeIconOptimization();
    }

	/// <summary>
	/// ���C�t�A�C�R���𐳂����\������
	/// example.) ���C�t��������Ƃ��A���C�t������Ƃ�
	/// </summary>
	public void LifeIconOptimization()
	{
		//�\������Ă�����̂�S�č폜
		foreach(var element in m_player_life_obj_list) {
			Destroy(element);
		}
		m_player_life_obj_list.Clear();

		//���݂̃��C�t�̏�Ԃ�\��
		for(int i = 0; i < m_PlayerLife_cs.GetLife(); i++) {
			var icon = Instantiate(m_icon_obj, new Vector2(this.transform.position.x + (i * m_icon_space), this.transform.position.y), Quaternion.identity, this.transform);
			m_player_life_obj_list.Add(icon);
		}
	}
}
