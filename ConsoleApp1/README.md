# ToReal<T>() Extension Method in C#

## Descrizione

`ToReal<T>()` è un metodo di estensione generico per oggetti in C# che assicura che un valore non sia `null`, restituendo una sua versione "reale" o predefinita. In pratica, converte un valore `null` in un valore sensato per il tipo specificato:

- Per i tipi *value* (es. `int`, `bool`), se non null restituisce il valore effettivo, altrimenti restituisce il valore di default (`0`, `false`, ecc.)
- Per i tipi *nullable* (es. `int?`, `bool?`), se non null restituisce il valore effettivo, altrimenti restituisce un'istanza di `Nullable<T>` contenente il valore di default del tipo sottostante
- Per le stringhe (`string`), se non null restituisce il valore effettivo, altrimenti restituisce una stringa vuota `""`
- Per gli array (`T[]`), se non null restituisce l'array attuale, altrimenti restituisce un array vuoto dello stesso tipo
- Per le classi (*reference types*) crea una nuova istanza se è disponibile un costruttore pubblico senza parametri, altrimenti lancia un'eccezione

## Firma

```csharp
public static T ToReal<T>(this T value)
```

## Esempi di utilizzo

### Tipi primitivi, nullable, stringhe e array

```csharp
int? numero = null;
int realeNumero = numero.ToReal(); // risultato: 0

string testo = null;
string realeStringa = testo.ToReal(); // risultato: ""

bool? flag = null;
bool flagReale = flag.ToReal(); // risultato: false

DateTime? data = null;
DateTime dataReale = data.ToReal(); // risultato: DateTime.MinValue

int[] numeri = null;
int[] arrayReale = numeri.ToReal(); // risultato: array vuoto di int
```

### Risultati dei relativi test

```json
[
    {
        "TestName": "ToReal_NullableIntNull_ReturnsZero",
        "Input": null,
        "Output": 0,
        "OutputJson": "0"
    },
    {
        "TestName": "ToReal_StringNull_ReturnsEmptyString",
        "Input": null,
        "Output": "",
        "OutputJson": "\"\""
    },
    {
        "TestName": "ToReal_NullableBoolNull_ReturnsFalse",
        "Input": null,
        "Output": false,
        "OutputJson": "false"
    },
    {
        "TestName": "ToReal_NullableDateTimeNull_ReturnsDateTimeMinValue",
        "Input": null,
        "Output": "0001-01-01T00:00:00",
        "OutputJson": "\"0001-01-01T00:00:00\""
    },
    {
        "TestName": "ToReal_IntArrayNull_ReturnsEmptyArray",
        "Input": null,
        "OutputLength": 0,
        "OutputJson": "[]"
    }
]
```

### Classi con costruttore pubblico senza parametri

```csharp
public class Persona
{
    public string Nome { get; set; }
    public int Età { get; set; }

    public Persona()
    {
        Nome = "Sconosciuto";
        Età = 0;
    }
}

Persona p = null;
Persona resultPersona = p.ToReal();
// resultPersona è una nuova istanza di Persona con Nome = "Sconosciuto" e Età = 0
```

### Classi senza costruttore pubblico senza parametri

```csharp
public class Prodotto
{
    public string Nome { get; set; }
    public decimal Prezzo { get; set; }

    public Prodotto(string nome, decimal prezzo)
    {
        Nome = nome;
        Prezzo = prezzo;
    }
}

Prodotto p = null;
try
{
    Prodotto resultProdotto = p.ToReal();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    // Output: Il tipo Prodotto non ha un costruttore pubblico senza parametri.
}
```

### Classi derivate con costruttore pubblico senza parametri

```csharp
public class Veicolo
{
    public string Marca { get; set; }

    public Veicolo()
    {
        Marca = "Sconosciuta";
    }
}

public class Auto : Veicolo
{
    public string Modello { get; set; }

    public Auto() : base()
    {
        Modello = "Modello base";
    }
}

Auto a = null;
Auto resultAuto = a.ToReal();
// resultAuto è una nuova istanza di Auto con Marca = "Sconosciuta" e Modello = "Modello base"
```

### Classi con costruttore privato (non istanziabili pubblicamente)

```csharp
public class Configurazione
{
    public string Nome { get; set; }

    private Configurazione()
    {
        Nome = "Default";
    }
}

Configurazione c = null;
try
{
    Configurazione resultConfig = c.ToReal();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    // Output: Il tipo Configurazione non ha un costruttore pubblico senza parametri.
}
```

### Classi astratte o interfacce (non istanziabili)

```csharp
public abstract class Animale
{
    public abstract string Suono();
}

Animale a = null;
try
{
    Animale resultAnimale = a.ToReal();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    // Output: Il tipo Animale non ha un costruttore pubblico senza parametri.
}
```

# 4. Vantaggi

- Elimina il bisogno di controlli espliciti per `null` in molte situazioni
- Aiuta a evitare `NullReferenceException`
- Ottimo per inizializzazioni o fallback sicuri
- Utile in scenari di deserializzazione, conversione dati o inizializzazione automatica

# 5. Comportamento Interno

Il metodo segue questo ordine logico:

- Se il valore è già non `null`, lo restituisce direttamente.
- Se `T` è `Nullable<U>`, restituisce `new Nullable<U>(default(U))`.
- Se `T` è `string`, restituisce stringa vuota `""`.
- Se `T` è un `value type`, restituisce `default(T)`.
- Se `T` è un `array`, restituisce un array vuoto del tipo corretto.
- Se `T` è una classe con costruttore pubblico senza parametri, restituisce una nuova istanza.
- In mancanza di alternative, restituisce `default(T)` (fallback di sicurezza).

# 6. Eccezioni

`InvalidOperationException`

Viene sollevata quando `T` è una classe senza un costruttore pubblico senza parametri.