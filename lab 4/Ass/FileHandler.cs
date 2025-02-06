
using System.Reflection.PortableExecutable;

public class FileHandler
{
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
            SEQConnection sEQConnection = new SEQConnection(); 
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
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
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");
        }
        catch (Exception ex)
        {
            SEQConnection sEQConnection = new SEQConnection();
            sEQConnection.write(LogCategory.Error, $"In WriteToFile: {ex.Message}");

        }
        return "";

       


    }
}