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
            // Caso: valore non null, restituisci il valore così com'è
            if (value != null)
            {
                return value;
            }
            // Metto questo check per primo, che sia un tipo nullable o no,
            // se il valore non è null mi viene restituito subito il valore stesso.
            // Questo mi evita di fare controlli inutili.

            // Ottieni il tipo generico T al runtime
            var type = typeof(T);
            // Usa l'operatore typeof per ottenere l'oggetto Type
            // che rappresenta il tipo generico T al momento della compilazione.

            // Se T è un tipo Nullable<U>, underlyingType sarà il tipo U; altrimenti null
            var underlyingType = Nullable.GetUnderlyingType(type);
            // Ottieni il tipo sottostante di T se è un Nullable<T>

            // Caso: T è Nullable<U>
            if (underlyingType != null)
            //serve a verificare se il tipo generico T è un tipo nullable,
            //ovvero una struttura nullable come int?, bool?, DateTime?, ecc.
            {
                // Se il valore è null, restituisci l'istanza di default di U avvolta in Nullable<U>
                if (value == null)
                {
                    var defaultValue = Activator.CreateInstance(underlyingType);
                    // Crea un'istanza del tipo sottostante U con il costruttore di default
                    return (T)Activator.CreateInstance(type, defaultValue);
                    //Crea una nuova istanza di Nullable<U>(cioè di type che è Nullable< U >)
                    //passando defaultValue come valore contenuto, e la restituisce.
                    //Quindi restituisce un Nullable che contiene il valore di default di U.
                }
                else
                {
                    // Se non è null, restituisci il valore originale
                    return value;
                }
            }

            // Caso: tipo stringa, se null restituisci stringa vuota
            if (type == typeof(string))
            {
                return (T)(object)"";
            }

            // Caso: tipo valore non nullable (struct, int, bool, ecc.)
            // restituisci valore di default (es. 0, false, struct vuota)
            if (type.IsValueType)
            {
                return (T)Activator.CreateInstance(type);
            }

            // Caso: tipo array
            // restituisci un array vuoto dello stesso tipo di elemento
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var emptyArray = Array.CreateInstance(elementType, 0);
                return (T)(object)emptyArray;
            }

            // Caso: tipo classe (reference type)
            if (type.IsClass)
            {
                // Verifica se esiste un costruttore pubblico senza parametri
                var ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor != null)
                {
                    // Se sì, crea e restituisci una nuova istanza
                    return (T)Activator.CreateInstance(type);
                }
                else
                {
                    // Altrimenti, lancia eccezione esplicita
                    throw new InvalidOperationException($"Il tipo {type.FullName} non ha un costruttore pubblico senza parametri.");
                }
            }

            // Fallback: restituisci default (per sicurezza, anche se non dovrebbe mai arrivarci)
            return default;
        }
    }
}
