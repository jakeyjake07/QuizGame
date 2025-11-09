using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    public class Question
    {
        public string Statement {  get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }

        public Question()
        {

        }

        public Question(string statement, int correctAnswer, string category, string imagePath, params string[] answers)
        {
            Statement = statement;
            CorrectAnswer = correctAnswer;
            Category = category;
            ImagePath = imagePath;
            Answers = answers;
        }

        public bool IsCorrect(int selectedIndex)
        {
            return selectedIndex == CorrectAnswer;
        }

    }
}
