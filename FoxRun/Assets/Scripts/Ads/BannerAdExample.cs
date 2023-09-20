using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour {
	[SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

	[SerializeField] string _androidAdUnitId = "Banner_Android";
	[SerializeField] string _iOSAdUnitId = "Banner_iOS";
	string _adUnitId = null; // ����̓T�|�[�g����Ă��Ȃ��v���b�g�t�H�[���ł� null �̂܂܂ł��B

	bool isShown = false;

	void Start()
	{
		isShown = false;
		Invoke("ShowBannerAd",2);

		// ���݂̃v���b�g�t�H�[���̍L�����j�b�g ID ���擾���܂��F
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
		// �o�i�[�̈ʒu��ݒ肵�܂��F
		Advertisement.Banner.SetPosition(_bannerPosition);
	}
	
	// �uLoad Banner�v�{�^�����N���b�N�����ƌĂяo����郁�\�b�h���������܂��F
	public void LoadBanner()
	{
		// SDK �Ƀ��[�h�C�x���g��ʒm����I�v�V������ݒ肵�܂��F
		BannerLoadOptions options = new BannerLoadOptions
		{
			loadCallback = OnBannerLoaded,
			errorCallback = OnBannerError
		};

		// �o�i�[�R���e���c�t���̍L�����j�b�g�����[�h���܂��F
		Advertisement.Banner.Load(_adUnitId, options);
	}

	// loadCallback �C�x���g���g���K�[�����Ǝ��s�����R�[�h���������܂��F
	void OnBannerLoaded()
	{
		Debug.Log("Banner loaded");

		ShowBannerAd();
	}

	// errorCallback �̃��[�h�C�x���g���g���K�[�����Ǝ��s�����R�[�h���������܂��F
	void OnBannerError(string message)
	{
		Debug.Log($"Banner Error: {message}");
		// �I�v�V�����Œǉ��̃R�[�h�����s���܂��i�ʂ̍L���̃��[�h�̎��s�Ȃǁj�B
	}

	// �uShow Banner�v�{�^�����N���b�N�����ƌĂяo����郁�\�b�h���������܂��F
	void ShowBannerAd()
	{
		if(!isShown){
			// SDK �ɕ\���C�x���g��ʒm����I�v�V������ݒ肵�܂��F
			BannerOptions options = new BannerOptions
			{
				clickCallback = OnBannerClicked,
				hideCallback = OnBannerHidden,
				showCallback = OnBannerShown
			};

			// ���[�h���ꂽ�o�i�[�L�����j�b�g��\�����܂��F
			Advertisement.Banner.Show(_adUnitId, options);

			isShown = true;
		}
	}

	// �uHide Banner�v�{�^�����N���b�N�����ƌĂяo����郁�\�b�h���������܂��F
	public void HideBannerAd()
	{
		// �o�i�[���\���ɂ��܂��F
		Advertisement.Banner.Hide();
	}

	void OnBannerClicked() { }
	void OnBannerShown() { }
	void OnBannerHidden() { }

	void OnDestroy()
	{

	}
}