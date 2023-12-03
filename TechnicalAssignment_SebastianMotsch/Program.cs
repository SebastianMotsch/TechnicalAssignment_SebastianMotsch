using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TechnicalAssignment_SebastianMotsch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt User for a File Name
            Console.WriteLine("Enter the name of the CSV file (without extension): ");
            string fileName = Console.ReadLine()!;

            // Check for null or empty input
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("No file name entered.");
                return;
            }

            // Search Directory for the CSV File
            string filePath = $"{fileName}.csv";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File does not exist.");
                return;
            }

            // Parse the CSV File
            List<string> validEmails = new List<string>();
            List<string> invalidEmails = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    // Split the CSV line into fields
                    string[] fields = line.Split(',');
                    if (fields.Length >= 3) // Assuming at least three fields: First Name, Last Name, Email Address
                    {
                        string email = fields[2].Trim(); // Email address is the third field
                        if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) // Basic email validation pattern
                        {
                            validEmails.Add(email);
                        }
                        else
                        {
                            invalidEmails.Add(email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or processing the file: {ex.Message}");
                return;
            }

            // Output Lists of Valid and Invalid Email Addresses
            Console.WriteLine("Valid Emails:");
            foreach (string email in validEmails)
            {
                Console.WriteLine(email);
            }

            Console.WriteLine("\nInvalid Emails:");
            foreach (string email in invalidEmails)
            {
                Console.WriteLine(email);
            }
        }
    }
}