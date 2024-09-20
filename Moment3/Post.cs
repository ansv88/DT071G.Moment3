
namespace Moment3
{
    //Klass som representerar ett inlägg i gästboken
    public class Post
    {
        public int Id { get; set; } //Unikt ID för varje inlägg (automatiskt genererat i programmet)
        public required string Name { get; set; }  //Namnet på den som skrivit inlägget (obligatoriskt)
        public required string Message { get; set; } //Själva meddelandet i inlägget (obligatoriskt)
    }
}