using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GAMESTAGETYPE
{
    GameStart,
    GamePlay,
    GameEnd


}

public class MapManager : MonoBehaviour
{


    public GameObject[] mapList = null;
    public GameObject[] mine = null;
    public GameObject[] obstacle = null;
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;
    private int damagespeed = 1;
    public GAMESTAGETYPE gamestageType;

    float interval = 0;

    void Awake()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        GAMESTAGETYPE gamestageType = GAMESTAGETYPE.GameStart;
    }
    void Start()
    {
        // StartCoroutine("healthdamage");
        if (gamestageType == GAMESTAGETYPE.GameStart)
        {

            gamestageType = GAMESTAGETYPE.GamePlay;
        }
    }


    void Update()
    {

        if (gamestageType == GAMESTAGETYPE.GamePlay)
        {

            interval += Time.deltaTime;
            if (interval > 6.6f)
            {
                GameObject obj = Instantiate(mapList[Random.Range(0, 2)]);
                obj.transform.position = new Vector3(-0.3f, -8.45f, 0);
                GameObject mineOBJ = Instantiate(mine[Random.Range(0, 2)]);
                mineOBJ.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 0), 0);
                interval = 0;
            }
           
        }



    }

    public IEnumerator healthdamage()
    {
        yield return new WaitForSeconds(3.0f);



    }
}
