using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizGame
{
    public class CategoryPlayViewModel : BaseViewModel
    {
        public List<string> AllCategories { get; set; }
        public Dictionary<string, bool> SelectedCategories { get; set; }
        public MainViewModel MainViewModel { get; set; }

        public CategoryPlayViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            AllCategories = QuizFileService.GetAllCategories();

          
            SelectedCategories = new Dictionary<string, bool>();
            foreach (var category in AllCategories)
            {
                SelectedCategories[category] = false;
            }
        }

        public async void PlaySelectedCategories()
        {
            
            var selectedCategoryNames = new List<string>();

            foreach (var categoryEntry in SelectedCategories)
            {
                if (categoryEntry.Value)
                {
                    selectedCategoryNames.Add(categoryEntry.Key);
                }
            }

            if (selectedCategoryNames.Count == 0)
            {
                MessageBox.Show("Please select at least one category!");
                return;
            }

            var categoryQuestions = await QuizFileService.GetQuestionsByCategoriesAsync(selectedCategoryNames);

            if (categoryQuestions.Count == 0)
            {
                MessageBox.Show("No questions found in the selected categories!");
                return;
            }

            
            string displayName;
            if (selectedCategoryNames.Count == 1)
            {
                displayName = selectedCategoryNames[0];
            }
            else
            {
                displayName = selectedCategoryNames.Count + " Categories";
            }

            Quiz categoryQuiz = new Quiz(displayName);
            foreach (var question in categoryQuestions)
            {
                categoryQuiz.Questions.Add(question);
            }

            MainViewModel.CurrentView = new PlayQuizView(MainViewModel, categoryQuiz);
        }
    }
}