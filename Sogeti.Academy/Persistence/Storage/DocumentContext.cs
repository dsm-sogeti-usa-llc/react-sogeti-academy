using System;
using System.Collections.Concurrent;
using Sogeti.Academy.Application.Storage;
using Sogeti.Academy.Infrastructure.Models;

namespace Sogeti.Academy.Persistence.Storage
{
    public class DocumentContext : IDocumentContext
    {
		private readonly ConcurrentDictionary<Type, object> _collections;
		private readonly string _endpointUrl;
		private readonly string _authKey;
		
		public DocumentContext(string endpointUrl, string authKey)
		{
			_authKey = authKey;
			_endpointUrl = endpointUrl;
			_collections = new ConcurrentDictionary<Type, object>();
		}
		
        public void Dispose()
        {
            foreach(var collection in _collections)
				((IDisposable)collection.Value).Dispose();
        }

        public IDocumentCollection<T> GetCollection<T>() where T : IModel<string>
        {
            var type = typeof(T);
			var collection = _collections.GetOrAdd(type, (t) => new DocumentCollection<T>(_endpointUrl, _authKey));
			return (IDocumentCollection<T>)collection;
        }
    }
}