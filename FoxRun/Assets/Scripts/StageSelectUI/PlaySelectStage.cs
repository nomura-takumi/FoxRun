using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlaySelectStage : MonoBehaviour
{
	int m_stage_index = -1;

    // Start is called before the first frame update
    void Start()
    {
		//�����̃X�e�[�W�ԍ����擾����
		m_stage_index = Convert.ToInt32(this.name.Substring(Convert.ToInt32(this.name.IndexOf("_") + 1)));
	}

	public void OnClick()
	{
		//���ʉ�
		this.transform.parent.GetComponent<AudioSource>().Play();

		//�t�F�[�h�A�E�g
		GameObject.Find("SYSTEM").GetComponent<FadeManager>().StartFadeOut();

		float time = 0;
		while (true) {
			time += Time.deltaTime;
			if(time >= 20.5f) {
				break;
			}
		}


		//�V�[���؂�ւ�
		int active_scene_idx = SceneManager.GetActiveScene().buildIndex;
		try {
			//�Ώۂ̃X�e�[�W�̃V�[�������[�h
			var async_load_scene = SceneManager.LoadSceneAsync(active_scene_idx + m_stage_index);
			async_load_scene.completed += (operation) => {
				if (!async_load_scene.isDone) {
					//�V�[�������݂��Ȃ�
					throw new System.Exception();
				}
			};
		}
		catch (System.Exception e) {
			Debug.Log("�V�[���̃����[�h�����܂�" + e.Message);
			SceneManager.LoadScene(active_scene_idx);
		}
	}
}
