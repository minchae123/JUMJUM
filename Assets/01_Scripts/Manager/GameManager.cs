using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private int curStage = 1;
	public int CurStage { get { return curStage; } set { curStage = value; } }

	private void Awake()
	{
		if (Instance != null)
			Debug.LogError("GameManager Error");
		Instance = this;

		curStage = 1;
	}
}
