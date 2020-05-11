using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance { private set; get; }

    public static int m_hiscore = 0;

    public int m_ammo = 100;

    Player m_player;

    Text txt_ammo;
    Text txt_hiscore;
    Text txt_life;
    Text txt_score;
    Button button_restart;

    private void Start()
    {
        Instance = this;
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GameObject uicanvas = GameObject.Find("Canvas");
        foreach (Transform t in uicanvas.transform.GetComponentsInChildren<Transform>())
        {
            if (t.name.CompareTo("txt_ammo") == 0)
            {
                txt_ammo = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_hiscore") == 0)
            {
                txt_hiscore = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_life") == 0)
            {
                txt_life = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_score") == 0)
            {
                txt_score = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("Restart Button") == 0)
            {
                button_restart = t.GetComponent<Button>();
                button_restart.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
                button_restart.gameObject.SetActive(false);
            }
        }
    }

    void SetScore(int score)
    {

    }
}
