using UnityEngine;
using UnityEngine.Events;
public class FloorButton : MonoBehaviour
{
	[SerializeField] private Renderer floorButtonRenderer;
	[SerializeField] private UnityEvent floorButtonEventEnter;
	[SerializeField] private UnityEvent floorButtonEventExit;
	private void OnTriggerEnter(Collider other)
	{
		floorButtonEventEnter.Invoke();
		floorButtonRenderer.material.color = Color.green;
	}
	private void OnTriggerExit(Collider other)
	{
		floorButtonEventExit.Invoke();
		floorButtonRenderer.material.color = Color.red;
	}
}
