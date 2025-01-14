using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GetTime : NetworkBehaviour
{
    string time;
    private Timer Timer;

    private bool pasado=false;

    [SerializeField] private string checkpointId;

    int playerCount = 0;


    void Start()
    {

        Timer = FindAnyObjectByType<Timer>();
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (!pasado)
            {
                playerChechpointTimeRpc();
                pasado = true;
            }

            //Debug.Log("Tiempo: " + time);
           
            
           
        }
        
    }

    [Rpc(SendTo.Owner)]
    private void playerChechpointTimeRpc()
    {
      
        playerCount++;
        if (playerCount > 1)
        {
            //Cogemos el tiempo de la cuenta atras que tenemos en el momento
            time = Timer.getTime();
            //Guardamos el tiempo
            Timer.saveTimes(time,checkpointId);
            //Llamamos a la funcion para destruir el checkpoint
            destoryCheckpointRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void destoryCheckpointRpc()
    {
        //Buscamos el componente Networkobject dentro del checkpoint teleport
        NetworkObject networkObject = this.gameObject.GetComponent<NetworkObject>();
        //Lo eliminamos
        networkObject.Despawn(true);

    }

}
