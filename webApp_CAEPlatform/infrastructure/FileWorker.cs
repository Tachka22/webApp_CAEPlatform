namespace webApp_CAEPlatform.infrastructure;

public class FileWorker
{
    #region Пути к файлам.
    /// <summary>
    /// Путь к перезаписываемому файлу.
    /// </summary>
    private const string file_ChangeBlockMeshString = "";
    /// <summary>
    /// Путь к файлу со стандартными настройками.
    /// </summary>
    private const string file_DefaultBlockMeshString = "";
    /// <summary>
    /// Путь к файлу записи вывода терминала.
    /// </summary>
    private const string file_TerminalLogs = "";
    /// <summary>
    /// Стриминговый буфер.
    /// </summary>
    private const string file_StreamingBuffer = "";
    #endregion
    
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
    

}