using UnityEngine;

public class Carta : MonoBehaviour
{


    
    //aqui todo se realizó siguiendo el guion del trabajo

    public Sprite anverso;
    public Sprite reverso;


    public int valor;


    private SpriteRenderer SpriteRenderer;

    private bool bocaArriba;//booleano que controla si la carta está boca arriba


    public int indice;


    private GameObject gameManager;


    void Awake()//evento que siempre se ejecuta anes del start
    {
       
        SpriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = GameObject.FindWithTag("GameManager");
    }

    
    void Start()
    {

        //para ponerlas todas del reverso
        //asignamos una propiedad scrite a reverso
        SpriteRenderer.sprite = reverso;

        
        bocaArriba = false;

    }


    void Update()
    {
        
    }


    private void OnMouseDown() //para hacer click
    {

        // buscamos el GameManager
        GameManager gm = gameManager.GetComponent<GameManager>();

        // si comprueba que está "esperando" o si la carta está boca arriba, no hace nada
        if (gm.EstadoActual == "esperando" || bocaArriba) return;

        // si detecta que todo está correcto, le damos la vuelta
        Voltear();
        gm.CambiarEstado(indice);

    }

    public void Voltear()
    {
      
        
            //si detecta que está bocaarriba no esta asi
            bocaArriba = !bocaArriba;

            if (bocaArriba)
            {
                SpriteRenderer.sprite = anverso; 
            }
            else
            {
                SpriteRenderer.sprite = reverso; 
            }
        
    }
}
