﻿using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services;

public class CityInfoRepository : ICityInfoRepository
{
    private readonly CityInfoDbContext _context;

    public CityInfoRepository(CityInfoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities
            .OrderBy(city => city.Name)
            .ToListAsync();
    }

    public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? query, int pageNumber, int pageSize)
    {
        var collection = _context.Cities as IQueryable<City>; // we want to gather all the benefits of the deffered execution

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            collection = collection.Where(city => city.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();
            collection = collection.Where(city =>
                city.Name.Contains(query)
                || (city.Description != null && city.Description.Contains(query)));
        }

        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionToReturn = await collection
            .OrderBy(city => city.Name)
            .Skip(pageSize * (pageNumber - 1)) // must be last :-)!
            .Take(pageSize)
            .ToListAsync();
            
        return (collectionToReturn, paginationMetadata);
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
    {
        var query = _context.Cities.AsQueryable();

        if (includePointsOfInterest)
        {
            query = query.Include(city => city.PointsOfInterest);
        }

        return await query.FirstOrDefaultAsync(city => city.Id == cityId);
    }

    public async Task<bool> CityExistsAsync(int cityId)
    {
        return await _context.Cities.AnyAsync(city => city.Id == cityId);
    }

    public async Task<bool> CityNameMatchesCityId(string name, int cityId)
    {
        return await _context.Cities.AnyAsync(city => city.Name == name && city.Id == cityId);
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
    {
        return await _context.PointsOfInterest
            .Where(point => point.CityId == cityId)
            .ToListAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
    {
        return await _context.PointsOfInterest
            .FirstOrDefaultAsync(point => point.CityId == cityId && point.Id == pointOfInterestId);
    }

    public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
    {
        var city = await GetCityAsync(cityId, false);
        if (city is not null)
        {
            city.PointsOfInterest.Add(pointOfInterest);
        }
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterest)
    {
        _context.PointsOfInterest.Remove(pointOfInterest);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }
}