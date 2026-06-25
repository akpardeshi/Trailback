using System;
using UnityEngine.Serialization;

namespace ModularForge.Trailback.Demo.Features
{
    [Serializable]
    public struct FeatureVersion : IComparable<FeatureVersion>
    {
        [FormerlySerializedAs("major")] public int Major;
        [FormerlySerializedAs("minor")] public int Minor;
        [FormerlySerializedAs("patch")] public int Patch;

        public FeatureVersion(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public int CompareTo(FeatureVersion other)
        {
            if (Major != other.Major)
            {
                return Major.CompareTo(other.Major);
            }

            if (Minor != other.Minor)
            {
                return Minor.CompareTo(other.Minor);
            }

            return Patch.CompareTo(other.Patch);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }
        
        public static bool operator >(FeatureVersion left, FeatureVersion right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(FeatureVersion left, FeatureVersion right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >=(FeatureVersion left, FeatureVersion right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(FeatureVersion left, FeatureVersion right)
        {
            return left.CompareTo(right) <= 0;
        }
    }
}