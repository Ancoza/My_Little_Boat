using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Environment : MonoBehaviour
{
    public static Environment SharedInstance;

    public List<Building> buildings;
    public List<Building> buildingsB;
    public List<Building> currentBuildings;
    public List<Building> currentBuildingsB;

    public float positionA, positionB;

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    void Start()
    {
        GenerateInitialBuildings();
    }
    public void AddBuilding()
    {
        AddA();
        AddB();
    }
    private void AddA()
    {
        int randomIdx = Random.Range(1, buildings.Count);

        Building building;

        if (currentBuildings.Count >= 1)
        {
            //Get the last building position
            int lastPosition = currentBuildings.Count - 1;
            Vector3 position = new Vector3(positionA, 0, currentBuildings[lastPosition].finishPosition.position.z);

            //Instantiate and add to currentBuildings
            building = Instantiate(buildings[randomIdx], transform, false);
            building.gameObject.transform.position = position;
            currentBuildings.Add(building);
        }
        else
        {
            Vector3 position = new Vector3(positionA, 0,0);
            //Create first Building
            building = Instantiate(buildings[randomIdx], transform, false);
            building.transform.position = position;
            currentBuildings.Add(building);
        }
    }
    private void AddB()
    {
        int randomIdx = Random.Range(1, buildingsB.Count);

        Building building;

        if (currentBuildingsB.Count >= 1)
        {
            //Get the last building position
            int lastPosition = currentBuildingsB.Count - 1;
            Vector3 position = new Vector3(positionB, 0, currentBuildingsB[lastPosition].finishPosition.position.z);
            
            //Instantiate and add to currentBuildings
            building = Instantiate(buildingsB[randomIdx], this.transform, false);
            building.gameObject.transform.position = position;
            currentBuildingsB.Add(building);
        }
        else
        {
            Vector3 position = new Vector3(positionB, 0,0);
            //Create first Building
            building = Instantiate(buildingsB[randomIdx], this.transform, false);
            building.gameObject.transform.position = position;
            currentBuildingsB.Add(building);
        }
    }
    public void DeleteBuilding()
    {
        Building oldBuilding = currentBuildings[0];
        currentBuildings.Remove(oldBuilding);
        Destroy(oldBuilding.gameObject);
        
        Building oldBuildingB = currentBuildingsB[0];
        currentBuildingsB.Remove(oldBuildingB);
        Destroy(oldBuildingB.gameObject);
    }
    public void DeleteAllBuildings()
    {
        for (int i = 0; i < currentBuildings.Count; i++)
        {
            DeleteBuilding();
        }
    }
    void GenerateInitialBuildings()
    {
        for (int i = 0; i < 5; i++)
        {
            AddBuilding();
        }
    }
}
