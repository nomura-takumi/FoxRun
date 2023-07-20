using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStageSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public IEnumerator ReturnStageSelectScene()
	{
		//Œø‰Ê‰¹
		var TapText_obj = GameObject.FindWithTag("TapText");
		TapText_obj.GetComponent<AudioSource>().Play();

		//Œˆ’è‚Ìƒtƒ‰ƒbƒVƒ…
		TapText_obj.GetComponent<TextFlash>().StartDecisionFlash();

		var TextFlashDecision_cs = TapText_obj.GetComponent<TextFlashDecision>();
		StartCoroutine(TextFlashDecision_cs.StartFlash(false));

		yield return new WaitForSecondsRealtime(2.5f);
		SceneManager.LoadScene(1);
	}
}
