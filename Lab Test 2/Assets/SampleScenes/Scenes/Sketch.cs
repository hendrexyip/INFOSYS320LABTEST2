using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
	public GameObject myPrefab;
	public Material material1;  
	public Material material2; 
	public Material material3; 
	public string jsonResponse;
	public GameObject cube; 
	//Variables for dataset
	private string TreeID;
	private string Location;
	private string EcologicalValue;
	private string HistoricalSignificance;
	private string WhenReadingRecorded;
	private float x;
	private float y;
	private float z;

	// Add and change variables

	public string _WebsiteURL = "http://hyip413.azurewebsites.net/tables/TreeSurvey?zumo-api-version=2.0.0";
	//change the name of the table
	void Start () {

		jsonResponse = Request.GET(_WebsiteURL);

		//Just in case something went wrong with the request we check the reponse and exit if there is no response.

		if (string.IsNullOrEmpty(jsonResponse))
		{
			return;
		}
		//changing all the pollution reading
		//We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
		// convert the table into an array (this is shown in deserialise) 
		TreeSurvey[] treesurveys = JsonReader.Deserialize<TreeSurvey[]>(jsonResponse);
		//****************CHANGE*************************                      **CHANGE**
		//Change all the pollution reading
		foreach (TreeSurvey treesurvey in treesurveys)
		{
			//THE S ^^
			Debug.Log("X is " + treesurvey.X + ", Y is " + treesurvey.Y + ", Z is " + treesurvey.Z + "" ) ;
			TreeID = treesurvey.TreeID;
			Location = treesurvey.Location;
			EcologicalValue = treesurvey.EcologicalValue;
			HistoricalSignificance = treesurvey.HistoricalSignificance;
			WhenReadingRecorded = treesurvey.WhenReadingRecorded;
			x = float.Parse(treesurvey.X);
			y = float.Parse(treesurvey.Y);
			z = float.Parse(treesurvey.Z);
			//chnage to the table names
			GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
			newCube.name = treesurvey.TreeID;			
			//adds label
			newCube.GetComponentInChildren<TextMesh> ().text= "(" + treesurvey.X + ", " + treesurvey.Y + ", " + treesurvey.Z + ")";
			//RIGHT CLICK SPHERE > 3D TEXT

			if (treesurvey.EcologicalValue == "High") {
				newCube.GetComponent<Renderer> ().material = material1;

			} else if (treesurvey.EcologicalValue == "Medium") {
				newCube.GetComponent<Renderer> ().material = material2;

			} else {

				newCube.GetComponent<Renderer> ().material = material3;

			}
		}


	}

	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitdetail= new RaycastHit();
			//whereever your mouse is you get that position
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			//if you hit the project, the action will then
			if (Physics.Raycast (ray, out hitdetail, 100)) {


				hitdetail.collider.gameObject.GetComponent<Renderer> ().material = material2;
				Debug.Log (hitdetail.collider.name);
				//going back to what happen 
				int index = int.Parse(hitdetail.collider.gameObject.name);
				//row 2 index 1 
				TreeSurvey[] treesurveys = JsonReader.Deserialize<TreeSurvey[]>(jsonResponse);

				//creating a cube where you hit the raycast
				//ReadingID is 1- so you add -1 otherwise you will get index out of bound
				GameObject newCube = (GameObject)Instantiate(cube, new Vector3(hitdetail.point.x, hitdetail.point.y, hitdetail.point.z), Quaternion.identity);
				newCube.GetComponentInChildren<TextMesh> ().text= "(" + treesurveys[index-1].Location + ", " + treesurveys[index-1].Y + ", " + treesurveys[index-1].Z + ")";
			}                                                                                           // Location is attribute

		}
	}
}


