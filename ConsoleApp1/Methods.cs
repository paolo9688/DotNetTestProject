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

        public static T ToReal<T>(this T value)
        {
            // Tipo valore non null:
            if (value != null)
            {
                return value;
            }

            // Tipo valore null e T è stringa:
            if (typeof(T) == typeof(string))
            {
                return (T)(object)"";
            }

            // Tipo valore null e T è valore:
            if (typeof(T).IsValueType)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }

            // Tipo riferimento non stringa:
            try
            {
                // Prova a creare un'istanza usando costruttore senza parametri
                return Activator.CreateInstance<T>();
            }
            catch
            {
                // Se non si può creare, torna default (null)
                return default;
            }
        }

        //public static T ToReal<T>(this Nullable<T> value) where T : struct
        //{
        //    return value.HasValue ? value.Value : default(T);
        //}

        //public static T ToReal<T>(this T value) where T : class, new()
        //{
        // return value != null ? value : new T();
        //}

        //public static string ToReal(this string value)
        //{
        //    return value != null ? value : "";
        //}

        //public static string ToReal(this string value)
        //{
        // return value != null ? value : "";
        //}

        //public static bool ToReal(this Nullable<bool> value)
        //{
        // return value.HasValue ? value.Value : false;
        //}

        //public static int ToReal(this Nullable<int> value)
        //{
        // return value.HasValue ? value.Value : 0;
        //}
    }
}
