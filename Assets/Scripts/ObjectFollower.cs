#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    private enum TipoSeguimiento {Fijo, MomentoLineal, Lerp}

    [Header("Objeto")]
    [Tooltip("Objeto a seguir")]
    [SerializeField] private GameObject body;
    private Rigidbody rb;

    [Tooltip("Fijo: El seguidor se ubica a una distancia fija del objeto seguido en los 3 ejes.\nMomentoLineal: el seguido se ubica a una distancia fija del objeto seguido en un espacio esférico apuntando en la dirección de movimiento del objeto seguido.\nLerp: Se utiliza un valor de lerp para seguir la posición y la rotación del objeto.")]
    [SerializeField] private TipoSeguimiento tipoSeguimiento;

    [Space(8)]
    [Header("Offset")]

    [Space(15)]
    [Tooltip("Distancia al objeto")]
    [SerializeField] private float objectDistance = 1f;

    [Tooltip("Angulo de declinación (Pitch) desde el cual se sigue al objeto")]
    [Range(-90f,90f)]
    [SerializeField] private float declinationAngle = 0f;

    [Tooltip("Angulo de ascención del seguidor respecto al objeto seguido")]
    [Range(-70f,70f)]
    [SerializeField] private float ascensionAngle = 0f;

    [Tooltip("Angulo de precesión del seguidor (Yaw) respecto al objeto seguido")]
    [Range(-45f,45f)]
    [SerializeField] private float precessionAngle = 0f;

    [Tooltip("Offset vertical para el seguidor")]
    [Range(0,5f)]
    [SerializeField] private float verticalOffset = 0f;

    [Space(15)]
    [Tooltip("Valor de Lerp para el seguimiento de la posición")]
    [SerializeField] private float pLerp = 0.2f;
    [Tooltip("Valor de Lerp para el seguimiento de la rotación")]
    [SerializeField] private float rLerp = 0.1f;
    
    private bool tailSet;
    private Vector3 offset;
    private GameObject tail;
    private Vector3 projMGlobal;

    void Start()
    {
        rb = body.GetComponent<Rigidbody>();
        projMGlobal = new Vector3(0,0,0);
        tailSet = false;
    }
    void Update(){
        float  _x = objectDistance*Mathf.Cos(declinationAngle * Mathf.Deg2Rad)*Mathf.Sin(ascensionAngle * Mathf.Deg2Rad);
        float  _y = objectDistance*Mathf.Sin(declinationAngle * Mathf.Deg2Rad);
        float  _z = -objectDistance*Mathf.Cos(declinationAngle * Mathf.Deg2Rad)*Mathf.Cos(ascensionAngle * Mathf.Deg2Rad);
        offset = new Vector3(_x, _y, _z);

        switch (tipoSeguimiento)
        {
            case TipoSeguimiento.Fijo:
                tailSet = false;
                break;
            case TipoSeguimiento.Lerp:
                if(!tailSet){
                    tail = new GameObject("Tail");
                    tail.SetActive(true);
                    tail.transform.position = body.transform.position + offset;
                    tail.transform.rotation = body.transform.rotation;
                    tail.transform.LookAt(body.transform.position);
                    tail.transform.Rotate(new Vector3(0,precessionAngle,0));
                    tail.transform.position = tail.transform.position + new Vector3(0, verticalOffset, 0);
                    tail.transform.SetParent(body.transform);
                    tailSet = true;
                }                
                break;
            case TipoSeguimiento.MomentoLineal:
                // Vector de proyección de M (momento lineal) sobre el plano horizontal de la escena
                projMGlobal = Vector3.ProjectOnPlane(rb.velocity, Vector3.up).normalized;
                tail.SetActive(false);
                offset = new Vector3(0,objectDistance*Mathf.Sin(declinationAngle * Mathf.Deg2Rad),0);
                tailSet = false;
                break;
        }
    }

    void LateUpdate()
    {
        #if DEBUG
            Debug.DrawRay(body.transform.position, projMGlobal, Color.blue);
            Debug.DrawRay(transform.position, 5*transform.forward, Color.red);
        #endif

        switch (tipoSeguimiento)
        {
            case TipoSeguimiento.Fijo:
                // Ubica el seguidor según el offset
                transform.position = body.transform.position + offset;
                // Giro del seguidor apuntando al objeto
                transform.LookAt(body.transform.position);
                // Desviación del seguidor
                transform.Rotate(new Vector3(0,precessionAngle,0));
                transform.position = transform.position + new Vector3(0, verticalOffset, 0);
                break;
            case TipoSeguimiento.MomentoLineal:
                // Posición del seguidor en línea con el objeto seguido en la dirección de M
                transform.position = body.transform.position - objectDistance * projMGlobal;
                // Ubica l seguidor según el offset
                transform.position = transform.position + offset;
                // Giro del seguidor apuntando al objeto
                transform.LookAt(body.transform.position);
                // Desviación del seguidor
                transform.Rotate(new Vector3(0,precessionAngle,0));
                break;
            case TipoSeguimiento.Lerp:
                transform.position = Vector3.Lerp(transform.position, tail.transform.position, pLerp);
                transform.rotation = Quaternion.Lerp(transform.rotation, tail.transform.rotation, rLerp);
                break;
        }
        
        
    }
}
