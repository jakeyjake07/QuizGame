using Newtonsoft.Json;
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
        public List<Question> Questions { get; set; }

        [JsonIgnore]
        public Random Randomizer { get; set; }


        public Quiz()
        {
            Questions = new List<Question>();
            Randomizer = new Random();
        }

        public Quiz(string title = "")
        {
            Title = title;
            Questions = new List<Question>();
            Randomizer = new Random();
        }
    
    
    public Question GetRandomQuestion()
        {
            int index = Randomizer.Next(0, Questions.Count);
            return Questions[index];
        }
    

        public void AddQuestion(string statment, int correctAnswer, string category, string imagePath, params string[] answers)
        {
            Question q = new Question(statment, correctAnswer, category, imagePath, answers);
            Questions.Add(q);
        }
    
        public void RemoveQuestion(int index)
        {
            Questions.Remove(Questions[index]);
        }
    
    }




}
