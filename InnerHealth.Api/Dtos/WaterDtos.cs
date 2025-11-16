using System.ComponentModel.DataAnnotations;

namespace InnerHealth.Api.Dtos;

/// <summary>
/// DTOs para endpoints de ingestão de água.
/// </summary>
public class WaterIntakeDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int AmountMl { get; set; }
}

public class CreateWaterIntakeDto
{
    /// <summary>
    /// Quantidade de água consumida em mililitros.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int AmountMl { get; set; }
}

public class UpdateWaterIntakeDto
{
    /// <summary>
    /// Quantidade de água consumida em mililitros.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int AmountMl { get; set; }
}