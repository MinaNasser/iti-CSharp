using System;
using System.IO;

public class FileHandler
{
    private SEQConnection sEQConnection = new SEQConnection();

    public void WriteToFile(string fileName, string content)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine($"{timestamp}: {content}");
                writer.WriteLine("\n=====================\n");
            }
        }
        catch (IOException ex)
        {
            sEQConnection.write(LogCategory.Error, $"IOException in WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.write(LogCategory.Error, $"Exception in WriteToFile: {ex.Message}");
        }
    }

    public string ReadFromFile(string fileName)
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            sEQConnection.write(LogCategory.Error, $"IOException in ReadFromFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.write(LogCategory.Error, $"Exception in ReadFromFile: {ex.Message}");
        }
        return string.Empty;
    }

    public void CopyFileToUpper(string sourceFileName, string destinationFileName)
    {
        if (!File.Exists(sourceFileName))
        {
            string errorMsg = "Error: The specified source file does not exist.";
            Console.WriteLine(errorMsg);
            sEQConnection.write(LogCategory.Error, errorMsg);
            return;
        }

        try
        {
            string content = ReadFromFile(sourceFileName);
            string upperContent = content.ToUpper();

            File.WriteAllText(destinationFileName, upperContent);

            string successMsg = "File copied successfully with all text in uppercase.";
            Console.WriteLine(successMsg);
            sEQConnection.write(LogCategory.Information, successMsg);
        }
        catch (Exception ex)
        {
            string errorMsg = $"An error occurred: {ex.Message}";
            Console.WriteLine(errorMsg);
            sEQConnection.write(LogCategory.Error, errorMsg);
        }
    }
}
