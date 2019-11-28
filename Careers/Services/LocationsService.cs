using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class LocationsService
    {
        private readonly CareersDbContext _context;

        public LocationsService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task<Country> AddCountryAsync(string country)
        {
            var result = await _context.Countries.AddAsync(new Country { Name = country.ToLower() });
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<City> AddCityAsync(string city, string country = "azerbaijan")
        {
            var selectedCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower() == country.ToLower());
            if (selectedCountry == null) return null;
            var result = await _context.Cities.AddAsync(new City { Name = country.ToLower(), CountryId = selectedCountry.Id });
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(string country = "azerbaijan")
        {
            var selectedCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower() == country.ToLower());
            if (selectedCountry == null) return null;
            return await _context.Cities.Where(x => x.CountryId == selectedCountry.Id).ToListAsync();
        }
    }
}
