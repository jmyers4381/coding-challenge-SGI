using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	//this script receives two integers from the manager script based on user input. 
	//It receives a year integer from the manager script and checks if that year falls between its birth and end years.

	public int birthYear;
	public int endYear;

	public bool alive;

	public void CheckIfAlive(int year)
	{
		if (year >= birthYear && year <= endYear) 
		{
			alive = true;
		} else 
		{
			alive = false;
		}
	}

}
