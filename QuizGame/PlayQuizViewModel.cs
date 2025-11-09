using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace QuizGame
{
    public class PlayQuizViewModel : BaseViewModel
    {
        public Quiz Quiz {  get; set; }
        public Question CurrentQuestion { get; set; }
        public int SelectedAnswerIndex { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered {  get; set; }


        private List<Question> _shuffledQuestions;
        private int _currentQuestionIndex;
        private MainViewModel _mainViewModel;

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

        private BitmapImage _currentQuestionImage;
        public BitmapImage CurrentQuestionImage
        {
            get { return _currentQuestionImage; }
            set
            {
                _currentQuestionImage = value;
                OnPropertyChanged();
                OnPropertyChanged("IsImageVisible"); 
            }
        }

       
        public Visibility IsImageVisible
        {
            get { return CurrentQuestionImage != null ? Visibility.Visible : Visibility.Collapsed; }
        }

        public PlayQuizViewModel(Quiz quiz, MainViewModel mainViewModel)
        {
            Quiz = quiz;
            _mainViewModel = mainViewModel;

            _shuffledQuestions = Quiz.Questions.OrderBy(q => Quiz.Randomizer.Next()).ToList();
            _currentQuestionIndex = 0;

            if (_shuffledQuestions.Count > 0)
            {
                CurrentQuestion = _shuffledQuestions[_currentQuestionIndex];
                UpdateQuestionImage();
            }

            SelectedAnswerIndex = -1;
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("ScoreText");
        }

        private void UpdateQuestionImage()
        {
            try
            {
                if (CurrentQuestion != null && !string.IsNullOrEmpty(CurrentQuestion.ImagePath))
                {
                    
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(CurrentQuestion.ImagePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    CurrentQuestionImage = bitmap;
                }
                else
                {
                    CurrentQuestionImage = null;
                }
            }
            catch (Exception ex)
            {
                
                CurrentQuestionImage = null;
            }
        }

        public void NextQuestion(int selectedIndex)
        {
            TotalAnswered++;
            if (CurrentQuestion.IsCorrect(selectedIndex))
            {
                CorrectAnswers++;
            }

         
            _currentQuestionIndex++;

            if (_currentQuestionIndex < _shuffledQuestions.Count)
            {
                CurrentQuestion = _shuffledQuestions[_currentQuestionIndex];
                OnPropertyChanged("CurrentQuestion");

              
                UpdateQuestionImage();
            }
            else
            {
                
                
                MessageBox.Show($"Quiz completed! Final score: {ScoreText}");
                _mainViewModel.ShowQuizSelection();
            }

            OnPropertyChanged("ScoreText");
        }

    
    }

}

