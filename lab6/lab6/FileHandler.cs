

public class FileHandler
{
    private SEQConnection sEQConnection = new SEQConnection();
    public void WriteToFile(string fileName, string content)
    {
        try
        {
            StreamWriter writer = new StreamWriter(fileName, true);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteLine($"{timestamp}: {content}");
            writer.WriteLine("\n=====================\n");
            writer.Close();
        }
        catch (IOException ex)
        {
           
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");
        }


    }

    public string ReadFromFile(string fileName)
    {
        try
        {
            StreamReader reader = new StreamReader(fileName);
            return reader.ReadToEnd();
        }
        catch (IOException ex)
        {
            sEQConnection.write(LogCategory.Error, $"In ReadFromFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.write(LogCategory.Error, $"In ReadFromFile: {ex.Message}");

        }
        return "";

       


    }


    public void CopyFileToUpper(string sourceFileName, string destinationFileName)
    {
        if (!File.Exists(sourceFileName))
        {
            Console.WriteLine("Error: The specified file does not exist.");
            sEQConnection.write(LogCategory.Error, "Error: The specified file does not exist.");
            return;
        }

        try
        {
            string content = this.ReadFromFile(sourceFileName);
            string upperContent = content.ToUpper();
            this.WriteToFile(destinationFileName, upperContent);
            Console.WriteLine("File copied successfully with all text in uppercase.");
            sEQConnection.write(LogCategory.Error, "File copied successfully with all text in uppercase.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            sEQConnection.write(LogCategory.Error, $"An error occurred: {ex.Message}");
        }
    }

    public void SummarizeFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Error: The specified file does not exist.");
            sEQConnection.write(LogCategory.Error, "Error: The specified file does not exist.");
            return;
        }

        try
        {
            string content = this.ReadFromFile(fileName);
            char[] characters = content.ToCharArray();
            int totalChars = characters.Length;
            int vowels = 0, consonants = 0, newLines = 0, numbers=0;
            string vowelLetters = "AEIOUaeiou";

            foreach (char c in characters)
            {
                if (char.IsLetter(c))
                {
                    if (vowelLetters.Contains(c))
                        vowels++;
                    else
                        consonants++;
                }
                else if (c == '\n')
                {
                    newLines++;
                }
                else if (char.IsDigit(c))
                {
                    numbers++;
                }
            }

            Console.WriteLine($"Total Characters: {totalChars}");
            Console.WriteLine($"Vowels: {vowels}");
            Console.WriteLine($"Consonants: {consonants}");
            Console.WriteLine($"Newlines: {newLines}");
            Console.WriteLine($"Numbers : {numbers}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            sEQConnection.write(LogCategory.Error, $"An error occurred: {ex.Message}");
        }
    }
}