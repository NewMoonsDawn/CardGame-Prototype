using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using CardGame;
public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;

    private Vector3 originalScale;
    public int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    [SerializeField]
    private float selectScale = 1.1f;
    [SerializeField]
    private Vector2 cardPlay;
    [SerializeField]
    private Vector3 playPosition;
    [SerializeField]
    private GameObject glowEffect;
    [SerializeField]
    private GameObject playArrow;
    [SerializeField]
    private float lerpFactor;
    [SerializeField]
    private int cardPlayDivider;
    [SerializeField]
    private float cardPlayMultiplier;
    [SerializeField]
    private bool needUpdateCardPlayPosition;
    [SerializeField]
    private int playPositionYDivider;
    [SerializeField]
    private float playPositionYMultiplier;
    [SerializeField]
    private int playPositionXDivider;
    [SerializeField]
    private float playPositionXMultiplier;
    [SerializeField]
    private bool needUpdatePlayPosition;

    private bool floating = false;
    private SpellbookManager spellbookManager;

    public int maxColumn = 2;

    private LayerMask spellbookLayerMask;
    private LayerMask characterLayerMask;

    private Card cardData;
    private CardDisplay cardDisplay;
    HandManager handManager;
    DiscardManager discardManager;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        updateCardPlayPosition();
        updatePlayPosition();
        spellbookManager = FindObjectOfType<SpellbookManager>();
        handManager = FindObjectOfType<HandManager>();
        discardManager = FindObjectOfType<DiscardManager>();
        cardDisplay = GetComponent<CardDisplay>();

        spellbookLayerMask = LayerMask.GetMask("Spellbook");
        characterLayerMask = LayerMask.GetMask("Characters");
        cardData = cardDisplay.cardData;
    }

    void Update()
    {
       if (needUpdateCardPlayPosition)
       {
            updateCardPlayPosition();
        }
        if (needUpdatePlayPosition)
        {
            updatePlayPosition();
       }

        switch (currentState)
        {
            case 1:
                {
                    HandleHoverState();
                    break;
                }
            case 2:
                {
                    HandleDragState();
                    if (!Input.GetMouseButton(0))
                    {
                        TransitionToStateZero();
                    }
                    break;
                }
            case 3:
                {
                    HandlePlayState();
                    break;
                }
        }
    }


    async private void TransitionToStateZero()
    {
        GameManager.Instance.PlayingCard = false;
        await LerpToPositionStateZero(originalPosition, originalRotation);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0 && !floating && GameManager.Instance.playerManager.playerTurn) //If its in an await, don't set original positions
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;
            currentState = 1;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale;
        if (currentState == 1)
        {
            TransitionToStateZero();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            if (Input.mousePosition.y < cardPlay.y*-1)
            {
                currentState = 3;
                //playArrow.SetActive(true);
            }
        }
    }



    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        if(!GameManager.Instance.PlayingCard)
        {
            GameManager.Instance.PlayingCard = true;
        }
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, playPosition, lerpFactor);
        rectTransform.localRotation = Quaternion.identity;

        if(!Input.GetMouseButton(0))
        {
            cardData = cardDisplay.cardData;
            if(cardData is Ritual ritual)
            {
             if (TryToPlayRitualCard(ritual))
               {
                   floating = false;
                   currentState = 0;
                   glowEffect.SetActive(false);
                   playArrow.SetActive(false);
               }
            }
            if (currentState != 0)
            {
                TransitionToStateZero();
            }
        }

        if (Input.mousePosition.y > cardPlay.y*-1+50)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
    private bool TryToPlayRitualCard(Ritual ritualCard)
    {
        SpellDisplay spell = spellbookManager.GetCurrentSpell();
        if (spell.spellData.spellAttachements.Count < spell.maxAttachments)
        {
            if (spell.CanApplyRitual(ritualCard))
            {
                if (ritualCard.typeLocked)
                {
                    if (ritualCard.applicableTypes.Contains(spell.spellData.Type))
                    {
                        PlayRitualCard(ritualCard, spell);
                        return true;
                    }
                    else
                    {
                        Debug.LogWarning("Incorrect typing");
                        return false;
                    }
                }
                else
                {
                    PlayRitualCard(ritualCard, spell);
                    return true;
                }
            }
            else return false;
        }
        else
        {
            Debug.LogWarning("Too many rituals!");
            return false;
        }
    }
    public void PlayRitualCard(Ritual ritualCard, SpellDisplay spell)
    {
        spell.ApplyRitual(ritualCard);
        discardManager.AddToDiscard(cardData);
        handManager.cardsInHand.Remove(gameObject);
        handManager.UpdateHandVisuals();
        Destroy(gameObject);
    }

    private async Task LerpToPositionStateZero(Vector3 targetPosition, Quaternion targetRotation)
    {
        floating = true;
        rectTransform.localScale = originalScale;
        currentState = 0;
        glowEffect.SetActive(false);
        playArrow.SetActive(false);
        for (int i = 0; i <= 10; i++)
        {
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, targetPosition, lerpFactor * i);
            await Task.Delay(10);
        }
        rectTransform.localPosition = targetPosition;
        rectTransform.localRotation = targetRotation;
        floating = false;

    }

    private void updateCardPlayPosition()
    {
        if (cardPlayDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;
            cardPlay.y = canvasRectTransform.rect.height * segment;
        }
    }

    private void updatePlayPosition()
    {
        if (canvasRectTransform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTransform.rect.width * segmentX;
            playPosition.y = canvasRectTransform.rect.height * segmentY;
        }
    }
}
