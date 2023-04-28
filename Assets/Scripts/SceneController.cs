using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 3;
    public const int gridColumns = 8;
    public const float offsetX = 2.85f;
    public const float offsetY = 3.5f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh triesLabel;
    [SerializeField] private TextMesh timerLabel;
    [SerializeField] private GameObject congrats;
    [SerializeField] private GameObject musicPlayer;

    private int _score = 0;
    private int _tries = 0;
    private float _timer = 0;
    private MainCard _firstRevealedCard;
    private MainCard _secondRevealedCard;

   
    public bool canReveal
    {
        get
        {
            return _secondRevealedCard == null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = originalCard.transform.position;

        int[] cardNumbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11 };
        cardNumbers = ShuffleCards(cardNumbers);

        MainCard card;
        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard);

                int index = j * gridColumns + i;
                int id = cardNumbers[index];
                card.ChangeSprite(id, images[id]);

                float xPos = startPosition.x + (i * offsetX);
                float yPos = startPosition.y + (j * offsetY);
                card.transform.position = new Vector3(xPos, yPos, startPosition.z);
            }
        }
    }

    private int[] ShuffleCards(int[] cardNumbers)
    {
        int[] shuffledCardNumbers = cardNumbers.Clone() as int[];

        for (int i = 0; i < shuffledCardNumbers.Length; i++)
        {
            int temp = shuffledCardNumbers[i];
            int r = UnityEngine.Random.Range(0, shuffledCardNumbers.Length);
            shuffledCardNumbers[i] = shuffledCardNumbers[r];
            shuffledCardNumbers[r] = temp;
        }
        return shuffledCardNumbers;
    }

    public void CardRevealed(MainCard card)
    {
        if(_firstRevealedCard == null)
        {
            _firstRevealedCard = card;
        }
        else
        {
            _secondRevealedCard = card;
            _tries++;
            triesLabel.text = "Tries: " + _tries;
            StartCoroutine(CheckCardMatchCoroutine());
        }
    }

    private IEnumerator CheckCardMatchCoroutine()
    {
        if(_firstRevealedCard.Id == _secondRevealedCard.Id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
            if (_score == 12)
            {
                musicPlayer.SetActive(false);
                congrats.SetActive(true);
                
            }
            yield return new WaitForSeconds(0.5f);
            _firstRevealedCard.gameObject.SetActive(false);
            _secondRevealedCard.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealedCard.Unreveal();
            _secondRevealedCard.Unreveal();
        }
        _firstRevealedCard = null;
        _secondRevealedCard = null;
    }
    // Update is called once per frame
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        timerLabel.text = "Timer: " + _timer.ToString("F0") + "s";
    }
}
