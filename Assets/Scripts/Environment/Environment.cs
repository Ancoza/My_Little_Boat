using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Environment : MonoBehaviour
{
    const byte SPACE_BETWEN_BUILDINGS = 2;
    const byte INITIAL_BUILDINGS = 5;
    public static Environment SharedInstance;
    
    public List<Building> buildingsRight;
    public List<Building> buildingsLeft;
    public List<Building> currentBuildingsRight;
    public List<Building> currentBuildingsLeft;

    bool isGenerated = false;

    void Awake()
    {
        //Singleton
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    
    #region Generate Buildings
    public void GenerateInitialBuildings()
    {
        if (!isGenerated) { 
            for (int i = 0; i < INITIAL_BUILDINGS; i++)
            {
                AddBuildings();
            }
            isGenerated = true;
        }
    }
    public void AddBuildings()
    {
        AddBuildingsRight();
        AddBuildingsLeft();
    }
    private void AddBuildingsRight()
    {
        int randomIdx = Random.Range(0, buildingsRight.Count);

        Building building;

        if (currentBuildingsRight.Count >= 1)
        {
            //Get the last building position
            int lastPosition = currentBuildingsRight.Count - 1;
            Vector3 position = new Vector3(SPACE_BETWEN_BUILDINGS, 0, currentBuildingsRight[lastPosition].finishPosition.position.z);

            //Instantiate and add to currentBuildings
            building = Instantiate(buildingsRight[randomIdx], transform, false);
            building.gameObject.transform.position = position;
            currentBuildingsRight.Add(building);
        }
        else
        {
            Vector3 position = new Vector3(SPACE_BETWEN_BUILDINGS, 0,0);
            //Create first Building
            building = Instantiate(buildingsRight[randomIdx], transform, false);
            building.transform.position = position;
            currentBuildingsRight.Add(building);
        }
    }
    private void AddBuildingsLeft()
    {
        int randomIdx = Random.Range(0, buildingsLeft.Count);

        Building building;

        if (currentBuildingsLeft.Count >= 1)
        {
            //Get the last building position
            int lastPosition = currentBuildingsLeft.Count - 1;
            Vector3 position = new Vector3(-SPACE_BETWEN_BUILDINGS, 0, currentBuildingsLeft[lastPosition].finishPosition.position.z);
            
            //Instantiate and add to currentBuildings
            building = Instantiate(buildingsLeft[randomIdx], this.transform, false);
            building.gameObject.transform.position = position;
            currentBuildingsLeft.Add(building);
        }
        else
        {
            Vector3 position = new Vector3(-SPACE_BETWEN_BUILDINGS, 0,0);
            //Create first Building
            building = Instantiate(buildingsLeft[randomIdx], this.transform, false);
            building.gameObject.transform.position = position;
            currentBuildingsLeft.Add(building);
        }
    }
    #endregion
    #region Delete Buildings
    public void DeleteBuildings()
    {
        //Delete Building Right
        Building oldBuildingRight = currentBuildingsRight[0];
        currentBuildingsRight.Remove(oldBuildingRight);
        Destroy(oldBuildingRight.gameObject);
        //Delete Building Left
        Building oldBuildingLeft = currentBuildingsLeft[0];
        currentBuildingsLeft.Remove(oldBuildingLeft);
        Destroy(oldBuildingLeft.gameObject);
    }
    public void DeleteAllBuildings()
    {
        for (int i = 0; i < currentBuildingsRight.Count; i++)
        {
            DeleteBuildings();
        }
    }
    #endregion
}
