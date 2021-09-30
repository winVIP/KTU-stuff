using System;

namespace OPP_egzas_kodas
{
    class Program
    {
        static void Main(string[] args)
        {
            WikiApp english = new Proxy("English");
            WikiApp lithuanian = new Proxy("Lithuanian");
            ClientLt clientLt = new ClientLt();
            Console.WriteLine("Isvertimo i anglu kalba atsakymas: " 
                              + clientLt.giveAnswer("Zodis", english));
            Console.WriteLine("Isvertimo i lietuviu kalba atsakymas: "
                              + clientLt.giveAnswer("Word", lithuanian));
        }
    }

    public class ClientLt
    {
        public string giveAnswer(string toTranslate, WikiApp language)
        {
            return language.askQuestion(toTranslate);
        }
    }

    public interface WikiApp
    {
        public string askQuestion(string toTranslate);
    }

    public class Proxy : WikiApp
    {
        private WIkiTranslator lithuanian = new WIkiTranslator();
        private WikiServiceEng english = new WikiServiceEng();
        private int language = -1;
        public Proxy(string Language)
        {
            if (Language.Equals("Lithuanian"))
            {
                language = 0;
            }
            else if (Language.Equals("English"))
            {
                language = 1;
            }
        }
        public string askQuestion(string toTranslate)
        {
            if (language == 0)
            {
                return toLithuanian(toTranslate);
            }
            else if (language == 1)
            {
                return toEnglish(toTranslate);
            }

            return "Tokios kalbos nera";
        }

        public string toEnglish(string toTranslate)
        {
            return english.askQuestion(toTranslate);
        }

        public string toLithuanian(string toTranslate)
        {
            return lithuanian.askQuestion(toTranslate);
        }
    }

    public class WIkiTranslator : WikiApp
    {
        public string askQuestion(string toTranslate)
        {
            return "Isversta i lietuviu";
        }
    }

    public class WikiServiceEng : WikiApp
    {
        public string askQuestion(string toTranslate)
        {
            return "Translated to english";
        }
    }
}
