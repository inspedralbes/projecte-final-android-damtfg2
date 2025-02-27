using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMarks : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject TextPrefab;
    private GameObject nuevo;
    private Marca? marca;
    private Teleport tp;
    private GameOptions options;

    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Teleport") && options.TakeTPs)
            {
            if (!nuevo)
            {
                nuevo = Instantiate(TextPrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 1.5f, other.gameObject.transform.position.z), playerPrefab.transform.rotation);
            }
            marca = tp.GetMarca(other.gameObject.transform.position);
            Debug.Log(marca);
            }
            
        }
    
    public Marca? TM()
    {
        if (marca != null)
        {
            tp.TakeMark((Marca)marca);
        }
        return marca;
    }

    private void OnTriggerExit(Collider other)
    {
            marca = null;
            Destroy(nuevo);
    }
    public void Awake()
    {
        tp = GetComponent<Teleport>();
        options = GetComponent<GameOptions>();
    }

}
