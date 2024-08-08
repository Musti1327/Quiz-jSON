using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    // Klasse, der repræsenterer et spørgsmål i quizzen
    public class Question
    {
        public string question { get; set; }  // Spørgsmålets tekst
        public List<string> options { get; set; }  // Mulige svarmuligheder
        public string correct_answer { get; set; }  // Korrekt svar på spørgsmålet
    }

    // Klasse, der repræsenterer quizzen, som indeholder en liste af spørgsmål
    public class Quiz
    {
        public List<Question> questions { get; set; }  // Liste af spørgsmål i quizzen
    }

    // Programmets hovedmetode
    static void Main(string[] args)
    {
        try
        {
            // Læs indholdet af filen "quiz.json"
            string json = File.ReadAllText("quiz.json");

            // Deserialiser JSON-teksten til en Quiz-objekt
            Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);

            // Gennemløb alle spørgsmål i quizzen
            foreach (var question in quiz.questions)
            {
                // Udskriv spørgsmålet
                Console.WriteLine(question.question);

                // Udskriv alle svarmuligheder
                for (int i = 0; i < question.options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.options[i]}");
                }

                // Bed brugeren om at vælge et svar
                Console.Write("vælg dit svar: ");
                int userAnswers;
                bool isValid = int.TryParse(Console.ReadLine(), out userAnswers);

                // Tjek om brugerens svar er gyldigt og inden for rækkevidde
                if (isValid && userAnswers > 0 && userAnswers <= question.options.Count)
                {
                    // Få brugerens valgte svar
                    string userAnswer = question.options[userAnswers - 1];

                    // Tjek om svaret er korrekt
                    if (userAnswer == question.correct_answer)
                    {
                        Console.WriteLine("korrekt");
                    }
                    else
                    {
                        Console.WriteLine("forkert");
                    }
                }
                else
                {
                    // Hvis svaret er ugyldigt, bed om et nyt svar
                    Console.WriteLine("ugyldig svar prøv igen.");
                }

                // Tilføj en tom linje for at adskille spørgsmålene
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Håndter eventuelle fejl, der opstår under læsning eller behandling af quizzen
            Console.WriteLine($"der opstod en fejl: {ex.Message}");
        }

        // Vent på, at brugeren trykker en tast, før programmet afsluttes
        Console.ReadKey();
    }
}