using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz {  get; set; }
        public Questions CurrentQuestion { get; set; }
        public int SelectedAnswerIndex { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered {  get; set; }
        public string ScoreText
        {
            get
            {
                int percent = 0;
                if (TotalAnswered > 0)
                {
                    percent = (int)((double)CorrectAnswers / TotalAnswered * 100);
                }
                return $"Score: {percent}%";
            }

        }

        public PlayQuizViewModel()
        {
            Quiz = new Quiz("Test Quiz");
            Quiz.AddQuestion("What's the capitol of Sweden", 0, "Stockholm", "Göteborg", "Malmö", "Gävle");
            Quiz.AddQuestion("What's the color of the sky", 2, "Red", "Green", "Blue", "Pink");
            Quiz.AddQuestion("How many legs does a cat have?", 1, "5", "4", "31", "12");

            CurrentQuestion = Quiz.GetRandomQuestion();
            SelectedAnswerIndex = -1;
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("ScoreText");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void NextQuestion(int selectedIndex)
        {
            TotalAnswered++;
            if (CurrentQuestion.isCorrect(selectedIndex))
            {
                CorrectAnswers++;
            }

            
            CurrentQuestion = Quiz.GetRandomQuestion();
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("ScoreText");
        
        }

    
    }

}

