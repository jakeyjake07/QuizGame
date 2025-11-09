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
    /// Interaction logic for CategorySelectionView.xaml
    /// </summary>
    public partial class CategorySelectionView : UserControl
    {
        public CategorySelectionViewModel ViewModel { get; set; }
        private Dictionary<string, CheckBox> _categoryCheckboxes = new Dictionary<string, CheckBox>();

        public CategorySelectionView(Quiz quiz, MainViewModel mainViewModel)
        {
            InitializeComponent();
            ViewModel = new CategorySelectionViewModel(quiz, mainViewModel);
            DataContext = ViewModel;
            CreateCategoryCheckboxes();
        }

        private void CreateCategoryCheckboxes()
        {
            CategoriesPanel.Children.Clear();
            _categoryCheckboxes.Clear();

            foreach (var category in ViewModel.AvailableCategories)
            {
                var checkBox = new CheckBox
                {
                    Content = category,
                    IsChecked = ViewModel.SelectedCategories[category],
                    Margin = new Thickness(5),
                    FontSize = 14
                };

               
                checkBox.Checked += (s, e) => ViewModel.SelectedCategories[category] = true;
                checkBox.Unchecked += (s, e) => ViewModel.SelectedCategories[category] = false;

                CategoriesPanel.Children.Add(checkBox);
                _categoryCheckboxes[category] = checkBox;
            }
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartQuiz();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var category in ViewModel.AvailableCategories)
            {
                ViewModel.SelectedCategories[category] = true;
                if (_categoryCheckboxes.ContainsKey(category))
                {
                    _categoryCheckboxes[category].IsChecked = true;
                }
            }
        }

        private void DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var category in ViewModel.AvailableCategories)
            {
                ViewModel.SelectedCategories[category] = false;
                if (_categoryCheckboxes.ContainsKey(category))
                {
                    _categoryCheckboxes[category].IsChecked = false;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainViewModel.ShowQuizSelection();
        }
    }
}
   

