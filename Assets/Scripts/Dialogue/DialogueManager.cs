using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Script that controls the dialogue system
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // SERIALIZABLES
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private Image darkBackground;

    [SerializeField]
    private Image characterImageLeft;

    [SerializeField]
    private Image characterImageRight;

    [SerializeField]
    private Animator dialogueAnimator;

    [SerializeField]
    private AudioSource dialogueAudioSource;

    [SerializeField]
    private float timeBetweenLetterTyping;

    // TRACKERS
    private Conversation currentConversation;
    private Dialogue currentDialogue;
    private string currentSentence;
    public bool inConversation = false;
    private bool currentTextTyping = false;

    private Coroutine typingCoroutine;
    private Image currentCharacterImage;
    private AudioClip currentCharacterTalkSound;
    private Queue<ConversationNode> conversationNodes;
    private Queue<string> sentences;
    private Queue<Sprite> characterImages;

    private InputManager input;

    // private PlayerUI playerUI;

    //MISC
    private float inactiveTalkerAlpha = 0.2f;
    public event Action OnConversationStart;
    public event Action OnConversationEnd;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        sentences = new Queue<string>();
        characterImages = new Queue<Sprite>();
        conversationNodes = new Queue<ConversationNode>();
    }

    void Start()
    {
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();

        input.PrimaryAbilityEvent += OnInteract;
        currentCharacterImage = null;
    }

    private void OnInteract()
    {
        //Used to advance the conversation when the interact button is pressed
        //if (MenuManager.gameIsPaused) {return;}

        if (inConversation && currentDialogue != null)
        {
            //If the text is in the process of appearing, causes it to appear fully. Otherwise the next sentence begins to display
            if (!currentTextTyping)
            {
                DisplayNextSentence();
            }
            else
            {
                FinishTypingSentence();
            }
        }
    }

    public void SetNameText()
    {
        nameText.text = "";
    }

    public void SetNameText(Dialogue dialogue)
    {
        nameText.text = dialogue.characterName;
    }

    private void ClearCharacterImages()
    {
        ClearCharacterImage(characterImageLeft);
        ClearCharacterImage(characterImageRight);
    }

    private void ClearCharacterImage(Image characterImage)
    {
        characterImage.enabled = false;
    }

    public void StartConversation(Conversation conversation)
    {
        inConversation = true;
        OnConversationStart?.Invoke();
        dialogueAnimator.SetTrigger("startConversation"); //Starts Conversation in Animator, causes dialogue UI elements to appear
        darkBackground.enabled = true;
        currentConversation = conversation;
        conversationNodes.Clear();
        if (SoundManager.Instance)
        {
            dialogueAudioSource.volume = SoundManager.Instance.GetSoundEffectVolume() / 4;
        }
        ClearCharacterImages();

        foreach (ConversationNode conversationNode in currentConversation.conversationNodes)
        {
            conversationNodes.Enqueue(conversationNode);
        }
        StartDialogue(conversationNodes.Dequeue());
    }

    public void StartDialogue(ConversationNode conversationNode)
    {
        currentDialogue = (Dialogue)conversationNode;
        sentences.Clear();
        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite image in currentDialogue.characterImages)
        {
            characterImages.Enqueue(image);
        }
        currentCharacterTalkSound = currentDialogue.characterTalkSound;
        if (currentCharacterTalkSound)
            dialogueAudioSource.clip = currentCharacterTalkSound;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        SetNameText(currentDialogue);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        //Changes sprite opacity to match whoever is currently speaking
        if (currentDialogue.rightSideCharacterImage)
        {
            characterImageLeft.color = new Color(255, 255, 255, inactiveTalkerAlpha);
            currentCharacterImage = characterImageRight;
        }
        else if (currentDialogue.leftSideCharacterImage)
        {
            characterImageRight.color = new Color(255, 255, 255, inactiveTalkerAlpha);
            currentCharacterImage = characterImageLeft;
        }
        else
        {
            characterImageLeft.color = new Color(255, 255, 255, inactiveTalkerAlpha);
            characterImageRight.color = new Color(255, 255, 255, inactiveTalkerAlpha);
            currentCharacterImage = null;
        }

        if (currentCharacterImage)
            currentCharacterImage.color = new Color(255, 255, 255, 1);

        if (characterImages.Count > 0)
        {
            Sprite image = characterImages.Dequeue();
            if (image != null)
            {
                currentCharacterImage.enabled = true;
                currentCharacterImage.sprite = image;
            }
            else
                ClearCharacterImage(currentCharacterImage);
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        currentSentence = sentence;
        currentTextTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            dialogueAudioSource.Play();
            yield return new WaitForSeconds(timeBetweenLetterTyping);
        }
        currentTextTyping = false;
    }

    private void FinishTypingSentence()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        dialogueText.text = currentSentence;
        currentTextTyping = false;
    }

    public void EndDialogue()
    {
        if (currentCharacterImage != null)
            currentCharacterImage.color = new Color(255, 255, 255, inactiveTalkerAlpha);

        SetNameText();
        StartCoroutine(TypeSentence(""));

        if (conversationNodes.TryDequeue(out ConversationNode nextNode))
        {
            StartDialogue(nextNode);
        }
        else
        {
            EndConversation();
        }
    }

    private void EndConversation()
    {
        dialogueAnimator.SetTrigger("endConversation"); //Another animator trigger
        darkBackground.enabled = false;
        OnConversationEnd?.Invoke();
        StartCoroutine(DialogueCooldown());
    }

    private IEnumerator DialogueCooldown()
    {
        yield return new WaitForEndOfFrame();
        inConversation = false;
    }
}
