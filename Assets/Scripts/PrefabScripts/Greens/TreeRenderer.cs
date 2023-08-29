using UnityEngine;

public class TreeRenderer : MonoBehaviour
{
	[Header("Tree sorting parts")]
	[SerializeField] private Renderer topPartRenderer;
	[SerializeField] private Renderer bottomPartRenderer;

	private void Update()
	{
		if (topPartRenderer && bottomPartRenderer)
		{
			if (topPartRenderer.bounds.min.y < bottomPartRenderer.bounds.max.y)
			{
				topPartRenderer.sortingOrder = bottomPartRenderer.sortingOrder + 1;
			}
			else
			{
				bottomPartRenderer.sortingOrder = topPartRenderer.sortingOrder + 1;
			}
		}
	}
}