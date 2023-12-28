namespace webApp_CAEPlatform.infrastructure;

public class FileWorker
{
    //TODO:Заполнить пути к файлам.
    #region Пути к файлам.
    /// <summary>
    /// Путь к перезаписываемому файлу.
    /// </summary>
    private const string file_ChangeBlockMeshString = "/home/vlad/Documents/csharp/webApp_CAEPlatform/webApp_CAEPlatform/OpenFoamResources/system/blockMeshDict";
    /// <summary>
    /// Путь к файлу со стандартными настройками.
    /// </summary>
    private const string file_DefaultBlockMeshString = "/home/vlad/Documents/csharp/webApp_CAEPlatform/webApp_CAEPlatform/OpenFoamResources/system/DefaultMesh";
    /// <summary>
    /// Путь к файлу записи вывода терминала.
    /// </summary>
    private const string file_TerminalLogs = "/home/vlad/Documents/csharp/webApp_CAEPlatform/webApp_CAEPlatform/logs/TerminalOutput.txt";
    /// <summary>
    /// Стриминговый буфер.
    /// </summary>
    private const string file_StreamingBuffer = "/home/vlad/Documents/csharp/webApp_CAEPlatform/webApp_CAEPlatform/logs/StreamingBuffer.txt";
    /// <summary>
    /// Директория создания папок решения.
    /// </summary>
    private const string OpenFoamDir = "/home/vlad/Documents/csharp/webApp_CAEPlatform/webApp_CAEPlatform/OpenFoamResources";
    #endregion
    
    /// <summary>
    /// Список файлов для очистки содержимого.
    /// </summary>
    private readonly List<string> _filesForClear = [ file_TerminalLogs, file_StreamingBuffer];
    
    /// <summary>
    /// Изменить значения координат для построения сетки.
    /// </summary>
    public async Task ChangeVarInFile(int changeL2,int changeL3,int changeH1,int changeH3)
    {
        string tempText;
        //Чтение подготовленного файла.
        using (var reader = new StreamReader(file_ChangeBlockMeshString))
            tempText = await reader.ReadToEndAsync();
        //Изменение.
        tempText = tempText.Replace("change_l2", changeL2.ToString())
            .Replace("change_l3", changeL3.ToString())
            .Replace("change_h1", changeH1.ToString())
            .Replace("change_h3", changeH3.ToString());
        //Запись значений.
        await using (var writer = new StreamWriter(file_ChangeBlockMeshString, false))
            await writer.WriteLineAsync(tempText);
    }
    
    /// <summary>
    /// Подготовка файла значений координат для записи переменных.
    /// </summary>
    public async Task ChangeOnDefaultParametrs()
    {   
        string tempText;
        //Чтение подготовленного файла.
        using (var reader = new StreamReader(file_DefaultBlockMeshString))
            tempText = await reader.ReadToEndAsync();
        // полная перезапись файла.
        await using (var writer = new StreamWriter(file_ChangeBlockMeshString, false))
            await writer.WriteLineAsync(tempText);
    }
    /// <summary>
    /// Записть в стиминговый буфер.
    /// </summary>
    /// <param name="text"></param>
    public async Task WriteInBufferAsync(string text)
    {
        await using var writer = new StreamWriter(file_StreamingBuffer, true); 
        await writer.WriteLineAsync(text);
    }
    /// <summary>
    ///Запись в стиминговый буфер.
    /// </summary>
    /// <param name="text"></param>
    public void WriteInBuffer(string text)
    {
        using var writer = new StreamWriter(file_StreamingBuffer, true); 
        writer.WriteLine(text);
    }
    /// <summary>
    /// Очистить все файлы логов прошлых решений.
    /// </summary>
    public async Task ClearAllFilesForLogs()
    {
        foreach (var item in _filesForClear)
            await File.WriteAllTextAsync(item,string.Empty);
    }
    /// <summary>
    /// Проверка исполняемой дериктории на наличие прошлых решений.
    /// </summary>
    /// <returns></returns>
    public bool CheckSolutionsFolder(int startTime, int endTime)
    {
        for (var i = startTime; i <= endTime; i++)
        {
            var path = string.Concat(OpenFoamDir + "/" + $"{i}");
            if (File.Exists(path))
                return false;
        }
        return true;
    }
}