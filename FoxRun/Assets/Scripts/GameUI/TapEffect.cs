using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapEffect : MonoBehaviour
{
	[SerializeField] private GameObject tapEffectObj;
    private ParticleSystem  perticle;
    private Camera			camera;

	private GameObject perticleObj;

	private void Awake()
	{
		if (tapEffectObj) {
			perticleObj = Instantiate(tapEffectObj);
		}
		else{
			perticleObj = GameObject.Find("TapEffect");
			camera = GameObject.Find("TapEffectCamera").GetComponent<Camera>();
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        perticle = perticleObj.transform.GetChild(0).GetComponent<ParticleSystem>();

		if (tapEffectObj) {
			camera = perticleObj.transform.GetChild(1).gameObject.GetComponent<Camera>();
		}
    }

	private void Update()
	{

		var mouse = Mouse.current;
		if (mouse != null) {
			if (mouse.leftButton.wasPressedThisFrame) {
				var mousePos = mouse.position.ReadValue();
				perticle.transform.position = camera.ScreenToWorldPoint(camera.transform.forward * 10 + new Vector3(mousePos.x, mousePos.y, 0));
				perticle.Emit(1);
			}
		}
	}
}
