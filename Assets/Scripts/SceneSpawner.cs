using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    public GameObject ground;
    public GameObject finishLine;
    public GameObject[] buildings;
    public GameObject[] obstacles;
    public GameObject[] academyItems;
    public GameObject[] happinessItems;
    public GameObject[] wealthItems;
    public GameObject[] healthItems;
    public float smallestInterval = 0.5f;
    public float largestInterval = 2f;

    public GameObject[] players;

    private Transform player;
    private float interval = 198f;
    private float nextTriggerPos = 50f;
    private Vector3 nextSpawnPos = new Vector3(0f, 0f, 198f);

    private int randomBuildingIndex;

    private float itemOffset = 150f;
    private int randomTypeIndex;
    private int randomItemIndex;
    private int randomX;
    private float limitX = 7f;
    private float yPos = 1.7f;
    private Vector3 itemPos;
    private GameObject item;

    private Vector3 finishLinePos = new Vector3(-0f, -0.8949966f, 6400f);

    void Awake()
    {
        Instantiate(players[CharacterIndex.index]);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (GameManager.instance.debug)
        {
            finishLinePos.z = 400f;
        }
        Instantiate(finishLine, finishLinePos, finishLine.transform.rotation);
        StartCoroutine(SpawnItems());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.finish)
        {
            return;
        }
        if (player == null || ground == null)
        {
            return;
        }
        if (player.position.z > nextTriggerPos)
        {
            Instantiate(ground, nextSpawnPos, Quaternion.identity);
            if (buildings.Length > 0)
            {
                randomBuildingIndex = Random.Range(0, buildings.Length);
                Instantiate(buildings[randomBuildingIndex], nextSpawnPos, Quaternion.identity);
            }
            nextTriggerPos += interval;
            nextSpawnPos.z += interval;
        }
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            if (GameManager.finish)
            {
                yield break;
            }
            randomTypeIndex = Random.Range(0, 6);
            if (randomTypeIndex == 0)
            {
                randomItemIndex = Random.Range(0, academyItems.Length);
                item = academyItems[randomItemIndex];
                itemPos = new Vector3(Random.Range(-limitX, limitX), yPos, player.position.z + itemOffset);
            }
            else if (randomTypeIndex == 1)
            {
                randomItemIndex = Random.Range(0, happinessItems.Length);
                item = happinessItems[randomItemIndex];
                itemPos = new Vector3(Random.Range(-limitX, limitX), yPos, player.position.z + itemOffset);
            }
            else if (randomTypeIndex == 2)
            {
                randomItemIndex = Random.Range(0, wealthItems.Length);
                item = wealthItems[randomItemIndex];
                itemPos = new Vector3(Random.Range(-limitX, limitX), yPos, player.position.z + itemOffset);
            }
            else if (randomTypeIndex == 3)
            {
                randomItemIndex = Random.Range(0, healthItems.Length);
                item = healthItems[randomItemIndex];
                itemPos = new Vector3(Random.Range(-limitX, limitX), yPos, player.position.z + itemOffset);
            }
            else
            {
                randomItemIndex = Random.Range(0, obstacles.Length);
                item = obstacles[randomItemIndex];
                itemPos = new Vector3(Random.Range(-6.3f, 6.3f), -0.3922f, player.position.z + itemOffset);
            }

            Instantiate(item, itemPos, item.transform.rotation);
            yield return new WaitForSeconds(Random.Range(smallestInterval, largestInterval));
        }
    }
}
