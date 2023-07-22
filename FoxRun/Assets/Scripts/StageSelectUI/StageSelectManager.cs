using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {
	[SerializeField] private float m_scroll_speed = 1.0f;
	[SerializeField] private GameObject m_Selecting_obj;

	private List<GameObject> m_aggregate_obj_list = new List<GameObject>();
	private int m_selecting_stage_element = 0;

	private Image m_RightButton_obj_image;
	private Image m_LeftButton_obj_image;

	//�W��I�u�W�F�N�g���X�g���̗v�f0�Ɨv�f1�̋�����ۑ�
	private float m_stage_distance = 0.0f;

	public enum ScrollDirection {
		RIGHT,
		LEFT,
	}

	// Start is called before the first frame update
	private void Start()
	{
		m_RightButton_obj_image = GameObject.Find("RightButton").GetComponent<Image>();
		m_LeftButton_obj_image = GameObject.Find("LeftButton").GetComponent<Image>();

		//�X�e�[�W���i�[
		foreach (Transform element in this.transform) {
			m_aggregate_obj_list.Add(element.gameObject);
		}

		//�v�f�Ԃ̍��W�����v�Z
		m_stage_distance = Mathf.Abs(m_aggregate_obj_list[0].transform.position.x - m_aggregate_obj_list[1].transform.position.x);
		
		//���{�^���̔�\��
		CheckSelectingStageState();
	}

	/// <summary>
	/// �[�̃X�e�[�W��I�𒆂ɂǂ��炩�̃{�^�����\���ɂ���
	/// </summary>
	private void CheckSelectingStageState()
	{
		if(m_selecting_stage_element == 0) {
			m_LeftButton_obj_image.enabled = false;
		}
		if(m_selecting_stage_element == m_aggregate_obj_list.Count - 1) {
			m_RightButton_obj_image.enabled = false;
		}
	}

	public IEnumerator Scroll(ScrollDirection direction)
	{
		//�I�𒆂̃X�e�[�W�ԍ�
		if (direction == ScrollDirection.RIGHT) {
			m_selecting_stage_element++;
		}
		else {
			m_selecting_stage_element--;
		}

		//�w�i�ύX
		GameObject.Find("BackGround").GetComponent<ChangeStageSelectBG>().ChangeBG(m_selecting_stage_element);

		//�X�e�[�W�J�n�̖�����(Stage_x�I�u�W�F�N�g�̃{�^���𖳌���)
		foreach (var element in m_aggregate_obj_list) {
			element.GetComponent<Button>().enabled = false;
		}

		//�{�^����\��
		m_RightButton_obj_image.enabled = false;
		m_LeftButton_obj_image.enabled = false;

		//�I�𒆃X�e�[�W�����I�u�W�F�N�g�폜
		Destroy(GameObject.FindWithTag("Selecting"));

		float move = 0.0f;

		while (true) {
			move += m_scroll_speed;

			//�W��I�u�W�F�N�g���ړ�������
			if (direction == ScrollDirection.RIGHT) {
				this.transform.position -= new Vector3(m_scroll_speed, 0.0f, 0.0f);
			}
			else {
				this.transform.position += new Vector3(m_scroll_speed, 0.0f, 0.0f);
			}
			if (move >= m_stage_distance) {
				//���W����
				this.transform.localPosition = new Vector3(250 * -m_selecting_stage_element, 0.0f, 0.0f);

				//�{�^���ĕ\��
				m_RightButton_obj_image.enabled = true;
				m_LeftButton_obj_image.enabled = true;

				//�I�𒆃X�e�[�W�����I�u�W�F�N�g�쐬
				var selecting_obj = Instantiate(m_Selecting_obj, this.transform.parent.position, Quaternion.identity, this.transform.parent.transform);
				selecting_obj.transform.SetSiblingIndex(1);

				CheckSelectingStageState();

				yield return new WaitForSecondsRealtime(0.5f);
				//�X�e�[�W�J�n�̗L����(Stage_x�I�u�W�F�N�g�̃{�^����L����)
				foreach (var element in m_aggregate_obj_list) {
					element.GetComponent<Button>().enabled = true;
				}
				
				yield break;
			}
			yield return null;
		}
	}
}