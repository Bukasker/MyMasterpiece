using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PrimaryQuest", menuName = "Quest/PrimaryQuest")]
public class QuestSettings : ScriptableObject
{
	public List<QuestSettings> missonsObjectives;

	public bool MissionEnded;

	public int misionState;
}