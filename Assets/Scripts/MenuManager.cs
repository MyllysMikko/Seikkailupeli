using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] PlayerController playerController;
    [SerializeField] DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerController.state == PlayerController.Playerstate.Alive)
            {
                playerController.state = PlayerController.Playerstate.Pause;
                menu.SetActive(true);
            }
        }
    }


    public void OnResume()
    {
        playerController.state = PlayerController.Playerstate.Alive;
        menu.SetActive(false);
    }

    public void OnPrintCompleted()
    {
        dataManager.PrintCompleted();
    }

    public void OnPrintInProgress()
    {
        dataManager.PrintInProgress();
    }
}
