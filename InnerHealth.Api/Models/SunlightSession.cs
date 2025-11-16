namespace InnerHealth.Api.Models;

/// <summary>
/// Representa uma sessão de exposição ao sol em minutos numa data.
/// </summary>
public class SunlightSession
{
    public int Id { get; set; }
    /// <summary>
    /// Data da exposição ao sol.
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// Duração da exposição ao sol em minutos.
    /// </summary>
    public int Minutes { get; set; }

    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}