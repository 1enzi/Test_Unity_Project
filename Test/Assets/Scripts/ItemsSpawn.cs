using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsSpawn : MonoBehaviour
{
    [Tooltip("Лист со всеми имеющимися объкутами для генерации.")]
    [SerializeField] private List<ItemsData> itemData;

    [Tooltip("Объект сетки для генерации предметов.")]
    [SerializeField] private GameObject gridObject;

    [Tooltip("Префаб родительской ячейки объекта сетки (задаёт предмету задний фон).")]
    [SerializeField] private GameObject CellParent;

    [Tooltip("Префаб ячейки объекта.")]
    [SerializeField] private GameObject CellObject;

    [Tooltip("Фиксированное количество предметов в одной линии сетки.")]
    [SerializeField] private int ItemsPerRow;

    [Tooltip("Объект текстового элемента для вывода задачи.")]
    [SerializeField] private GameObject questionText;

    [Tooltip("Максимальное число уровней сложности в игре.")]
    [SerializeField] private int maxLevelCount;

    public static Dictionary<GameObject, ItemScript> Items;
    private Queue<GameObject> currentItems;
    private int[] exceptions;

    public GameObject RestartObject;

    public static bool LevelIsActive { get; set; }
    private int RandomRight { get; set; }
    private int exceptionsCount { get; set; }
    private int maxItemsNumber { get; set; }
    private int currentLevel { get; set; }
    private int SpawnCounter { get; set; }

    List<GameManagement.levelData> levelsData = new List<GameManagement.levelData>();

    private int GetRand()
    {
        int RightRand = Random.Range(0, ((ItemsPerRow * currentLevel) - 1));

        return RightRand;
    }

    private void ResetSpawnCounter()
    {
        SpawnCounter = 0;
    }

    private void ResetLevel()
    {
        currentLevel = 1;
    }

    private void NewLevel()
    {
        currentLevel++;
    }

    public static void NewLevelIsActive()
    {
        if (!LevelIsActive) 
        {
            LevelIsActive = true;
        }

        else
        {
            LevelIsActive = false;
        }
    }

    public void ClearCreatedItems()
    {
        foreach (Transform child in gridObject.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateLevel()
    {
        if (currentLevel <= maxLevelCount)
        {
            for (int i = 0; i < (ItemsPerRow * currentLevel); i++)
            {
                RandomRight = GetRand();
                var newCellObject = Instantiate(CellObject);
                var script = newCellObject.GetComponent<ItemScript>();
                Items.Add(newCellObject, script);
                currentItems.Enqueue(newCellObject);
            }

            GridSpawn.SetGridSize(gridObject, levelsData, currentLevel);
            StartCoroutine(SpawnItems());
            NewLevelIsActive();
        }

        else
        {
            RestartObject.SetActive(true);
        }
    }

    private void Start()
    {
        RestartObject.SetActive(false);
        ResetSpawnCounter();
        exceptionsCount = 0;

        ResetLevel();

        Items = new Dictionary<GameObject, ItemScript>();
        currentItems = new Queue<GameObject>();
        levelsData.Add(new GameManagement.levelData { levelName = "1", levelsLeft = 2, countOfRows = 1 });
        levelsData.Add(new GameManagement.levelData { levelName = "2", levelsLeft = 1, countOfRows = 2 });
        levelsData.Add(new GameManagement.levelData { levelName = "3", levelsLeft = 0, countOfRows = 3 });

        for (int i = 0; i < levelsData.Count; i++)
        {
            maxItemsNumber += levelsData[i].countOfRows * ItemsPerRow;
        }
        
        exceptions = new int[maxItemsNumber];
    }

    private void Update()
    {
        if (!LevelIsActive)
        {
            ClearCreatedItems();
            CreateLevel();
        }
    }

    private IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(0.1f);

        if (SpawnCounter < 0)
        {
            ResetSpawnCounter();
            StartCoroutine(SpawnItems());
        }

        else if (SpawnCounter < (ItemsPerRow * currentLevel))
        {
            var newCellParent = Instantiate(CellParent, gridObject.GetComponent<Transform>(), false);            
            var newCellObject = currentItems.Dequeue();
            newCellObject.transform.SetParent(newCellParent.GetComponent<Transform>(), false);

            var script = Items[newCellObject];         

            int rand;
            bool flag;

            do
            {
                flag = true;
                rand = Random.Range(0, itemData.Count);

                if (exceptionsCount != 0)
                {
                    for (int i = 0; i < exceptionsCount; i++)
                    {
                        if (rand == exceptions[i])
                        {
                            flag = false;
                        }
                    }
                }
            }
            while (!flag);

            exceptions[exceptionsCount] = rand;
            exceptionsCount++;

            script.Init(itemData[rand]);

            GameManagement.BounceEffect(newCellObject);

            if (SpawnCounter == RandomRight)
            {
                string RightText = script.GetName();
                script.SetRightAnswer();
                questionText.GetComponent<Text>().text = "Find " + RightText;
                GameManagement.HalfBounceEffect(questionText);
            }

            SpawnCounter++;
            StartCoroutine(SpawnItems());
        }

        else if (SpawnCounter == ItemsPerRow * currentLevel)
        {            
            ResetSpawnCounter();
            NewLevel();
            StopCoroutine(SpawnItems());
        }
    }
}
