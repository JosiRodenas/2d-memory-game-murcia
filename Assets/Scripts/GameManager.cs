using UnityEngine;
using System.Collections;

using TMPro; //Si usamos TextMeshPro lo necesitamos, en nuesro caso es el marcador
using UnityEngine.SceneManagement; //Paquete para reiniciar la escena

public class GameManager : MonoBehaviour
{
    public GameObject[] cartas;

    private int[] cartas_boca_arriba = new int[2];//aqui le decimos el tamaño con int[2]

    private string estado;
    public string EstadoActual => estado;

    public TextMeshProUGUI textoMarcador;//esto es nuestro marcador de puntos
    public GameObject botonReiniciar;//noton reinciar


    private int parejasLocalizadas = 0; //variable para obtener las parejas bien hechas
    private int totalParejas = 5;


    public GameObject panelInicio;


   

  

    void Start()
    {

        //activamos el panel
        panelInicio.SetActive(true);
        



        //la verdad, no sabia que se podian encontrar y usar las etiquetas de esta manera a la hora de programar
        cartas = GameObject.FindGameObjectsWithTag("Carta");//aqui le decimos que encuentre los que tengan la etiqueta carta
       
        AjustarPosiciones();
        
        
        //le metemos el valor -1

        cartas_boca_arriba[0] = -1;
        cartas_boca_arriba[1] = -1;


        //recorremos con un bucle for para meter en el array los objetos con la tag "carta"
        for (int i = 0; i < cartas.Length; i++)
        {


            //con GetComponent le decimos que coja el compenente indice que se llama Carta
            cartas[i].GetComponent<Carta>().indice = i;
        }

        //seteamos el estado a inicial
        estado = "inicial";

        botonReiniciar.SetActive(true);


        ActualizarMarcador();


        
    }

    public void CambiarEstado(int indiceP)
    {
        // Si estamos esperando a que se volteen las cartas, no hacemos nada
        if (estado == "esperando") return;

        
        //entramos en el if comporbando el estado
        if (estado == "inicial")
        {
            cartas_boca_arriba[0] = indiceP;

            //cambiamos estado
            estado = "carta_descubierta";
        }

        else if (estado == "carta_descubierta") 
        {
            // Evitar que se pulse la misma carta dos veces
            if (cartas_boca_arriba[0] == indiceP) return;

            cartas_boca_arriba[1] = indiceP;


            //metemos en dos variables las cartas que se van clicando y cogemos el valor de estas para ve si es el mismo, este valor se ha colocado a mano en unity
            int valor1 = cartas[cartas_boca_arriba[0]].GetComponent<Carta>().valor;
            int valor2 = cartas[cartas_boca_arriba[1]].GetComponent<Carta>().valor;

            //entamos en bucle if para comparar valores y ver si son pareja

            if (valor1 == valor2)
            {
                Debug.Log("¡Has enconrado una pareja!");

                parejasLocalizadas++; // sumamos a la variable

                ActualizarMarcador();

                if (parejasLocalizadas == totalParejas)
                {
                    Debug.Log("¡Has ganadoo!");
                }



                ResetearTurno(); 
            }
            else
            {
                Debug.Log("No son pareja, prueba otra vez :( ");

                //inicializamos corrutina para ver que las cartas se den la vuelta lentamene
                StartCoroutine(Voltear_con_espera());
            }
        }
    }
    void ActualizarMarcador()
    {
        //esto se vera en el TextMexPRO
       
        textoMarcador.GetComponent<TextMeshProUGUI>().text = "Has encontrado: " + parejasLocalizadas + " de " + totalParejas;


    }


    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//funcion para reseear ola escena
    }

    IEnumerator Voltear_con_espera()
    {
        estado = "esperando";


        yield return new WaitForSeconds(1f); // Espera 1 segundo    1 float

        // Volteamos las dos cartas que no son pareja activando el metodo voltear
        cartas[cartas_boca_arriba[0]].GetComponent<Carta>().Voltear();
        cartas[cartas_boca_arriba[1]].GetComponent<Carta>().Voltear();

        ResetearTurno();
    }

    void ResetearTurno()
    {

        //rseteamos a esado inicial
        cartas_boca_arriba[0] = -1;
        cartas_boca_arriba[1] = -1;


        estado = "inicial";
    }


    void AjustarPosiciones()
    {
        // Recorremos el arrays de las cartas y
        for (int i = 0; i < cartas.Length; i++)
        {
            // conforme se recorre guardamos en una variable tipo vector3 

            //aqui tenemos que explicar que   Vector3   es un tipo de variable que guarda las tres posiciones de los ejes (z,x,y)   
            
            Vector3 tempPos = cartas[i].transform.position;

            //En Unity transform.position es una porpiedad de los objeos que siempe devuelve un Vector3




            // generamos posiciones aleatorias y le asignamos variable
            int PosicionAleator = Random.Range(0, cartas.Length);//el primer parametro es inclusivo y el segundo es exclusivo


            // Cambiamos las posiciones entre la carta actual y la aleatoria
            cartas[i].transform.position = cartas[PosicionAleator].transform.position;
            cartas[PosicionAleator].transform.position = tempPos;
        }
    }

    //se lo asigno al boton y cuando lo pulse se quita el planel
    public void quitarPanel()
    {
        panelInicio.SetActive(false);
    }
}