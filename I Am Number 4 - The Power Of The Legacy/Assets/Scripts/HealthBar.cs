using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private List<GameObject> healthBars;

    public int min;
    public int max;

    public GameObject startSprite;
    public GameObject endSprite;

    private int direction = 1;

    private void Awake()
    {
        healthBars = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {
        Vector3 previousPosition = startSprite.transform.position;
        float widthOfSingleBar = startSprite.GetComponent<Renderer>().bounds.size.x;
        if (endSprite.transform.position.x - startSprite.transform.position.x < 0)
        {
            direction = -1;
        }

        for (int i = min; i <= max; i++)
        {
            Vector3 currentPosition = new Vector3(previousPosition.x, previousPosition.y, previousPosition.z);
            GameObject healthBarSingle;

            if (i < max)
            {
                healthBarSingle = Instantiate(startSprite);
            }
            else
            {
                healthBarSingle = Instantiate(endSprite);
            }

            healthBarSingle.transform.parent = startSprite.transform.parent;
            healthBarSingle.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);

            SpriteRenderer healthBarSingleSpriteRenderer = healthBarSingle.GetComponent<SpriteRenderer>();
            healthBarSingleSpriteRenderer.enabled = true;

            previousPosition = new Vector3(currentPosition.x + (widthOfSingleBar * direction), previousPosition.y, previousPosition.z);

            healthBars.Add(healthBarSingle);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseHealth(int amountOfHealthLost)
    {
        if (healthBars.Count > 0)
        {
            int lengthOfHealthBars = healthBars.Count - 1;
            for (int i = lengthOfHealthBars; i > lengthOfHealthBars - amountOfHealthLost; i--)
            {
                GameObject currentHealthBarSingle = healthBars[i];

                Destroy(currentHealthBarSingle);
                healthBars.Remove(currentHealthBarSingle);
            }
        }

        if (healthBars.Count == 0)
        {
            // TODO: Character dies.
        }
    }
}
