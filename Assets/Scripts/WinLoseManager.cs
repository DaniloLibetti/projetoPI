using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour
{
    


    [SerializeField]
    private Health _health;
    private float _timer;
    [SerializeField]
    private TextMeshProUGUI _showTimer;
    [SerializeField]
    private TextMeshProUGUI _winLoseText;
    private bool _startStopTimer = true;
    public int _shipMaterials;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        //if (_startStopTimer)
        //{
        //    _timer += Time.deltaTime;
        //    int minutes = Mathf.FloorToInt(_timer / 60.0f);
        //    int seconds = Mathf.FloorToInt(_timer - minutes * 60);
        //    _showTimer.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        //}


        if(_health._health <= .1f)
        {
            _startStopTimer = false;
            StartCoroutine(YouLose());
        }
        if(_shipMaterials >= 100)
        {
            _startStopTimer = false;
            StartCoroutine(YouWin());
        }
    }

    IEnumerator YouLose()
    {
        _winLoseText.text = "You Lose!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator YouWin()
    {
        _winLoseText.text = "Your Win!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScene");
    }
}
