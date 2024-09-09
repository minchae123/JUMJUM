using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public Door connectDoor;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		connectDoor.Open();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		connectDoor.Close();
	}
}
