using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    public class Quiz
    {
        public string Title { get; set; }
        public List<Questions> Questions { get; set; }
        public Random Randomizer { get; set; }


        public Quiz(string title = "")
        {
            Title = title;
            Questions = new List<Questions>();
            Randomizer = new Random();
        }
    
    
    public Questions GetRandomQuestion()
        {
            int index = Randomizer.Next(0, Questions.Count);
            return Questions[index];
        }
    

        public void AddQuestion(string statment, int correctAnswer, params string[] answers)
        {
            Questions q = new Questions(statment, correctAnswer, answers);
            Questions.Add(q);
        }
    
        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions[index]);
        }
    
    }




}
