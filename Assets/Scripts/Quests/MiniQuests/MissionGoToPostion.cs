using UnityEngine;

[CreateAssetMenu(fileName = "New Mini Quest GoToPosition", menuName = "Quest/MiniQuestGoToPosition")]
public class MissionGoToPostion : QuestSettings
{
	public Vector3 CompliteMissionPosition;
	public float DistansToCompliteMission;
	public float ActualDistance;
}
