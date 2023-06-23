using UnityEngine;

[CreateAssetMenu(fileName = "New Mini Quest CollectItems", menuName = "Quest/MiniQuestCollectItems")]
public class MissionCollectItem : QuestSettings
{
	public Item ItemToCollect;
	public int AmoutOfItemToCollect;
	public int AmountOfCollectedItems;
}
