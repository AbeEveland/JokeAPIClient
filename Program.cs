using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using ConsoleTables;

namespace OneListClient
{
    class Program
    {
        class OfficialJokeAPIItem
        {
            public int id { get; set; }
            public string type { get; set; }
            public string setup { get; set; }
            public string punchline { get; set; }
        }
        static async Task showTenJokes(string token)
        {


            var client = new HttpClient();

            var url = $"https://official-joke-api.appspot.com/jokes/programming/ten";


            var getTenJokes = await client.GetStreamAsync(url);

            var tenJokes = await JsonSerializer.DeserializeAsync<List<OfficialJokeAPIItem>>(getTenJokes);



            var table = new ConsoleTable("id", "type", "setup");

            foreach (var item in tenJokes)
            {
                table.AddRow(item.id, item.type, item.setup);
            }
            table.Write(Format.Minimal);
        }
        static async Task oneRandomJoke()
        {
            //try
            //{
            var client = new HttpClient();

            var url = $"https://official-joke-api.appspot.com/random_joke";
            var getRandomJoke = await client.GetStreamAsync(url);
            var randomJoke = await JsonSerializer.DeserializeAsync<OfficialJokeAPIItem>(getRandomJoke);
            //var table = new ConsoleTable("id", "type", "setup");



            Console.WriteLine(randomJoke.setup);
            Console.WriteLine(randomJoke.punchline);




            //}


            // catch (HttpRequestException)

            // {
            //     Console.WriteLine("I could not find that item!");
            // }
        }

        static async Task Main(string[] args)
        {

            var token = "";
            if (args.Length == 0)
            {
                Console.Write("What jokes would you like? ");
                token = Console.ReadLine();
            }
            else
            {
                token = args[0];
            }
            var keepGoing = true;
            while (keepGoing)
            {
                Console.Clear();
                Console.Write("Get (T)en programming jokes,(O)ne random joke or (Q)uit: ");
                var choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "O":
                        await oneRandomJoke();
                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();
                        break;
                    case "Q":
                        keepGoing = false;
                        break;
                    case "T":
                        await showTenJokes(token);
                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
