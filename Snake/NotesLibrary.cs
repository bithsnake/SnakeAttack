using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class NoteLibrary { }
    /*
        Hade inte ritkigt tid att fixa till denna klass som var tänkt som någon slags
        musik spelare.
    
        Funderade på om man hade kunnat hämta noterna/hz med antingen for each på något sätt och mata in dom i en array på fint sätt för att skapa en
        slags melodi slinga  genom att loopa arrayen (Så som jag nu manuellt gjort i GameStart klassen och GameOver klassen). :)

        PS Console.Beep kan endast spela upp int's tydligen men jag skrev dessa doubles ändå i sin rätta form!
        Det här är faktiskt Hz som representerar toner!
     */


    //{
    //    public double GetNote(string note)
    //    {
    //        Dictionary<string, double> Note = new Dictionary<string, double>();
    //        Note.Add("c5",523.251);
    //        Note.Add("c#5",554.365);
    //        Note.Add("d5",587.330);
    //        Note.Add("d#5",622.254);
    //        Note.Add("e5",659.255);
    //        Note.Add("f5",698.456);
    //        Note.Add("f#5",739.989);
    //        Note.Add("g5",783.991);
    //        Note.Add("g#5",830.609);
    //        Note.Add("a5",880.000);
    //        Note.Add("a#5",932.328);
    //        Note.Add("b5", 987.767);

            
    //        return Note.ContainsValue(note) ? Note.Values(note) : 0.0d;
    //    }

}
