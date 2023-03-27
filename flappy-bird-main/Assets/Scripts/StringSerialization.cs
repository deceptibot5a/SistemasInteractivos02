using System;
using UnityEngine;
using FullSerializer;


public static class StringSerialization
{
    private static readonly fsSerializer Serialiazer = new fsSerializer();

    public static string Serialize(Type type, object value) {
        Serialiazer.TrySerialize(type, value, out var data).AssertSuccessWithoutWarnings();

        return fsJsonPrinter.CompressedJson(data);
    }

    public static object Deserialize(Type type, string serializedState) {
        fsData data = fsJsonParser.Parse(serializedState);

        object deserialized = null;
        Serialiazer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

        return deserialized;
    }
}
