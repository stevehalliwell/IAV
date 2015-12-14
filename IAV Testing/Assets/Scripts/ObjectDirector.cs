﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDirector : MonoBehaviour {

    // Link the FFTInt script to get Bin information
    public FFTInt fftInt;
    // Difference in scale was hard to see so a multiplier makes the difference more noticeable
    public float scaleMultiplier = 10;
    // Array of gameobjects
    public GameObject[] objectsInScene;
    // Dictionary of gameobject starting positions
    private Dictionary<GameObject, Vector3> objectsStartingPosition;

    // Cube to test methods
    public GameObject testCube;
    // Will be used for beat detections
    float sum;
    public float fMax = 200;

    // Use this for initialization
    void Start () {

        // Start a new dictionary
        objectsStartingPosition = new Dictionary<GameObject, Vector3>();
        // Fill the dictionary
        FillDictionaryStartingPositions(objectsStartingPosition, objectsInScene);
	}
	
	// Update is called once per frame
	void Update () {

		ObjectArraySizeChange(objectsInScene);
        //MoveObject(testCube, 1);
        ObjectArrayMovement(objectsInScene);
        //BandVol(100, 1000);
        //Debug.Log(sum);

	}

    // Change the scale of an object using a particular binp
    public void ChangeSize(GameObject objectInScene, int binNum)
    {
        if (objectInScene != null)
        {
            // Set a starting scale, so the object doesnt grow out of proportion
            Vector3 startingScale = new Vector3(1, 1, 1);
            // Set a new vector to store the values from the bins
            Vector3 scaleChange = new Vector3(fftInt.avgBins[binNum] * scaleMultiplier, fftInt.avgBins[binNum] * scaleMultiplier * 10, fftInt.avgBins[binNum] * scaleMultiplier);
            // Set the localScale of the object in the scene using the startingScale and scaleChange
            objectInScene.transform.localScale = new Vector3(startingScale.x + scaleChange.x, startingScale.y + scaleChange.y, startingScale.z + scaleChange.z);
        }
        else
        {
            // If there is no objects
            Debug.LogError("The gameObject you are trying to scale is null");
        }       
    }

    // Function to change scale of all objects in an array
    void ObjectArraySizeChange(GameObject[] objects)
    {
        if (objects.Length > 1)
        { 
        // Loop over array and set a gameobject and bin number
        for(int i = 0; i < objects.Length; i++)
        {
            // Array Error Here
            // Does not cause scene to crash
            ChangeSize(objects[i].gameObject, i);
        }
        }
        // Error message so people understand if they are doing something wrong
        else
        {
            Debug.LogError("The list of objects you are trying to scale is equal to 0");
        }
    }

    // Move an object around depending on the value of the bin it is attached to
    public void MoveObject(GameObject objectInScene, int binNum)
    {
        if (objectInScene != null && objectsStartingPosition.ContainsKey(objectInScene))
        { 
            // This needs to be tweeked
            // Need to make it so the start position is only found once
            //Vector3 startingPos = objectInScene.transform.position;

            // Dictionary used to store the initial position
            Vector3 startingPos = objectsStartingPosition[objectInScene];

            // New position from Bins
            Vector3 newPos = new Vector3(fftInt.avgBins[binNum], fftInt.avgBins[binNum], fftInt.avgBins[binNum]);

            // Change the position
            objectInScene.transform.position = new Vector3(startingPos.x + newPos.x, startingPos.y + newPos.y, startingPos.z + newPos.z);
        }
        else
        {
            // If there is no objects
            Debug.LogError("The gameObject you are trying to move is null");
        }
    }


    // Function for moving multiple objects using bins
    void ObjectArrayMovement(GameObject[] objects)
    {
        if(objects.Length > 1)
        { 
        // Iterate through list and move objects
        for(int i = 0; i < objects.Length; i++)
        {
            MoveObject(objects[i], i);
        }
        }
        // Error message so people understand if they are doing something wrong
        else
        {
            Debug.LogError("The list of objects you are trying to scale is equal to 0");
        }
    }


    void FillDictionaryStartingPositions(Dictionary<GameObject, Vector3> dictionary, GameObject[] arrayOfObjects)
    {

        // Run a loop and add the objects and their starting positions into the dictionary
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            dictionary.Add(arrayOfObjects[i], arrayOfObjects[i].transform.position);
        }



    }

    //Trying to get beat detection

    //public float BandVol(float fLow, float fHigh)
    //{
    //    fLow = Mathf.Clamp(fLow, 20, fMax);
    //    fHigh = Mathf.Clamp(fHigh, fLow, fMax);
    //    int n1 = Mathf.FloorToInt(fLow * fftInt.sampleSize / fMax);
    //    int n2 = Mathf.FloorToInt(fHigh * fftInt.sampleSize / fMax);
    //    sum = 0;
    //    for (int i = n1; i < n2; i++)
    //    {
    //        sum += fftInt.samples[i];
    //    }
    //    return sum / (n2 - n1);
    //}

}
