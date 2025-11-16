namespace InnerHealth.Api.Models;

/// <summary>
/// Representa uma ingestão de água em uma data. A quantidade é armazenada em mililitros.
/// </summary>
public class WaterIntake
{
    public int Id { get; set; }
    /// <summary>
    /// Data da ingestão. Só a data importa; o horário é ignorado.
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// Quantidade de água consumida em mililitros.
    /// </summary>
    public int AmountMl { get; set; }
    
    // Chave estrangeira pro perfil de usuário.
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}