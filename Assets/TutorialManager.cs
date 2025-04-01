using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Reference to the UI panel that displays tutorial messages.
    public GameObject tutorialPanel;
    // Text component where the tutorial message is displayed.
    public Text tutorialText;
    // Button that the player clicks to advance the tutorial.
    public Button nextButton;

    // A queue to store tutorial steps.
    private Queue<TutorialStep> tutorialSteps = new Queue<TutorialStep>();

    void Start()
    {
        // Initialize tutorial steps.
        // 'waitForEvent' is true for steps that require an in-game action before continuing.
        tutorialSteps.Enqueue(new TutorialStep("Welcome to the Plant Nurturing Simulator! Your goal is to nurture a plant through proper care.", false));
        tutorialSteps.Enqueue(new TutorialStep("Step 1: Pick up the seed bag to plant your seed. Move your character to the seed bag.", true));
        tutorialSteps.Enqueue(new TutorialStep("Step 2: When you approach the pot, the seed will be planted automatically.", true));
        tutorialSteps.Enqueue(new TutorialStep("Step 3: Fill the watering can by going to the well.", true));
        tutorialSteps.Enqueue(new TutorialStep("Step 4: Bring the filled watering can to the plant to water it. Watch as the soil texture changes.", true));
        tutorialSteps.Enqueue(new TutorialStep("Step 5: Use the lamp switch to toggle the LED lamp on or off, adjusting the light as needed.", true));
        tutorialSteps.Enqueue(new TutorialStep("Step 6: Apply fertiliser to boost plant growth by interacting with the fertiliser bags.", true));
        tutorialSteps.Enqueue(new TutorialStep("Great job! You have learned the basics. Now continue nurturing your plant and watch it grow!", false));

        // Set up the button listener.
        nextButton.onClick.AddListener(OnNextButtonClicked);

        // Show the first tutorial step.
        ShowNextStep();
    }

    // Display the next tutorial step.
    void ShowNextStep()
    {
        if (tutorialSteps.Count > 0)
        {
            TutorialStep step = tutorialSteps.Dequeue();
            tutorialText.text = step.message;
            tutorialPanel.SetActive(true);

            // If this step waits for an in-game event, disable the Next button.
            nextButton.gameObject.SetActive(!step.waitForEvent);
        }
        else
        {
            // No more steps; hide the tutorial panel.
            tutorialPanel.SetActive(false);
        }
    }

    // Called when the player clicks the "Next" button.
    void OnNextButtonClicked()
    {
        ShowNextStep();
    }

    // Call this from other scripts to enable the Next button when an event is completed.
    public void AdvanceTutorial()
    {
        nextButton.gameObject.SetActive(true);
    }
}

// Class to represent a single tutorial step.
public class TutorialStep
{
    public string message;       // The tutorial message.
    public bool waitForEvent;    // If true, this step waits for an in-game event.

    public TutorialStep(string message, bool waitForEvent)
    {
        this.message = message;
        this.waitForEvent = waitForEvent;
    }
}
