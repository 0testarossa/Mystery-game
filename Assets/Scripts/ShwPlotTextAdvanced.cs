using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ShwPlotTextAdvanced : MonoBehaviour
{
    [Serializable]
    public class DialogStepSettings
    {
        public int stepNumber;
    }

    private Text textComponent;
    private bool showNextText;
    public int fromDialogPage;
    public int toDialogPage;
    private int currentDialogPage;
    private bool canSkipText;
    private IEnumerator coroutine;
    //public string scriptToChangeScene;
    private int currentImportantStepIndex;
    [SerializeField]public DialogStepSettings[] imortantSteps;
    /*private string[] nextPlot = { "pierwszy1 " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC, "pierwszy2", "pierwszy3", "pierwszy4", "drugi1", "drugi2", "drugi3", "drugi4", "trzci1", "trzeci2",
    "trzeci3", "trzeci4", "czwarty1", "czwarty2", "czwarty3", "czwarty4"};*/
    private string[] nextPlot;
    public string npcColor;
    private bool isDialogAvailable;
    private Dictionary<string, string> plot = new Dictionary<string, string>()
        {
            { "cactus", "Gadaj co wiesz! Ja mam mówić? Pogięło Cię, chcę znać prawdę! Ehhhh... po pierwsze " +
            "nie rozumiem co jest z tym kotem i kurą. Wiadomo, że mój przyjaciel był niebiesko czerwony. " + 
            "Ostatnio spędzał ze mną mniej czasu. Więcej z kurą i królikiem. Z kotem nigdy go nie widziałem. " +
            "Ale za to nie widziałem też ostatnio jej z widłami. Nie wykluczam, że królik albo kot użyli ich " +
            "do rozprawienia się z moim przyjacielem. Pewnie leżą teraz gdzieś w środku pola kukurydzy. " +
            "Kura też nie zgłaszała zaginięcia wideł więc pewnie współpracuje z którymś z nich! " +
            "Czy byłem zły na przyjaciela? Nieeeee... czemu? Jestem wściekły, że królik ma coś wspólnego " + 
            "z marchwią i ten bandyta kolegował się z moim przyjacielem. Co z tego, że dorobił się brudnego majątku. "+
            "Takie coś nie jest nigdy bezpieczne... Kiedy ostatni raz się z nim widziałem? Rozmawialiśmy sobie o " +
            "starych czasach jak jeździliśmy razem po świecie. Złapałem go jak wracał z kamiennych struktur. " +
            "A dalej nie wiem... pewnie to były jego ostatnie chwile... ktoś dziabnął go tymi widłami i tyle! " +
            "Mam dość, powiedziałem wszystko co wiem. I jak detektywie... kto jest winny, aż mnie nosi żeby go... "},
            { "cat", "Czy kura jest daltonistką? Aż tak dobrze mojej żony nie poznałem.... chociaż kto i tam wie. " + 
            "Ta relacja to jakiś żart. Kura mi się czepiała, że zostawiłem samochód na stacji benzynowej. " + 
            "Przecież widać to auto stąd. Zresztą dokąd miałbym jechać. To i tak jedyny samochód w tej dolinie " +
            "więc nikomu nie zajmuję miejsca. Wiem, że kaktus interesował się sportowymi autami kiedyś. " + 
            "Ale nikt nie wie co się stało z jego samochodem. Królik? Hmmmm, widziałem go z pomarańczowym " +
            "przyjacielem kaktusa. " +
            "Czy jestem daltonistą? Raczej nie. Chociaż nigdy nie badałem sobie oczu. Ale jeśli chodzi o kolory " +
            "to chyba jedyna rzecz o którą się nie kłóciliśmy z kurą. Ostatnio nie mam na nic czasu bo przewożę " +
            "sporo towaru moim traktorem. Jakiego towaru? Hmmmm... różnego. Trochę takiego... trochę kukurydzy. " +
            "Niemal codziennie mam pełną przyczepę. Ostatni raz kiedy spotkałem się z przyjacielem kaktusa? " + 
            "Chyba po tym jak rozmawialiśmy z królikiem i przeszliśmy się na spacer. Chwilkę przeszedł się z królikiem " +
            "a potem jeszcze z nim porozmawiałem. Przeszliśmy się koło bramy. Chciał się jeszcze przejść do kamiennych " +
            "struktur z jakiegoś powodu, ale nie wnikałem. To był też dzień kiedy kura na mnie nakrzyczała za nie " +
            "odstawienie samochodu... Myślę, że powinieneś pogadać z samym kaktusem. Strasznie go nosi od momentu " +
            "gdy jego przyjaciel zaginął."
            },
            { "rabbit", "Z jakim przyjacielem kaktusa? Pomarańczowym? Kura Ci tak powiedziała? " +
            "Ona jest daltonistką. Zacznijmy od tego, że szukasz niebieskiego przyjaciela. I nie "+ 
            "mieliśmy żadnego zatargu. Co ona Ci nagadała? Mieliśmy wspólny biznes. Nie mogę powiedzieć jaki... " +
            "Jeśli chodzi o marchew to jestem czysty! Od kilku miesięcy żadnej nie wcinałem!" +
            "Nie nosi mnie dlatego, że ją jem tylko dlatego, że jestem królikiem! Z tego powodu " + 
            "przeszliśmy się na pusty obszar w polu kukurydzy. Nic tam nie ma i niczego nie widać, dlatego " +
            "jest dość kameralnie. Można odpocząć od codzienności. Nawet nie musisz tego sprawdzać bo zaraz po " +
            "przesiedzeniu tam kilku minut poszliśmy do lasu. Nasza podróż skończyła się przy dyni w środku lasu. " +
            "Jak mi nie wierzysz to sobie jej poszukaj. Dziwię się, że ta kura tak mnie oczernia. Pogadałbym z  kotem. " + 
            "Oni żyją razem, ale nie zdziwiłbym się, gdyby chciał się od niej uwolnić. Zawsze robi mu awantury. " +
            "Ah i jeszcze jedno... kaktus jest mocno wściekły od kąd zacząłem rozmawiać z jego przyjacielem... a nie z nim. "},
            { "chicken", "Razem z kotem mieszkamy na tej farmie od wielu lat. Wszyscy bardzo się wspieraliśmy " +
            "w tej dolinie. Nie mam zielonego pojęcia co się stało z pomarańczowym przyjacielem kaktusa. " +
            "Oni byli nierozerwalni. Czy pomarańczowy przyjaciel kaktusa miał wrogów? Nieeeee! " +
            "Chyba... w sumie ostatnio królik się z nim pokłócił. Pomarańczowy mówił mi, że ma umówione spotkanie "+
            "z królikiem przy wiatraku. Tego razu widziałam go ostatni raz. Myślę, że powinieneś porozmawiać z królikiem. "+
            "Zresztą to podejrzany typ. Cały czas wcina tę marchew. Skąd od w ogóle ją ma? W tej dolinie jej nie uprawiamy! " +
            "Co prawda sama nie zajmuję się uprawami od lat bo jestem schorowana a kot nigdy nie chce trzymać samochodu "+
            "w pobliżu domu gdybym musiała gdzieś wyjechać. Jak ja już nie mam nawet siły moich wideł unieść! " +
            "Wracając do królika to na pewno ma kontakt z kryminalistami i mógłby skrzywdzić przyjaciela kaktusa. "}
        };

    GameObject message;
    bool isPlayerNear;
    private backpack backpackScript;
    private bool wasTalking;

    void Start()
    {
        nextPlot = new string[] { "pierwszy1 " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC, "pierwszy2", "pierwszy3", "pierwszy4", "drugi1", "drugi2", "drugi3", "drugi4", "trzci1", "trzeci2",
        "trzeci3", "trzeci4", "czwarty1", "czwarty2", "czwarty3", "czwarty4"};
        isDialogAvailable = false;
        canSkipText = false;
        currentImportantStepIndex = 0;
        textComponent = GameObject.Find("TextNPC").GetComponent<Text>();
        textComponent.text = "";
        showNextText = false;
        currentDialogPage = fromDialogPage;

        message = GameObject.Find("Message");
        isPlayerNear = false;
        GameObject player = GameObject.Find("Player");
        backpackScript = player.GetComponent<backpack>();
        wasTalking = false;
    }

    void Update()
    {
        string correctNPC = GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC;
        nextPlot = new string[] { "Polecam Ci porozmawiać z: " + correctNPC, "Może rozmawiając z " + correctNPC + " dowiesz się więcej", "Naprawdę nie powiem Ci więcej niż " + correctNPC, "Ko ko ko ko " + correctNPC, "Pogadaj z: " + correctNPC, "A zagadaj sobie do: " + correctNPC, "Co prawda " + correctNPC + " nie jest tak szybki jak ja... ale może wie więcej", "<Odgłos królika chrupiącego marchew> " + correctNPC, "Nie polecam Ci gadać z moją żoną... pewnie pomyśli, że ją teraz obgaduję... ale może " + correctNPC + " powie coś więcej", "Ta stara kura umie tylko gdakać... ale może " +correctNPC + " powie coś więcej",
        "Miał <liże łapkę> " + correctNPC, "Za koci miętkę to bym za Ciebie pogadał z "+correctNPC, "AAAAAAA... który to?!? Leć ino raz do " + correctNPC + "!!!", "A Ty tu co jeszcze robisz?!? Marny z Ciebie detektyw! Leć do " + correctNPC, "Zaraz stracę cierpliwość! Zjeżdżaj do " + correctNPC, "I ty się nazywasz detektywem? Ogłuchłeś? Idź do "+ correctNPC};
        if (showNextText)
        {
            showNewPlot();
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !isDialogAvailable)
        {
            isDialogAvailable = true;
            if(!wasTalking)
            {
                wasTalking = true;
                backpackScript.putSuspect(npcColor);
                message.GetComponent<Text>().text = "";
            }
        }

        if (isDialogAvailable && Input.GetKeyDown(KeyCode.F))
        {

            print("Correct: " + npcColor + " " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC);
            if (coroutine != null)
            {
                if (npcColor == GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC ||
                    GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC == "finished")
                {
                        
                    canSkipText = true;
                    StopCoroutine(coroutine);
                    coroutine = AnimateText(plot[npcColor]);
                    showNextText = true;
                    GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().updateCorrectNPC();
                    plot[npcColor] = "powiedzialem co wiedzialem";
                }
                else
                {
                    if (currentDialogPage < toDialogPage)
                    {
                        canSkipText = true;
                        StopCoroutine(coroutine);
                        currentDialogPage++;

                        coroutine = AnimateText(nextPlot[currentDialogPage]);
                        showNextText = true;
                    }
                    else
                    {
                        currentDialogPage = fromDialogPage;
                        canSkipText = true;
                        StopCoroutine(coroutine);
                        coroutine = AnimateText(nextPlot[currentDialogPage]);
                        showNextText = true;
                    }
                }
            }
            else
            {
                if (npcColor == GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC ||
                    GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC == "finished")
                {
                    canSkipText = true;
                    coroutine = AnimateText(plot[npcColor]);
                    showNextText = true;
                    GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().updateCorrectNPC();
                    plot[npcColor] = "powiedzialem co wiedzialem";
                }
                else
                {
                    if (currentDialogPage < toDialogPage)
                    {
                        canSkipText = true;
                        currentDialogPage++;

                        coroutine = AnimateText(nextPlot[currentDialogPage]);
                        showNextText = true;
                    }
                    else
                    {
                        currentDialogPage = fromDialogPage;
                        canSkipText = true;
                        coroutine = AnimateText(nextPlot[currentDialogPage]);
                        showNextText = true;
                    }
                }
            }
        }
    }

    void showNewPlot()
    {
        StartCoroutine(coroutine);
        showNextText = false;
    }

    public IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            if (canSkipText)
            {
                canSkipText = false;
            }
            str += strComplete[i++];
            textComponent.text = str;
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            message.GetComponent<Text>().text = "Press [F] to talk with " + npcColor;
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            isDialogAvailable = false;
            canSkipText = true;
            textComponent.text = "";
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            message.GetComponent<Text>().text = "";
            isPlayerNear = false;
        }
    }
}
