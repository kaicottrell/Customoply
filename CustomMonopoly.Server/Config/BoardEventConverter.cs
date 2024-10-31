using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CustomMonopoly.Server.ViewModels;

public class BoardEventConverter : JsonConverter<BoardEvent>
{
    public override BoardEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            if (doc.RootElement.TryGetProperty("EventType", out JsonElement eventTypeElement))
            {
                string eventType = eventTypeElement.GetString();
                switch (eventType)
                {
                    case "AvailableForPurchase":
                        return JsonSerializer.Deserialize<AvailableForPurchaseEvent>(doc.RootElement.GetRawText(), options);
                    case "HomeNoAction":
                        return JsonSerializer.Deserialize<HomeNoActionEvent>(doc.RootElement.GetRawText(), options);
                    case "RentRequired":
                        return JsonSerializer.Deserialize<RentRequiredEvent>(doc.RootElement.GetRawText(), options);
                    // Add other cases as needed
                }
            }
        }
        throw new JsonException("Unknown event type");
    }

    public override void Write(Utf8JsonWriter writer, BoardEvent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}
