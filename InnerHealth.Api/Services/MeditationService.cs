using Microsoft.EntityFrameworkCore;
using InnerHealth.Api.Data;
using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <inheritdoc />
public class MeditationService : IMeditationService
{
    private readonly ApplicationDbContext _context;
    private const int RecommendedMinutes = 5;
    public MeditationService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MeditationSession>> GetSessionsAsync(DateOnly date)
    {
        return await _context.MeditationSessions.Where(m => m.Date == date).AsNoTracking().ToListAsync();
    }
    public async Task<int> GetDailyTotalAsync(DateOnly date)
    {
        return await _context.MeditationSessions.Where(m => m.Date == date).SumAsync(m => m.Minutes);
    }
    public async Task<IDictionary<DateOnly, int>> GetWeeklyTotalsAsync(DateOnly weekStart)
    {
        var totals = new Dictionary<DateOnly, int>();
        for (int i = 0; i < 7; i++)
        {
            var day = weekStart.AddDays(i);
            totals[day] = 0;
        }
        var weekEnd = weekStart.AddDays(7);
        var grouped = await _context.MeditationSessions.Where(m => m.Date >= weekStart && m.Date < weekEnd)
            .GroupBy(m => m.Date)
            .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Minutes) })
            .ToListAsync();
        foreach (var item in grouped)
        {
            totals[item.Date] = item.Total;
        }
        return totals;
    }
    public int GetRecommendedDailyMinutes() => RecommendedMinutes;
    public async Task<MeditationSession> AddSessionAsync(int minutes)
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var user = await _context.UserProfiles.FirstOrDefaultAsync();
        if (user == null)
        {
            user = new UserProfile { Weight = 0, Height = 0, Age = 0 };
            _context.UserProfiles.Add(user);
            await _context.SaveChangesAsync();
        }
        var session = new MeditationSession { Date = date, Minutes = minutes, UserProfileId = user.Id };
        _context.MeditationSessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }
    public async Task<MeditationSession?> UpdateSessionAsync(int id, int minutes)
    {
        var session = await _context.MeditationSessions.FindAsync(id);
        if (session == null) return null;
        session.Minutes = minutes;
        await _context.SaveChangesAsync();
        return session;
    }
    public async Task<bool> DeleteSessionAsync(int id)
    {
        var session = await _context.MeditationSessions.FindAsync(id);
        if (session == null) return false;
        _context.MeditationSessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }
}