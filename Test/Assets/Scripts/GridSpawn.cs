using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridSpawn : MonoBehaviour
{
    public static void SetGridSize(GameObject gridObject, List<GameManagement.levelData> levelsData, int currentLevel)
    {
        int CurrentLevelsLeft = 0;
        int CurrentCountOfRows = 0;

        foreach (var data in levelsData.Where(n => n.levelName == currentLevel.ToString()))
        {
            CurrentLevelsLeft = data.levelsLeft;
            CurrentCountOfRows = data.countOfRows;
        }

        float gridWidth = 100;
        float gridHeigth = 100;

        gridObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gridWidth, gridHeigth - (CurrentLevelsLeft * (gridHeigth / 3)));
        gridObject.GetComponent<GridLayoutGroup>().constraintCount = CurrentCountOfRows;
    }
}
