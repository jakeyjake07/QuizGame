using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    public class Questions
    {
        public string Statement {  get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswers { get; set; }


        public Questions(string statement, int correctAnswer, params string[] answers)
        {
            Statement = statement;
            CorrectAnswers = correctAnswer;
            Answers = answers;
        }

        public bool isCorrect(int selectedIndex)
        {
            return selectedIndex == CorrectAnswers;
        }

    }
}
