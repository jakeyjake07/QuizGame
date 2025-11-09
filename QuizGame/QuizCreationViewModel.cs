using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QuizGame
{
    public class QuizCreationViewModel : BaseViewModel
    {
        private string _quizTitle;
        public string QuizTitle
        {
            get { return _quizTitle; }
            set
            {
                _quizTitle = value;
                OnPropertyChanged();
            }
        }
        public Quiz CurrentQuiz { get; set; }
        public string NewQuestionStatement { get; set; }
        public string[] NewQuestionAnswers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string NewQuestionCategory { get; set; }

        public string NewQuestionImagePath { get; set; }
        public BitmapImage NewQuestionImage { get; set; }

        public QuizCreationViewModel()
        {
            CurrentQuiz = new Quiz();
            NewQuestionAnswers = new string[4] { "", "", "", "" };
        }

        public void LoadImageForQuestion(string imagePath)
        {
            NewQuestionImagePath = imagePath;
            NewQuestionImage = QuizFileService.LoadImage(imagePath);
            OnPropertyChanged("NewQuestionImage");
            OnPropertyChanged("NewQuestionImagePath");

        }


        public void ClearQuestionImage()
        {
            NewQuestionImagePath = null;
            NewQuestionImage = null;
            OnPropertyChanged("NewQuestionImage");
            OnPropertyChanged("NewQuestionImagePath");
        }



        public void AddQuestion()
        {
            if (
               string.IsNullOrWhiteSpace(NewQuestionStatement) ||
                string.IsNullOrWhiteSpace(NewQuestionAnswers[0]) ||
                string.IsNullOrWhiteSpace(NewQuestionAnswers[1]) ||
                string.IsNullOrWhiteSpace(NewQuestionAnswers[2]) ||
                string.IsNullOrWhiteSpace(NewQuestionAnswers[3]) ||
                string.IsNullOrWhiteSpace(NewQuestionCategory))

              {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            string questionId = Guid.NewGuid().ToString();
            string savedImagePath = null;

            if (!string.IsNullOrEmpty(NewQuestionImagePath) )
            {
                savedImagePath = QuizFileService.SaveQuestionImageAsync(NewQuestionImagePath, questionId).Result;
            }

            CurrentQuiz.AddQuestion(NewQuestionStatement, CorrectAnswerIndex, NewQuestionCategory, savedImagePath, NewQuestionAnswers);

            NewQuestionStatement = "";
            NewQuestionAnswers = new string[4] { "", "", "", "" };
            CorrectAnswerIndex = 0;
            NewQuestionCategory = "";
            ClearQuestionImage();

            OnPropertyChanged("NewQuestionStatment");
            OnPropertyChanged("NewQuestionAnswers");
            OnPropertyChanged("CorrectAnswerIndex");
            OnPropertyChanged("NewQuestionCategory");



        }

        public async Task SaveQuiz()
        {
            
            if (string.IsNullOrWhiteSpace(QuizTitle))
            {
                MessageBox.Show("Please enter a quiz title!");
                return;
            }

            if (CurrentQuiz.Questions.Count == 0)
            {
                MessageBox.Show("Please add at least one question!");
                return;
            }

            
            CurrentQuiz.Title = QuizTitle.Trim();
            await QuizFileService.SaveQuizAsync(CurrentQuiz);
            MessageBox.Show($"Quiz '{CurrentQuiz.Title}' saved successfully!");
        }









    }
}
