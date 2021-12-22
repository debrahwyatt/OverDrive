using UnityEngine;

public class Populate : MonoBehaviour
{
    //Prefab list
    public GameObject tree0;
    public GameObject tree1;
    public GameObject bush0;
    public GameObject bush1;
    public GameObject building0;

    private int mapSize = 1500;
    private int forestSize = 250;

    void Start()
    {

        cities();
        nature();

        //how big is the world?
        // plane: 1500 x 1500
        // want it spherical though...

        //what is the list of prefabs?
        //just tree0

        //what is the density of each item
        //size of object / 1500*1500 - constant

        //keep objects from spawning inside each other
        //pick a spawning point for everything

        //spawn

    }

    private void cities()
    {
        for (int i = 0; i < Random.Range(25, 200); i++)
        {
            Quaternion buildingRotation = new Quaternion(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f);
            newStructure(building0, buildingRotation);
        }

        GameObject newStructure(GameObject structure, Quaternion plantRotation)
        {
            GameObject newStructure = Instantiate(
                structure,
                new Vector3(Random.Range(-mapSize, mapSize), 0, Random.Range(-mapSize, mapSize)),
                plantRotation
            );
            return newStructure;
        }
    }
    private void nature()
    {
        for (int i = 0; i < Random.Range(1000, 1000); i++)
        {
            Quaternion plantRotation = new Quaternion(Random.Range(-0.1f, 0.1f), Random.Range(-1f, 1f), Random.Range(-0.1f, 0.1f), 1f);
            float scale = Random.Range(0.5f, 2f);

            newPlant(tree0, plantRotation, scale);
            newPlant(tree1, plantRotation, scale);
            newPlant(bush0, plantRotation, scale);
            newPlant(bush1, plantRotation, scale);
        }

        GameObject newPlant(GameObject plant, Quaternion plantRotation, float scale)
        {
            GameObject newPlant = Instantiate(
                plant,
                new Vector3(Random.Range(-forestSize, forestSize), 0, Random.Range(-forestSize, forestSize)),
                plantRotation
            );
            newPlant.transform.localScale = new Vector3(scale, scale, scale);
            return newPlant;
        }
    }
}
