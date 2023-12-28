using Newtonsoft.Json.Linq;
using webApp_CAEPlatform.model;

namespace webApp_CAEPlatform.controllers;

public class HomeController
{
    private Parameters _parameters;
    
    
    /// <summary>
    /// Читаем значения формы
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task ReadFromBody(HttpContext context)
    {
        try
        {
            //Парсим json и передаём в модель
            string body;
            using (var sr = new StreamReader(context.Request.Body))
                body = sr.ReadToEndAsync().Result;
            var items = JObject.Parse(body);
            _parameters = new()
            {
                H1 = (int)(items["h1"] ?? throw new InvalidOperationException()),
                H3 = (int)(items["h3"] ?? throw new InvalidOperationException()),
                L2 = (int)(items["l2"] ?? throw new InvalidOperationException()),
                L3 = (int)(items["l3"] ?? throw new InvalidOperationException())
            };
            Console.WriteLine(_parameters.ToString()); //Выводим в консоль для проверки.

            //Перенаправление на страницу вывода результата вычислений.
            context.Response.StatusCode = 302;
            context.Response.Redirect("/result");
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { message = "Некорректные данные" });
        }
    }
}