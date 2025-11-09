using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizGame
{
    public class EditQuizViewModel : BaseViewModel
    {
        public Quiz CurrentQuiz { get; set; }
        public Question SelectedQuestion { get; set; }

        public string EditStatement { get; set; }
        public string[] EditAnswers { get; set; }
        public int EditCorrectAnswerIndex { get; set; }
        public string EditCategory { get; set; }


        public EditQuizViewModel(Quiz quiz)
        {
            CurrentQuiz = quiz;
            EditAnswers = new string[4] { "", "", "", "" };
        }
    
    
    
        public void SelectQuestionForEditing()
        {
            if (SelectedQuestion != null)
            {
                EditStatement = SelectedQuestion.Statement;
                EditCategory = SelectedQuestion.Category;

                EditAnswers = new string[4]
                {

                EditAnswers[0] = SelectedQuestion.Answers[0],
                EditAnswers[1] = SelectedQuestion.Answers[1],
                EditAnswers[2] = SelectedQuestion.Answers[2],
                EditAnswers[2] = SelectedQuestion.Answers[3]

                };

                EditCorrectAnswerIndex = SelectedQuestion.CorrectAnswer;

                OnPropertyChanged("EditStatement");
                OnPropertyChanged("EditCategory");
                OnPropertyChanged("EditAnswers");
                OnPropertyChanged("EditCorrectAnswerIndex");
            }
        }
    


        public void UpdateQuestion()
        {
            if (SelectedQuestion == null)
            {
                MessageBox.Show("Please select a question to edit.");
                return;
            }


            if (string.IsNullOrWhiteSpace(EditStatement) ||
             string.IsNullOrWhiteSpace(EditAnswers[0]) ||
             string.IsNullOrWhiteSpace(EditAnswers[1]) ||
             string.IsNullOrWhiteSpace(EditAnswers[2]) ||
             string.IsNullOrWhiteSpace(EditAnswers[3]) ||
             string.IsNullOrWhiteSpace(EditCategory))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            SelectedQuestion.Statement = EditStatement;
            SelectedQuestion.Category = EditCategory;
            SelectedQuestion.Answers[0] = EditAnswers[0];
            SelectedQuestion.Answers[1] = EditAnswers[1];
            SelectedQuestion.Answers[2] = EditAnswers[2];
            SelectedQuestion.Answers[3] = EditAnswers[3];
            SelectedQuestion.CorrectAnswer = EditCorrectAnswerIndex;

            MessageBox.Show("Question updated successfully.");

            OnPropertyChanged("CurrentQuiz");

        }


        public void DeleteSelectedQuestion()
        {
            if (SelectedQuestion != null)
            {
                CurrentQuiz.Questions.Remove(SelectedQuestion);
                SelectedQuestion = null;
                OnPropertyChanged("CurrentQuiz");
                MessageBox.Show("Question deleted.");

            } 
        }
    
        public async Task SaveQuiz()
        {
            await QuizFileService.SaveQuizAsync(CurrentQuiz);
            MessageBox.Show("Quiz saved successfully.");
        }
    
    }


}
