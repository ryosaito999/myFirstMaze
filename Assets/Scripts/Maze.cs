using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    // prefab of maze cells
    public MazeCell cellPrefab;

    // create matrix of maze cells
    private MazeCell[,] cells;

    //delay time
    public float generationStepDelay;

    //size of maze (20x20)
    public IntVector2 size;

    //List of cells already created ( for maze making algorithm)
    List<MazeCell> activeCells = new List<MazeCell>();


    public static IntVector2[] vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0),
    };

    public const int Count = 4;

    public MazeCell getCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        IntVector2 coordinates = RandomCoordinates;
        while (ContainsCoordinates(coordinates) && getCell(coordinates) == null)
        {
            yield return delay;
            CreateCell(coordinates);
            coordinates += getRandomDirectionVector();
        }
    }

    private void CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public IntVector2 getRandomDirectionVector()
    {
        int n = Random.Range(0, Count);
        return vectors[n];
    }
}

