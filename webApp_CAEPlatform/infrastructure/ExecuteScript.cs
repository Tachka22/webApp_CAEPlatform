using System.Diagnostics;

namespace webApp_CAEPlatform.infrastructure;

public class ExecuteScript
{
    //TODO:Заполнить пути.
    #region Пути к файлам логирования терминала
    private const string pathTo_TerminalLogs = "";
    #endregion
    
    private Process myProcess;
    
    private async Task ExecuteScriptBase(string path)
    {
        try
        {
            if (path == string.Empty)
                throw new Exception("path not found");
            
            using (myProcess = new Process())
            {
                myProcess.StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };
                myProcess.OutputDataReceived += ProcessOnOutputDataReceived;
                myProcess.EnableRaisingEvents = true;  
                myProcess.Start();
                myProcess.BeginOutputReadLine();
                myProcess.BeginErrorReadLine();
                await myProcess.WaitForExitAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        using var writer = new StreamWriter(pathTo_TerminalLogs, true);
        var outLine = e.Data;
        writer.WriteLine(outLine);
        Console.WriteLine(outLine);
    }
}