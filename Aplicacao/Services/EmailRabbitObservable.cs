using Aplicacao.Contratos;
using Aplicacao.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class EmailRabbitObservable : IEmailRabbitObservable
    {
        private List<IObserver<EmailDTO>> _observers;
        public EmailRabbitObservable()
        {
            _observers = new List<IObserver<EmailDTO>>();
        }

        public Task Send(EmailDTO dados) => Task.Run(() => _observers.ForEach(obs => obs.OnNext(dados)));

        public IDisposable Subscribe(IObserver<EmailDTO> observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return null;
        }
    }
}
