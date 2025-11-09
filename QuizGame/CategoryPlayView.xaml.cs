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
    /// Interaction logic for CategoryPlayView.xaml
    /// </summary>
    public partial class CategoryPlayView : UserControl
    {
        public CategoryPlayViewModel ViewModel { get; set; }
        private Dictionary<string, CheckBox> _categoryCheckboxes = new Dictionary<string, CheckBox>();

        public CategoryPlayView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            ViewModel = new CategoryPlayViewModel(mainViewModel);
            DataContext = ViewModel;
            CreateCategoryCheckboxes();
        }

        public void CreateCategoryCheckboxes()
        {
            CategoriesPanel.Children.Clear();
            _categoryCheckboxes.Clear();

            foreach (var category in ViewModel.AllCategories)
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

     
        public void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var category in ViewModel.AllCategories)
            {
                ViewModel.SelectedCategories[category] = true;
                if (_categoryCheckboxes.ContainsKey(category))
                {
                    _categoryCheckboxes[category].IsChecked = true;
                }
            }
        }

        public void DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var category in ViewModel.AllCategories)
            {
                ViewModel.SelectedCategories[category] = false;
                if (_categoryCheckboxes.ContainsKey(category))
                {
                    _categoryCheckboxes[category].IsChecked = false;
                }
            }
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainViewModel.ShowQuizSelection();
        }


        public void PlayCategories_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PlaySelectedCategories();
        }
    }

}
