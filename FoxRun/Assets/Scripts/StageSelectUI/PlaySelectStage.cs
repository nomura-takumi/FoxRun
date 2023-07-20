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
		//自分のステージ番号を取得する
		m_stage_index = Convert.ToInt32(this.name.Substring(Convert.ToInt32(this.name.IndexOf("_") + 1)));
	}

	public void OnClick()
	{
		//効果音
		this.transform.parent.GetComponent<AudioSource>().Play();

		//フェードアウト
		GameObject.Find("SYSTEM").GetComponent<FadeManager>().StartFadeOut();

		float time = 0;
		while (true) {
			time += Time.deltaTime;
			if(time >= 20.5f) {
				break;
			}
		}


		//シーン切り替え
		int active_scene_idx = SceneManager.GetActiveScene().buildIndex;
		try {
			//対象のステージのシーンをロード
			var async_load_scene = SceneManager.LoadSceneAsync(active_scene_idx + m_stage_index);
			async_load_scene.completed += (operation) => {
				if (!async_load_scene.isDone) {
					//シーンが存在しない
					throw new System.Exception();
				}
			};
		}
		catch (System.Exception e) {
			Debug.Log("シーンのリロードをします" + e.Message);
			SceneManager.LoadScene(active_scene_idx);
		}
	}
}
