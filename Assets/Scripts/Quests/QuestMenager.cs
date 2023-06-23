using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenager : MonoBehaviour
{
	public List<QuestSettings> ActiveQuests;
	public List<QuestSettings> ComplitedQuests;
	public List<Enemy> KilledEnemy;
	public Transform playerTransform;
	public Item itemToCheckAddItem;
	public Item itemToCheckRemoveItem;
	private void Update()
	{
		for (int i = 0; i < ActiveQuests.Count; i++)
		{
			if (ActiveQuests[i] != null)
			{
				foreach (QuestSettings quest in ActiveQuests[i].missonsObjectives)
				{
					if (quest.GetType() == typeof(MissionGoToPostion))
					{
						var questGoToPos = quest as MissionGoToPostion;
						questGoToPos.ActualDistance = Vector3.Distance(playerTransform.position, questGoToPos.CompliteMissionPosition);
						if (questGoToPos.ActualDistance <= questGoToPos.DistansToCompliteMission)
						{
							quest.MissionEnded = true;
							compliteMission();
						}
					}

					if (quest.GetType() == typeof(MissionKillEnemy))
					{
						var questkillEnemy = quest as MissionKillEnemy;
						for (int j = 0; j < KilledEnemy.Count; j++)
						{
							if (questkillEnemy.EnemyTypes == KilledEnemy[j].enemyTypes)
							{
								questkillEnemy.AmountOfKilledEnemies++;
								if (questkillEnemy.AmountOfKilledEnemies == questkillEnemy.AmountEnemyToKill)
								{
									quest.MissionEnded = true;
									compliteMission();
								}
							}
						}
					}

					if (quest.GetType() == typeof(MissionCollectItem))
					{
						var questCollectItems = quest as MissionCollectItem;
						if (itemToCheckAddItem.ItemName == questCollectItems.ItemToCollect.ItemName)
						{
							questCollectItems.AmountOfCollectedItems++;
							if (questCollectItems.AmoutOfItemToCollect == questCollectItems.AmountOfCollectedItems)
							{
								quest.MissionEnded = true;
								compliteMission();
							}
						}
						if (itemToCheckRemoveItem.ItemName == questCollectItems.ItemToCollect.ItemName)
						{
							questCollectItems.AmountOfCollectedItems--;
							if (questCollectItems.AmoutOfItemToCollect != questCollectItems.AmountOfCollectedItems)
							{
								quest.MissionEnded = false;
								uncompliteMission();
							}
						}
					}
				}
			}
		}
	}

	private void compliteMission()
	{
		for (int i = 0; i < ActiveQuests.Count; i++)
		{
			if (ActiveQuests[i] != null)
			{
				if (ActiveQuests[i].missonsObjectives[ActiveQuests[i].misionState].MissionEnded == true)
				{
					ActiveQuests[i].misionState++;
					if (ActiveQuests[i].misionState == ActiveQuests[i].missonsObjectives.Count)
					{
						Debug.Log("Primary Mission Complited");
						ComplitedQuests.Add(ActiveQuests[i]);
						ActiveQuests.Remove(ActiveQuests[i]);
					}
				}
			}
		}
	}

	private void uncompliteMission()
	{
		for (int i = 0; i < ActiveQuests.Count; i++)
		{
			if (ActiveQuests[i] != null)
			{
				if (ActiveQuests[i].missonsObjectives[ActiveQuests[i].misionState].MissionEnded == false)
				{
					ActiveQuests[i].misionState--;
					if (ActiveQuests[i].misionState != ActiveQuests[i].missonsObjectives.Count)
					{
						Debug.Log("Primary Mission UnComplited");
						ComplitedQuests.Remove(ActiveQuests[i]);
						ActiveQuests.Add(ActiveQuests[i]);
					}
				}
			}
		}
	}
}
