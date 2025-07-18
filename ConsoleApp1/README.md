# ToReal&lt;T&gt;() Extension Method in C#

## Descrizione

`ToReal<T>()` è un metodo di estensione generico per oggetti in C# che assicura che un valore non sia `null`, restituendo una sua versione "reale" o predefinita. In pratica, converte un valore `null` in un valore sensato per il tipo specificato:

- Per i tipi *value* (es. `int`, `bool`), se il valore non è null restituisce il valore stesso, altrimenti restituisce il valore di default (0, false, ecc.).
- Per i tipi *nullable* (es. `int?`, `bool?`), se il valore non è null restituisce il valore stesso, altrimenti restituisce un’istanza di Nullable<T> contenente il valore di default del tipo sottostante.
- Per le stringhe (`string`), se il valore non è null restituisce la stringa stessa, altrimenti restituisce una stringa vuota `""`.
- Per gli array (`T[]`), se il valore non è null restituisce l’array stesso, altrimenti restituisce un array vuoto dello stesso tipo.
- Per le classi (*reference types*), se esiste un costruttore pubblico senza parametri crea e restituisce una nuova istanza; altrimenti lancia un’eccezione.

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

Guid? guid = null;
Guid guidReale = guid.ToReal(); // risultato: empty Guid
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
    },
    {
        "TestName": "ToReal_NullableGuidNull_ReturnsGuidEmpty",
        "Input": null,
        "Output": "00000000-0000-0000-0000-000000000000",
        "OutputJson": "\"00000000-0000-0000-0000-000000000000\""
    }
]
```

### Classe con costruttore pubblico senza parametri, istanza nulla

```csharp
public class MyClassWithDefaultCtor
{
    public int Number { get; set; }

    public MyClassWithDefaultCtor()
    {
        Number = 42;
    }
}

MyClassWithDefaultCtor myClass = null;
MyClassWithDefaultCtor resultMyClass = myClass.ToReal();
// resultMyClass è una nuova istanza di MyClassWithDefaultCtor con Number = 42
```

### Risultato del relativo test


```json
{
    "TestName": "ToReal_ClassWithDefaultCtorNull_ReturnsNewInstance",
    "Input": null,
    "OutputIsNull": false,
    "Number": 42,
    "OutputJson": "{\"Number\":42}"
}
```

### Classe con costruttore pubblico senza parametri, istanza non nulla

```csharp
public class MyClassWithDefaultCtor
{
    public int Number { get; set; }

    public MyClassWithDefaultCtor()
    {
        Number = 42;
    }
}

MyClassWithDefaultCtor myClass = new MyClassWithDefaultCtor();
MyClassWithDefaultCtor resultMyClass = myClass.ToReal();
// resultMyClass è la stessa istanza di MyClassWithDefaultCtor con Number = 42
```

### Risultato del relativo test

```json
{
    "TestName": "ToReal_ClassWithDefaultCtorNotNull_ReturnsSameInstance",
    "InputNumber": 42,
    "OutputIsSameInstance": true,
    "OutputJson": "{\"Number\":42}"
}
```

### Classe senza costruttore pubblico senza parametri, lancia eccezione

```csharp
class MyClassWithoutDefaultCtor
{
    public MyClassWithoutDefaultCtor(int x) { }
}

MyClassWithoutDefaultCtor myClass = null;
MyClassWithoutDefaultCtor resultMyClass = myClass.ToReal();
// Il tipo MyClassWithoutDefaultCtor non ha un costruttore pubblico senza parametri.
```

## Vantaggi

- Elimina il bisogno di controlli espliciti per `null` in molte situazioni
- Aiuta a evitare `NullReferenceException`
- Ottimo per inizializzazioni o fallback sicuri
- Utile in scenari di deserializzazione, conversione dati o inizializzazione automatica

## Comportamento Interno

Il metodo segue questo ordine logico:

- Se il valore è già non `null`, lo restituisce direttamente.
- Se `T` è `Nullable<U>`, restituisce `new Nullable<U>(default(U))`.
- Se `T` è `string`, restituisce stringa vuota `""`.
- Se `T` è un `value type`, restituisce `default(T)`.
- Se `T` è un `array`, restituisce un array vuoto del tipo corretto.
- Se `T` è una classe con costruttore pubblico senza parametri, restituisce una nuova istanza.
- In mancanza di alternative, restituisce `default(T)` (fallback di sicurezza).

## Eccezioni

`InvalidOperationException`

Viene sollevata quando `T` è una classe senza un costruttore pubblico senza parametri.