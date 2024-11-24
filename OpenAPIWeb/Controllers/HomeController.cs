using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using OpenAPIWeb.Models;



namespace OpenAPIWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    readonly IChatClient _chatClient;

    public HomeController(ILogger<HomeController> logger, IChatClient chatClient)
    {
        _logger = logger;
        _chatClient = chatClient;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> PromptAction(string prompt)
    {

        StringBuilder promptBuilder = new StringBuilder();

        promptBuilder.Append($"user asked: {prompt} \n\n Here is the CMS1500 data: \n\n");

        foreach (var item in _HCFAData)
        {
            promptBuilder.AppendLine($"HCFAId: {item.HCFAId}, DOS: {item.DOS}, Fee: {item.Fee}, HCFAStatus: {item.HCFAStatus}, PatientName: {item.PatientName}");
        }

        promptBuilder.AppendLine("Please provide the result in HTML format. \n\n");


        var response = await _chatClient.CompleteAsync(promptBuilder.ToString());

        return View(nameof(Index), response);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public static readonly List<CMS1500Data> _HCFAData = new List<CMS1500Data>()
    {
        new CMS1500Data{HCFAId=1,Fee=52.0m, DOS=DateTime.Parse("1/18/2024"), HCFAStatus=(int)EnumHCFAStatus.Submitted , PatientName="John"},
        new CMS1500Data{HCFAId=2,Fee=88.0m, DOS=DateTime.Parse("3/18/2024"), HCFAStatus=(int)EnumHCFAStatus.Denied  , PatientName="Jane"},
        new CMS1500Data{HCFAId=3,Fee=777.0m, DOS=DateTime.Parse("3/20/2024"), HCFAStatus=(int)EnumHCFAStatus.Approved  , PatientName="Doe"},
        new CMS1500Data{HCFAId=4, Fee=123.0m,DOS=DateTime.Parse("3/21/2024"), HCFAStatus=(int)EnumHCFAStatus.Submitted  , PatientName="Smith"},
        new CMS1500Data{HCFAId=5,Fee=25.0m, DOS=DateTime.Parse("3/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Approved  , PatientName="Williams"},
        new CMS1500Data{HCFAId=6,Fee=88.0m, DOS=DateTime.Parse("3/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Denied  , PatientName="Johnson"},
        new CMS1500Data{HCFAId=7, Fee=33.0m,DOS=DateTime.Parse("1/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Submitted  , PatientName="Brown"},
        new CMS1500Data{HCFAId=8,Fee=78.0m, DOS=DateTime.Parse("1/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Approved  , PatientName="Jones"},
        new CMS1500Data{HCFAId=9, Fee=12.0m,DOS=DateTime.Parse("1/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Denied  , PatientName="Miller"},
        new CMS1500Data{HCFAId=10,Fee=88.0m, DOS=DateTime.Parse("4/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Submitted  , PatientName="Davis"},
        new CMS1500Data{HCFAId=11,Fee=89.0m, DOS=DateTime.Parse("4/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Approved  , PatientName="Garcia"},
        new CMS1500Data{HCFAId=12, Fee=32.0m,DOS=DateTime.Parse("4/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Denied  , PatientName="Rodriguez"},
        new CMS1500Data{HCFAId=13,Fee=77.0m, DOS=DateTime.Parse("5/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Submitted  , PatientName="Wilson"},
        new CMS1500Data{HCFAId=14,Fee=12.0m, DOS=DateTime.Parse("5/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Approved  , PatientName="Martinez"},
        new CMS1500Data{HCFAId=15, Fee=99.0m,DOS=DateTime.Parse("5/28/2024"), HCFAStatus=(int)EnumHCFAStatus.Denied  , PatientName="Hernandez"},


    };
}

public enum EnumHCFAStatus
{
    Submitted = 1,
    Approved = 2,
    Denied = 3
}

public class CMS1500Data
{
    public int HCFAId { get; set; }
    public DateTime DOS { get; set; }

    public int HCFAStatus { get; set; }

    public string PatientName { get; set; }

    public decimal Fee { get; set; }

}

