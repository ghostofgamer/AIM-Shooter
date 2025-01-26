using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecalDestroyer : MonoBehaviour {

	public float lifeTime = 5.0f;

	private Coroutine _coroutine;
	
	private void OnEnable()
	{
		if(_coroutine != null)
			StopCoroutine(_coroutine);

		_coroutine = StartCoroutine(TurnOff());
	}

	private IEnumerator TurnOff()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
		// Destroy(gameObject);
	}
}
