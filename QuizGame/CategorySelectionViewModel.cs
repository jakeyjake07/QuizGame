using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizGame
{
    public class CategorySelectionViewModel : BaseViewModel
    {
        public Quiz Quiz { get; set; }
        public List<string> AvailableCategories { get; set; }
        public Dictionary<string, bool> SelectedCategories { get; set; }
        public MainViewModel MainViewModel { get; set; }

        public CategorySelectionViewModel(Quiz quiz, MainViewModel mainViewModel)
        {
            Quiz = quiz;
            MainViewModel = mainViewModel;

            AvailableCategories = new List<string>();
            foreach (var question in Quiz.Questions)
            {
                if (!AvailableCategories.Contains(question.Category))
                {
                    AvailableCategories.Add(question.Category);
                }
            }

           
            SelectedCategories = new Dictionary<string, bool>();
            foreach (var category in AvailableCategories)
            {
                SelectedCategories[category] = true;
            }
        }

        public void StartQuiz()
        {
            
            var filteredQuestions = new List<Question>();

            foreach (var question in Quiz.Questions)
            {
                
                if (SelectedCategories.ContainsKey(question.Category) && SelectedCategories[question.Category])
                {
                    filteredQuestions.Add(question);
                }
            }

            if (filteredQuestions.Count == 0)
            {
                MessageBox.Show("No questions found in the selected categories! Please select at least one category.");
                return;
            }

            
            Quiz filteredQuiz = new Quiz(Quiz.Title);
            foreach (var question in filteredQuestions)
            {
                filteredQuiz.Questions.Add(question);
            }

            
            MainViewModel.CurrentView = new PlayQuizView(MainViewModel, filteredQuiz);
        }
    }
 
}
