using System.Diagnostics;

namespace webApp_CAEPlatform.infrastructure;

public class ExecuteScript
{
    //TODO:Заполнить пути.
    #region Пути к файлам логирования терминала
    private const string pathTo_TerminalLogs = "";
    #endregion
    #region Пути к исполняемым файлам.
    /// <summary>
    /// Скрипт запуска решателя.
    /// </summary>
    private const string pathTo_RunSolverScript = "";
    /// <summary>
    /// Скрипт очистки прошлых решений.
    /// </summary>
    private const string pathTo_ClearLastSolutionScript = "";
    /// <summary>
    /// Скрипт создания сетки модели.
    /// </summary>
    private const string pathTo_CreateMesh = "";
    /// <summary>
    /// Скрипт запуска приложения: "ParaView Desktop"
    /// </summary>
    private const string pathTo_OpenParaViewDesktop = "";
    #endregion
    
    private Process myProcess;
    
    
    
    /// <summary>
    /// Базовый метод исполнения скриптов.
    /// </summary>
    /// <param name="path">скрипт</param>
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
    /// <summary>
    /// Запись вывода терминала.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        using var writer = new StreamWriter(pathTo_TerminalLogs, true);
        var outLine = e.Data;
        writer.WriteLine(outLine);
        Console.WriteLine(outLine);
    }
}