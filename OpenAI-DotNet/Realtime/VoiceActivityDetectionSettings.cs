﻿// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;

namespace OpenAI.Realtime
{
    [Obsolete("Use new IVoiceActivityDetectionSettings classes: SemanticVAD, ServerVAD, and DisabledVAD")]
    public sealed class VoiceActivityDetectionSettings : IVoiceActivityDetectionSettings
    {
        public VoiceActivityDetectionSettings() { }

        public VoiceActivityDetectionSettings(
            TurnDetectionType type = TurnDetectionType.Server_VAD,
            float? detectionThreshold = null,
            int? prefixPadding = null,
            int? silenceDuration = null,
            bool createResponse = true)
        {
            switch (type)
            {
                default:
                case TurnDetectionType.Server_VAD:
                    Type = TurnDetectionType.Server_VAD;
                    DetectionThreshold = detectionThreshold;
                    PrefixPadding = prefixPadding;
                    SilenceDuration = silenceDuration;
                    CreateResponse = createResponse;
                    break;
                case TurnDetectionType.Disabled:
                    Type = TurnDetectionType.Disabled;
                    DetectionThreshold = null;
                    PrefixPadding = null;
                    SilenceDuration = null;
                    CreateResponse = false;
                    break;
            }
        }

        [JsonInclude]
        [JsonPropertyName("type")]
        [JsonConverter(typeof(Extensions.JsonStringEnumConverter<TurnDetectionType>))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TurnDetectionType Type { get; private set; }

        [JsonInclude]
        [JsonPropertyName("create_response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool CreateResponse { get; private set; }

        [JsonInclude]
        [JsonPropertyName("interrupt_response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool InterruptResponse { get; private set; }

        [JsonInclude]
        [JsonPropertyName("threshold")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? DetectionThreshold { get; private set; }

        [JsonInclude]
        [JsonPropertyName("prefix_padding_ms")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PrefixPadding { get; private set; }

        [JsonInclude]
        [JsonPropertyName("silence_duration_ms")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SilenceDuration { get; private set; }

        public static VoiceActivityDetectionSettings Disabled() => new(TurnDetectionType.Disabled);
    }
}
