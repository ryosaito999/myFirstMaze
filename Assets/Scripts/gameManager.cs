using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
    public Maze mazePrefab;
    private Maze mazeInstance;

    // Use this for initialization
    void Start () {
        beginGame();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartGame();
        }
    }

    private void beginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.generate());
    }

    private void restartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        beginGame();
    }
}
