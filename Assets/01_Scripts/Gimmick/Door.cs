using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private BoxCollider2D doorCollider;

	private void Awake()
	{
		doorCollider = GetComponent<BoxCollider2D>();
	}

	public void Open()
	{
		doorCollider.enabled = false;
	}

	public void Close()
	{
		doorCollider.enabled = true;
	}
}
