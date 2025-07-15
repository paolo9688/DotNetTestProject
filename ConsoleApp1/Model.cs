using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum RuoloUtente_Enum
    {
        CLIENTE,
        ADMIN
    }

    public class Utente_Type
    {
        // --- Attributi ---:

        public Guid? UtenteId { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Indirizzo { get; set; }
        public DateTime? DataRegistrazione { get; set; }
        public RuoloUtente_Enum? RuoloUtente { get; set; }

        // --- Costruttori ---:

        // Costruttore di default:
        public Utente_Type()
        {
            this.DataRegistrazione = DateTime.Now;
            this.RuoloUtente = RuoloUtente_Enum.CLIENTE;
        }

        // Costruttore parametrico:
        public Utente_Type(string nome, string cognome, string email, string password, string indirizzo)
            : this()
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Email = email;
            this.Password = password;
            this.Indirizzo = indirizzo;
        }

        // --- Metodi ---:

        // Metodo ToString():
        public override string ToString()
        {
            string data = DataRegistrazione.HasValue
                ? DataRegistrazione.Value.ToShortDateString()
                : "N/D";
            return $"{Nome} {Cognome} - {Email} - {Indirizzo} - Registrato il: {data} - Ruolo: {RuoloUtente}";
        }
    }
}
