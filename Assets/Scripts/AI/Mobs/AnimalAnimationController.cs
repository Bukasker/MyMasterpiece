using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isAnimalWild;
    [SerializeField] private float timeToChangeAnimation;
    [SerializeField] private bool isInAnimation;
	public EnemyStats animalStats;
    private IEnumerator ChangeAnimationCorutine()
    {
		int randomNumber = Random.Range(1, 100);
		//Dodaæ if jeœli czas wiekszy niz to spij nie wykonuj innych animaji 
		//Reset rano 

		if (!isInAnimation)
		{
			isInAnimation = true;
			if ((randomNumber >= 0) && (randomNumber <= 25))
			{
				animator.SetTrigger("Idle");
			}
			if ((randomNumber > 26) && (randomNumber <= 75))
			{
				animator.SetTrigger("Walk");
				//MoveToRandomPosition
			}
			if ((randomNumber > 76) && (randomNumber <= 100))
			{
				animator.SetTrigger("Grazing");
			}
		}
		yield return new WaitForSeconds(timeToChangeAnimation);
		StartCoroutine(ChangeAnimationCorutine());
    }
    private enum AnimationType
    {
        Idle,
        Walk,
        Run,
        Grazing,
        Sleep,
        Die
    }
	private void AnimationOnDmg()
	{
		if (isAnimalWild)
		{
			if (animalStats.currentHealth != animalStats.MaxHealth && animalStats.currentHealth != 0)
			{
				isInAnimation = false;
				animator.SetTrigger("Run");
				//FastMoveToRandomFarPositon
			}
			else if (animalStats.currentHealth == 0)
			{
				isInAnimation = false;
				//turnOffMoving
				animator.SetTrigger("Die");
			}
			
		}
	}
	public void SetBoolAsFalse()
	{
		isInAnimation = false;
	}
}
