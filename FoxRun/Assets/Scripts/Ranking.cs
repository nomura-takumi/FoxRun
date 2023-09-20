using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour
{
	private List<NCMBObject> rankList	= new();
	private string sceneName = "";

	[SerializeField] private GameObject withinRankingText;

	private void Awake()
	{
		sceneName = SceneManager.GetActiveScene().name;
		sceneName = sceneName.Substring(3);
	}

	public void ShowRanking()
    {
		Text text = GetComponent<Text>();
		text.text = "";

		//スコア順に並べてランキング表示
		NCMBQuery<NCMBObject> query = new(sceneName);

		query.OrderByDescending("Score");
		query.Limit = 5;
		query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
			if (e != null) {

			}
			else {
				int rank = 1;

				foreach (NCMBObject obj in objList) {
					text.text += rank++.ToString() + ":" + obj["Score"] + Environment.NewLine;
					rankList.Add(obj);
				}
			}
		});

		//圏内判定
		CheckWithinRank();
	}

	private void CheckWithinRank()
	{
		int rank = 1;

		//最新のレコード順に並べ替えてランク内に入っているかチェック
		NCMBQuery<NCMBObject> query = new(sceneName);

		query.OrderByDescending("createDate");
		query.Limit = 1;
		query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
			if (e != null) {

			}
			else {
				var latestID    = objList[0].ObjectId;

				foreach (NCMBObject obj in rankList) {
					if (obj.ObjectId == latestID) {
						//表示
						var you = Instantiate(withinRankingText, Vector3.zero, Quaternion.identity, this.transform);

						RectTransform rectTransform = you.transform as RectTransform;
						rectTransform.anchoredPosition = new Vector2(65, (rank - 1) * -26); 
						break; 
					}
					else { rank++; }
				}
			}
		});
	}
}
