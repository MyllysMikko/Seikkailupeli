using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectible : MonoBehaviour
{

    public QuestGiver questGiver;
    [SerializeField] bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;

        if (questGiver.quest.questIsCompleted)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public virtual void Interact()
    {
        questGiver.currentCollectibles++;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("H‰‰‰hhh");
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
