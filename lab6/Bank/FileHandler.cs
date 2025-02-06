
using System.Reflection.PortableExecutable;

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
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");

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
}