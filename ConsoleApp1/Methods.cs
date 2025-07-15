using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            Type utenteType = utente.GetType();
            PropertyInfo[] elencoProprieta = utenteType.GetProperties();

            // Ciclo attraverso le proprietà dell'utente e ne verifico i valori:
            foreach (PropertyInfo proprieta in elencoProprieta)
            {
                object valoreProprieta = proprieta.GetValue(utente);
                if (valoreProprieta != null && valoreProprieta.ToString().ToLower().Contains(chiave))
                {
                    return true;
                }
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
                Type utenteType = utente.GetType();
                PropertyInfo[] elencoProprieta = utenteType.GetProperties();

                // Ciclo attraverso le proprietà dell'utente e ne verifico i valori:
                foreach (PropertyInfo proprieta in elencoProprieta)
                {
                    object valoreProprieta = proprieta.GetValue(utente);
                    if (valoreProprieta != null && valoreProprieta.ToString().ToLower().Contains(chiave))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
