using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour {
	[SerializeField] private float m_scroll_speed = 1.0f;

	private List<GameObject> m_aggregate_obj_list = new List<GameObject>();
	private int m_selecting_stage_number = 0;

	private Image m_RightButton_obj_image;
	private Image m_LeftButton_obj_image;

	//集約オブジェクトリスト中の要素0と要素1の距離を保存
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

		//ステージを格納
		foreach (Transform element in this.transform) {
			m_aggregate_obj_list.Add(element.gameObject);
		}

		//要素間の座標距離計算
		m_stage_distance = Mathf.Abs(m_aggregate_obj_list[0].transform.position.x - m_aggregate_obj_list[1].transform.position.x);
		
		//左ボタンの非表示
		CheckSelectingStageState();
	}

	/// <summary>
	/// 端のステージを選択中にどちらかのボタンを非表示にする
	/// </summary>
	private void CheckSelectingStageState()
	{
		if(m_selecting_stage_number == 0) {
			m_LeftButton_obj_image.enabled = false;
		}
		if(m_selecting_stage_number == m_aggregate_obj_list.Count - 1) {
			m_RightButton_obj_image.enabled = false;
		}
	}

	public IEnumerator Scroll(ScrollDirection direction)
	{
		//選択中のステージ番号
		if(direction == ScrollDirection.RIGHT) {
			m_selecting_stage_number++;
		}
		else {
			m_selecting_stage_number--;
		}

		//背景変更
		GameObject.Find("BackGround").GetComponent<ChangeStageSelectBG>().ChangeBG(m_selecting_stage_number);

		//ステージ開始の無効化(Stage_xオブジェクトのボタンを無効化)
		foreach (var element in m_aggregate_obj_list) {
			element.GetComponent<Button>().enabled = false;
		}

		//ボタン非表示
		m_RightButton_obj_image.enabled = false;
		m_LeftButton_obj_image.enabled = false;

		float move = 0.0f;

		while (true) {
			move += m_scroll_speed;

			//集約オブジェクトを移動させる
			if (direction == ScrollDirection.RIGHT) {
				this.transform.position -= new Vector3(m_scroll_speed, 0.0f, 0.0f);
			}
			else {
				this.transform.position += new Vector3(m_scroll_speed, 0.0f, 0.0f);
			}
			if (move >= m_stage_distance) {
				//ボタン再表示
				m_RightButton_obj_image.enabled = true;
				m_LeftButton_obj_image.enabled = true;

				CheckSelectingStageState();

				yield return new WaitForSecondsRealtime(0.5f);
				//ステージ開始の有効化(Stage_xオブジェクトのボタンを有効化)
				foreach (var element in m_aggregate_obj_list) {
					element.GetComponent<Button>().enabled = true;
				}

				yield break;
			}
			yield return null;
		}
	}
	/// <summary>
	/// ボタン操作によってステージ集約オブジェクトが左に動く
	/// </summary>
	public IEnumerator RightScroll()
	{
		float move = 0.0f;
		while (true) {
			move += m_scroll_speed;

			//集約オブジェクトを移動させる
			this.transform.position -= new Vector3(m_scroll_speed, 0.0f, 0.0f);

			if (move >= m_stage_distance) {
				yield break;
			}
			yield return null;
		}
	}

	/// <summary>
	/// ボタン操作によってステージ集約オブジェクトが右に動く
	/// </summary>
	public IEnumerator LeftScroll()
	{
		yield return new WaitForSecondsRealtime(0.1f);
	}
}