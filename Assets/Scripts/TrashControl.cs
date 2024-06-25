using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TrashControl : MonoBehaviour
{

    public GameObject[] trashPrefabs;
    public BoxCollider2D gridArea;
    public float spawnInterval = 2f;
    public int maxTrashCount = 5;
    public GameObject gameOverBackground;

    private int trashCount = 0;
    private int currentTrashCount = 0;
    private const int TrashThreshold = 10;
    private const float MinSpawnInterval = 1f;

    public GameObject backGround;
    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 0f, spawnInterval);
        if (gameOverBackground != null)
        {
            gameOverBackground.SetActive(false); 
        }

    }

    private void SpawnTrash()
    {
        Vector3 spawnPosition = RandomizePosition();
        GameObject trash = RandomizeTrash(spawnPosition);

        trashCount++;
        currentTrashCount++;
        CheckGameOver();

        if(trashCount >= TrashThreshold)
        {
            spawnInterval = Mathf.Max(MinSpawnInterval, spawnInterval - 1f);

            CancelInvoke(nameof(SpawnTrash));
            InvokeRepeating(nameof(SpawnTrash), spawnInterval, spawnInterval);

            trashCount = 0;
        }

    }
    private Vector3 RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);



        return new Vector3(x, y, 0.0f);
    }


    private GameObject RandomizeTrash(Vector3 position)
    {
        int index = Random.Range(0, trashPrefabs.Length);
        GameObject selectedTrash = trashPrefabs[index];
        GameObject instantiatedTrash = Instantiate(selectedTrash, position, Quaternion.identity);


        instantiatedTrash.AddComponent<TrashDestroyer>().trashControl = this;

        return instantiatedTrash;
    }

    private void HandleTrashDestroyed()
    {
       currentTrashCount--;
    }

    private void CheckGameOver()
    {
        if (currentTrashCount >= maxTrashCount)
        {
            
            if (gameOverBackground != null)
            {
                gameOverBackground.SetActive(true);
            }

            
            CancelInvoke(nameof(SpawnTrash));
            
            //Time.timeScale = 0;

            backGround.SetActive(true);

            DestroyAllTrash();
        }
    }



    public void RestartGame()
    {
        backGround.SetActive(false);
        gameOverBackground.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void DestroyAllTrash()
    {
        TrashDestroyer[] allTrash = FindObjectsOfType<TrashDestroyer>();
        foreach (TrashDestroyer trash in allTrash)
        {
            Destroy(trash.gameObject);
        }
    }

    private class TrashDestroyer : MonoBehaviour
    {
        public TrashControl trashControl;

        private void OnDestroy()
        {
            if (trashControl != null)
            {
                trashControl.HandleTrashDestroyed();
            }
        }
    }



}
