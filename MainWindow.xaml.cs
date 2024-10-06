using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace TextFileManipulation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Save Employee Data to a File
        private void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get input values
                string employeeName = txtEmployeeName.Text;
                string employeeNumber = txtEmployeeNumber.Text;
                double payRate = Convert.ToDouble(txtPayRate.Text);
                double hoursWorked = Convert.ToDouble(txtHoursWorked.Text);

                // Compute regular and overtime pay
                double totalPay = 0;
                if (hoursWorked > 40)
                {
                    // Overtime calculation for hours over 40
                    totalPay = (40 * payRate) + ((hoursWorked - 40) * (1.5 * payRate));
                }
                else
                {
                    // Regular pay
                    totalPay = hoursWorked * payRate;
                }

                // Choose file location
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Save employee data to the selected file
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine($"Employee Name: {employeeName}");
                        writer.WriteLine($"Employee Number: {employeeNumber}");
                        writer.WriteLine($"Total Pay: {totalPay:C}"); // Format as currency
                    }
                    MessageBox.Show("Employee data saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Load Employee Data from a File
        private void LoadEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text file (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    // Read and display employee data
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    StringBuilder sb = new StringBuilder();
                    foreach (string line in lines)
                    {
                        sb.AppendLine(line);
                    }
                    txtDisplay.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
