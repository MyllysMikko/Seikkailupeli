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
        // Kun painetaan ESC, avaa valikko
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerController.state == PlayerController.Playerstate.Alive)
            {
                playerController.state = PlayerController.Playerstate.Pause;
                menu.SetActive(true);
            }
        }
    }

    // Sulkee valikon ja jatkaa peliä.
    public void OnResume()
    {
        playerController.state = PlayerController.Playerstate.Alive;
        menu.SetActive(false);
    }

    // Kutsuu DataManagerin PrintCompleted metodia. Konsoliin tulostuu kaikki valmiit Questit.
    public void OnPrintCompleted()
    {
        dataManager.PrintCompleted();
    }

    // Kutsuu DataManagerin PrintInProgress metodia. Konsoliin tulostuu kaikki keskeneräiset Questit.
    public void OnPrintInProgress()
    {
        dataManager.PrintInProgress();
    }
}
