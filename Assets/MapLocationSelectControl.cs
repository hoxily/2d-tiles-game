using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLocationSelectControl : MonoBehaviour
{
    public Transform locationRoot;
    public Text placeNameText;
    public Transform selectMarker;

    private MapLocationData[] locationList;
    private int selectedIndex;

    private float nextAddRepeatTime;
    private float nextSubRepeatTime;
    public float repeatInterval = 0.0625f;
    public float repeatDelay = 0.25f;
    private bool isAddButtonDown = false;
    private bool isSubButtonDown = false;

    private void Start()
    {
        locationList = new MapLocationData[locationRoot.childCount];
        for (int i = 0; i < locationRoot.childCount; i++)
        {
            Transform child = locationRoot.GetChild(i);
            locationList[i] = child.GetComponent<MapLocationData>();
        }
        SelectLocationByIndex(0);
    }

    private void SelectLocationByIndex(int index)
    {
        selectedIndex = index;
        MapLocationData location = locationList[index];
        selectMarker.localPosition = location.transform.localPosition;
        placeNameText.text = location.locationName;
    }

    private void SelectLocationByDeltaIndex(int deltaIndex)
    {
        int index = selectedIndex + deltaIndex;
        while (index < 0)
        {
            index += locationList.Length;
        }
        index = index % locationList.Length;
        SelectLocationByIndex(index);
    }

    private void Update()
    {
        int deltaIndex = 0;

        if (Input.GetKeyDown(KeyCode.W))
        {
            deltaIndex++;

            isAddButtonDown = true;
            nextAddRepeatTime = Time.time + repeatDelay;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isAddButtonDown = false;
        }
        if (isAddButtonDown && Time.time >= nextAddRepeatTime)
        {
            deltaIndex++;
            nextAddRepeatTime = Time.time + repeatInterval;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            deltaIndex--;

            isSubButtonDown = true;
            nextSubRepeatTime = Time.time + repeatDelay;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isSubButtonDown = false;
        }
        if (isSubButtonDown && Time.time >= nextSubRepeatTime)
        {
            deltaIndex--;
            nextSubRepeatTime = Time.time + repeatInterval;
        }

        if (deltaIndex != 0 && isAddButtonDown != isSubButtonDown)
        {
            SelectLocationByDeltaIndex(deltaIndex);
        }
    }

    [ContextMenu("Scale Location Coordinates")]
    private void ScaleLocationCoordinates()
    {
        for (int i = 0; i < locationRoot.childCount; i++)
        {
            Transform child = locationRoot.GetChild(i);
            child.localPosition = child.localPosition * 8;
        }
    }
}
