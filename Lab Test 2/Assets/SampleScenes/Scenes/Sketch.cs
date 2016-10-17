using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
	public GameObject myPrefab;
	public Material material1;  
	public Material material2; 
	public Material material3; 

	//Variables for dataset
	private string readingID;
	private string location;
	private string safetyCategory;
	private string safetyMeasure;
	private string whenReadingRecorded;
	private float x;
	private float y;
	private float z;

	public string _WebsiteURL = "http://kwon709.azurewebsites.net/tables/WaterPollutionReading?zumo-api-version=2.0.0";

	void Start () {

		string jsonResponse = Request.GET(_WebsiteURL);

		//Just in case something went wrong with the request we check the reponse and exit if there is no response.

		if (string.IsNullOrEmpty(jsonResponse))
		{
			return;
		}

		//We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
		// convert the table into an array (this is shown in deserialise) 
		WaterPollutionReading[] waterpollutionreadings = JsonReader.Deserialize<WaterPollutionReading[]>(jsonResponse);


		foreach (WaterPollutionReading waterpollutionreading in waterpollutionreadings)
		{

			Debug.Log("X is " + waterpollutionreading.X + ", Y is " + waterpollutionreading.Y + ", Z is " + waterpollutionreading.Z + "" ) ;
			readingID = waterpollutionreading.ReadingID;
			location = waterpollutionreading.Location;
			safetyCategory = waterpollutionreading.SafetyCategory;
			safetyMeasure = waterpollutionreading.SafetyMeasure;
			whenReadingRecorded = waterpollutionreading.WhenReadingRecorded;
			x = float.Parse(waterpollutionreading.X);
			y = float.Parse(waterpollutionreading.Y);
			z = float.Parse(waterpollutionreading.Z);

			GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
			newCube.name = waterpollutionreading.ReadingID;			
			//adds label
			newCube.GetComponentInChildren<TextMesh> ().text= "(" + waterpollutionreading.X + ", " + waterpollutionreading.Y + ", " + waterpollutionreading.Z + ")";

		}


	}

	void Update () {} 
}

//Update is called once per frame    


//if (waterpollutionreading.X) {
// float y = where it is on the y axis- where is it rotating on the y axis- You have three different levels and this is changed by float y
//float y = 0.55f;
//float z = 16.0f;
// Create a new cube for every object in that table
//GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
// <shows what you can put in- refer to unity>().field it is
// changes cube size based on script in mycubescript- you can change this value directly
//setsize(method) vs setsize = field
//newCube.GetComponent<mySphereScript>().setSize(0.3f);
//newCube.GetComponent<mySphereScript>().rotateSpeed = perc;