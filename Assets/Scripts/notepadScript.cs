using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class notepadScript : MonoBehaviour
{
    private const string text = "1. Głos w mojej głowie karze mi napisać takie zdanie: 'W S A D - chodzenie, SPCJA - pomijanie strony, F - Interackja, E - plecak. T - zapiski notatnika' i wstawić zdjęcie klawiatury.Czy to świadczy, że jestem uzależniony od gier komputerowych? (Przebadać się po powrocie do domu)\n" +
        "2. Do siebie: 1)Zrezygnować z kursu kaligrafii... pisanie w ten sposób jest mało czytelne. Literka Z jest dziwna... zebra... heh 2) Znalazłem 2 zdjęcia doliny z przelotu samolotu 40 lat temu. Trudna przeprawa przez góry czyni to miejsce mało odwiedzanym. Według oficjalnej wiedzy, mieszkańcy żyją tam z rolnictwa.\n" +
        "3. Charakterystyczne jest spore jezioro. To w jego okolicy zaginął przyjaciel pewnej postaci. Przydałoby się porozmawiać z mieszkańcami oraz poszukać podejrzanych przedmiotów i miejsc. (Czy największą trudnością tej zagadki będzie rozszyfrowanie mojego własnego pisma???)\n" +
        "4. Zdjęcie z zaginionym - po lewej on we własnej osobie. Po prawej... wygląda na przyjaciela... chociaż wyraz twarzy podpowiada mi, że to mogłaby być fałszywa przyjaźń(?)... nie oceniać książki po okładce... najpierw przesłuchania!\n" +
        "5. Też jak byłem młody marzyłem o podobnym samochodzie. Ciekawe czy będę mieć sposobność zobaczyć tę furę... w każdym razie na pewno muszę przesłuchać kaktusa.\n" +
        "6. Kura - starsza wiekiem. Zdjęcie sprzed wielu lat... jak pozostałe. Po co ja to piszę, przecież i tak nie zapomnę! Ważne: Zacząć przepytywanie od niej!\n" +
        "7. Kot - Chyba siedzi na samochodzie... chociaż kto i tam wie... zdjęcie jest w strasznym stanie. \n" +
        "8. Królik... ku mojemu zdziwieniu... był już karany?!? Na 5 mieszkańców jeden był przestępcą? Warto brać to pod uwagę. Do siebie: Nie zapominać znowu o obecności kapłana! Przed aresztowaniem skonsultować podejrzenia! Uwaga dodatkowa: mam 8 godzin, zanim kaktus nie zdewastuje doliny... czy takie groźby nie są karalne???\n";

    bool isShown;
    // Start is called before the first frame update
    void Start()
    {
        isShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isShown = !isShown;
            if (isShown)
            {
                GameObject.Find("Notepad").GetComponent<Text>().text = text;
                GameObject.Find("NotepadView/Panel").GetComponent<Image>().enabled = true;
                GameObject.Find("Canvas/Timer").GetComponent<TextMeshProUGUI>().enabled = false;
            }
            else
            {
                GameObject.Find("Notepad").GetComponent<Text>().text = "";
                GameObject.Find("NotepadView/Panel").GetComponent<Image>().enabled = false;
                GameObject.Find("Canvas/Timer").GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
    }
}
