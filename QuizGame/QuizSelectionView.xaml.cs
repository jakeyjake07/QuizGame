using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for QuizSelectionView.xaml
    /// </summary>
    public partial class QuizSelectionView : UserControl
    {
        public MainViewModel MainViewModel { get; set; }
        public QuizSelectionView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            MainViewModel = mainViewModel;
            DataContext = MainViewModel;
        }
    
    
        public void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesListView.SelectedItem != null)
            {
                string selectedQuiz = QuizzesListView.SelectedItem.ToString();
                MainViewModel.PlayQuiz(selectedQuiz);
            }
        }

        public void PlayByCategory_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.CurrentView = new CategoryPlayView(MainViewModel);
        }

        public void CreateButton_Click(Object sender, RoutedEventArgs e)
        {
            MainViewModel.CurrentView = new QuizCreationView(MainViewModel);
        }
        
        public async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesListView.SelectedItem != null)
            {
                string selectedQuiz = QuizzesListView.SelectedItem.ToString();
                Quiz quiz = await QuizFileService.LoadQuizAsync(selectedQuiz);

                if (quiz != null)
                {
                    MainViewModel.CurrentView = new EditQuizView(MainViewModel, quiz);
                }

                else
                {
                    MessageBox.Show($"Quiz '{selectedQuiz}' not found.");
                }
            }

            else
            {
                MessageBox.Show("Please select a quiz to edit.");
            }
        
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuizzesListView.SelectedItem != null)
            {
                string selectedQuiz = QuizzesListView.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete the quiz '{selectedQuiz}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool deleted = QuizFileService.DeleteQuiz(selectedQuiz);
                    if (deleted)
                    {
                        MessageBox.Show($"Quiz '{selectedQuiz}' deleted successfully!");
                        MainViewModel.ShowQuizSelection();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete quiz '{selectedQuiz}'.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a quiz to delete!");
            }
        }

    }


}
