using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Poziom2DialogueManager : MonoBehaviour
{
	public Text nameText;
	public Text dialogueText;
	public GameObject player;
	public GameObject holder;
	public Animator animator;
	public GameObject buttonYes;
	public GameObject buttonNo;
	public GameObject buttonNext;
	public Texture2D cursorArea;

	private Queue<string> sentences;

	public Sprite imageDialogue2;
	public GameObject Dialogbox;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log("Click");
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		if (scene.name == "Ending")
        {
			if (sentences.Count == 1)
            {
				buttonYes.SetActive(true);
				buttonNo.SetActive(true);
				buttonNext.SetActive(false);
				Dialogbox.GetComponent<Image>().sprite = imageDialogue2;
			}

		}

			string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}



	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		Debug.Log("End of conversation");
		holder.SetActive(true);
		Vector2 hotspot = new Vector2(cursorArea.width / 2f, cursorArea.height / 2f);
		Cursor.SetCursor(cursorArea, hotspot, CursorMode.Auto);
		player.GetComponent<PlayerMovement>().enabled = true;
		player.GetComponent<PlayerMousePosition>().enabled = true;
	}
}
