using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour {
	[SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

	[SerializeField] string _androidAdUnitId = "Banner_Android";
	[SerializeField] string _iOSAdUnitId = "Banner_iOS";
	string _adUnitId = null; // これはサポートされていないプラットフォームでは null のままです。

	bool isShown = false;

	void Start()
	{
		isShown = false;
		Invoke("ShowBannerAd",2);

		// 現在のプラットフォームの広告ユニット ID を取得します：
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
		// バナーの位置を設定します：
		Advertisement.Banner.SetPosition(_bannerPosition);
	}
	
	// 「Load Banner」ボタンがクリックされると呼び出されるメソッドを実装します：
	public void LoadBanner()
	{
		// SDK にロードイベントを通知するオプションを設定します：
		BannerLoadOptions options = new BannerLoadOptions
		{
			loadCallback = OnBannerLoaded,
			errorCallback = OnBannerError
		};

		// バナーコンテンツ付きの広告ユニットをロードします：
		Advertisement.Banner.Load(_adUnitId, options);
	}

	// loadCallback イベントがトリガーされると実行されるコードを実装します：
	void OnBannerLoaded()
	{
		Debug.Log("Banner loaded");

		ShowBannerAd();
	}

	// errorCallback のロードイベントがトリガーされると実行されるコードを実装します：
	void OnBannerError(string message)
	{
		Debug.Log($"Banner Error: {message}");
		// オプションで追加のコードを実行します（別の広告のロードの試行など）。
	}

	// 「Show Banner」ボタンがクリックされると呼び出されるメソッドを実装します：
	void ShowBannerAd()
	{
		if(!isShown){
			// SDK に表示イベントを通知するオプションを設定します：
			BannerOptions options = new BannerOptions
			{
				clickCallback = OnBannerClicked,
				hideCallback = OnBannerHidden,
				showCallback = OnBannerShown
			};

			// ロードされたバナー広告ユニットを表示します：
			Advertisement.Banner.Show(_adUnitId, options);

			isShown = true;
		}
	}

	// 「Hide Banner」ボタンがクリックされると呼び出されるメソッドを実装します：
	public void HideBannerAd()
	{
		// バナーを非表示にします：
		Advertisement.Banner.Hide();
	}

	void OnBannerClicked() { }
	void OnBannerShown() { }
	void OnBannerHidden() { }

	void OnDestroy()
	{

	}
}