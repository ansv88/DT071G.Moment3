namespace Moment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skapa en instans av Guestbook-klassen som hanterar alla inlägg
            var guestBook = new Guestbook();

            //Sökväg till JSON-filen där inläggen sparas/laddas
            var filePath = "guestbook.json";

            //Ladda inlägg från fil vid programmets start
            guestBook.LoadPosts(filePath);

            //Loop för att visa menyn och hantera användarens val
            while (true)
            {
                //Skriv ut menyn
                guestBook.PrintMenu();

                //Efterfråga inmatning från användaren för att välja menyval
                Console.Write("\nVal: ");
                var userInput = Console.ReadLine();

                //Switch-sats för att hantera användarens menyval
                switch (userInput)
                {
                    case "1":
                        //Variabler för namn och meddelande som ska läggas till
                        string name;
                        string message;

                        //Kontrollera/validera att namn inte är tomt eller bara har mellanslag
                        do
                        {
                            Console.Write("\nNamn: ");
                            name = Console.ReadLine()!; //Använder "!" för att undvika null-värden

                            //Kontroll om namnet är tomt eller bara har mellanslag
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Du måste ange ett namn.");
                            }
                        } while (string.IsNullOrWhiteSpace(name)); //Fortsätt be om namn tills det är korrekt

                        //Kontrollera/validera att meddelande inte är tomt eller bara har mellanslag
                        do
                        {
                            Console.Write("Ditt meddelande: ");
                            message = Console.ReadLine()!;

                            //Kontroll om meddelandet är tomt eller bara har mellanslag
                            if (string.IsNullOrWhiteSpace(message))
                            {
                                Console.WriteLine("Du måste ange ett meddelande.");
                            }

                        } while (string.IsNullOrWhiteSpace(message)); //Fortsätt be om meddelande tills det är korrekt

                        //Skapa ett nytt inlägg efter att namn och meddelande har validerats
                        var newPost = guestBook.CreatePost(guestBook.GetNextPostId(), name, message);

                        //Lägg till det nya inlägget i gästboken
                        guestBook.AddPost(newPost);

                        //Spara allt till JSON-filen efter att ett inlägg har lagts till
                        guestBook.SavePost(filePath);
                        break;

                    case "2":
                        //Visa alla inlägg som finns i gästboken
                        guestBook.DisplayAllPosts();

                        //Vänta på att användaren trycker på en knapp innan skärmen rensas
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case "3":
                        //Visa alla inlägg för att underlätta valet av inlägg att ta bort
                        guestBook.DisplayAllPosts();

                        //Ta bort ett inlägg baserat på ID
                        Console.Write("\n\nAnge ID:t på inlägget du vill ta bort: ");

                        if (int.TryParse(Console.ReadLine(), out int postId)) //Försök omvandla användarens inmatning till ett heltal
                        {
                            guestBook.RemovePost(postId); //Om omvandlingen lyckas, ta bort inlägget med angivet ID
                            guestBook.SavePost(filePath); //Spara den uppdaterade listan med inlägg till JSON-filen
                        }
                        else //Om omvandlingen misslyckas (exempelvis felaktigt format), visa felmeddelande
                        {
                            Console.WriteLine("\nFelaktigt format. Försök igen. \nTryck på valfri knapp för att fortsätta.");

                            //Vänta på att användaren trycker på en knapp innan skärmen rensas
                            Console.ReadLine();
                            Console.Clear();
                        }

                        break;

                    case "x":
                        //Avsluta programmet och spara alla inlägg innan avslut
                        guestBook.SavePost(filePath);
                        return;

                    default:
                        //Hantera ogiltiga menyval genom att visa ett felmeddelande
                        Console.WriteLine("\nVälj ett giltigt menyval. \nTryck på valfri knapp för att fortsätta.");

                        //Vänta på att användaren trycker på en knapp innan skärmen rensas
                        Console.ReadLine();
                        Console.Clear();
                        break;

                }
            }
        }
    }
}
