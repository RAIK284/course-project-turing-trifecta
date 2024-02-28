using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public static class SpectrumCardModelBuilderExtensions
{
    public static void SeedSpectrumCards(this ModelBuilder modelBuilder)
    {
        var SpectrumCardID_1 = Guid.Parse("d443399e-4292-45d3-903e-937743e049d3");
        var SpectrumCardID_2 = Guid.Parse("8e4a470c-1df2-4bf5-a034-2004628eae90");
        var SpectrumCardID_3 = Guid.Parse("c5150589-e161-4096-9c73-e72b75e8d0e7");
        var SpectrumCardID_4 = Guid.Parse("77b7290e-583f-463d-b643-b961d353e7f3");
        var SpectrumCardID_5 = Guid.Parse("4508e7d3-2d87-4d3f-9811-a80ceef6c14b");
        var SpectrumCardID_6 = Guid.Parse("ca7c9ab3-71b1-4376-a452-4e1ce108e070");
        var SpectrumCardID_7 = Guid.Parse("d737e617-8b15-48aa-84b6-80eb11b8d09a");
        var SpectrumCardID_8 = Guid.Parse("a32543d2-1da3-4d39-b533-66014de89889");
        var SpectrumCardID_9 = Guid.Parse("af711bff-924a-407b-b712-99e13b0cbf9f");
        var SpectrumCardID_10 = Guid.Parse("4776e959-b795-4ec1-aa5f-440d786387d3");

        modelBuilder.Entity<SpectrumCard>().HasData(
            new SpectrumCard
            {
                ID = SpectrumCardID_1,
                LeftName = "Good",
                RightName = "Bad"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_2,
                LeftName = "Highly Attractive",
                RightName = "Mildly Attractive"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_3,
                LeftName = "Cold",
                RightName = "Hot"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_4,
                LeftName = "Weird",
                RightName = "Normal"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_5,
                LeftName = "Colorful",
                RightName = "Colorless"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_6,
                LeftName = "High Calorie",
                RightName = "Low Calorie"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_7,
                LeftName = "Feels Good",
                RightName = "Feels Bad"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_8,
                LeftName = "Expensive",
                RightName = "Cheap"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_9,
                LeftName = "Overrated Weapon",
                RightName = "Underrated Weapon"
            },
            new SpectrumCard
            {
                ID = SpectrumCardID_10,
                LeftName = "Common",
                RightName = "Rare"
            }
        );
    }
}