using System;

namespace Dominio.Entidades
{
    public class Base
    {
        public Guid Id { get; init; }
        public DateTime DataCriacao { get; init; }
        public Base()
        {
            DataCriacao = DateTime.Now;
        }
    }
}