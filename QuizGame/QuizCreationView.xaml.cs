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
    /// Interaction logic for QuizCreationView.xaml
    /// </summary>
    public partial class QuizCreationView : UserControl
    {
        public QuizCreationViewModel ViewModel { get; set; }
        public MainViewModel MainViewModel { get; set; }


        public QuizCreationView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            MainViewModel = mainViewModel;
            ViewModel = new QuizCreationViewModel();
            DataContext = ViewModel;
        }
    
    
        public async void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddQuestion();
        }
        
        public async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveQuiz();
            MainViewModel.ShowQuizSelection();
        }


        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.ShowQuizSelection();
        }

        public void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
            openFileDialog.FilterIndex = 1;

            
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
               
                string selectedImagePath = openFileDialog.FileName;

               
                ViewModel.LoadImageForQuestion(selectedImagePath);
            }
        }


        public void ClearImage_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearQuestionImage();
        }



    }
}
