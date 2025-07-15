using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Methods
    {
        public static bool CheckUtente(this Utente_Type utente, string parolaChiave)
        {
            if (string.IsNullOrWhiteSpace(parolaChiave))
            {
                throw new ArgumentNullException("Attenzione: la parola inserita non è valida.", parolaChiave);
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
                return true;
            }
            return false;
        }

        public static bool CheckUtenti(this List<Utente_Type> utenti, string parolaChiave)
        {
            if (string.IsNullOrWhiteSpace(parolaChiave))
            {
                throw new ArgumentNullException("Attenzione: la parola inserita non è valida.", parolaChiave);
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
                    return true;
                }
            }
            return false;
        }
    }
}
