
namespace Dominio.Entidades
{
    public class Email : Base
    {
        public string Identificador { get; init; }
        public string Assunto { get; init; }
        public string Destinatario { get; init; }
        public string Remetente { get; init; }
        public string Mensagem { get; init; }
    }
}