using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using PDollarGestureRecognizer;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementRecognizer : MonoBehaviour
{
    
    // variables to store the center of the drawing
    [DoNotSerialize]
    public Vector3 center;
    
    public GameObject rightHand;
    public GameObject leftHand;
    
    public MagicManager magicManager;
    
    public LineRenderer lineRenderer;
    private LineRenderer lineRendererInstance = new LineRenderer();
    
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float activationThreshold = 0.1f;
    public Transform movementReference;

    public float movementThreshold = 0.05f;
    private GameObject debugPrefab;

    public float recognitionThreshold = 0.5f;
    
    public TMP_Text gestureText;
    
    private bool canCast = true;
    private bool coroutineStarted = false;
    
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent OnGestureRecognized;
    
    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionList = new List<Vector3>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("MyGestures/");
        
        foreach (TextAsset gestureXml in gesturesXml)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
            Debug.Log(GestureIO.ReadGestureFromXML(gestureXml.text).Name);
        }
             
        gestureText.text = "";
        // print each loaded gesture name on the textemesh
        foreach (var item in trainingSet)
        {
            gestureText.text += item.Name + "\n";
        }


    }

    // Update is called once per frame
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed,
            activationThreshold);

        if (canCast)
        {

            if (!isMoving && isPressed)
            {
                StartMovement();
            }
            else if (isMoving && !isPressed)
            {
                EndMovement();
            }
            else if (isMoving && isPressed)
            {
                UpdateMovement();
            }
        }
        else
        {
            
            if (!coroutineStarted)
            {
                StartCoroutine(ResetCast());
                coroutineStarted = true;
            }
        }
    }

    void StartMovement()
    {
        isMoving = true;
        positionList.Clear();
        positionList.Add(movementReference.position);

        if (lineRenderer != null)
        {
            lineRendererInstance = Instantiate(lineRenderer, movementReference.position, Quaternion.identity);
            lineRendererInstance.material = lineRenderer.material;
            lineRendererInstance.startColor = lineRenderer.startColor;
            lineRendererInstance.endColor = lineRenderer.endColor;
            lineRendererInstance.startWidth = 0.01f; // set the start width to a smaller value
            lineRendererInstance.endWidth = 0.01f; // set the end width to a smaller value
            lineRendererInstance.widthMultiplier = 0.05f; // set the width multiplier to a smaller value
            lineRendererInstance.positionCount = 0;
        }
    }

    void UpdateMovement()
    {
        Vector3 lastPosition = positionList[positionList.Count - 1];
        positionList.Add(movementReference.position);
        if (Vector3.Distance(lastPosition, movementReference.position) > movementThreshold)
        {

            if (debugPrefab != null)
                Destroy(Instantiate(debugPrefab, movementReference.position, Quaternion.identity), 3);
        }       
        
        if (lineRendererInstance != null)
        {
            lineRendererInstance.positionCount = positionList.Count;
            lineRendererInstance.SetPositions(positionList.ToArray());
        }
       
    }
    void EndMovement()
    {
        isMoving = false;

        Point[] pointArray = new Point[positionList.Count];
    
        for (int i = 0; i < positionList.Count; i++)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(positionList[i]);
            pointArray[i] = new Point(screenPosition.x, screenPosition.y, 0);
        }
    
        if (lineRendererInstance != null)
        {
            lineRendererInstance.positionCount = positionList.Count;
            lineRendererInstance.SetPositions(positionList.ToArray());
        }
    
        Gesture newGesture = new Gesture(pointArray);

        Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
        
        StartCoroutine(RemoveDrawing(lineRendererInstance));
        
        //gestureText.text = result.GestureClass + " " + result.Score;
        // store the average all positions in the positon list
        //center = Vector3.zero;
        //foreach (var item in positionList)
       // {
       //     center += item;
       // }
       //center /= positionList.Count;


       gestureText.text = "got before the switch";
       
        switch (result.GestureClass)
        {
            case "fireball":
                if (result.Score > 0.8f)
                {
                    magicManager.spawnFireball();
                    canCast = false;
                    gestureText.text = "Spawn Fireball";
                }
                else
                    gestureText.text = "Fireball (could not recognize)";
                
                break;
            

            case "lightning":
                if (result.Score > 0.6f)
                {
                    magicManager.spawnLightning();
                    canCast = false;
                    gestureText.text = "Spawn Lightning";
                }
                else
                    gestureText.text = "Lightning (could not recognize)";
                break;
            case "freeze":
                if (result.Score > 0.8f)
                {
                    magicManager.spawnFreeze();
                    canCast = false;
                    gestureText.text = "Spawn Freeze";
                }
                else 
                    gestureText.text = "Freeze (could not recognize)";
                break;
            case "arcane":
                if (result.Score > 0.7f)
                {
                    magicManager.spawnArcane();
                    canCast = false;
                    gestureText.text = "Spawn Arcane";
                }
                else
                    gestureText.text = "Arcane (could not recognize)";
                break;
            default:
                gestureText.text = "Could not recognize";
                break;

        }

        //gestureText.text = "got here";




    }

    IEnumerator RemoveDrawing(LineRenderer lineRenderer)
    {
        yield return new WaitForSeconds(1);
        Destroy(lineRenderer);
    }

    IEnumerator ResetCast()
    {
        yield return new WaitForSeconds(3);
        canCast = true;
        coroutineStarted = false;
    }
}