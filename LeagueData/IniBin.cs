using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Linq;
using System.Globalization;

namespace LeagueData
{
    public sealed class IniBin
    {
        public readonly struct Value
        {
            public object Raw { get; }

            public Value(int v) => Raw = v;
            public Value(float v) => Raw = v;
            public Value(bool v) => Raw = v ? 1 : 0;
            public Value(float a, float b) => Raw = new Vector2(a, b);
            public Value(float a, float b, float c) => Raw = new Vector3(a, b, c);
            public Value(float a, float b, float c, float d) => Raw = new Vector4(a, b, c, d);
            public Value(string v) => Raw = v;

            public int? Int()
            {
                switch (Raw)
                {
                    case int v: return v;
                    case float v: return (int)v;
                    case string v:
                        if (int.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
                        {
                            return result;
                        }
                        return 0;
                    default: return null;
                }
            }

            public float? Float()
            {
                switch (Raw)
                {
                    case int v: return v;
                    case float v: return v;
                    case string v:
                        if (int.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
                        {
                            return result;
                        }
                        return 0;
                    default: return null;
                }
            }

            public bool? Bool()
            {
                switch(Raw)
                {
                    case int v: return v != 0;
                    case float v: return v != 0.0f;
                    case string v: return v.Length > 0 && v[0] == '1';
                    default: return null;
                }
            }

            public bool? StringBool()
            {
                switch(String())
                {
                    case string v: return v.ToLower() == "yes";
                    default: return null;
                }
            }

            public bool? IntBool()
            {
                switch(Int())
                {
                    case int v: return v != 0;
                    default: return null;
                }
            }

            public Vector2? Vector2()
            {
                switch (Raw)
                {
                    case Vector2 v: return v;
                    case string v:
                        var list = v.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (list.Length < 2)
                        {
                            return null;
                        }
                        if (!float.TryParse(list[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float a))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float b))
                        {
                            return null;
                        }
                        return new Vector2(a, b);
                    default: return null;
                }
            }

            public Vector3? Vector3()
            {
                switch (Raw)
                {
                    case Vector3 v: return v;
                    case string v:
                        var list = v.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (list.Length < 3)
                        {
                            return null;
                        }
                        if (!float.TryParse(list[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float a))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float b))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float c))
                        {
                            return null;
                        }
                        return new Vector3(a, b, c);
                    default: return null;
                }
            }

            public Vector4? Vector4()
            {
                switch (Raw)
                {
                    case Vector4 v: return v;
                    case string v:
                        var list = v.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (list.Length < 4)
                        {
                            return null;
                        }
                        if (!float.TryParse(list[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float a))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float b))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float c))
                        {
                            return null;
                        }
                        if (!float.TryParse(list[3], NumberStyles.Any, CultureInfo.InvariantCulture, out float d))
                        {
                            return null;
                        }
                        return new Vector4(a, b, c, d);
                    default: return null;
                }
            }
            
            public string String()
            {
                switch (Raw)
                {
                    case string v: return v;
                    case int v: return string.Format(CultureInfo.InvariantCulture, "{0}", v);
                    case float v: return string.Format(CultureInfo.InvariantCulture, "{0}", v);
                    case Vector2 v: return string.Format(CultureInfo.InvariantCulture, "{0} {1}", v.X, v.Y);
                    case Vector3 v: return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", v.X, v.Y, v.Z);
                    case Vector4 v: return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3}", v.X, v.Y, v.Z, v.W);
                    default: return null;
                }
            }

            public T? StringEnum<T>() where T : struct, Enum
            {
                switch(Raw)
                {
                    case string v: return System.Enum.TryParse(v, true, out T result) ? (T?)result : null;
                }
                return null;
            }


            public IReadOnlyList<int> IntList(params int[] defval)
            {
                var result = defval.ToList();
                if(String() is string str)
                {
                    var list = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < Math.Min(list.Length, defval.Length); i++)
                    {
                        if (int.TryParse(list[0], NumberStyles.Any, CultureInfo.InvariantCulture, out int ival))
                        {
                            result[i] = ival;
                        }
                        else if (float.TryParse(list[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float fval))
                        {
                            result[i] = (int)fval;
                        }
                    }
                }
                return result;
            }
        }

        public static uint Hash(string section, string name)
        {
            uint hash = 0;

            foreach (var c in section)
            {
                hash = (hash * 65599u) + (byte)(c >= 'A' && c <= 'Z' ? (c - 'A' + 'a') : c);
            }

            hash = (hash * 65599u) + (byte)'*';

            foreach (var c in name)
            {
                hash = (hash * 65599u) + (byte)(c >= 'A' && c <= 'Z' ? (c - 'A' + 'a') : c);
            }

            return hash;
        }

        public Value this[string section, string name] => _values.TryGetValue(Hash(section, name), out Value r) ? r : default;


        [Flags]
        private enum ValueFlags
        {
            Int = 1 << 0,
            Float = 1 << 1,
            FloatSmall = 1 << 2,
            Short = 1 << 3,
            Byte = 1 << 4,
            Bit = 1 << 5,
            Vector3Small = 1 << 6,
            Vector3 = 1 << 7,
            Vector2Small = 1 << 8,
            Vector2 = 1 << 9,
            Vector4Small = 1 << 10,
            Vector4 = 1 << 11,
            String = 1 << 12,
        }

        private static uint[] ReadHashes(BinaryReader reader)
        {
            ushort size = reader.ReadUInt16();
            var result = new uint[size];
            for (var i = 0; i < size; i++)
            {
                result[i] = reader.ReadUInt32();
            }
            return result;
        }

        private readonly IReadOnlyDictionary<uint, Value> _values;

        public IniBin(BinaryReader reader)
        {
            var version = reader.ReadByte();
            var stringDataLength = reader.ReadUInt16();
            var valueFlags = (ValueFlags)reader.ReadUInt16();

            if (version != 2)
            {
                throw new IOException("Inibin version != 2");
            }

            var values = new Dictionary<uint, Value>();

            if (valueFlags.HasFlag(ValueFlags.Int))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    values.Add(hash, new Value(reader.ReadInt32()));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Float))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    values.Add(hash, new Value(reader.ReadSingle()));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.FloatSmall))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    values.Add(hash, new Value(reader.ReadByte() / 10.0f));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Short))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    values.Add(hash, new Value(reader.ReadInt16()));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Byte))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    values.Add(hash, new Value(reader.ReadByte()));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Bit))
            {
                var hashes = ReadHashes(reader);
                var bits = reader.ReadBytes(hashes.Length / 8 + (hashes.Length % 8 != 0 ? 1 : 0));
                for (var i = 0; i < hashes.Length; i++)
                {
                    values.Add(hashes[i], new Value((bits[i / 8] & (1 << (i % 8))) != 0));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector3Small))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadByte() / 10.0f;
                    var y = reader.ReadByte() / 10.0f;
                    var z = reader.ReadByte() / 10.0f;
                    values.Add(hash, new Value(x, y, z));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector3))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadSingle();
                    var y = reader.ReadSingle();
                    var z = reader.ReadSingle();
                    values.Add(hash, new Value(x, y, z));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector2Small))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadByte() / 10.0f;
                    var y = reader.ReadByte() / 10.0f;
                    values.Add(hash, new Value(x, y));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector2))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadSingle();
                    var y = reader.ReadSingle();
                    values.Add(hash, new Value(x, y));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector4Small))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadByte() / 10.0f;
                    var y = reader.ReadByte() / 10.0f;
                    var z = reader.ReadByte() / 10.0f;
                    var w = reader.ReadByte() / 10.0f;
                    values.Add(hash, new Value(x, y, z, w));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.Vector4))
            {
                var hashes = ReadHashes(reader);
                foreach (var hash in hashes)
                {
                    var x = reader.ReadSingle();
                    var y = reader.ReadSingle();
                    var z = reader.ReadSingle();
                    var w = reader.ReadSingle();
                    values.Add(hash, new Value(x, y, z, w));
                }
            }

            if (valueFlags.HasFlag(ValueFlags.String))
            {
                var hashes = ReadHashes(reader);
                var offsets = new ushort[hashes.Length];

                for (var i = 0; i < offsets.Length; i++)
                {
                    offsets[i] = reader.ReadUInt16();
                }

                var stringData = reader.ReadBytes(stringDataLength);

                for (var i = 0; i < hashes.Length; i++)
                {
                    var hash = hashes[i];
                    var offset = offsets[i];
                    var str = Encoding.UTF8.GetString(stringData.Skip(offset).TakeWhile((b) => b != 0).ToArray());
                    values.Add(hash, new Value(str));
                }
            }

            _values = values;
        }
    }
}
