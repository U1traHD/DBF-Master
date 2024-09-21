using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace DBF_Master
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<FileItem> Files { get; set; } = new ObservableCollection<FileItem>();

        public MainWindow()
        {
            InitializeComponent();
            FilesDataGrid.ItemsSource = Files;
        }

        private async void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    Files.Add(new FileItem { FileName = System.IO.Path.GetFileName(fileName), FullPath = fileName });
                }
            }
        }

        private async void FilesDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FilesDataGrid.SelectedItem is FileItem selectedFile)
            {
                try
                {
                    string content = await Task.Run(() => System.IO.File.ReadAllText(selectedFile.FullPath));
                    FileContentTextBox.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
                }
            }
            else
            {
                FileContentTextBox.Text = string.Empty;
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilesDataGrid.SelectedItem is FileItem selectedFile)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = selectedFile.FileName,
                    DefaultExt = System.IO.Path.GetExtension(selectedFile.FileName)
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        System.IO.File.WriteAllText(saveFileDialog.FileName, FileContentTextBox.Text);
                        MessageBox.Show("Файл успешно сохранён.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите файл для сохранения изменений.");
            }
        }
    }

    public class FileItem
    {
        public string? FileName { get; set; }
        public string? FullPath { get; set; }
    }
}