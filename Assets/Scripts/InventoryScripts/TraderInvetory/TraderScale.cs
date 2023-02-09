using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TraderScale : MonoBehaviour
{
	private bool leftPlateLaden;
	private bool rightPlateLaden;

	[SerializeField] private Image leftItemIcon;
	[SerializeField] private Image rightItemIcon;
	[SerializeField] private Sprite goldIcon;

	public PlayerStats playerStats;
	private Inventory instace;
	public Animator animator;
	private Item Item;
	private int Weight;
	private bool IsBuying;
	public void AddItemToScles(Item item, bool isBuying)
	{
		Item = item;
		IsBuying = isBuying;
		StartCoroutine(Coroutine());
	}
	public void ResetUI()
	{
		leftItemIcon.sprite = null;
		rightItemIcon.sprite = null;
		leftItemIcon.enabled = false;
		rightItemIcon.enabled = false;
		Weight = 0;
		animator.SetInteger("Weight", Weight);
	}
	IEnumerator Coroutine()
	{
		if(leftItemIcon.sprite == null)
		{
			if ((Item.Value <= playerStats.Gold && rightItemIcon.sprite == null) || !IsBuying)
			{
				Weight -= 1;
				animator.SetInteger("Weight", Weight);
				rightItemIcon.enabled = true;
				rightItemIcon.sprite = goldIcon;
				yield return new WaitForSeconds(1.7f);
			}
			leftItemIcon.enabled = true;
			leftItemIcon.sprite = Item.Icon;
			Weight += 1;
			animator.SetInteger("Weight", Weight);
			Item = null;
			IsBuying = false;
		}
	}
}

