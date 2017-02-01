using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	public InputField inputName;
	public InputField inputBirth;
	public InputField inputEnd;

	public Text listOfPeople;
	public Text total;
	public Text highest;
	public Text error;

	private GameObject person;
	public GameObject[] everyone;
	public int[] years;

	public int mostPeopleAlive;
	public int numberAlive;
	public int liveliestYear;



	void Start () 
	{

		//fill in the array of years
		for (int i = 0; i < 100; i++) 
		{
			years[i] = i + 1900;

		}

	}


	// Called when the user is done entering values for a person
	public void AddPerson()
	{
		//create a person gameobject and give it the person script and tag it
		person = new GameObject(inputName.text);
		person.AddComponent<Person>();
		person.transform.tag = "Person";


		//Get the years from the inputfields if they're integers
		if(int.TryParse((inputBirth.text), out person.GetComponent<Person> ().birthYear))
		{

		}
		if(int.TryParse((inputEnd.text), out person.GetComponent<Person> ().endYear))
		{

		}

		//check to exclude years out of range and time travellers
		if (person.GetComponent<Person> ().birthYear >= 1900 && person.GetComponent<Person> ().endYear >= person.GetComponent<Person> ().birthYear && person.GetComponent<Person> ().endYear < 2000) 
		{
			PopulateList ();

			//clear out the name inputfield so the inputfield can capitalize the name without using shift
			inputName.text = "";
			error.text = "";
		} else 
		{
			error.text = "INVALID YEAR";
		}
	}


	//puts the names and years of people into the list
	public void PopulateList()
	{
		listOfPeople.text += "\n\t" + person.transform.name +"\t\t" + person.GetComponent<Person> ().birthYear + " - " + person.GetComponent<Person> ().endYear;

	}

	//add all of the people to an array and figure out which year(s) had the most people alive

	public void Calculate()
	{
		//create a list out of all the people then go through every year and check how many of them were alive
		everyone = GameObject.FindGameObjectsWithTag ("Person");
		foreach (int year in years) 
		{
			if (numberAlive > mostPeopleAlive) 
			{
				mostPeopleAlive = numberAlive;
			}
			numberAlive = 0;

			foreach (GameObject person in everyone) 
			{
				person.GetComponent<Person> ().CheckIfAlive (year);
				if (person.GetComponent<Person> ().alive) 
				{
					numberAlive++;
				}
			}

			//set the record for most people alive
			if (numberAlive >= mostPeopleAlive) 
			{
				liveliestYear = year;
			}
		}

		total.text = "Year(s) with the most people alive: ";
		highest.text = "Highest number of living people at once: " + mostPeopleAlive;

		//run back through it and compare every year to the record number of people
		foreach (int year in years) 
		{
			numberAlive = 0;
			foreach (GameObject person in everyone) 
			{
				person.GetComponent<Person> ().CheckIfAlive (year);
				if (person.GetComponent<Person> ().alive) 
				{
					numberAlive++;
				}
			}
			if (numberAlive >= mostPeopleAlive) 
			{
				liveliestYear = year;
				//add all of the liveliest years to the total text field
				total.text += liveliestYear + "\t";
			}
		}

	}

}
