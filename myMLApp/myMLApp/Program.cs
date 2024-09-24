/*Applikationen använder maskininlärning för att avgöra om recensioner och omdömen är positiva eller negativa. Jag skapade två modeller, ena är tränad på engelska omdömen från yelp och den andra är tränad på samma omdömen som först översattes till svenska.
 // Markus Vickman 2024-09-23 */
using MyMLApp;

//Programmet MLPredictSentiment
namespace MLPredictSentiment
{
    //Klassen Predict som innehåller main() och alla metoder som används i applikationen
    public class Predict
    {

        //Main uppgiften i programmet. Här startar programmet. 
        public static Task Main()
        {

            //Consolen rensas och programmets namn skrivs ut. Information om programmet och Språkval läses upp.
            Console.Clear();
            Console.WriteLine("# # #  TESTA ETT OMDÖME         # # #");
            Console.WriteLine("# # #  POSITIVT eller NEGATIVT  # # #\r\n");
            Console.WriteLine("Applikationen använder maskininlärning för att bedömma om en recension eller ett omdöme är positivt eller negativt. " +
                "Modellen är tränad på engelska, men det går även att testa modellen som jag tränade på översatt data.\r\n" +
                "\r\nVälj språk\r\n[1] Engelska \r\n[2] Svenska");

            //Språkval lagras som en sträng
            string language = Console.ReadLine()!;

            //Läser in omdömet som ska testas
            Console.WriteLine("Skriv omdömet du vill testa:");
            string phrase = Console.ReadLine()!;

            //Inmatningarna för språk och omdöme körs i en metod för testning av inmatade värden
            Predict.CheckInput(phrase, language);

            return Task.CompletedTask;
        }

        //En metod med båda ML-modellerna för svenska och engelska. Tar in parametrarna phrase för omdöme och language för språkval
        public static void MLModelPredict(string phrase, string language)
        {
            //Om språkval är 2 körs omdömet i modellen för svenska.
            if (language == "2")
            {
                //Läser testdata
                var swedishSampleData = new SentimentModelSwedishTranslated.ModelInput()
                {
                    Col0 = phrase
                };

                //Laddar modellen och förutser 1 eller 0 i kolumnen samt skriver ut svaret som positivt eller negativt istället för 1 eller 0.
                var resultSwedish = SentimentModelSwedishTranslated.Predict(swedishSampleData);
                var sentiment = resultSwedish.PredictedLabel == 1 ? "Positivt" : "Negativt";
                Console.WriteLine(/*$"Text: {swedishSampleData.Col0}\n*/$"\r\nOmdömet är: {sentiment}");
            }


            else if (language == "1")
            {
                //Läser testdata
                var sampleData = new SentimentModel.ModelInput()
                {
                    Col0 = phrase
                };

                //Laddar modellen och förutser 1 eller 0 i kolumnen samt skriver ut svaret som positivt eller negativt istället för 1 eller 0.
                var result = SentimentModel.Predict(sampleData);
                var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
                Console.WriteLine(/*$"Text: {sampleData.Col0}\n*/$"\r\nSentiment: {sentiment}");
            }
            Predict.ProgramCoices();
        }

        //Metod för att fortsätta eller avsluta programmet
        public static void ProgramCoices()
        {
            Console.WriteLine("\r\n[1] Testa en ny mening.\r\n[2] Avsluta programmet.");
            string coice = Console.ReadLine()!;

            //Vid val 1 startas programmet om
            if (coice == "1")
            {
                Predict.Main();
            }

            //Vid val 2 körs rensas konsollen och sedan avslutas programmet
            else if (coice == "2")
            {
                Console.Clear();
                Environment.Exit(0);
            }

            //Om valet inte är 1 eller 2 så skrivs ett felmeddelande och valen skrivs ut igen.
            else
            {
                Console.WriteLine("\r\nFelinmatat värde! Svara 1 eller 2.");
                Predict.ProgramCoices();
            }
        }

            //Metod för att testa om inmatat språk eller omdöme är rätt
            public static void CheckInput(string phrase, string language)
            {

            //Om check inte är 0 i slutet av metoden har fel hittats 
                int check = 0;

            //testar om språkval är rätt
                if (language == "1" | language == "2")
            {
            }

            //Om spåkval inte är rätt läggs ett fel till i check och ett felmeddelande skrivs ut.
            else
            {
                check++;
                Console.WriteLine("Felinmatat värde! Du måste välja [1] för Engelska eller [2] för Svenska.");

            }

            // Om frasen är mindre än 3 tecken läggs ett fel till och ett felmeddelande skrivs ut.
            if (phrase.Length < 3)
            {
                Console.WriteLine("Felinmatad mening! Du måste skriv in minst ett ord!");
                check++;
            }

            //Om omdömet är längre än 2 går det bara vidare
            else
            {
            }

            //om inga fel hittades körs metoden för att förutse om omdömet är positivt eller negativt
            if (check == 0)
            {
                //Metoden för maskininlärningsmodellen körs med argument för omdöme och språk
                Predict.MLModelPredict(phrase, language);
            }

            //Annars körs programmet om och användaren får skriva in rätt värden denna gång.
            else
            {
                Console.WriteLine("Tryck enter för att fortsätta..");
                Console.ReadLine();
                Predict.Main();
            }
        }
    }
}
