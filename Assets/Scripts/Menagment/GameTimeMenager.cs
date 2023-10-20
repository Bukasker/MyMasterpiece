using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeMenager : MonoBehaviour
{
	public double Time = 5.5;
	public int Days = 0;
	public int Months = 0;
	public int Years = 0;
	public float Ticks = 8;
	public bool stopGameTime;

	public static GameTimeMenager instance;

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		StartCoroutine(GameTime());
	}

	private IEnumerator GameTime()
	{
		while (!stopGameTime)
		{
			yield return new WaitForSeconds(Ticks);
			Time += 0.10;

			double remainder = Time % 1.0;
			if (remainder >= 0.5)
			{
				Time = Math.Round(Time, 2);
				Time = Math.Floor(Time) + 1.0;
			}
			if (Time >= 24.10)
			{
				Time = 0;
				Days++;
				if (Days >= 30)
				{
					Days = 0;
					Months++;
					if (Months >= 13)
					{
						Months = 0;
						Years++;
					}
				}
			}
		}
	}
}
