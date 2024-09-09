using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StageManager : MonoBehaviour
{
	public static StageManager Instance;

	public List<GameObject> Stages;

	private GameObject curStageObject;

	private void Awake()
	{
		if (Instance != null)
			Debug.LogError("StageManager Error");
		Instance = this;
	}

	private void Start()
	{
		LoadStage(GameManager.Instance.CurStage);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			LoadStage(GameManager.Instance.CurStage);
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			ClearStage();
		}
	}

	public void LoadStage(int num)
	{
		if (curStageObject == null)
		{
			curStageObject = Instantiate(Stages[num - 1], transform);
		}
	}

	public void ClearStage()
	{
		Destroy(curStageObject);
		curStageObject = null;

		LoadStage(++GameManager.Instance.CurStage);
	}
}
