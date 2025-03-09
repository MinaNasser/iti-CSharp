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
        catch (IOException ex) when (ex.Message.Contains("being used by another process"))
        {
            sEQConnection.Write(LogCategory.Error, $"File is currently in use: {ex.Message}");
        }
        catch (IOException ex)
        {
            sEQConnection.Write(LogCategory.Error, $"IOException in WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.Write(LogCategory.Error, $"Exception in WriteToFile: {ex.Message}");
        }
    }

    public string ReadFromFile(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
            {
                sEQConnection.Write(LogCategory.Warning, $"File not found: {fileName}");
                return string.Empty;
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
        catch (FileNotFoundException ex)
        {
            sEQConnection.Write(LogCategory.Error, $"FileNotFoundException: {ex.Message}");
        }
        catch (IOException ex)
        {
            sEQConnection.Write(LogCategory.Error, $"IOException in ReadFromFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.Write(LogCategory.Error, $"Exception in ReadFromFile: {ex.Message}");
        }
        return string.Empty;
    }

    public void CopyFileToUpper(string sourceFileName, string destinationFileName)
    {
        if (!File.Exists(sourceFileName))
        {
            string errorMsg = $"Error: Source file '{sourceFileName}' does not exist.";
            Console.WriteLine(errorMsg);
            sEQConnection.Write(LogCategory.Error, errorMsg);
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(sourceFileName))
            using (StreamWriter writer = new StreamWriter(destinationFileName, false))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line.ToUpper());
                }
            }

            string successMsg = "File copied successfully with all text in uppercase.";
            Console.WriteLine(successMsg);
            sEQConnection.Write(LogCategory.Information, successMsg);
        }
        catch (Exception ex)
        {
            string errorMsg = $"An error occurred: {ex.Message}";
            Console.WriteLine(errorMsg);
            sEQConnection.Write(LogCategory.Error, errorMsg);
        }
    }
}
