using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Striker, RWUI, BWUI;
    public TextMeshProUGUI RedTeamScore, BlueTeamScore, TurnScore;
    private int redscore, bluescore, turnScore;
    [SerializeField] bool canFinish = false, ballFinished =false, strikerFinished = false, GameFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        turnScore = 0;
        TurnScore.text = turnScore.ToString();
        turnScore = PlayerPrefs.GetInt(nameof(turnScore));
        TurnCount();
        redscore = PlayerPrefs.GetInt(nameof(redscore));
        RedTeamScore.text = redscore.ToString();
        bluescore = PlayerPrefs.GetInt(nameof(bluescore));
        BlueTeamScore.text = bluescore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnScore >= 9) 
        {
           GameFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Striker"))
        {
            if (canFinish && !ballFinished) 
            {
                BlueTeamScoreUpdate(1);
                PlayerPrefs.SetInt(nameof(bluescore), bluescore);
            }          
        }

        if(other.CompareTag("Ball"))
        {
            if (canFinish && !strikerFinished)
            {
                RedTeamScoreUpdate(1);
                PlayerPrefs.SetInt(nameof(redscore), redscore);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            Striker.GetComponent<StrikerController>().Run = true;
            Invoke(nameof(CanFinishActive), 1f);
        }
    }

    void CanFinishActive()
    {
        canFinish = true;
        Striker.AddComponent<Rigidbody>();
    }

    void RedTeamScoreUpdate(int amount)
    {
        ballFinished = true;
        redscore += amount;
        RedTeamScore.text = redscore.ToString();
        PlayerPrefs.SetInt(nameof(redscore), redscore);
        Invoke("NextTurn", 1f);
    }

    void BlueTeamScoreUpdate(int amount)
    {
        strikerFinished = true;
        bluescore += amount;
        BlueTeamScore.text = bluescore.ToString();
        
        Invoke("NextTurn", 1f);
    }

    private void TurnCount()
    {
        turnScore++;
        TurnScore.text = turnScore.ToString();
        PlayerPrefs.SetInt(nameof(turnScore), turnScore);
    }

    void NextTurn()
    {
        if(!GameFinished)
        {
           SceneManager.LoadScene(0);
        }else if(GameFinished && redscore > bluescore)
        {
            Time.timeScale = 0;
            RWUI.SetActive(true);
        }
        else if (GameFinished && redscore < bluescore)
        {
            Time.timeScale = 0;
            BWUI.SetActive(true);
        }

    }
}
