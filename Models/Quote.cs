using System.Reflection.Metadata.Ecma335;
namespace _100_BackEnd.Models;

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }

    public Quote(int id, string text, string author, string image)
    {
        this.Id = id;
        this.Text = text;
        this.Author = author;
        this.Image = image;
    }

    public override string ToString()
    {
        return this.Id + " - " + this.Text + " - " + this.Author + " - " + this.Image;
    }
}