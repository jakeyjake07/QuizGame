using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QuizGame
{
    public static class QuizFileService
    {
        private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizGame");

        private static readonly string ImagesPath = Path.Combine(AppDataPath, "Images");

        static QuizFileService()
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }

            if (!Directory.Exists(ImagesPath))
            {
                Directory.CreateDirectory(ImagesPath);
            }
        }
        


        public static async Task<string> SaveQuestionImageAsync(string sourceImagePath, string questionId)
        {
            try
            {
                string extension = Path.GetExtension(sourceImagePath);
                string fileName = $"{questionId}{extension}";
                string destinationPath = Path.Combine(ImagesPath, fileName);

                File.Copy(sourceImagePath, destinationPath, true);
                return destinationPath;

            }
        
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving image: {ex.Message}");
                return null;
            }      
        }


        public static BitmapImage LoadImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                return null;
        
        
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ImagesPath);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
            catch
            {
                return null;
            }
        
        }





        public static async Task SaveQuizAsync(Quiz quiz)
        {
            try
            {

            string fileName = $"{quiz.Title}.json";
            string filePath = Path.Combine(AppDataPath, fileName);

                //Debug save
                MessageBox.Show($"Trying to save to: {filePath}");

            string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
     
            }

            catch (Exception ex)
            {
                //Debug save
                MessageBox.Show($"Error Saving quiz {ex.Message}");
            }
        }
    
        public static async Task<Quiz> LoadQuizAsync(string quizName)
        {
            string fileName = $"{quizName}.json";
            string filePath = Path.Combine(AppDataPath , fileName);

            if (!File.Exists(filePath))
            {
                return null;
            }
        
        
            string json = await File.ReadAllTextAsync(filePath);
            Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);
            return quiz;
        
        }
        
        public static string[] GetAvailableQuizzes()
        {
            if (!Directory.Exists(AppDataPath))
            {
                return new string[0];
            }
            string[] jsonFiles = Directory.GetFiles(AppDataPath, "*.json");
            string[] quizNames = new string[jsonFiles.Length];

            for (int i = 0; i < jsonFiles.Length; i++)
            {
                quizNames[i] = Path.GetFileNameWithoutExtension(jsonFiles[i]); 
            }

            return quizNames;
        }


        public static bool DeleteQuiz(string quizName)
        {
            try
            {
                string fileName = $"{quizName}.json";
                string filePath = Path.Combine(AppDataPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting quiz: {ex.Message}");
                return false;
            }
        }


        public static async Task CreateDefaultQuizAsync()
        {
            string[] existingQuizzes = GetAvailableQuizzes();
            if (existingQuizzes.Length > 0)
                return;

            Quiz defaultQuiz = new Quiz("General Knowledge Quiz");

            defaultQuiz.AddQuestion("What is the capital of Japan?", 1, "Geography", "Seoul", "Tokyo", "Beijing", "Bangkok");
            defaultQuiz.AddQuestion("How many days are in a leap year?", 2, "Science", "365", "364", "366", "367");
            defaultQuiz.AddQuestion("What is the largest ocean on Earth?", 0, "Geography", "Pacific Ocean", "Atlantic Ocean", "Indian Ocean", "Arctic Ocean");
            defaultQuiz.AddQuestion("Which programming language is WPF built with?", 2, "Technology", "Java", "Python", "C#", "C++");
            defaultQuiz.AddQuestion("What is the square root of 64?", 1, "Math", "7", "8", "9", "10");
            defaultQuiz.AddQuestion("Which year did World War II end?", 2, "History", "1944", "1946", "1945", "1943");
            defaultQuiz.AddQuestion("What is the hardest natural substance on Earth?", 0, "Science", "Diamond", "Gold", "Iron", "Platinum");
            defaultQuiz.AddQuestion("How many bones are in the human body?", 1, "Science", "205", "206", "207", "208");
            defaultQuiz.AddQuestion("What is the capital of France?", 1, "Geography", "London", "Paris", "Berlin", "Madrid");
            defaultQuiz.AddQuestion("Which planet is known as the Red Planet?", 2, "Science", "Venus", "Jupiter", "Mars", "Saturn");

            await SaveQuizAsync(defaultQuiz);
      
        }


        public static async Task<List<Question>> GetQuestionsByCategoriesAsync(List<string> categories)
        {
            var allQuestions = new List<Question>();
            string[] quizFiles = Directory.GetFiles(AppDataPath, "*.json");

            foreach (string filePath in quizFiles)
            {
                try
                {
                    string json = await File.ReadAllTextAsync(filePath);
                    Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);

                    
                    foreach (var question in quiz.Questions)
                    {
                        if (categories.Contains(question.Category))
                        {
                            allQuestions.Add(question);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    continue;
                }
            }

            return allQuestions;
        }

        public static List<string> GetAllCategories()
        {
            var allCategories = new List<string>();

            if (!Directory.Exists(AppDataPath))
                return allCategories;

            string[] quizFiles = Directory.GetFiles(AppDataPath, "*.json");

            foreach (string filePath in quizFiles)
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);

                    
                    foreach (var question in quiz.Questions)
                    {
                        if (!allCategories.Contains(question.Category))
                        {
                            allCategories.Add(question.Category);
                        }
                    }
                }
                catch (Exception ex)
                {
                  
                    continue;
                }
            }

            return allCategories;
        }




    }



}
