using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{

    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController sceneController;
    private int _id;
    private AudioSource flip;
    private void Awake()
    {
        flip = GetComponent<AudioSource>();
    }

    public int Id
    {
        get { return _id; }
    }

    public void OnMouseDown()
    {
        if(cardBack.activeSelf && sceneController.canReveal)
        {   
            flip.Play();
            cardBack.SetActive(false);
            sceneController.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
