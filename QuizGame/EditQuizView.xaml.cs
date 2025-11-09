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
    /// Interaction logic for EditQuizView.xaml
    /// </summary>
    public partial class EditQuizView : UserControl
    {
        public EditQuizViewModel ViewModel { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public EditQuizView(MainViewModel mainViewModel, Quiz quiz)
        {
            InitializeComponent();
            MainViewModel = mainViewModel;
            ViewModel = new EditQuizViewModel(quiz);
            DataContext = ViewModel;
        }
    
    
        public void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectQuestionForEditing();
        }
        
        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateQuestion();
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedQuestion();
        }
    
    
        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveQuiz();
        }
    
    
        public void BackButton_Click(Object sender, RoutedEventArgs e)
        {
            MainViewModel.ShowQuizSelection();
        }
    }

}
