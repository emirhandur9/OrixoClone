using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject levelButtonsPanel;


    [SerializeField] GameObject[] levelPrefabs;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI levelIndex;
    
    private int currentLevelIndex;
    private GameObject currentLevel;
    private int maxLevelCount;
    public List<EmptyNode> nodes = new List<EmptyNode>();

    private void Start()
    {
        currentLevelIndex = 0;
        maxLevelCount = levelPrefabs.Length;

        
        
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CheckLevelCompleted())
            {
                StartCoroutine(WaitAndGoNextLevel());
            }
        }
        
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        levelButtonsPanel.SetActive(true);
        CreateLevel(levelPrefabs[currentLevelIndex]);
    }

    public void GoMainMenu()
    {
        //Destroy(currentLevel);
        //levelButtonsPanel.SetActive(false);
        //startPanel.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        Destroy(currentLevel);
        CreateLevel(levelPrefabs[currentLevelIndex]);
    }
    public void CreateLevel(GameObject prefab)
    {
        nodes.Clear();
        GameObject level = Instantiate(prefab);
        level.transform.parent = canvas.transform;
        level.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        currentLevel = level;
        levelIndex.text = (currentLevelIndex + 1).ToString();
        FindNodes();
    }

    public void NextLevel()
    {
        if (currentLevelIndex == maxLevelCount - 1) return;
        Destroy(currentLevel);
        currentLevelIndex++;
        CreateLevel(levelPrefabs[currentLevelIndex]);
    }

    public void BackLevel()
    {
        if (currentLevelIndex == 0) return;
        Destroy(currentLevel);
        currentLevelIndex--;
        CreateLevel(levelPrefabs[currentLevelIndex]);
    }

    private void FindNodes()
    {
        foreach (var item in FindObjectsOfType<EmptyNode>())
        {
            nodes.Add(item);
        }
    }
    public bool CheckLevelCompleted()
    {
        //bool value = false;
        if (nodes.Count == 0) return false;
        foreach (var item in nodes)
        {
            if(item != null)
            {
                if (!item.IsFull && !item.dontChange)
                    return false;
            }
            
        }

        return true;
    }

    IEnumerator WaitAndGoNextLevel()
    {
        yield return new WaitForSeconds(2);
        NextLevel();
    }
}
