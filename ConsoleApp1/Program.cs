using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string parolaChiave = "sara";
            const int numero = 0;
            const bool flag = false;

            int? numerino = null;
            Console.WriteLine(numerino.ToReal());
            int? numerino2 = 5;
            Console.WriteLine(numerino2.ToReal()); // Stampa 5
            //Console.ReadLine();

            // creo una nuova lista di utenti:
            List<Utente_Type> utenti = new List<Utente_Type>
            {
                new Utente_Type("Mario", "Rossi", "mario.rossi@email.com", "Password1!", "Via Roma 1"),
                new Utente_Type("Luigi", "Verdi", "luigi.verdi@email.com", "Password2!", "Via Milano 2"),
                new Utente_Type("Anna", "Bianchi", "anna.bianchi@email.com", "Password3!", "Via Napoli 3"),
                new Utente_Type("Giulia", "Neri", "giulia.neri@email.com", "Password4!", "Via Torino 4"),
                new Utente_Type("Paolo", "Russo", "paolo.russo@email.com", "Password5!", "Via Firenze 5"),
                new Utente_Type("Sara", "Gialli", "sara.gialli@email.com", "Password6!", "Via Venezia 6"),
                new Utente_Type("Luca", "Marrone", "luca.marrone@email.com", "Password7!", "Via Genova 7"),
                new Utente_Type("Elena", "Viola", "elena.viola@email.com", "Password8!", "Via Bologna 8"),
                new Utente_Type("Marco", "Blu", "marco.blu@email.com", "Password9!", "Via Bari 9"),
                new Utente_Type("Francesca", "Rosa", "francesca.rosa@email.com", "Password10!", "Via Lecce 10")
            };

            // stampo la lista utenti:
            Console.WriteLine("Lista utenti:\n");

            foreach (Utente_Type utente in utenti)
            {
                Console.WriteLine(utente.ToString());
            }

            // controllo se la parola chiave è presente in uno degli utenti ciclando su tutta la lista utenti:
            foreach (Utente_Type utente in utenti)
            {
                if (utente.CheckUtente(parolaChiave))
                {
                    Console.WriteLine($"\nLa parola '{parolaChiave}' soddisfa i criteri di ricerca per l'utente {utente.Nome} {utente.Cognome}.");
                }
            }

            // controllo se la parola chiave è presente in uno degli utenti richiamando l'apposito metodo:
            if (utenti.CheckUtenti(parolaChiave))
            {
                Console.WriteLine($"\nLa parola '{parolaChiave}' soddisfa i criteri di ricerca per la lista di utenti.");
            } else
            {
                Console.WriteLine("\nNessun utente soddisfa i criteri di ricerca.");
            }
        }
    }
}
