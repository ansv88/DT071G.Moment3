using System.Text.Json;

namespace Moment3
{
    internal class Guestbook
    {
        //En lista för alla gästboksinlägg
        private List<Post> posts { get; set; } = new List<Post>();

        //Ladda inlägg från JSON-fil (deserialisering)
        public void LoadPosts(string filePath)
        {
            //Kontrollera om filen finns innan den försöker läsas
            if (File.Exists(filePath))
            {
                //Läs in hela filens innehåll som en JSON-sträng
                var json = File.ReadAllText(filePath);

                //Deserialisera JSON-strängen till en lista med Post-objekt
                //Om deserialiseringen misslyckas (exempelvis om filen är tom) initieras en tom lista
                posts = JsonSerializer.Deserialize<List<Post>>(json) ?? new List<Post>();
            }
        }

        //Spara inlägg till JSON-fil (serialisering)
        public void SavePost(string filePath)
        {
            //Serialisera listan med inlägg till en JSON-sträng
            var json = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });  //WriteIndented: formaterar JSON:en så den blir lättläst (med radbrytningar och indrag)
            File.WriteAllText(filePath, json); //Skriv JSON-strängen till den angivna filen
        }

        //Lägg till ett nytt inlägg i gästboken
        public void AddPost(Post post)
        {
            //Lägg till det nya inlägget i listan
            posts.Add(post);
            Console.WriteLine("\nGästboksinlägg tillagt. \nTryck på valfri knapp för att fortsätta.");

            //Vänta på att användaren trycker på en knapp innan skärmen rensas
            Console.ReadLine();
            Console.Clear();
        }

        //Visa alla gästboksinlägg
        public void DisplayAllPosts()
        {
            //Kontrollera om det finns några inlägg
            if (posts.Any())
            {
                //Gå igenom varje inlägg och skriv ut innehållet
                foreach (var post in posts)
                {
                    Console.WriteLine();
                    Console.WriteLine($"[{post.Id}] {post.Name} - {post.Message}");
                }
            }
            else
            {
                //Om det inte finns några inlägg, visa ett meddelande
                Console.WriteLine("\n[ Det finns inga inlägg. ] \nTryck på valfri knapp för att fortsätta.");
            }
        }

        //Ta bort ett inlägg baserat på ID
        public void RemovePost(int postId)
        {
            //Hitta inlägget som ska tas bort genom att jämföra ID:et med det angivna postId
            var postToRemove = posts.FirstOrDefault(p => p.Id == postId);

            //Kontrollera om inlägget hittades
            if (postToRemove != null)
            {
                //Om det hittades, ta bort inlägget från listan
                posts.Remove(postToRemove);
                Console.WriteLine($"\nInlägg med ID {postId} borttaget. \nTryck på valfri knapp för att fortsätta.");
            }
            else
            {
                //Om inlägget inte hittades, visa ett felmeddelande
                Console.WriteLine($"\nInlägg med ID {postId} hittades inte. \nTryck på valfri knapp för att fortsätta.");
            }
            //Vänta på att användaren trycker på en knapp innan skärmen rensas
            Console.ReadLine();
            Console.Clear();
        }

        //Hämta nästa tillgängliga ID för ett nytt inlägg
        public int GetNextPostId()
        {
            //Kontrollera om listan med inlägg är tom
            if (posts.Count == 0)
            {
                //Om listan är tom, returnera 1 som nästa ID
                return 1;
            }

            //Om det finns inlägg, hitta det högsta ID:t och lägg till 1
            int highestId = posts.Max(p => p.Id);
            return highestId + 1;
        }

        //Skapa ett nytt Post-objekt med de angivna parametrarna (ID, namn och meddelande)
        public Post CreatePost(int id, string name, string message)
        {
            //Returnera ett nytt inlägg (Post) med angivna värden
            return new Post { Id = id, Name = name, Message = message };
        }

        //Skriv ut menyn till konsollen
        internal void PrintMenu()
        {
            Console.WriteLine("░█▀█░█▀█░█▀█░█▀▀░█░░░▀█▀░█▀▀░▀░█▀▀░░░█▀▀░█░█░█▀▀░█▀▀░▀█▀░█▀▄░█▀█░█▀█░█░█\r\n░█▀█░█░█░█░█░█▀▀░█░░░░█░░█▀▀░░░▀▀█░░░█░█░█░█░█▀▀░▀▀█░░█░░█▀▄░█░█░█░█░█▀▄\r\n░▀░▀░▀░▀░▀░▀░▀▀▀░▀▀▀░▀▀▀░▀▀▀░░░▀▀▀░░░▀▀▀░▀▀▀░▀▀▀░▀▀▀░░▀░░▀▀░░▀▀▀░▀▀▀░▀░▀▀"); //ASCII-art
            Console.WriteLine("\n\nMENY");
            Console.WriteLine("\n1. Skriv i gästboken");
            Console.WriteLine("2. Visa alla inlägg");
            Console.WriteLine("3. Ta bort inlägg");
            Console.WriteLine("\nX. Avsluta");
            Console.WriteLine(" ");
            Console.WriteLine(new string('-', 30)); //Linje före nästa del
        }
    }
}
