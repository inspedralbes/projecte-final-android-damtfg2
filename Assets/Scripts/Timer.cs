using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI generalTimerText;
    [SerializeField] private TextMeshProUGUI levelCountdownText;
    [SerializeField] private GameObject LoseCanvas;


    private float timerUp = 0f;
    private float timerDown = 2f;

    private bool isPaused = false;
    

    private ArrayList checkpointTimes = new ArrayList();


    private void Start()
    {
        //Mostramos el tiempo al inico
        formatTime();

        // Iniciar la actualizaci�n del temporizador cada segundo
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }



    // M�todo para actualizar el temporizador
    private void UpdateTimer()
    {
        if (!isPaused)
        {
            if (timerDown > 0)
            {
                // Incrementar el temporizador general
                timerUp += 1f;

                // Restar a la cuenta atras
                timerDown -= 1f;

                formatTime();
            }
            else {
                LoseCanvas.SetActive(true);
                isPaused = true;
                
            }
        }
    }

    // M�todo para iniciar el temporizador y la cuenta atras
    public void StartTimer()
    {
        isPaused = false;
    }

    // M�todo para pausar el temporizador y la cuenta atras
    public void PauseTimer()
    {
        isPaused = true;
        Debug.Log("Tiempo: " + generalTimerText.text);
        Debug.Log("Cuenta atr�s: " + levelCountdownText.text);

    }

    // M�todo para reiniciar la cuenta atras
    public void ResetCountdown(float tiempo)
    {
        timerDown = tiempo;
        formatTime();

    }

    // M�todo para reiniciar el temporizador general
    public void ResetTimer(float tiempo)
    {
        timerUp = 0;
        formatTime();

    }

    // M�todo para formatear el tiempo
    public void formatTime()
    {
        // Actualizar el texto del temporizador en formato de minutos y segundos
        int minutesUp = Mathf.FloorToInt(timerUp / 60f);
        int secondsUp = Mathf.FloorToInt(timerUp % 60f);

        // Actualizar el texto de la cuenta atras en formato de minutos y segundos
        int minutesDown = Mathf.FloorToInt(timerDown / 60f);
        int secondsDown = Mathf.FloorToInt(timerDown % 60f);
        generalTimerText.text = string.Format("{0:00}:{1:00}", minutesUp, secondsUp);
        levelCountdownText.text = string.Format("{0:00}:{1:00}", minutesDown, secondsDown);

    }

    public string getTime()
    {
        //Cogemos el tiempo de la cuenta atras 
        string time = levelCountdownText.text;        
        return time;

    }

    //Funcion para guardar el tiempo cuando se destruya el checkpoint
    public void saveTimes(string time,string name) {
        
        string checkpointKey = "Checkpoint " + name;
        checkpointTimes.Add(new KeyValuePair<string, string>(checkpointKey, time));
        

        foreach (KeyValuePair<string, string> pair in checkpointTimes)
        {
            Debug.Log(pair.Key + ", Tiempo: " + pair.Value);
        }
    }



}