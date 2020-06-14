using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookText : MonoBehaviour
{
    private Text textComponent;
    private Text textComponent2;
    private bool showNextText;
    private int fromDialogPage;
    private int toDialogPage;
    private int currentDialogPage;
    private bool canSkipText;
    private IEnumerator coroutine;
    private string[] plot = {"NOTATNIK: Głos w mojej głowie karze mi wstawić zdjęcie klawiatury i napisać takie zdanie: 'W S A D - chodzenie, SPCJA - pomijanie strony, F - Interackja, E - plecak. T - wydrukowany NOTATNIK'. Czy to świadczy, że jestem uzależniony od gier komputerowych? (Przebadać się po powrocie do domu)",
        "Do siebie: 1)Zrezygnować z kursu kaligrafii... pisanie w ten sposób jest mało czytelne. Literka Z jest dziwna... zebra... heh 2) Znalazłem 2 zdjęcia doliny z przelotu samolotu 40 lat temu. Trudna przeprawa przez góry czyni to miejsce mało odwiedzanym. Według oficjalnej wiedzy, mieszkańcy żyją tam z rolnictwa.",
        "Charakterystyczne jest spore jezioro. To w jego okolicy zaginął przyjaciel pewnej postaci. Przydałoby się porozmawiać z mieszkańcami oraz poszukać podejrzanych przedmiotów i miejsc. (Czy największą trudnością tej zagadki będzie rozszyfrowanie mojego własnego pisma???)",
        "Zdjęcie z zaginionym - po lewej on we własnej osobie. Po prawej... wygląda na przyjaciela... chociaż wyraz twarzy podpowiada mi, że to mogłaby być fałszywa przyjaźń(?)... nie oceniać książki po okładce... najpierw przesłuchania!",
        "Też jak byłem młody marzyłem o podobnym samochodzie. Ciekawe czy będę mieć sposobność zobaczyć tę furę... w każdym razie na pewno muszę przesłuchać kaktusa.",
        "Kura - starsza wiekiem. Zdjęcie sprzed wielu lat... jak pozostałe. Po co ja to piszę, przecież i tak nie zapomnę! Ważne: Zacząć przepytywanie od niej!",
        "Kot - Chyba siedzi na samochodzie... chociaż kto i tam wie... zdjęcie jest w strasznym stanie. ",
        "Królik... ku mojemu zdziwieniu... był już karany?!? Na 5 mieszkańców jeden był przestępcą? Warto brać to pod uwagę. Do siebie: Nie zapominać znowu o obecności kapłana! Przed aresztowaniem skonsultować podejrzenia! Uwaga dodatkowa: mam 8 godzin, zanim kaktus nie zdewastuje doliny... czy takie groźby nie są karalne???"
     };

    // Start is called before the first frame update
    void Start()
    {
        fromDialogPage = 0;
        toDialogPage = plot.Length - 1;
        canSkipText = false;
        textComponent = GameObject.Find("TextLeft").GetComponent<Text>();
        textComponent.text = "";
        textComponent2 = GameObject.Find("TextRight").GetComponent<Text>();
        textComponent2.text = "";
        showNextText = true;
        currentDialogPage = fromDialogPage;
        coroutine = AnimateText(plot[currentDialogPage]);
    }

    // Update is called once per frame
    void Update()
    {
        if (showNextText)
        {
            showNewPlot();
        }

        if (Input.GetKeyDown("space"))
        {
            if (currentDialogPage < toDialogPage)
            {
                canSkipText = true;
                StopCoroutine(coroutine);
                currentDialogPage++;
                coroutine = AnimateText(plot[currentDialogPage]);
                showNextText = true;
            }
            else
            {
                SceneManager.LoadScene(0);
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
                //break;
            }
            str += strComplete[i++];
            if(currentDialogPage % 2 == 0)
            {
                textComponent.text = str;
            } else
            {
                textComponent2.text = str;
            }
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }
}
