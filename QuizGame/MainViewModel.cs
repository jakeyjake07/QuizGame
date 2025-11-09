using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame
{
    public class MainViewModel : BaseViewModel
    {

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string[] AvailableQuizzes { get; set; }

        public MainViewModel()
        {
            
            ShowQuizSelection();
        }


        public async Task ShowQuizSelection()
        {

            await QuizFileService.CreateDefaultQuizAsync();
            AvailableQuizzes = QuizFileService.GetAvailableQuizzes();
            CurrentView = new QuizSelectionView(this);
            OnPropertyChanged("AvailableQuizzes");
        }

        public async void PlayQuiz(string quizName)
        {
            try
            {
                Quiz quiz = await QuizFileService.LoadQuizAsync(quizName);

                if (quiz != null)
                {
                    
                    var categories = quiz.Questions.Select(q => q.Category).Distinct().ToList();

                    if (categories.Count > 1)
                    {
                        
                        CurrentView = new CategorySelectionView(quiz, this);
                    }
                    else
                    {
                        
                        CurrentView = new PlayQuizView(this, quiz);
                    }
                }
                else
                {
                    MessageBox.Show($"Quiz '{quizName}' not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quiz: {ex.Message}");
            }

        }

       

     

    }
}
