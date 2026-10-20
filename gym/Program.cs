using Spectre.Console;
using gym.Domain.Entities;
using gym.Domain.ValueObjects;
using gym.Application.Services;


var gymService = new GymService();
var activeClient = new Client("Alex Marinescu", Email.Create("alex@gym.ro"));

while (true) 
{
    Console.Clear();
    
    AnsiConsole.Write(new FigletText("GYM MASTER").Centered().Color(Color.Cyan1));

    var role = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[yellow]Selectați modul de acces:[/]")
            .AddChoices(new[] { "🛡️ Administrator", "👤 Client", "❌ Ieșire" }));

    if (role == "❌ Ieșire") break;

    if (role == "🛡️ Administrator") 
    {
        HandleAdminMenu(gymService);
    } 
    else 
    {
        HandleClientMenu(gymService, activeClient);
    }
}

void HandleAdminMenu(GymService service) 
{
    while (true) 
    {
        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]Panou Admin[/]")
                .AddChoices(new[] { "➕ Creare Clasă", "📊 Monitorizare Activitate", "🔙 Înapoi" }));

        if (action == "🔙 Înapoi") break;

        if (action == "➕ Creare Clasă") 
        {
            var type = AnsiConsole.Ask<string>("Tipul clasei (ex: Cardio/Forță):");
            var cap = AnsiConsole.Ask<int>("Capacitate zonă:");
            var trainer = AnsiConsole.Ask<string>("Nume antrenor:");
            
            service.CreateClass(type, cap, trainer);
            AnsiConsole.MarkupLine("[green]✔ Clasa a fost creată![/]");
            Thread.Sleep(1000);
        } 
        else if (action == "📊 Monitorizare Activitate") 
        {
            
            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("Zonă").AddColumn("Antrenor").AddColumn("Ocupare");
            
            foreach (var c in service.Classes) 
            {
                table.AddRow(c.Type, c.TrainerName, $"{c.ParticipantIds.Count}/{c.Capacity}");
            }
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("\n[grey]Apasă orice tastă pentru a reveni...[/]");
            Console.ReadKey();
        }
    }
}

void HandleClientMenu(GymService service, Client client) 
{
    while (true) 
    {
        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold blue]Portal Client[/]")
                .AddChoices(new[] { "💳 Cumpără Abonament", "📅 Rezervă Loc la Clasă", "🔙 Înapoi" }));

        if (action == "🔙 Înapoi") break;

        if (action == "💳 Cumpără Abonament") 
        {
            
            client.AddSubscription(new Subscription("Lunar", 1));
            AnsiConsole.MarkupLine("[bold green]✔ Abonament activat![/]");
            Thread.Sleep(1000);
        }
        else if (action == "📅 Rezervă Loc la Clasă") 
        {
            if (!client.HasActiveSubscription()) 
            {
                AnsiConsole.MarkupLine("[red]⚠ Eroare: Nu aveți un abonament activ![/]");
                Thread.Sleep(1500);
                continue;
            }

            if (!service.Classes.Any()) 
            {
                AnsiConsole.MarkupLine("[grey]Nu există clase disponibile momentan.[/]");
                Thread.Sleep(1000);
                continue;
            }

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<GymClass>()
                    .Title("Alege clasa:")
                    .UseConverter(c => $"{c.Type} - {c.TrainerName} ({c.ParticipantIds.Count}/{c.Capacity})")
                    .AddChoices(service.Classes));

            
            if (service.MakeReservation(selected.Id, client))
                AnsiConsole.MarkupLine("[green]✔ Rezervare confirmată![/]");
            else
                AnsiConsole.MarkupLine("[red]❌ Locuri epuizate![/]");
            
            Thread.Sleep(1500);
        }
    }
}