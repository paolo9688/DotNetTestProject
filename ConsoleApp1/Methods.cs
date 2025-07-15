using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Methods
    {
        public static void CheckUtente(this Utente_Type utente, string parolaChiave)
        {
            if (string.IsNullOrWhiteSpace(parolaChiave))
            {
                Console.WriteLine("\nLa parola inserita non è valida.");
                return;
            }

            string chiave = parolaChiave.ToLower();

            if ((utente.Nome != null && utente.Nome.ToLower().Contains(chiave)) ||
                (utente.Email != null && utente.Email.ToLower().Contains(chiave)) ||
                (utente.Cognome != null && utente.Cognome.ToLower().Contains(chiave)) ||
                (utente.Password != null && utente.Password.ToLower().Contains(chiave)) ||
                (utente.Indirizzo != null && utente.Indirizzo.ToLower().Contains(chiave)) ||
                (utente.DataRegistrazione != null && utente.DataRegistrazione.Value.ToString("d").ToLower().Contains(chiave)) ||
                (utente.RuoloUtente != null && utente.RuoloUtente.ToString().ToLower().Contains(chiave)))
            {
                Console.WriteLine($"\nLa parola '{parolaChiave}' soddisfa i criteri di ricerca per l'utente {utente.Nome} {utente.Cognome}.");
            } else
            {
                Console.WriteLine($"\nLa parola '{parolaChiave}' non soddisfa i criteri di ricerca per l'utente {utente.Nome} {utente.Cognome}.");
            }
        }

        public static void CheckUtenti(this List<Utente_Type> utenti, string parolaChiave)
        {
            if (string.IsNullOrWhiteSpace(parolaChiave))
            {
                Console.WriteLine("\nLa parola inserita non è valida.");
                return;
            }

            string chiave = parolaChiave.ToLower();

            foreach (Utente_Type utente in utenti)
            {
                if ((utente.Nome != null && utente.Nome.ToLower().Contains(chiave)) ||
                    (utente.Cognome != null && utente.Cognome.ToLower().Contains(chiave)) ||
                    (utente.Email != null && utente.Email.ToLower().Contains(chiave)) ||
                    (utente.Password != null && utente.Password.ToLower().Contains(chiave)) ||
                    (utente.Indirizzo != null && utente.Indirizzo.ToLower().Contains(chiave)) ||
                    (utente.DataRegistrazione != null && utente.DataRegistrazione.Value.ToString("d").ToLower().Contains(chiave)) ||
                    (utente.RuoloUtente != null && utente.RuoloUtente.ToString().ToLower().Contains(chiave)))
                {
                    Console.WriteLine($"\nLa parola '{parolaChiave}' soddisfa i criteri di ricerca per l'utente {utente.Nome} {utente.Cognome}.");
                }
                else
                {
                    // Console.WriteLine($"\nLa parola '{parolaChiave}' non soddisfa i criteri di ricerca per l'utente {utente.Nome} {utente.Cognome}.");
                }
            }
        }
    }
}
