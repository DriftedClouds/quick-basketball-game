using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs;

    private GameController gameController;
    private float spawnRate = 10.0f;

    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private Vector3 leftWallLocation = new Vector3(1.5f, 4.0f, 3.2f);
    private Quaternion leftWallRotation = Quaternion.Euler(Vector3.right * -90);
    private Vector3 rightWallLocation = new Vector3(1.5f, 4.0f, -3.2f);
    private Quaternion rightWallRotation = Quaternion.Euler(Vector3.right * 90);
    private Vector3 backWallLocation = new Vector3(-3, 5.3f, 0);
    private Quaternion backWallRotation = Quaternion.identity;

    private List<PanelSpawnTransform> PanelTransforms = new List<PanelSpawnTransform>();
    private GameObject currentPanel;

    //TODO
    //Game is RNG heavy because of the panel colors. Could try and find a way to make it more consistent.

    private enum Location
    {
        Left,
        Right,
        Back
    }

    struct PanelSpawnTransform
    {
        public Location LocationIndex;
        public Vector3 LocationCoords;
        public Quaternion Rotation;

        public PanelSpawnTransform(Location locationIndex, Vector3 location, Quaternion rotation)
        {
            this.LocationIndex = locationIndex;
            this.LocationCoords = location;
            this.Rotation = rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AssignPanelTransforms();
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
        //Use this code to automatically change panel locations
        //StartCoroutine(SpawnPanel());
        SpawnNewPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    IEnumerator SpawnPanel()
    {
        while (gameController.CheckIfGameActive())
        {
            SpawnNewPanel();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void SpawnNewPanel()
    {
        int index = Random.Range(0, panelPrefabs.Count);

        if (gameController.CheckIfGameActive())
        {
            int locationIndex = RandomSquareIndex();

            //Delete panel
            if (currentPanel != null)
            {
                Destroy(currentPanel);
            }

            //TODO
            //Can make the game more interesting by getting a spawn position and rotation.
            //Maybe get a random # then that will return position and rotation for that #.
            currentPanel = Instantiate(panelPrefabs[index], GetSpawnPosition(locationIndex), panelPrefabs[index].transform.rotation * GetSpawnRotation(locationIndex));
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    private Vector3 GetSpawnPosition(int locationIndex)
    {
        Vector3 location = new Vector3();
        location = PanelTransforms[locationIndex].LocationCoords;

        return location;
    }

    private Quaternion GetSpawnRotation(int locationIndex)
    {
        Quaternion rotation = Quaternion.identity;
        rotation = PanelTransforms[locationIndex].Rotation;

        return rotation;
    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    private int RandomSquareIndex()
    {
        return Random.Range(0, PanelTransforms.Count);
    }

    //TODO this can probably be refactored in some way to loop through all locations/rotations
    //One possibility is at the start of this class, to use object and collection initializer to declare new transforms
    //with hardcoded values when declaring this list, bypassing the need for these location/rotation variables.
    private void AssignPanelTransforms()
    {
        PanelTransforms.Add(new PanelSpawnTransform(Location.Left, leftWallLocation, leftWallRotation));
        PanelTransforms.Add(new PanelSpawnTransform(Location.Right, rightWallLocation, rightWallRotation));
        PanelTransforms.Add(new PanelSpawnTransform(Location.Back, backWallLocation, backWallRotation));
    }
}
