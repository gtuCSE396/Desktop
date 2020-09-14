using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Linq;
using System.Diagnostics.Tracing;
using System.Reflection;

public class Hard_DrawGraph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;

    [SerializeField] private GameObject xGraphPanelObject;
    [SerializeField] private GameObject yGraphPanelObject;
    [SerializeField] private GameObject distanceGraphPanelObject;

    private RectTransform xGraphPanelContainer;
    private RectTransform yGraphPanelContainer;
    private RectTransform distanceGraphPanelContainer;

    private int counter = 1;
    private int queuesIndex = 0;

    private int maxGraphElement = 17;

    private bool stop = false;

    private int[] xQueue;
    private int[] yQueue;
    private int[] distanceQueue;

    private void Awake() {

        xQueue = new int[maxGraphElement];      // These arrays holds the current data on the graph
        yQueue = new int[maxGraphElement];
        distanceQueue = new int[maxGraphElement];

        xGraphPanelContainer = xGraphPanelObject.GetComponent<RectTransform>();
        yGraphPanelContainer = yGraphPanelObject.GetComponent<RectTransform>();
        distanceGraphPanelContainer = distanceGraphPanelObject.GetComponent<RectTransform>();

        stop = false;
    }

    public void UpdateGraphs(float xPosition, float yPosition, float zPosition)
    {
        if (counter != maxGraphElement)
            counter++;

        if (counter == maxGraphElement)
        {
            shiftArrayLeft(xQueue);         // If maximum element on the graph exceeded, then shift the array to the left and put the element to the right-most space.
            shiftArrayLeft(yQueue);
            shiftArrayLeft(distanceQueue);

            if (GameObject.FindGameObjectsWithTag("mortal").Length > 0) // This condition kills the previous graph nodes and connections.
            {
                GameObject[] mortals = GameObject.FindGameObjectsWithTag("mortal");
                foreach (GameObject mortal in mortals)
                    GameObject.Destroy(mortal);
            }
        }

        xQueue[queuesIndex] = (int)xPosition;         // Update the current data on the graph.
        yQueue[queuesIndex] = (int)yPosition;
        distanceQueue[queuesIndex] = (int)zPosition;

        if (queuesIndex != maxGraphElement - 1)
        {
            queuesIndex++;
        }

        ShowGraph(xQueue, xGraphPanelContainer, 0);            // Display graphs on each panel individually.
        ShowGraph(yQueue, yGraphPanelContainer, 0);
        ShowGraph(distanceQueue, distanceGraphPanelContainer, 1);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, RectTransform container) {         // Draws a node on the graph.
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(container, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        gameObject.tag = "mortal";
        return gameObject;
    }

    private void ShowGraph(int[] values, RectTransform container, int mode) {                       // Displays the graph.

        float graphHeight = container.rect.height;
        float yMaximum = 690f;
        float xSize = 20f;
        float xStartPoint = 30f;
        float yStartPoint = 15f;
        GameObject lastCircleGameObject = null;
        if (mode == 1)
            yMaximum = 200f;
        for (int i = 0; i < queuesIndex; i++) {
            float xPosition = xStartPoint + i * xSize;
            float yPosition = yStartPoint + (values[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), container);
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, container);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, RectTransform container) {      // Draws a connection between nodes.
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.tag = "mortal";
        gameObject.transform.SetParent(container, false);
        gameObject.GetComponent<Image>().color = new Color(0,0,0, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void shiftArrayLeft(int [] arr)     // Shifts an array to the left.
    {
        for(int i = 1; i < arr.Count(); i++)
        {
            arr[i - 1] = arr[i];
        }
    }

    public void Stop()
    {
        stop = true;

        queuesIndex = 0;
        counter = 1;

        xQueue = new int[maxGraphElement];      // These arrays holds the current data on the graph
        yQueue = new int[maxGraphElement];
        distanceQueue = new int[maxGraphElement];

        if (GameObject.FindGameObjectsWithTag("mortal").Length > 0) // This condition kills the previous graph nodes and connections.
        {
            GameObject[] mortals = GameObject.FindGameObjectsWithTag("mortal");
            foreach (GameObject mortal in mortals)
                GameObject.Destroy(mortal);
        }
    }
    public void StartAgain()
    {
        stop = false;
    }

}
