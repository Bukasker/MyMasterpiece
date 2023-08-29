using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollistionUseResource : MonoBehaviour
{
	[SerializeField] private UseResourceSource useResourceSource;
	public GameObject Resource;
	private void OnTriggerEnter(Collider col)
	{
		{
			if (col.CompareTag("Selectable") == false) return;
			Resource = col.gameObject;
			useResourceSource.UseSource(Resource);
			
		}
	}
}
