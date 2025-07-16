E' stato creato il metodo ToReal() generico che restituisce un valore per qualsiasi tipo di dato:

public static T ToReal<T>(this T value)
{
    if (value != null)
    {
        return value;
    }

    if (typeof(T) == typeof(string))
    {
        return (T)(object)"";
    }

    if (typeof(T).IsValueType)
    {
        return (T)Activator.CreateInstance(typeof(T));
    }

    return default;
}

Il problema di questo metodo è che per i tipi riferimento non stringa può restituire null:

- Se value non è null, lo restituisce.

- Se value è null e T è stringa, restituisce stringa vuota "".

- Se value è null e T è tipo valore, restituisce default(T) (cioè un'istanza “vuota” di   quel valore).

- Se value è null e T è un tipo riferimento diverso da stringa, restituisce default che è null.

Perché?

Perché il metodo non sa come creare un’istanza “di default” di una classe qualunque.
Activator.CreateInstance funziona per i tipi valore e per le classi che hanno un costruttore pubblico senza parametri, ma:

Nel metodo non viene usato per tipi riferimento diversi da stringa.

Quindi restituisce semplicemente default, che per tipi riferimento è null.

Si propone quindi una versione migliorata di ToReal() che:

- Per tipi valore e stringhe mantiene il comportamento originale.

- Per tipi riferimento diversi da stringa prova a creare un’istanza con il costruttore senza parametri.

- Se non è possibile, ritorna comunque null.

public static T ToReal<T>(this T value)
{
    if (value != null)
    {
        return value;
    }

    if (typeof(T) == typeof(string))
    {
        return (T)(object)"";
    }

    if (typeof(T).IsValueType)
    {
        return (T)Activator.CreateInstance(typeof(T));
    }

    // Tipo riferimento non stringa
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

In questa versione:

- Se value è null e T è un tipo riferimento (es. una classe), tenta di crearne una nuova istanza.

- Se la classe ha un costruttore pubblico senza parametri, viene restituita quella nuova istanza.

- Se la classe non ha un costruttore senza parametri (o è astratta), Activator.CreateInstance<T>() lancerà eccezione e si ritorna default (cioè null).

N.B. Questo metodo funziona solo se le classi hanno costruttore pubblico senza parametri.