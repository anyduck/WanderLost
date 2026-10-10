using MessagePack;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WanderLost.Shared;

public static class Utils
{
    public static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Converters =
        {
            new JsonStringEnumConverter()
        },
    };

    /// <summary>
    /// Build message pack options shared by both client and server
    /// </summary>
    public static void BuildMessagePackOptions(MessagePackHubProtocolOptions messagePackOptions)
    {
        messagePackOptions.SerializerOptions = MessagePackSerializerOptions.Standard
            .WithResolver(MessagePack.Resolvers.CompositeResolver.Create(
                    MessagePack.Resolvers.NativeGuidResolver.Instance,
                    MessagePack.Resolvers.StandardResolver.Instance
                ))
            .WithSecurity(MessagePackSecurity.UntrustedData)
            .WithCompression(MessagePackCompression.Lz4Block);
    }

    public static readonly IReadOnlyDictionary<string, string> ServerMerges = new Dictionary<string, string>()
    {
        { "Shadespire", "Arcturus" },
        { "Petrania", "Arcturus" },
        { "Tragon", "Arcturus" },
        { "Stonehearth", "Arcturus" },
        { "Kurzan", "Vairgrys" },
        { "Prideholme", "Vairgrys" },
        { "Yorn", "Vairgrys" },
        { "Feiton", "Vairgrys" },
        { "Sirius", "Elpon" },
        { "Sceptrum", "Elpon" },
        { "Procyon", "Arcturus" },
        { "Beatrice", "Arcturus" },
        { "Rethramis", "Arcturus" },
        { "Tortoyk", "Arcturus" },
        { "Moonkeep", "Arcturus" },
        { "Punika", "Arcturus" },
        { "Agaton", "Vairgrys" },
        { "Vern", "Vairgrys" },
        { "Antares", "Elpon" },
        { "Slen", "Elpon" },
        { "Calvasus", "Arcturus" },
        { "Mokoko", "Arcturus" },
        { "Lazenith", "Arcturus" },
        { "Ezrebet", "Elpon" },
        { "Trixion", "Elpon" },
        { "Armen", "Elpon" },
        { "Evergrace", "Arcturus" },
        { "Aldebaran", "Vairgrys" },
        { "Zosma", "Inanna" },
        { "Vykas", "Inanna" },
        { "Danube", "Inanna" },
        { "Elzowin", "Inanna" },
        { "Kharmine", "Vairgrys" },
        { "Adrinne", "Inanna" },
        { "Sasha", "Inanna" },
        { "Bergstrom", "Thaemine" },
        { "Enviska", "Thaemine" },
        { "Shandi", "Thaemine" },
        { "Akkan", "Thaemine" },
        { "Kazeros", "Vairgrys" },
        { "Blackfang", "Vairgrys" },
        { "Neria", "Arcturus" },
        { "Wei", "Elpon" },
        { "Kadan", "Elpon" },
        { "Asta", "Elpon" },
        { "Zinnervale", "Elpon" },
        { "Nia", "Arcturus" },
        { "Ealyn", "Arcturus" },
        { "Thirain", "Arcturus" },
        { "Kayangel", "Arcturus" },
        { "Galatur", "Vairgrys" },
        { "Azena", "Vairgrys" },
        { "Ladon", "Inanna" },
        { "Una", "Inanna" },
        { "Karta", "Inanna" },
        { "Azakiel", "Inanna" },
        { "Avesta", "Inanna" },
        { "Regulus", "Inanna" },
        { "Rohendel", "Thaemine" },
        { "Mari", "Thaemine" },
        { "Valtan", "Thaemine" },
        { "Lauriel", "Thaemine" },
        { "Arthetine", "Vairgrys" },
        { "Elgacia", "Vairgrys" },
        { "Nineveh", "Inanna" },
        { "Luterra", "Vairgrys" },
        { "Brelshaza", "Thaemine" },
        { "Ortuus", "Arcturus" },
        { "Ratik", "Elpon" },
        { "Balthorr", "Inanna" },
        { "Gienah", "Arcturus" },
    };

    public static bool HasMergedServer(string server, [NotNullWhen(true)] out string? mergedServer)
    {
        mergedServer = null;
        while (ServerMerges.TryGetValue(server, out var newServer))
        {
            server = mergedServer = newServer;
        }
        return !string.IsNullOrWhiteSpace(mergedServer);
    }
}
