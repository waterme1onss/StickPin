using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Transform startPoint;
    private Transform spawnPoint;
    private Pin currentPin;
    public GameObject Pin;
    private bool isGameOver = false;
    private int score = 0;
    private Camera mainCamera;
    public Text scoreText;
    public float animationSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        SpawnPin();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver) return;
        if(Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currentPin.StartFly();
            SpawnPin();
        }
    }

    void SpawnPin()
    {
        currentPin = GameObject.Instantiate(Pin,spawnPoint.position, Pin.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if(isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while(true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, animationSpeed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, animationSpeed * Time.deltaTime);
            if(Mathf.Abs(mainCamera.orthographicSize - 4) < 0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
