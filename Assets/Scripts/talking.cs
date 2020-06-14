using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talking : MonoBehaviour
{

    private bool isTalking;
    private bool isEnded;
    public GameObject text;
    private TextMesh textMesh;
    public GameObject door;
    private string[] dialog;
    private string[] dialogAnswer;
    private bool[] answers;
    private bool[] isQuestion;
    private bool isWaitingForAnswer;
    private int dialogNumber;

    public
    // Start is called before the first frame update
    void Start()
    {
        dialogNumber = 0;

        isEnded = false;
        isTalking = false;
    
        textMesh = text.GetComponent<TextMesh>();
        dialog = new string[]{
            "Czesc! Stoje tu od niepamietnych czasow!",
            "Potrzebuje twojej pomocy...",
            "nawet najwieksi medrcy nie znaja odpowiedzi.",
            "Jesl Ci sie uda, te wrota odblokuja Ci przejscie!",
            "Ile to jest 2 + 2 x 4? \n 1) 16 \n 2) 8 \n 3) 10 \n4)Liczba Grahama"
        };
        answers = new bool[] { false, false, true, false };
        isQuestion = new bool[] { false, false, false, false, true };
        dialogAnswer = new string[]
        {
            "Niemal wszyscy daja sie na to nabrac\n " +
            "Bede udawac, ze nigdy Cie nie spotkalem\n" +
            "Sprobuj jeszcze raz...",
            "Szkoda ze Ci sie nie udalo\n" +
            "Ale jak nie dzisiaj to jutro!\n " +
            "Albo jak tylko odejdziesz i podejdziesz do mnie na nowo!",
            "Wow! Jestes wybrancem który rozumie podstawy matematyki!\n" +
            "Ewentualnie jestes wybrancem, który probował kilka razy! Brawo!",
            "Czy Ty w ogóle wiesz czym jest Liczba Grahama?\n" +
            "Wylacz te gre wygoogluj sobie i wroc bogatszy o te wiedze..."
            
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(dialogNumber < isQuestion.Length)
                {
                    if (!isQuestion[dialogNumber])
                    {
                        dialogNumber += 1;
                        textMesh.text = dialog[dialogNumber];
                    }
                }
                
            }
            if (isQuestion[dialogNumber])
            {
                int answerNumber = -1;
                if (Input.GetKeyDown(KeyCode.Alpha1)){ answerNumber = 0; }
                else if (Input.GetKeyDown(KeyCode.Alpha2)) { answerNumber = 1; }
                else if (Input.GetKeyDown(KeyCode.Alpha3)) { answerNumber = 2; }
                else if (Input.GetKeyDown(KeyCode.Alpha4)) { answerNumber = 3; }

                if(answerNumber != -1)
                {
                    textMesh.text = dialogAnswer[answerNumber];
                    isEnded = answers[answerNumber];
                    isTalking = false;

                    //OPEN DOR
                    if (isEnded)
                    {
                        //MovingDoor movingDoor = door.GetComponent<MovingDoor>();
                        //movingDoor.opened = true;
                    }
                }

            }
            
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if (!isEnded)
            {
                isTalking = true;
                dialogNumber = 0;
                textMesh.text = dialog[dialogNumber];
            }
        }
        

    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            if (!isEnded)
            {
                isTalking = false;
                textMesh.text = "!";
            }
            else
            {
                textMesh.text = "";
            }
        }

    }
}
