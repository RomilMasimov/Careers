using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Careers.Services
{
    public class MemoryService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryService(IMemoryCache memoryCacheoCache)
        {
            _memoryCache = memoryCacheoCache;
        }

        public void foo()
        {
            _memoryCache.GetOrCreate("",x=>x.Value);
            object value;

            if (!_memoryCache.TryGetValue("key", out value))
            {
                _memoryCache.Set("key", value);

            }
        }

    }
}
