using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;
    //数据控制组件
    private GameObject dataGameobject;
    //数据控制文本
    private DataControl datacontrol;

    //难度提升文本
    public Text levelUPText;

    void Start()
    {

        //PlayerPrefs.SetString("level", "level1");


        //获取数据控制组件
        dataGameobject = GameObject.FindGameObjectWithTag("dataControl");
        //获取数据控制文本组件
        if (dataGameobject != null)
        {
            datacontrol = dataGameobject.GetComponent<DataControl>();
        }
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject enemy= Instantiate(hazard, spawnPosition, spawnRotation);
                enemy.name = "enemy"+(datacontrol.GetEnemyListLength()+1);
                datacontrol.addEnemy(enemy);
                yield return new WaitForSeconds(waveWait / hazardCount);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
    //LEVELUP文字动画协成
    IEnumerator levelWait()
    {
        levelUPText.DOText("LEVEL UP!", 1.5f);
        yield return new WaitForSeconds(2.5f);
        levelUPText.text = "";
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        //每到一定分数等级提升
       // PlayerPrefs.SetString("level", "level" + score / 200);
        //LEVELUP动画
        if (score % 300 == 0&&score>=300)
        {
            StartCoroutine(levelWait());
            hazardCount++;
        }
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        SocketHelper s = SocketHelper.GetInstance();
        s.SendMessage("GameOver;"+score.ToString()+";");
    }
    public void Reduce(int number)
    {
        score -= number;
    }
}